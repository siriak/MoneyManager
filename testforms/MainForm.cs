using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace testforms
{
    public partial class MainForm : Form
    {
        private Button btnOpenTransactionsForm;
        private Button btnQuit;

        public MainForm()
        {
            InitializeComponent();
            
            Text = "Money Manager";
            Size = new Size(1000, 800);
            btnOpenTransactionsForm = new Button { Text = "Load Transactions", Location = new Point(10, 10) };
            btnQuit = new Button { Text = "Quit", Location = new Point(200, 10) };

            btnOpenTransactionsForm.Click += ShowTransactionsForm;
            btnQuit.Click += ButtonQuitClick;

            State.Init();

            Controls.Add(btnOpenTransactionsForm);
            Controls.Add(btnQuit);
        }

        //TODO: select period


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
