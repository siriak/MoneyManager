using System;
using System.Collections.Generic;
using System.Drawing;
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
        Random rnd;

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

            categories.Items.AddRange(State.Categories.ToArray());
            categories.SelectedIndexChanged += (o, e) => RefreshChart();
            categories.SelectedIndexChanged += (o, e) => RefreshList();
            categories.SetItemChecked(0, true);

            await State.Init();
        }

        private void RefreshList()
        {
            lbTransactions.Items.Clear();

            foreach (var c in categories.CheckedItems)
            {
                lbTransactions.Items.AddRange(State.GetTransactions(categories.GetItemText(c))
                    .SkipWhile(t => t.TimeStamp.DateTime < startDate)
                    .TakeWhile(t => t.TimeStamp.DateTime <= endDate)
                    .Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Description}").Reverse()
                    .ToArray());
            }
        }

        private void RefreshChart()
        {
            chartSeries.Series.Clear();

            foreach (var c in categories.CheckedItems)
            {
                var series = new Series
                {
                    Name = categories.GetItemText(c),
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    Color = Color.Red,
                    ChartType = SeriesChartType.Line
                };

                var timeSeries = State.GetTimeSeries(categories.GetItemText(c), 0.99);

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
