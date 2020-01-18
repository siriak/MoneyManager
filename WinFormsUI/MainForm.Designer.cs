namespace WinFormsUI
{
    partial class MainForm
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
            this.btnOpenChart = new System.Windows.Forms.Button();
            this.btnOpenTransactionsForm = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenChart
            // 
            this.btnOpenChart.Location = new System.Drawing.Point(202, 10);
            this.btnOpenChart.Name = "btnOpenChart";
            this.btnOpenChart.Size = new System.Drawing.Size(75, 23);
            this.btnOpenChart.TabIndex = 0;
            this.btnOpenChart.Text = "Open Chart";
            this.btnOpenChart.UseVisualStyleBackColor = true;
            // 
            // btnOpenTransactionsForm
            // 
            this.btnOpenTransactionsForm.Location = new System.Drawing.Point(31, 11);
            this.btnOpenTransactionsForm.Name = "btnOpenTransactionsForm";
            this.btnOpenTransactionsForm.Size = new System.Drawing.Size(111, 23);
            this.btnOpenTransactionsForm.TabIndex = 1;
            this.btnOpenTransactionsForm.Text = "Load Transactions";
            this.btnOpenTransactionsForm.UseVisualStyleBackColor = true;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(576, 12);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnOpenTransactionsForm);
            this.Controls.Add(this.btnOpenChart);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenChart;
        private System.Windows.Forms.Button btnOpenTransactionsForm;
        private System.Windows.Forms.Button btnQuit;
    }
}