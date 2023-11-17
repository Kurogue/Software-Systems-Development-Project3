namespace SSDProject2CP
{
    partial class Form_stockCharts
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_candleSticks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_stockVolume = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker_startDateForm2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDateForm2 = new System.Windows.Forms.DateTimePicker();
            this.label_startDateForm2 = new System.Windows.Forms.Label();
            this.label_endDateForm2 = new System.Windows.Forms.Label();
            this.button_stockUpdate = new System.Windows.Forms.Button();
            this.comboBox_stockPatterns = new System.Windows.Forms.ComboBox();
            this.label_stockPatterns = new System.Windows.Forms.Label();
            this.button_clearAnnotations = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart_candleSticks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_stockVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_candleSticks
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_candleSticks.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_candleSticks.Legends.Add(legend1);
            this.chart_candleSticks.Location = new System.Drawing.Point(12, 12);
            this.chart_candleSticks.Name = "chart_candleSticks";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_candleSticks.Series.Add(series1);
            this.chart_candleSticks.Size = new System.Drawing.Size(1012, 387);
            this.chart_candleSticks.TabIndex = 0;
            this.chart_candleSticks.Text = "chart1";
            // 
            // chart_stockVolume
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_stockVolume.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart_stockVolume.Legends.Add(legend2);
            this.chart_stockVolume.Location = new System.Drawing.Point(12, 405);
            this.chart_stockVolume.Name = "chart_stockVolume";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_stockVolume.Series.Add(series2);
            this.chart_stockVolume.Size = new System.Drawing.Size(1012, 209);
            this.chart_stockVolume.TabIndex = 1;
            this.chart_stockVolume.Text = "chart2";
            // 
            // dateTimePicker_startDateForm2
            // 
            this.dateTimePicker_startDateForm2.Location = new System.Drawing.Point(1051, 134);
            this.dateTimePicker_startDateForm2.Name = "dateTimePicker_startDateForm2";
            this.dateTimePicker_startDateForm2.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_startDateForm2.TabIndex = 2;
            // 
            // dateTimePicker_endDateForm2
            // 
            this.dateTimePicker_endDateForm2.Location = new System.Drawing.Point(1051, 213);
            this.dateTimePicker_endDateForm2.Name = "dateTimePicker_endDateForm2";
            this.dateTimePicker_endDateForm2.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_endDateForm2.TabIndex = 3;
            // 
            // label_startDateForm2
            // 
            this.label_startDateForm2.AutoSize = true;
            this.label_startDateForm2.Location = new System.Drawing.Point(1119, 103);
            this.label_startDateForm2.Name = "label_startDateForm2";
            this.label_startDateForm2.Size = new System.Drawing.Size(66, 16);
            this.label_startDateForm2.TabIndex = 4;
            this.label_startDateForm2.Text = "Start Date";
            // 
            // label_endDateForm2
            // 
            this.label_endDateForm2.AutoSize = true;
            this.label_endDateForm2.Location = new System.Drawing.Point(1122, 191);
            this.label_endDateForm2.Name = "label_endDateForm2";
            this.label_endDateForm2.Size = new System.Drawing.Size(63, 16);
            this.label_endDateForm2.TabIndex = 5;
            this.label_endDateForm2.Text = "End Date";
            // 
            // button_stockUpdate
            // 
            this.button_stockUpdate.Location = new System.Drawing.Point(1113, 278);
            this.button_stockUpdate.Name = "button_stockUpdate";
            this.button_stockUpdate.Size = new System.Drawing.Size(83, 34);
            this.button_stockUpdate.TabIndex = 6;
            this.button_stockUpdate.Text = "Update";
            this.button_stockUpdate.UseVisualStyleBackColor = true;
            this.button_stockUpdate.Click += new System.EventHandler(this.button_stockUpdate_Click);
            // 
            // comboBox_stockPatterns
            // 
            this.comboBox_stockPatterns.FormattingEnabled = true;
            this.comboBox_stockPatterns.Location = new System.Drawing.Point(1051, 384);
            this.comboBox_stockPatterns.Name = "comboBox_stockPatterns";
            this.comboBox_stockPatterns.Size = new System.Drawing.Size(200, 24);
            this.comboBox_stockPatterns.TabIndex = 7;
            this.comboBox_stockPatterns.SelectedIndexChanged += new System.EventHandler(this.comboBox_stockPatterns_SelectedIndexChanged);
            // 
            // label_stockPatterns
            // 
            this.label_stockPatterns.AutoSize = true;
            this.label_stockPatterns.Location = new System.Drawing.Point(1103, 354);
            this.label_stockPatterns.Name = "label_stockPatterns";
            this.label_stockPatterns.Size = new System.Drawing.Size(93, 16);
            this.label_stockPatterns.TabIndex = 8;
            this.label_stockPatterns.Text = "Stock Patterns";
            // 
            // button_clearAnnotations
            // 
            this.button_clearAnnotations.Location = new System.Drawing.Point(1113, 430);
            this.button_clearAnnotations.Name = "button_clearAnnotations";
            this.button_clearAnnotations.Size = new System.Drawing.Size(83, 34);
            this.button_clearAnnotations.TabIndex = 9;
            this.button_clearAnnotations.Text = "Clear";
            this.button_clearAnnotations.UseVisualStyleBackColor = true;
            this.button_clearAnnotations.Click += new System.EventHandler(this.button_clearAnnotations_Click);
            // 
            // Form_stockCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 626);
            this.Controls.Add(this.button_clearAnnotations);
            this.Controls.Add(this.label_stockPatterns);
            this.Controls.Add(this.comboBox_stockPatterns);
            this.Controls.Add(this.button_stockUpdate);
            this.Controls.Add(this.label_endDateForm2);
            this.Controls.Add(this.label_startDateForm2);
            this.Controls.Add(this.dateTimePicker_endDateForm2);
            this.Controls.Add(this.dateTimePicker_startDateForm2);
            this.Controls.Add(this.chart_stockVolume);
            this.Controls.Add(this.chart_candleSticks);
            this.Name = "Form_stockCharts";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_stockCharts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_candleSticks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_stockVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chart_candleSticks;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart_stockVolume;
        private System.Windows.Forms.Label label_startDateForm2;
        private System.Windows.Forms.Label label_endDateForm2;
        private System.Windows.Forms.Button button_stockUpdate;
        public System.Windows.Forms.DateTimePicker dateTimePicker_startDateForm2;
        public System.Windows.Forms.DateTimePicker dateTimePicker_endDateForm2;
        private System.Windows.Forms.ComboBox comboBox_stockPatterns;
        private System.Windows.Forms.Label label_stockPatterns;
        private System.Windows.Forms.Button button_clearAnnotations;
    }
}