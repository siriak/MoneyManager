using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsUI
{
    public partial class TransactionEditor : Form
    {
        public TransactionEditor(MainForm mainForm) => InitializeComponent(mainForm);

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            var cardNumber = txtboxCardNumber.Text;
            var category = txtboxCategory.Text;
            var description = txtboxDescription.Text;

            var transactionRecordIndex = mainForm.lbTransactions.SelectedIndex;
            var transaction = mainForm.displayedTransactions[transactionRecordIndex];
            
            var newTransaction = new Transaction(cardNumber, transaction.Date, transaction.Amount, description, category, transaction.GetHashCode());

            StateManager.UpdateTransaction(newTransaction);
            Close();
        }
    }
}
