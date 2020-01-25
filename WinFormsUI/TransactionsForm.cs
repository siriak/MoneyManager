using Core;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsUI
{
    public partial class TransactionsForm : Form
    {
        DateTime startDate = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
        DateTime endDate = DateTime.Now.Date;

        public TransactionsForm()
        {
            InitializeComponent();
        }

        void RefreshList()
        {
            lbTransactions.Invoke(new Action(() =>
            {
                lbTransactions.Items.Clear();
                lbTransactions.Items.AddRange(State.Transactions.SkipWhile(t => t.TimeStamp < startDate).TakeWhile(t => t.TimeStamp <= endDate).Select(t => (object)$"{t.Amount.Amount} {t.Amount.Currency}: {t.Description}").Reverse().ToArray());
            }));
        }

        private void btnBackToMenu_Click(object sender, EventArgs e) => Close();

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            startDate = dateTimePickerStart.Value;
            RefreshList();
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            endDate = dateTimePickerEnd.Value;
            RefreshList();
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            dateTimePickerStart.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePickerEnd.Value = DateTime.Now.Date;
            State.OnStateUpdated += RefreshList;
            RefreshList();
        }

        private void TransactionsForm_FormClosed(object sender, FormClosedEventArgs e) => State.OnStateUpdated -= RefreshList;
    }
}
