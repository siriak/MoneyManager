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
        }

        private void btnOpenTransactionsForm_Click(object sender, EventArgs e)
        {
            var transForm = new TransactionsForm();
            Hide();
            transForm.Show();

            transForm.FormClosed += (o, e) => Show();
        }

        private void btnOpenChart_Click(object sender, EventArgs e)
        {
            var timeSeriesForm = new TimeSeriesForm();
            Hide();
            timeSeriesForm.Show();

            timeSeriesForm.FormClosed += (o, e) => Show();
        }

        private void btnQuit_Click(object sender, EventArgs e) => Application.Exit();

        private async void MainForm_Load(object sender, EventArgs e) => await State.Init();
    }
}
