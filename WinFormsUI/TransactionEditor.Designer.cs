using System;
using System.Windows.Forms;

namespace WinFormsUI
{
    partial class TransactionEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtboxCardNumber = new System.Windows.Forms.TextBox();
            this.txtboxCategory = new System.Windows.Forms.TextBox();
            this.txtboxDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();

            // lblCardNumber
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Location = new System.Drawing.Point(20, 10);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(53, 13);
            this.lblCardNumber.TabIndex = 11;
            this.lblCardNumber.Text = "Card Number";
            // txtboxCardNumber
            this.txtboxCardNumber.AutoSize = true;
            this.txtboxCardNumber.Location = new System.Drawing.Point(20, 40);
            this.txtboxCardNumber.Name = "txtboxCardNumber";
            this.txtboxCardNumber.Size = new System.Drawing.Size(360, 20);
            this.txtboxCardNumber.TabIndex = 11;

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 80);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(53, 13);
            this.lblCategory.TabIndex = 11;
            this.lblCategory.Text = "Category";
            // txtboxCategory
            this.txtboxCategory.AutoSize = true;
            this.txtboxCategory.Location = new System.Drawing.Point(20, 110);
            this.txtboxCategory.Name = "txtboxCategory";
            this.txtboxCategory.Size = new System.Drawing.Size(360,70);
            this.txtboxCategory.TabIndex = 11;
            this.txtboxCategory.Multiline = true;

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 205);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(53, 13);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.Text = "Description";
            // txtboxDescription
            this.txtboxDescription.AutoSize = true;
            this.txtboxDescription.Location = new System.Drawing.Point(20, 235);
            this.txtboxDescription.Name = "txtboxDescription";
            this.txtboxDescription.Size = new System.Drawing.Size(360,85);
            this.txtboxDescription.TabIndex = 11;
            this.txtboxDescription.Multiline = true;

            //btnSave
            this.btnSave.Location = new System.Drawing.Point(20, 350);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Clicked);

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Text = "TransactionEditor";
            this.Controls.Add(this.lblCardNumber);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtboxCardNumber);
            this.Controls.Add(this.txtboxCategory);
            this.Controls.Add(this.txtboxDescription);
            this.Controls.Add(this.btnSave);
        }

        public System.Windows.Forms.Label lblCardNumber;
        public System.Windows.Forms.Label lblCategory;
        public System.Windows.Forms.Label lblDescription;
        public System.Windows.Forms.TextBox txtboxCardNumber;
        public System.Windows.Forms.TextBox txtboxCategory;
        public System.Windows.Forms.TextBox txtboxDescription;
        public System.Windows.Forms.Button btnSave;
        #endregion
    }
}