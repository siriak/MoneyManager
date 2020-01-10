using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace testforms
{
    internal class MainForm : Form
    {       
        private Button btn;
        private ListBox rtb;
        private Label label;

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Text = "Money Manager";

            Size = new Size(1000, 800);            
            btn = new Button { Text = "Load Transactions", Location = new Point(10, 10) };
            rtb = new ListBox { Text = "Load Transactions", Location = new Point(10, 50), Width = 800, Height = 500 };
            label = new Label { Location = new Point(200, 15) };            
            //btn.Click += LoadTransactions;
            Load += LoadTransactions;

            Controls.Add(btn);
            Controls.Add(rtb);
            Controls.Add(label);
        }

        async void LoadTransactions(object sender, EventArgs e)
        {
            btn.Enabled = false;

            var credentials = ConfigManager.GetCredentials();
            var transactions = new List<Transaction>();

            await Task.WhenAll(credentials.Select(c => PrivatTransactionsImporter.ImportTransactions(c, (ts, d) => {
                transactions.AddRange(ts);
                rtb.Invoke(new Action(() => rtb.Items.AddRange(ts.Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Descriprtion}").ToArray())));
                label.Text = d.ToString();
            })));

            btn.Enabled = true;
        }

        private void RenderCategory(Index index)
        {
            // max and min index
            // changes for average in month
        }
    }
}