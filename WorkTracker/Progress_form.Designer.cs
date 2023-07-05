﻿namespace WorkTracker
{
    partial class Progress_form
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
            this.MainFormOpening_button = new System.Windows.Forms.Button();
            this.Progress_trackBar = new System.Windows.Forms.TrackBar();
            this.Day_label = new System.Windows.Forms.Label();
            this.Days7_label = new System.Windows.Forms.Label();
            this.Month_label = new System.Windows.Forms.Label();
            this.Progress_pictureBox = new System.Windows.Forms.PictureBox();
            this.Commit_richTextBox = new System.Windows.Forms.RichTextBox();
            this.Commit_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Commit_vScrollBar = new System.Windows.Forms.VScrollBar();
            this.DayMean_pictureBox = new System.Windows.Forms.PictureBox();
            this.DayMean_label = new System.Windows.Forms.Label();
            this.CompDuration_label = new System.Windows.Forms.Label();
            this.From_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.To_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.From_label = new System.Windows.Forms.Label();
            this.To_label = new System.Windows.Forms.Label();
            this.CompDurationWithStop_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayMean_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainFormOpening_button
            // 
            this.MainFormOpening_button.Location = new System.Drawing.Point(1001, 610);
            this.MainFormOpening_button.Name = "MainFormOpening_button";
            this.MainFormOpening_button.Size = new System.Drawing.Size(94, 50);
            this.MainFormOpening_button.TabIndex = 0;
            this.MainFormOpening_button.Text = "MainFormOpening_button";
            this.MainFormOpening_button.UseVisualStyleBackColor = true;
            this.MainFormOpening_button.Click += new System.EventHandler(this.MainFormOpening_button_Click);
            // 
            // Progress_trackBar
            // 
            this.Progress_trackBar.LargeChange = 1;
            this.Progress_trackBar.Location = new System.Drawing.Point(351, 34);
            this.Progress_trackBar.Maximum = 2;
            this.Progress_trackBar.Name = "Progress_trackBar";
            this.Progress_trackBar.Size = new System.Drawing.Size(394, 56);
            this.Progress_trackBar.TabIndex = 1;
            // 
            // Day_label
            // 
            this.Day_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Day_label.AutoSize = true;
            this.Day_label.Location = new System.Drawing.Point(349, 11);
            this.Day_label.Name = "Day_label";
            this.Day_label.Size = new System.Drawing.Size(74, 20);
            this.Day_label.TabIndex = 2;
            this.Day_label.Text = "Day_label";
            // 
            // Days7_label
            // 
            this.Days7_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Days7_label.AutoSize = true;
            this.Days7_label.Location = new System.Drawing.Point(504, 11);
            this.Days7_label.Name = "Days7_label";
            this.Days7_label.Size = new System.Drawing.Size(88, 20);
            this.Days7_label.TabIndex = 3;
            this.Days7_label.Text = "Days7_label";
            // 
            // Month_label
            // 
            this.Month_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Month_label.AutoSize = true;
            this.Month_label.Location = new System.Drawing.Point(670, 11);
            this.Month_label.Name = "Month_label";
            this.Month_label.Size = new System.Drawing.Size(91, 20);
            this.Month_label.TabIndex = 4;
            this.Month_label.Text = "Month_label";
            // 
            // Progress_pictureBox
            // 
            this.Progress_pictureBox.Location = new System.Drawing.Point(12, 74);
            this.Progress_pictureBox.Name = "Progress_pictureBox";
            this.Progress_pictureBox.Size = new System.Drawing.Size(1083, 199);
            this.Progress_pictureBox.TabIndex = 5;
            this.Progress_pictureBox.TabStop = false;
            // 
            // Commit_richTextBox
            // 
            this.Commit_richTextBox.Location = new System.Drawing.Point(12, 312);
            this.Commit_richTextBox.Name = "Commit_richTextBox";
            this.Commit_richTextBox.Size = new System.Drawing.Size(305, 348);
            this.Commit_richTextBox.TabIndex = 6;
            this.Commit_richTextBox.Text = "";
            // 
            // Commit_dateTimePicker
            // 
            this.Commit_dateTimePicker.Location = new System.Drawing.Point(12, 279);
            this.Commit_dateTimePicker.Name = "Commit_dateTimePicker";
            this.Commit_dateTimePicker.Size = new System.Drawing.Size(334, 27);
            this.Commit_dateTimePicker.TabIndex = 7;
            // 
            // Commit_vScrollBar
            // 
            this.Commit_vScrollBar.Location = new System.Drawing.Point(320, 339);
            this.Commit_vScrollBar.Name = "Commit_vScrollBar";
            this.Commit_vScrollBar.Size = new System.Drawing.Size(26, 294);
            this.Commit_vScrollBar.TabIndex = 8;
            // 
            // DayMean_pictureBox
            // 
            this.DayMean_pictureBox.Location = new System.Drawing.Point(370, 349);
            this.DayMean_pictureBox.Name = "DayMean_pictureBox";
            this.DayMean_pictureBox.Size = new System.Drawing.Size(725, 217);
            this.DayMean_pictureBox.TabIndex = 9;
            this.DayMean_pictureBox.TabStop = false;
            // 
            // DayMean_label
            // 
            this.DayMean_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DayMean_label.AutoSize = true;
            this.DayMean_label.Location = new System.Drawing.Point(670, 315);
            this.DayMean_label.Name = "DayMean_label";
            this.DayMean_label.Size = new System.Drawing.Size(111, 20);
            this.DayMean_label.TabIndex = 10;
            this.DayMean_label.Text = "DayMean_label";
            // 
            // CompDuration_label
            // 
            this.CompDuration_label.AutoSize = true;
            this.CompDuration_label.Font = new System.Drawing.Font("Segoe UI", 14.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompDuration_label.Location = new System.Drawing.Point(404, 584);
            this.CompDuration_label.Name = "CompDuration_label";
            this.CompDuration_label.Size = new System.Drawing.Size(248, 32);
            this.CompDuration_label.TabIndex = 11;
            this.CompDuration_label.Text = "CompDuration_label";
            // 
            // From_dateTimePicker
            // 
            this.From_dateTimePicker.Location = new System.Drawing.Point(32, 34);
            this.From_dateTimePicker.Name = "From_dateTimePicker";
            this.From_dateTimePicker.Size = new System.Drawing.Size(250, 27);
            this.From_dateTimePicker.TabIndex = 12;
            // 
            // To_dateTimePicker
            // 
            this.To_dateTimePicker.Location = new System.Drawing.Point(817, 34);
            this.To_dateTimePicker.Name = "To_dateTimePicker";
            this.To_dateTimePicker.Size = new System.Drawing.Size(250, 27);
            this.To_dateTimePicker.TabIndex = 13;
            // 
            // From_label
            // 
            this.From_label.AutoSize = true;
            this.From_label.Location = new System.Drawing.Point(116, 6);
            this.From_label.Name = "From_label";
            this.From_label.Size = new System.Drawing.Size(82, 20);
            this.From_label.TabIndex = 14;
            this.From_label.Text = "From_label";
            // 
            // To_label
            // 
            this.To_label.AutoSize = true;
            this.To_label.Location = new System.Drawing.Point(910, 6);
            this.To_label.Name = "To_label";
            this.To_label.Size = new System.Drawing.Size(64, 20);
            this.To_label.TabIndex = 15;
            this.To_label.Text = "To_label";
            // 
            // CompDurationWithStop_label
            // 
            this.CompDurationWithStop_label.AutoSize = true;
            this.CompDurationWithStop_label.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompDurationWithStop_label.Location = new System.Drawing.Point(404, 630);
            this.CompDurationWithStop_label.Name = "CompDurationWithStop_label";
            this.CompDurationWithStop_label.Size = new System.Drawing.Size(337, 31);
            this.CompDurationWithStop_label.TabIndex = 16;
            this.CompDurationWithStop_label.Text = "CompDurationWithStop_label";
            // 
            // Progress_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 670);
            this.Controls.Add(this.CompDurationWithStop_label);
            this.Controls.Add(this.To_label);
            this.Controls.Add(this.From_label);
            this.Controls.Add(this.To_dateTimePicker);
            this.Controls.Add(this.From_dateTimePicker);
            this.Controls.Add(this.CompDuration_label);
            this.Controls.Add(this.DayMean_label);
            this.Controls.Add(this.DayMean_pictureBox);
            this.Controls.Add(this.Commit_vScrollBar);
            this.Controls.Add(this.Commit_dateTimePicker);
            this.Controls.Add(this.Commit_richTextBox);
            this.Controls.Add(this.Progress_pictureBox);
            this.Controls.Add(this.Month_label);
            this.Controls.Add(this.Days7_label);
            this.Controls.Add(this.Day_label);
            this.Controls.Add(this.Progress_trackBar);
            this.Controls.Add(this.MainFormOpening_button);
            this.Name = "Progress_form";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Progress_form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayMean_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button MainFormOpening_button;
        private TrackBar Progress_trackBar;
        private Label Day_label;
        private Label Days7_label;
        private Label Month_label;
        private PictureBox Progress_pictureBox;
        private RichTextBox Commit_richTextBox;
        private DateTimePicker Commit_dateTimePicker;
        private VScrollBar Commit_vScrollBar;
        private PictureBox DayMean_pictureBox;
        private Label DayMean_label;
        private Label CompDuration_label;
        private DataGridView dataGridView1;
        private DateTimePicker From_dateTimePicker;
        private DateTimePicker To_dateTimePicker;
        private Label From_label;
        private Label To_label;
        private Label CompDurationWithStop_label;
    }
}