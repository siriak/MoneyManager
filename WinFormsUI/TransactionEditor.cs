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
        private string cardNumber;
        private string[] categories;
        private string description;

        public TransactionEditor() => InitializeComponent();

        private void txtboxDescription_TextChanged(object sender, EventArgs e)
        {
            cardNumber = txtboxCardNumber.Text;
        }

        private void txtboxCategory_TextChanged(object sender, EventArgs e)
        {
            categories = txtboxCategory.Text.Split(", ");
        }

        private void txtboxCardNumber_TextChanged(object sender, EventArgs e)
        {
            description = txtboxDescription.Text;
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
        }
    }
}
