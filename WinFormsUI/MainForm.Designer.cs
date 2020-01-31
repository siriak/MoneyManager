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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabLedger = new System.Windows.Forms.TabPage();
            this.lbTransactions = new System.Windows.Forms.ListBox();
            this.tabTrends = new System.Windows.Forms.TabPage();
            this.chartSeries = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.clbCategories = new System.Windows.Forms.CheckedListBox();
            this.tabs.SuspendLayout();
            this.tabLedger.SuspendLayout();
            this.tabTrends.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSeries)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabTrends);
            this.tabs.Controls.Add(this.tabLedger);
            this.tabs.Location = new System.Drawing.Point(284, 29);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(512, 425);
            this.tabs.TabIndex = 3;
            // 
            // tabLedger
            // 
            this.tabLedger.Controls.Add(this.lbTransactions);
            this.tabLedger.Location = new System.Drawing.Point(4, 22);
            this.tabLedger.Name = "tabLedger";
            this.tabLedger.Padding = new System.Windows.Forms.Padding(3);
            this.tabLedger.Size = new System.Drawing.Size(504, 399);
            this.tabLedger.TabIndex = 0;
            this.tabLedger.Text = "Ledger";
            this.tabLedger.UseVisualStyleBackColor = true;
            // 
            // lbTransactions
            // 
            this.lbTransactions.FormattingEnabled = true;
            this.lbTransactions.Location = new System.Drawing.Point(20, 13);
            this.lbTransactions.Name = "lbTransactions";
            this.lbTransactions.Size = new System.Drawing.Size(463, 368);
            this.lbTransactions.TabIndex = 1;
            // 
            // tabTrends
            // 
            this.tabTrends.Controls.Add(this.chartSeries);
            this.tabTrends.Location = new System.Drawing.Point(4, 22);
            this.tabTrends.Name = "tabTrends";
            this.tabTrends.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrends.Size = new System.Drawing.Size(504, 399);
            this.tabTrends.TabIndex = 1;
            this.tabTrends.Text = "Trends";
            this.tabTrends.UseVisualStyleBackColor = true;
            // 
            // chartSeries
            // 
            chartArea1.Name = "Main Chart Area";
            this.chartSeries.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend";
            this.chartSeries.Legends.Add(legend1);
            this.chartSeries.Location = new System.Drawing.Point(19, 42);
            this.chartSeries.Name = "chartSeries";
            this.chartSeries.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            this.chartSeries.Size = new System.Drawing.Size(461, 336);
            this.chartSeries.TabIndex = 1;
            this.chartSeries.Text = "chartTrends";
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(31, 109);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEnd.TabIndex = 14;
            this.dateTimePickerEnd.ValueChanged += new System.EventHandler(this.dateTimePickerEnd_ValueChanged);
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Checked = false;
            this.dateTimePickerStart.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(31, 55);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStart.TabIndex = 13;
            this.dateTimePickerStart.Value = new System.DateTime(2020, 1, 19, 15, 10, 57, 0);
            this.dateTimePickerStart.ValueChanged += new System.EventHandler(this.dateTimePickerStart_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(28, 93);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(50, 13);
            this.lblEndDate.TabIndex = 12;
            this.lblEndDate.Text = "End date";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(28, 39);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(53, 13);
            this.lblStartDate.TabIndex = 11;
            this.lblStartDate.Text = "Start date";
            // 
            // clbCategories
            // 
            this.clbCategories.CheckOnClick = true;
            this.clbCategories.FormattingEnabled = true;
            this.clbCategories.Location = new System.Drawing.Point(31, 174);
            this.clbCategories.Name = "clbCategories";
            this.clbCategories.Size = new System.Drawing.Size(200, 214);
            this.clbCategories.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 466);
            this.Controls.Add(this.clbCategories);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dateTimePickerStart);
            this.Name = "MainForm";
            this.Text = "Money Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabs.ResumeLayout(false);
            this.tabLedger.ResumeLayout(false);
            this.tabTrends.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartSeries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabLedger;
        private System.Windows.Forms.ListBox lbTransactions;
        private System.Windows.Forms.TabPage tabTrends;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSeries;
        private System.Windows.Forms.CheckedListBox clbCategories;
    }
}