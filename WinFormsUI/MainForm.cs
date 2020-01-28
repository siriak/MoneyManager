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
        Date startDate, endDate;
        public static event Action OnFilteringUpdated;

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

            await State.Init();
        }

        private void RefreshList()
        {
            lbTransactions.Items.Clear();
            lbTransactions.Items.AddRange(State.Transactions.SkipWhile(t => t.TimeStamp.DateTime < startDate).TakeWhile(t => t.TimeStamp.DateTime <= endDate).Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Description}").Reverse().ToArray());
        }

        private void RefreshChart()
        {
            series.Points.Clear();
            var timeSeries = State.GetTimeSeries("all", 0.99);
            
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                series.Points.AddXY(date.ToString(), timeSeries[date]);
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
