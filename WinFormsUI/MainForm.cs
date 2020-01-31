using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core;

namespace WinFormsUI
{
    public partial class MainForm : Form
    {
        Date startDate, endDate;
        event Action OnFilteringUpdated;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            State.OnStateUpdated += RefreshList;
            State.OnStateUpdated += RefreshChart;
            OnFilteringUpdated += RefreshList;
            OnFilteringUpdated += RefreshChart;

            dateTimePickerStart.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePickerEnd.Value = DateTime.Now.Date;

            clbCategories.Items.AddRange(State.Categories.ToArray());
            clbCategories.SelectedIndexChanged += (o, e) => RefreshChart();
            clbCategories.SelectedIndexChanged += (o, e) => RefreshList();
            clbCategories.SetItemChecked(0, true);

            await State.Init();
        } 

        private void RefreshList()
        {
            lbTransactions.Items.Clear();

            lbTransactions.Items.AddRange(State.GetTransactionsUnion(clbCategories.CheckedItems.Cast<object>().Select(clbCategories.GetItemText), startDate, endDate)
                .Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Description}")
                .Reverse()
                .ToArray());
        }

        private void RefreshChart()
        {
            chartSeries.Series.Clear();

            foreach (var c in clbCategories.CheckedItems)
            {
                var series = new Series
                {
                    Name = clbCategories.GetItemText(c),
                    ChartType = SeriesChartType.Line,
                };

                var timeSeries = State.GetTimeSeries(clbCategories.GetItemText(c), 0.99);

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    series.Points.AddXY(date.ToString(), timeSeries[date]);
                }

                chartSeries.Series.Add(series);
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
    }
}
