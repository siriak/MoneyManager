using Core;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsUI
{
    public partial class TransactionsForm : Form
    {
        private ListBox lbTransactions;

        public TransactionsForm()
        {
            InitializeComponent();
            Text = "Transactions";
            Size = new Size(1000, 800);
            
            var btnBackToMenu = new Button { Text = "Back to menu", Location = new Point(10, 10) };
            btnBackToMenu.Click += OnBtnBack;

            lbTransactions = new ListBox { Text = "Load Transactions", Location = new Point(10, 50), Width = 800, Height = 500 };
            State.OnStateUpdated += RefreshList;
            lbTransactions.Items.AddRange(State.Transactions.Reverse().Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Descriprtion}").ToArray());

            Controls.Add(btnBackToMenu);
            Controls.Add(lbTransactions);
        }

        void OnBtnBack(object sender, EventArgs e)
        {
            State.OnStateUpdated -= RefreshList;
            this.Close();
        }

        void RefreshList()
        {
            lbTransactions.Invoke(new Action(() => 
            {
                lbTransactions.Items.Clear();
                lbTransactions.Items.AddRange(State.Transactions.Reverse().Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Descriprtion}").ToArray());
            }));
        }
    }
}
