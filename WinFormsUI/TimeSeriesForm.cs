using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsUI
{
    public partial class TimeSeriesForm : Form
    {
        public TimeSeriesForm()
        {
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

            for (var i = State.TimeSeries.Start; i <= State.TimeSeries.End; i = i.AddDays(1))
            {
                series.Points.AddXY(State.TimeSeries[i].Date.ToString(), State.TimeSeries[i].Value);
            }
            chartSeries.Series.Add(series);
        }

        private void TimeSeriesForm_Load(object sender, EventArgs e)
        {
            State.OnStateUpdated += RefreshChart;

            RefreshChart();
        }
    }
}
