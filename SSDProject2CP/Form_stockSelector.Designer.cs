namespace SSDProject2CP
{
    partial class Form_stockSelector
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
            this.label_startDate = new System.Windows.Forms.Label();
            this.label_endDate = new System.Windows.Forms.Label();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.button_readTicker = new System.Windows.Forms.Button();
            this.openFileDialog_stockSelect = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label_startDate
            // 
            this.label_startDate.AutoSize = true;
            this.label_startDate.Location = new System.Drawing.Point(111, 39);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(66, 16);
            this.label_startDate.TabIndex = 0;
            this.label_startDate.Text = "Start Date";
            // 
            // label_endDate
            // 
            this.label_endDate.AutoSize = true;
            this.label_endDate.Location = new System.Drawing.Point(111, 130);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(63, 16);
            this.label_endDate.TabIndex = 1;
            this.label_endDate.Text = "End Date";
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(31, 71);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(242, 22);
            this.dateTimePicker_startDate.TabIndex = 2;
            this.dateTimePicker_startDate.Value = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(31, 164);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(236, 22);
            this.dateTimePicker_endDate.TabIndex = 3;
            // 
            // button_readTicker
            // 
            this.button_readTicker.Location = new System.Drawing.Point(89, 235);
            this.button_readTicker.Name = "button_readTicker";
            this.button_readTicker.Size = new System.Drawing.Size(107, 31);
            this.button_readTicker.TabIndex = 4;
            this.button_readTicker.Text = "Load Stock";
            this.button_readTicker.UseVisualStyleBackColor = true;
            this.button_readTicker.Click += new System.EventHandler(this.button_readTicker_Click);
            // 
            // openFileDialog_stockSelect
            // 
            this.openFileDialog_stockSelect.Multiselect = true;
            // 
            // Form_stockSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 329);
            this.Controls.Add(this.button_readTicker);
            this.Controls.Add(this.dateTimePicker_endDate);
            this.Controls.Add(this.dateTimePicker_startDate);
            this.Controls.Add(this.label_endDate);
            this.Controls.Add(this.label_startDate);
            this.Name = "Form_stockSelector";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_startDate;
        private System.Windows.Forms.Label label_endDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_startDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker_endDate;
        private System.Windows.Forms.Button button_readTicker;
        private System.Windows.Forms.OpenFileDialog openFileDialog_stockSelect;
    }
}

