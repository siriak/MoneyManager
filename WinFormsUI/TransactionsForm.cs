using Core;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsUI
{
    public partial class TransactionsForm : Form
    {
        DateTime startDate = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
        DateTime endDate = DateTime.Now;

        public TransactionsForm()
        {
            InitializeComponent();
            dateTimePickerStart.Format = DateTimePickerFormat.Short;
            dateTimePickerEnd.Format = DateTimePickerFormat.Short;
            dateTimePickerStart.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePickerEnd.Value = DateTime.Now;
            dateTimePickerStart.Text = "1/19/2019";
            dateTimePickerStart.Refresh();
        }

        void RefreshList()
        {
            lbTransactions.Invoke(new Action(() =>
            {
                lbTransactions.Items.Clear();
                lbTransactions.Items.AddRange(State.Transactions.Reverse().Where(t => t.TimeStamp > startDate && t.TimeStamp < endDate).Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Descriprtion}").ToArray());
            }));
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            State.OnStateUpdated -= RefreshList;
            this.Close();
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePickerStart.Value;
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePickerEnd.Value;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            State.OnStateUpdated += RefreshList;
            RefreshList();
        }
    }
}
