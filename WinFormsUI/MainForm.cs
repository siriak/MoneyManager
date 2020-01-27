using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core;

namespace WinFormsUI
{
    public partial class MainForm : Form
    {
        Date startDate = new Date(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
        Date endDate = new Date(DateTime.Now);

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            State.OnStateUpdated += RefreshList;
            State.OnStateUpdated += RefreshChart;

            dateTimePickerStart.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePickerEnd.Value = DateTime.Now.Date;

            await State.Init();
        }

        private void RefreshList()
        {
            lbTransactions.Invoke(new Action(() =>
            {
                lbTransactions.Items.Clear();
                lbTransactions.Items.AddRange(State.Transactions.SkipWhile(t => t.TimeStamp.DateTime < startDate).TakeWhile(t => t.TimeStamp.DateTime <= endDate).Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Description}").Reverse().ToArray());
            }));
        }

        private void RefreshChart()
        {
            chartSeries.Series.Clear();
            var series = new Series
            {
                Name = "Time Series",
                Color = Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            var timeSeries = State.GetTimeSeries("all", 0.99);
            // TODO: Display chart using selected dates
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                series.Points.AddXY(date.ToString(), timeSeries[date]);
            }
            chartSeries.Series.Add(series);
        }        

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePickerStart.Value.ToDate();
            RefreshList();
            RefreshChart();
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePickerEnd.Value.ToDate();
            RefreshList();
            RefreshChart();
        }
    }
}
