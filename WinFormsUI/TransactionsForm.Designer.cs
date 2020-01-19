namespace WinFormsUI
{
    partial class TransactionsForm
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
            this.btnBackToMenu = new System.Windows.Forms.Button();
            this.lbTransactions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnBackToMenu
            // 
            this.btnBackToMenu.Location = new System.Drawing.Point(29, 28);
            this.btnBackToMenu.Name = "btnBackToMenu";
            this.btnBackToMenu.Size = new System.Drawing.Size(86, 23);
            this.btnBackToMenu.TabIndex = 0;
            this.btnBackToMenu.Text = "Back to menu";
            this.btnBackToMenu.UseVisualStyleBackColor = true;
            // 
            // lbTransactions
            // 
            this.lbTransactions.FormattingEnabled = true;
            this.lbTransactions.Location = new System.Drawing.Point(29, 79);
            this.lbTransactions.Name = "lbTransactions";
            this.lbTransactions.Size = new System.Drawing.Size(548, 303);
            this.lbTransactions.TabIndex = 1;
            // 
            // TransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbTransactions);
            this.Controls.Add(this.btnBackToMenu);
            this.Name = "TransactionsForm";
            this.Text = "TransactionsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBackToMenu;
        private System.Windows.Forms.ListBox lbTransactions;
    }
}
