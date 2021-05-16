using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core;
using Core.Categories;

namespace WinFormsUI
{
    public partial class MainForm : Form
    {
        private Date startDate, endDate;
        private double smoothingRatio;
        private Category[] _orderedCategories;
        private int _listPosition;

        public MainForm() => InitializeComponent();
        private event Action OnFilteringUpdated = () => { };

        private void MainForm_Load(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            clbCategories.ItemCheck += async (o, e) =>
            {
                // No lock here because this code only executes in
                // UI thread, which means critical section cannot
                // be executed in different threads simultaneously
                cts.Cancel();
                cts = new CancellationTokenSource();
                var ct = cts.Token;

                const int debounceDelayMs = 100;
                await Task.Delay(debounceDelayMs);
                if (ct.IsCancellationRequested)
                {
                    return;
                }

                OnFilteringUpdated();
            };

            dateTimePickerStart.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePickerEnd.Value = DateTime.Now.Date;

            LoadCategories();
            LoadTransactions();

            OnFilteringUpdated += RefreshList;
            OnFilteringUpdated += RefreshChart;
            OnFilteringUpdated += RestoreScrollPosition;
            State.OnStateChanged += FileManager.SaveUpdatedTransactions;
            State.OnStateChanged += RefreshCategories;
            State.OnStateChanged += RefreshList;
            State.OnStateChanged += RefreshChart;

            FileManager.SaveUpdatedTransactions();
            FileManager.SaveAutoCategoriesToFile();           
            
            RefreshCategories();
            RefreshList();
            RefreshChart();
        }

        private void RestoreScrollPosition()
        {
            lbTransactions.SelectedIndex = _listPosition;
        }

        private void LoadCategories()
        {
            var regexCategoriesJson = FileManager.GetRegexCategories();
            var autoCategoriesJson = FileManager.GetAutoCategories();
            var compositeCategoriesJson = FileManager.GetCompositeCategories();

            StateManager.LoadCategories(regexCategoriesJson, autoCategoriesJson, compositeCategoriesJson);
        }
        
        private void LoadTransactions()
        {
            var filesUsb = FileManager.GetUsbFiles();
            var filesPb = FileManager.GetPbFiles();
            var filesKb = FileManager.GetKbFiles();

            var modifiedTransactions = FileManager.GetTransactions();

            StateManager.LoadTransactions(filesUsb.Concat(filesPb).Concat(filesKb), modifiedTransactions);
        }

        private void RefreshCategories()
        {
            var isFirstLoad = clbCategories.Items.Count == 0;

            var selectedCategories = clbCategories.CheckedIndices
                .Cast<int>()
                .Select(i => _orderedCategories[i].Name)
                .ToList();

            clbCategories.Items.Clear();

            _orderedCategories = State.Instance.Categories
                .OrderBy(CategoriesExtensions.CategoriesOrederer)
                .ToArray();
            
            string categoryWithPrefix = string.Empty;
            var indicesToCheck = new List<int>();

            for (int i = 0; i < _orderedCategories.Length; i++)
            {
                var c = _orderedCategories[i];
                var timeSeries = StateHelper.GetCumulativeTimeSeries(c.Name, c.Increment, c.Capacity);
                var todayData = timeSeries[Date.Today];
                var todayRelative = todayData / c.Capacity;

                categoryWithPrefix = DisplayManager.AddPrefixToCategory(todayRelative, c);

                clbCategories.Items.Add(categoryWithPrefix);

                if (selectedCategories.FirstOrDefault(sc => sc == _orderedCategories[i].Name) != null)
                {
                    indicesToCheck.Add(i);
                }
            }

            foreach (var i in indicesToCheck)
            {
                clbCategories.SetItemChecked(i, true);
            }

            if (isFirstLoad)
            {
                clbCategories.SetItemChecked(0, true);
            }
        }

        private void RefreshList()
        {
            lbTransactions.Items.Clear();

            var displayedTransactions = GetTransactionsToDisplay();

            lbTransactions.Items.AddRange(displayedTransactions
                .Select(t =>
                {
                    var categories = chboxAllCategories.Checked
                        ? State.Instance.GetAllMatchingCategories(t)
                        : State.Instance.GetAllMatchingCategoriesOfType<CompositeCategory>(t);
                    return DisplayManager.FormatLedgerRecord(t, categories);
                })
                .ToArray());
        }

        private Transaction[] GetTransactionsToDisplay()
        {
            var categoriesNames = clbCategories.CheckedIndices
                .Cast<int>()
                .Select(i => _orderedCategories[i].Name);          

            return StateHelper.GetTransactionsUnion(
                          categoriesNames,
                          startDate,
                          endDate).Reverse().ToArray();
        }

        private void RefreshChart()
        {
            chartSeriesSmoothed.Series.Clear();
            chartSeriesCumulative.Series.Clear();
            
            var selectedCategories = clbCategories.CheckedIndices
                .Cast<int>()
                .Select(i => _orderedCategories[i].Name);

            var smoothedSeriesToRemove = chartSeriesSmoothed.Series.Where(s => selectedCategories.All(c => c != s.Name));
            var cumulativeSeriesToRemove = chartSeriesCumulative.Series.Where(s => selectedCategories.All(c => c != s.Name));

            foreach (var s in smoothedSeriesToRemove)
            {
                chartSeriesSmoothed.Series.Remove(s);
            }

            foreach (var s in cumulativeSeriesToRemove)
            {
                chartSeriesCumulative.Series.Remove(s);
            }

            foreach (var c in selectedCategories)
            {
                if (chartSeriesSmoothed.Series.FindByName(c) is null)
                {
                    var newSeries = new Series
                    {
                        Name = c,
                        ChartType = SeriesChartType.Line
                    };
                    chartSeriesSmoothed.Series.Add(newSeries);
                }

                if (chartSeriesCumulative.Series.FindByName(c) is null)
                {
                    var newSeries = new Series
                    {
                        Name = c,
                        ChartType = SeriesChartType.Line
                    };
                    chartSeriesCumulative.Series.Add(newSeries);
                }

                var smoothedSeries = chartSeriesSmoothed.Series.FindByName(c);
                smoothedSeries.Points.Clear();

                var cumulativeSeries = chartSeriesCumulative.Series.FindByName(c);
                cumulativeSeries.Points.Clear();

                var name = c.Replace($"({Levels.Empty}) ", "")
                    .Replace($"({Levels.Low}) ", "")
                    .Replace($"({Levels.Full}) ", "");
                
                var category = State.Instance.Categories.First(category => category.Name == name);
                var smoothedTimeSeries = StateHelper.GetSmoothedTimeSeries(name, smoothingRatio);
                var cumulativeTimeSeries = StateHelper.GetCumulativeTimeSeries(name, category.Increment, category.Capacity);

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    _ = smoothedSeries.Points.AddXY(date.ToString(), smoothedTimeSeries[date]);
                }

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    _ = cumulativeSeries.Points.AddXY(date.ToString(), cumulativeTimeSeries[date]);
                }
            }
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePickerStart.Value.ToDate();
            OnFilteringUpdated();
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePickerEnd.Value.ToDate();
            OnFilteringUpdated();
        }

        private void txtboxSmoothingRatio_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtboxSmoothingRatio.Text, out var newSmoothingRatio)
                && newSmoothingRatio <= 1 && newSmoothingRatio >= 0)
            {
                smoothingRatio = newSmoothingRatio;
                OnFilteringUpdated();
            }
        }

        private void chboxAllCategories_CheckedChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void lb_DoubleClick(object sender, MouseEventArgs e)
        {
            _listPosition = lbTransactions.SelectedIndex;

            var transactionRecordIndex = lbTransactions.SelectedIndex;
            var transaction = GetTransactionsToDisplay()[transactionRecordIndex];

            var transactionEditor = new TransactionEditor(transaction);
            transactionEditor.txtboxCardNumber.Text = transaction.CardNumber;
            transactionEditor.txtboxCategory.Text = transaction.Category;
            transactionEditor.txtboxDescription.Text = transaction.Description;
            transactionEditor.ShowDialog();
        }
    }
}
