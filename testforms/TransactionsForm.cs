using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testforms
{
    public partial class TransactionsForm : Form
    {
        private ListBox rtb;
        private Label label;

        public TransactionsForm()
        {
            InitializeComponent();
            Text = "Transactions";
            Size = new Size(1000, 800);
            
            var btn_back = new Button { Text = "Back to menu", Location = new Point(10, 10) };
            btn_back.Click += OnBtnBack;

            rtb = new ListBox { Text = "Load Transactions", Location = new Point(10, 50), Width = 800, Height = 500 };
            State.OnStateUpdated += RefreshList;
            rtb.Items.AddRange(State.Transactions.Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Descriprtion}").ToArray());
            
            label = new Label { Location = new Point(200, 15) };            

            Controls.Add(btn_back);
            Controls.Add(rtb);
            Controls.Add(label);
        }

        void OnBtnBack(object sender, EventArgs e)
        {
            this.Close();            
            State.OnStateUpdated -= RefreshList;
        }

        void RefreshList()
        {
            rtb.Invoke(new Action(() => 
            {
                rtb.Items.Clear();
                rtb.Items.AddRange(State.Transactions.Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Descriprtion}").ToArray());
            }));
        }
    }
}
