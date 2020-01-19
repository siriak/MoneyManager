namespace WinFormsUI
{
    partial class TimeSeriesForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartSeries = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartSeries)).BeginInit();
            this.SuspendLayout();
            // 
            // chartSeries
            // 
            chartArea1.Name = "ChartArea1";
            this.chartSeries.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartSeries.Legends.Add(legend1);
            this.chartSeries.Location = new System.Drawing.Point(78, 63);
            this.chartSeries.Name = "chartSeries";
            this.chartSeries.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartSeries.Series.Add(series1);
            this.chartSeries.Size = new System.Drawing.Size(636, 321);
            this.chartSeries.TabIndex = 0;
            this.chartSeries.Text = "chartSeries";
            // 
            // TimeSeriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chartSeries);
            this.Name = "TimeSeriesForm";
            this.Text = "TimeSeries";
            this.Load += new System.EventHandler(this.TimeSeriesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSeries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartSeries;
    }
}