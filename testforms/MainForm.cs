using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace testforms
{
    internal class MainForm : Form
    {       
        private Button btnLoadTrans;
        private Button btnQuit;

        public MainForm()
        {
            InitializeComponent();
        }        

        //TODO: select period

        private void InitializeComponent()
        {
            Text = "Money Manager";
            Size = new Size(1000, 800);
            btnLoadTrans = new Button { Text = "Load Transactions", Location = new Point(10, 10) };
            btnQuit = new Button { Text = "Quit", Location = new Point(200, 10) };

            btnLoadTrans.Click += ShowTransactionsForm;
            btnQuit.Click += ButtonQuitClick;

            State.Init();

            Controls.Add(btnLoadTrans);
            Controls.Add(btnQuit);
        }

        private void ShowTransactionsForm(object sender, EventArgs e)
        {
            var transForm = new TransactionsForm();
            this.Hide();
            transForm.Show();

            transForm.FormClosed += TransForm_FormClosed;
        }

        private void TransForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Show();
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
