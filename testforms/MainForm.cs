using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace testforms
{
    internal class MainForm : Form
    {       
        private Button btn_loadTrans;
        private Button btn_quit;

        public MainForm()
        {
            InitializeComponent();
        }        

        //TODO: select period

        private void InitializeComponent()
        {
            Text = "Money Manager";
            Size = new Size(1000, 800);
            btn_loadTrans = new Button { Text = "Load Transactions", Location = new Point(10, 10) };
            btn_quit = new Button { Text = "Quit", Location = new Point(200, 10) };

            btn_loadTrans.Click += ShowTransactionsForm;
            btn_quit.Click += ButtonQuitClick;

            Controls.Add(btn_loadTrans);
            Controls.Add(btn_quit);
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
