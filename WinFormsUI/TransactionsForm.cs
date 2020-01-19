using Core;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsUI
{
    public partial class TransactionsForm : Form
    {
        public TransactionsForm()
        {
            InitializeComponent();
            
            btnBackToMenu.Click += OnBtnBack;
           
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
