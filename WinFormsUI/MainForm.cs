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

            State.Init();
        }

        //TODO: select period        

        private void RenderCategory(Index index)
        {
            // max and min index
            // changes for average in month
        }

        private void btnOpenTransactionsForm_Click(object sender, EventArgs e)
        {
            var transForm = new TransactionsForm();
            this.Hide();
            transForm.Show();

            transForm.FormClosed += (o, e) => Show();
        }

        private void btnOpenChart_Click(object sender, EventArgs e)
        {
            var timeSeriesForm = new TimeSeriesForm();
            this.Hide();
            timeSeriesForm.Show();

            timeSeriesForm.FormClosed += (o, e) => Show();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
