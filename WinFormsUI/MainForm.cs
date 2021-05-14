using System;
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
        string WorkingDirectory => Directory.GetCurrentDirectory() + "/data/";
        string CategoriesDirectory => WorkingDirectory + "categories/";
        string AutoCategoriesFileName => CategoriesDirectory + "autoCategories.json";
        string CompositeCategoriesFileName => CategoriesDirectory + "compositeCategories.json";
        string RegexCategoriesFileName => CategoriesDirectory + "regexCategories.json";
        string TransactionsFileName => WorkingDirectory + "transactions.json";

        string UsbDirectory => WorkingDirectory + "ukrsibbank/";
        string KredobankDirectory => WorkingDirectory + "kredobank/";
        string PrivatebankDirectory => WorkingDirectory + "privatbank/";

        private Date startDate, endDate;
        private double smoothingRatio;
        private Category[] _orderedCategories;

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
            State.OnStateChanged += SaveUpdatedTransactions;
            State.OnStateChanged += RefreshCategories;
            State.OnStateChanged += RefreshList;
            State.OnStateChanged += RefreshChart;

            SaveUpdatedTransactions();
            File.WriteAllText(AutoCategoriesFileName, StateManager.SaveCategories().autoCategoriesJson);            
            
            RefreshCategories();
            RefreshList();
            RefreshChart();
        }

        private void SaveUpdatedTransactions()
        {
            File.WriteAllText(TransactionsFileName, State.Instance.SaveTransactionsToJson());
        }

        private void LoadCategories()
        {
            if (!File.Exists(RegexCategoriesFileName))
            {
                File.WriteAllText(RegexCategoriesFileName, "[]");
            }
            var regexCategoriesJson = File.ReadAllText(RegexCategoriesFileName);

            if (!File.Exists(AutoCategoriesFileName))
            {
                File.WriteAllText(AutoCategoriesFileName, "[]");
            }

            var autoCategoriesJson = File.ReadAllText(AutoCategoriesFileName);

            if (!File.Exists(CompositeCategoriesFileName))
            {
                File.WriteAllText(CompositeCategoriesFileName, "[]");
            }

            var compositeCategoriesJson = File.ReadAllText(CompositeCategoriesFileName);

            StateManager.LoadCategories(regexCategoriesJson, autoCategoriesJson, compositeCategoriesJson);
        }
        
        private void LoadTransactions()
        {
            var filesUsb = Directory.GetFiles(UsbDirectory, "*.*", SearchOption.AllDirectories)
                .Select(f => ("usb", (Stream)File.OpenRead(f)));
            var filesPb = Directory.GetFiles(PrivatebankDirectory, "*.*", SearchOption.AllDirectories)
                .Select(f => ("pb", (Stream)File.OpenRead(f)));
            var filesKb = Directory.GetFiles(KredobankDirectory, "*.*", SearchOption.AllDirectories)
                .Select(f => ("kb", (Stream)File.OpenRead(f)));

            if (!File.Exists(TransactionsFileName))
            {
                File.WriteAllText(TransactionsFileName, "[]");
            }
            var modifiedTransactions = File.ReadAllText(TransactionsFileName);

            StateManager.LoadTransactions(filesUsb.Concat(filesPb).Concat(filesKb), modifiedTransactions);
        }

        private void RefreshCategories()
        {
            var isFirstLoad = clbCategories.Items.Count == 0;

            var selectedCategories = clbCategories.CheckedItems
                                                 .Cast<object>()
                                                 .Select(clbCategories.GetItemText)
                                                 .ToList();

            clbCategories.Items.Clear();

            _orderedCategories = State.Instance.Categories.OrderBy(CategoriesOrederer).ToArray();
            
            string prefix = string.Empty;

            for (int i = 0; i < _orderedCategories.Length; i++)
            {
                var c = _orderedCategories[i];
                var timeSeries = StateHelper.GetCumulativeTimeSeries(c.Name, c.Increment, c.Capacity);
                var todayData = timeSeries[Date.Today];
                var todayRelative = todayData / c.Capacity;

                prefix = todayRelative switch
                {
                    _ when todayRelative <= 0 => Levels.Empty.ToString(),
                    _ when todayRelative <= 0.1 => Levels.Low.ToString(),
                    _ when todayRelative < 1 => "", 
                    _ => Levels.Full.ToString(),                    
                };

                clbCategories.Items.Add(string.IsNullOrEmpty(prefix) ? c.Name : string.Concat($"({prefix}) ", c.Name));
            }

            foreach (var c in selectedCategories)
            {
                clbCategories.SetItemChecked(clbCategories.FindStringExact(c), true);
            }

            if (isFirstLoad)
            {
                clbCategories.SetItemChecked(0, true);
            }
        }

        private string CategoriesOrederer(Category category)
        {
            return category switch
            {
                CompositeCategory cc => "1" + category.Name,
                RegexCategory rc => "2" + category.Name,
                AutoCategory ac => "3" + category.Name,
                _ => throw new NotSupportedException(),
            };
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

        private void lb_DoubleClick(object sender, EventArgs e)
        {
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
