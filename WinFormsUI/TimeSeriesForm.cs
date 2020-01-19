using Core;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsUI
{
    public partial class TimeSeriesForm : Form
    {
        public TimeSeriesForm()
        {
            // TODO: Move to designer
            InitializeComponent();
        }

        void RefreshChart()
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
            for (var date = new Date(2019, 1, 31); date <= new Date(2020, 1, 31); date = date.AddDays(1))
            {
                series.Points.AddXY(date.ToString(), timeSeries[date]);
            }
            chartSeries.Series.Add(series);
        }

        private void TimeSeriesForm_Load(object sender, EventArgs e)
        {
            State.OnStateUpdated += RefreshChart;

            RefreshChart();
        }

        private void TimeSeriesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            State.OnStateUpdated -= RefreshChart;
        }
    }
}
