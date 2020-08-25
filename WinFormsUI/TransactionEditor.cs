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
        private Transaction transaction;

        public TransactionEditor(Transaction transaction)
        {
            this.transaction = transaction;
            InitializeComponent();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            var cardNumber = txtboxCardNumber.Text;
            var category = txtboxCategory.Text;
            var description = txtboxDescription.Text;
            
            var newTransaction = new Transaction(cardNumber, transaction.Date, transaction.Amount, description, category, transaction.GetHashCode());

            StateManager.UpdateTransaction(newTransaction);
            Close();
        }
    }
}
