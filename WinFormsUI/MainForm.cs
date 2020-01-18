using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace WinFormsUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            btnOpenTransactionsForm.Click += ShowTransactionsForm;
            btnOpenChart.Click += ShowTimeSeriesForm;
            btnQuit.Click += ButtonQuitClick;

            State.Init();
        }

        //TODO: select period
        private void ShowTimeSeriesForm(object sender, EventArgs e)
        {
            var timeSeriesForm = new TimeSeriesForm();
            this.Hide();
            timeSeriesForm.Show();

            timeSeriesForm.FormClosed += (o, e) => { Show(); };
        }

        private void ShowTransactionsForm(object sender, EventArgs e)
        {
            var transForm = new TransactionsForm();
            this.Hide();
            transForm.Show();

            transForm.FormClosed += (o, e) => { Show(); };
        }

        private void ButtonQuitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RenderCategory(Index index)
        {
            // max and min index
            // changes for average in month
        }
    }
}
