using System;
using System.Windows.Forms;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSmoothedTrends = new System.Windows.Forms.TabPage();
            this.chartSeriesSmoothed = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabCumulativeTrends = new System.Windows.Forms.TabPage();
            this.chartSeriesCumulative = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabLedger = new System.Windows.Forms.TabPage();
            this.lbTransactions = new System.Windows.Forms.ListBox();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.chboxAllCategories = new System.Windows.Forms.CheckBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblSmoothingRatio = new System.Windows.Forms.Label();
            this.txtboxSmoothingRatio = new System.Windows.Forms.TextBox();
            this.clbCategories = new System.Windows.Forms.CheckedListBox();
            this.tabs.SuspendLayout();
            this.tabSmoothedTrends.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.chartSeriesSmoothed)).BeginInit();
            this.tabCumulativeTrends.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.chartSeriesCumulative)).BeginInit();
            this.tabLedger.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSmoothedTrends);
            this.tabs.Controls.Add(this.tabCumulativeTrends);
            this.tabs.Controls.Add(this.tabLedger);
            this.tabs.Location = new System.Drawing.Point(233, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1019, 657);
            this.tabs.TabIndex = 3;
            // 
            // tabSmoothedTrends
            // 
            this.tabSmoothedTrends.Controls.Add(this.chartSeriesSmoothed);
            this.tabSmoothedTrends.Location = new System.Drawing.Point(4, 22);
            this.tabSmoothedTrends.Name = "tabSmoothedTrends";
            this.tabSmoothedTrends.Padding = new System.Windows.Forms.Padding(3);
            this.tabSmoothedTrends.Size = new System.Drawing.Size(1011, 631);
            this.tabSmoothedTrends.TabIndex = 1;
            this.tabSmoothedTrends.Text = "Smoothed Trends";
            this.tabSmoothedTrends.UseVisualStyleBackColor = true;
            // 
            // chartSeriesSmoothed
            // 
            chartArea1.Name = "Main Chart Area";
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            chartArea1.AxisX.Interval = 1;
            chartArea1.AxisX.LabelStyle.Format = "yyyy.MM.dd";
            this.chartSeriesSmoothed.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend";
            this.chartSeriesSmoothed.Legends.Add(legend1);
            this.chartSeriesSmoothed.Location = new System.Drawing.Point(6, 6);
            this.chartSeriesSmoothed.Name = "chartSeriesSmoothed";
            this.chartSeriesSmoothed.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chartSeriesSmoothed.Size = new System.Drawing.Size(999, 619);
            this.chartSeriesSmoothed.TabIndex = 1;
            this.chartSeriesSmoothed.Text = "chartTrends";
            // 
            // tabCumulativeTrends
            // 
            this.tabCumulativeTrends.Controls.Add(this.chartSeriesCumulative);
            this.tabCumulativeTrends.Location = new System.Drawing.Point(4, 22);
            this.tabCumulativeTrends.Name = "tabCumulativeTrends";
            this.tabCumulativeTrends.Padding = new System.Windows.Forms.Padding(3);
            this.tabCumulativeTrends.Size = new System.Drawing.Size(1011, 631);
            this.tabCumulativeTrends.TabIndex = 1;
            this.tabCumulativeTrends.Text = "Cumulative Trends";
            this.tabCumulativeTrends.UseVisualStyleBackColor = true;
            // 
            // chartSeriesCumulative
            // 
            chartArea2.Name = "Main Chart Area";
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            chartArea2.AxisX.Interval = 1;
            chartArea2.AxisX.LabelStyle.Format = "yyyy.MM.dd";
            this.chartSeriesCumulative.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend";
            this.chartSeriesCumulative.Legends.Add(legend2);
            this.chartSeriesCumulative.Location = new System.Drawing.Point(6, 6);
            this.chartSeriesCumulative.Name = "chartSeriesCumulative";
            this.chartSeriesCumulative.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.chartSeriesCumulative.Size = new System.Drawing.Size(999, 619);
            this.chartSeriesCumulative.TabIndex = 1;
            this.chartSeriesCumulative.Text = "chartTrends";
            // 
            // tabLedger
            // 
            this.tabLedger.Controls.Add(this.lbTransactions);
            this.tabLedger.Location = new System.Drawing.Point(4, 22);
            this.tabLedger.Name = "tabLedger";
            this.tabLedger.Padding = new System.Windows.Forms.Padding(3);
            this.tabLedger.Size = new System.Drawing.Size(1011, 631);
            this.tabLedger.TabIndex = 0;
            this.tabLedger.Text = "Ledger";
            this.tabLedger.UseVisualStyleBackColor = true;
            // 
            // lbTransactions
            // 
            this.lbTransactions.FormattingEnabled = true;
            this.lbTransactions.Location = new System.Drawing.Point(6, 6);
            this.lbTransactions.Name = "lbTransactions";
            this.lbTransactions.Size = new System.Drawing.Size(999, 615);
            this.lbTransactions.TabIndex = 1;
            this.lbTransactions.Font = new System.Drawing.Font("Consolas", 14);
            this.lbTransactions.HorizontalScrollbar = true;
            this.lbTransactions.DoubleClick += new System.EventHandler(this.lb_DoubleClick);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "yyyy.MM.dd";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(12, 86);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEnd.TabIndex = 14;
            this.dateTimePickerEnd.ValueChanged += new System.EventHandler(this.dateTimePickerEnd_ValueChanged);
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Checked = false;
            this.dateTimePickerStart.CustomFormat = "yyyy.MM.dd";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(12, 35);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStart.TabIndex = 13;
            this.dateTimePickerStart.Value = new System.DateTime(2020, 1, 19, 15, 10, 57, 0);
            this.dateTimePickerStart.ValueChanged += new System.EventHandler(this.dateTimePickerStart_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(12, 65);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(50, 13);
            this.lblEndDate.TabIndex = 12;
            this.lblEndDate.Text = "End date";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(12, 14);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(53, 13);
            this.lblStartDate.TabIndex = 11;
            this.lblStartDate.Text = "Start date";
            //
            // cbxIsOnlyCustom
            //
            this.chboxAllCategories.AutoSize = true;
            this.chboxAllCategories.Location = new System.Drawing.Point(12, 425);
            this.chboxAllCategories.Name = "cbxAllCategories";
            this.chboxAllCategories.Size = new System.Drawing.Size(50, 13);
            this.chboxAllCategories.TabIndex = 12;
            this.chboxAllCategories.Text = "Show All Categories";
            this.chboxAllCategories.Checked = false;
            this.chboxAllCategories.CheckedChanged += new System.EventHandler(this.chboxAllCategories_CheckedChanged);
            // 
            // lblSmoothingRatio
            // 
            this.lblSmoothingRatio.AutoSize = true;
            this.lblSmoothingRatio.Location = new System.Drawing.Point(12, 480);
            this.lblSmoothingRatio.Name = "lblSmoothingRatio";
            this.lblSmoothingRatio.Size = new System.Drawing.Size(53, 13);
            this.lblSmoothingRatio.TabIndex = 11;
            this.lblSmoothingRatio.Text = "Smoothing Ratio";
            // 
            // txtboxSmoothingRatio
            // 
            this.txtboxSmoothingRatio.AutoSize = true;
            this.txtboxSmoothingRatio.Location = new System.Drawing.Point(12, 500);
            this.txtboxSmoothingRatio.Name = "txtboxSmoothingRatio";
            this.txtboxSmoothingRatio.Size = new System.Drawing.Size(93, 23);
            this.txtboxSmoothingRatio.TabIndex = 11;
            this.txtboxSmoothingRatio.TextChanged += new System.EventHandler(this.txtboxSmoothingRatio_TextChanged);
            this.txtboxSmoothingRatio.Text = "0.99";
            // 
            // clbCategories
            // 
            this.clbCategories.CheckOnClick = true;
            this.clbCategories.FormattingEnabled = true;
            this.clbCategories.Location = new System.Drawing.Point(12, 140);
            this.clbCategories.Name = "clbCategories";
            this.clbCategories.Size = new System.Drawing.Size(200, 274);
            this.clbCategories.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.clbCategories);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.lblSmoothingRatio);
            this.Controls.Add(this.txtboxSmoothingRatio);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.chboxAllCategories);
            this.Name = "MainForm";
            this.Text = "Money Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabs.ResumeLayout(false);
            this.tabSmoothedTrends.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.chartSeriesSmoothed)).EndInit();
            this.tabCumulativeTrends.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.chartSeriesCumulative)).EndInit();
            this.tabLedger.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataVisualization.Charting.Chart chartSeriesCumulative;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSeriesSmoothed;
        private System.Windows.Forms.CheckedListBox clbCategories;
        private System.Windows.Forms.CheckBox chboxAllCategories;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblSmoothingRatio;
        private System.Windows.Forms.TextBox txtboxSmoothingRatio;
        private System.Windows.Forms.ListBox lbTransactions;
        private System.Windows.Forms.TabPage tabCumulativeTrends;
        private System.Windows.Forms.TabPage tabLedger;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabSmoothedTrends;

        #endregion
    }
}