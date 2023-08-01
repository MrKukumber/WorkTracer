namespace WorkTracer
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
            this.ReturnToMain_button = new System.Windows.Forms.Button();
            this.Commit_richTextBox = new System.Windows.Forms.RichTextBox();
            this.Commit_vScrollBar = new System.Windows.Forms.VScrollBar();
            this.CompDurationText_label = new System.Windows.Forms.Label();
            this.Until_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Since_label = new System.Windows.Forms.Label();
            this.Until_label = new System.Windows.Forms.Label();
            this.CompDurationWithPauseText_label = new System.Windows.Forms.Label();
            this.CreatDuration_label = new System.Windows.Forms.Label();
            this.CreatDurationWithPause_label = new System.Windows.Forms.Label();
            this.ProgrDuration_label = new System.Windows.Forms.Label();
            this.ProgrDurationWithPause_label = new System.Windows.Forms.Label();
            this.DebugDuration_label = new System.Windows.Forms.Label();
            this.DebugDurationWithPause_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.RecordingTimeWithPause_label = new System.Windows.Forms.Label();
            this.RecordingTime_label = new System.Windows.Forms.Label();
            this.CreatTime_label = new System.Windows.Forms.Label();
            this.ProgrTime_label = new System.Windows.Forms.Label();
            this.DebugTime_label = new System.Windows.Forms.Label();
            this.CompDuration_label = new System.Windows.Forms.Label();
            this.CompDurationWithPause_label = new System.Windows.Forms.Label();
            this.RangeCommit_label = new System.Windows.Forms.Label();
            this.SameDate_label = new System.Windows.Forms.Label();
            this.SameDate_checkBox = new System.Windows.Forms.CheckBox();
            this.Since_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.RecordSinceText_label = new System.Windows.Forms.Label();
            this.RecordSinceDate_label = new System.Windows.Forms.Label();
            this.RecordUntilText_label = new System.Windows.Forms.Label();
            this.RecordUntilDate_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReturnToMain_button
            // 
            this.ReturnToMain_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ReturnToMain_button.Location = new System.Drawing.Point(966, 408);
            this.ReturnToMain_button.Name = "ReturnToMain_button";
            this.ReturnToMain_button.Size = new System.Drawing.Size(110, 70);
            this.ReturnToMain_button.TabIndex = 0;
            this.ReturnToMain_button.Text = "ReturnToMain_button";
            this.ReturnToMain_button.UseVisualStyleBackColor = true;
            this.ReturnToMain_button.Click += new System.EventHandler(this.MainFormOpening_button_Click);
            // 
            // Commit_richTextBox
            // 
            this.Commit_richTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Commit_richTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Commit_richTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Commit_richTextBox.Location = new System.Drawing.Point(21, 57);
            this.Commit_richTextBox.Name = "Commit_richTextBox";
            this.Commit_richTextBox.Size = new System.Drawing.Size(286, 423);
            this.Commit_richTextBox.TabIndex = 6;
            this.Commit_richTextBox.Text = "1234567890123456789012345678901234567890";
            // 
            // Commit_vScrollBar
            // 
            this.Commit_vScrollBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Commit_vScrollBar.LargeChange = 1;
            this.Commit_vScrollBar.Location = new System.Drawing.Point(329, 57);
            this.Commit_vScrollBar.Maximum = 5;
            this.Commit_vScrollBar.Name = "Commit_vScrollBar";
            this.Commit_vScrollBar.Size = new System.Drawing.Size(26, 394);
            this.Commit_vScrollBar.TabIndex = 8;
            this.Commit_vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Commit_vScrollBar_Scroll);
            // 
            // CompDurationText_label
            // 
            this.CompDurationText_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CompDurationText_label.Font = new System.Drawing.Font("Segoe UI", 12.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompDurationText_label.Location = new System.Drawing.Point(358, 307);
            this.CompDurationText_label.Name = "CompDurationText_label";
            this.CompDurationText_label.Size = new System.Drawing.Size(438, 35);
            this.CompDurationText_label.TabIndex = 11;
            this.CompDurationText_label.Text = "CompDurationText_label";
            // 
            // Until_dateTimePicker
            // 
            this.Until_dateTimePicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Until_dateTimePicker.Location = new System.Drawing.Point(788, 37);
            this.Until_dateTimePicker.Name = "Until_dateTimePicker";
            this.Until_dateTimePicker.Size = new System.Drawing.Size(250, 27);
            this.Until_dateTimePicker.TabIndex = 13;
            this.Until_dateTimePicker.CloseUp += new System.EventHandler(this.Until_dateTimePicker_CloseUp);
            // 
            // Since_label
            // 
            this.Since_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Since_label.Location = new System.Drawing.Point(413, 9);
            this.Since_label.Name = "Since_label";
            this.Since_label.Size = new System.Drawing.Size(248, 20);
            this.Since_label.TabIndex = 14;
            this.Since_label.Text = "Since_label";
            this.Since_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Until_label
            // 
            this.Until_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Until_label.Location = new System.Drawing.Point(788, 9);
            this.Until_label.Name = "Until_label";
            this.Until_label.Size = new System.Drawing.Size(250, 20);
            this.Until_label.TabIndex = 15;
            this.Until_label.Text = "Until_label";
            this.Until_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CompDurationWithPauseText_label
            // 
            this.CompDurationWithPauseText_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CompDurationWithPauseText_label.Font = new System.Drawing.Font("Segoe UI", 12.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompDurationWithPauseText_label.Location = new System.Drawing.Point(358, 387);
            this.CompDurationWithPauseText_label.Name = "CompDurationWithPauseText_label";
            this.CompDurationWithPauseText_label.Size = new System.Drawing.Size(438, 70);
            this.CompDurationWithPauseText_label.TabIndex = 16;
            this.CompDurationWithPauseText_label.Text = "CompDurationWithPauseText_label";
            // 
            // CreatDuration_label
            // 
            this.CreatDuration_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CreatDuration_label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CreatDuration_label.Location = new System.Drawing.Point(3, 0);
            this.CreatDuration_label.Name = "CreatDuration_label";
            this.CreatDuration_label.Size = new System.Drawing.Size(163, 25);
            this.CreatDuration_label.TabIndex = 17;
            this.CreatDuration_label.Text = "CreatDuration_label";
            this.CreatDuration_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CreatDurationWithPause_label
            // 
            this.CreatDurationWithPause_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CreatDurationWithPause_label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CreatDurationWithPause_label.Location = new System.Drawing.Point(3, 62);
            this.CreatDurationWithPause_label.Name = "CreatDurationWithPause_label";
            this.CreatDurationWithPause_label.Size = new System.Drawing.Size(163, 25);
            this.CreatDurationWithPause_label.TabIndex = 18;
            this.CreatDurationWithPause_label.Text = "CreatDurationWithPause_label";
            this.CreatDurationWithPause_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProgrDuration_label
            // 
            this.ProgrDuration_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ProgrDuration_label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProgrDuration_label.Location = new System.Drawing.Point(172, 0);
            this.ProgrDuration_label.Name = "ProgrDuration_label";
            this.ProgrDuration_label.Size = new System.Drawing.Size(172, 25);
            this.ProgrDuration_label.TabIndex = 19;
            this.ProgrDuration_label.Text = "ProgrDuration_label";
            this.ProgrDuration_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProgrDurationWithPause_label
            // 
            this.ProgrDurationWithPause_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ProgrDurationWithPause_label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProgrDurationWithPause_label.Location = new System.Drawing.Point(172, 62);
            this.ProgrDurationWithPause_label.Name = "ProgrDurationWithPause_label";
            this.ProgrDurationWithPause_label.Size = new System.Drawing.Size(172, 25);
            this.ProgrDurationWithPause_label.TabIndex = 20;
            this.ProgrDurationWithPause_label.Text = "ProgrDurationWithPause_label";
            this.ProgrDurationWithPause_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DebugDuration_label
            // 
            this.DebugDuration_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DebugDuration_label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DebugDuration_label.Location = new System.Drawing.Point(350, 0);
            this.DebugDuration_label.Name = "DebugDuration_label";
            this.DebugDuration_label.Size = new System.Drawing.Size(168, 25);
            this.DebugDuration_label.TabIndex = 21;
            this.DebugDuration_label.Text = "DebugDuration_label";
            this.DebugDuration_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DebugDurationWithPause_label
            // 
            this.DebugDurationWithPause_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DebugDurationWithPause_label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DebugDurationWithPause_label.Location = new System.Drawing.Point(350, 62);
            this.DebugDurationWithPause_label.Name = "DebugDurationWithPause_label";
            this.DebugDurationWithPause_label.Size = new System.Drawing.Size(168, 25);
            this.DebugDurationWithPause_label.TabIndex = 22;
            this.DebugDurationWithPause_label.Text = "DebugDurationWithPause_label";
            this.DebugDurationWithPause_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.70317F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.29683F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 173F));
            this.tableLayoutPanel1.Controls.Add(this.DebugDurationWithPause_label, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.DebugDuration_label, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ProgrDuration_label, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CreatDurationWithPause_label, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ProgrDurationWithPause_label, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.CreatDuration_label, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(534, 156);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(521, 125);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // RecordingTimeWithPause_label
            // 
            this.RecordingTimeWithPause_label.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RecordingTimeWithPause_label.Location = new System.Drawing.Point(365, 218);
            this.RecordingTimeWithPause_label.Name = "RecordingTimeWithPause_label";
            this.RecordingTimeWithPause_label.Size = new System.Drawing.Size(163, 44);
            this.RecordingTimeWithPause_label.TabIndex = 24;
            this.RecordingTimeWithPause_label.Text = "RecordingTimeWithPause_label";
            this.RecordingTimeWithPause_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RecordingTime_label
            // 
            this.RecordingTime_label.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RecordingTime_label.Location = new System.Drawing.Point(365, 163);
            this.RecordingTime_label.Name = "RecordingTime_label";
            this.RecordingTime_label.Size = new System.Drawing.Size(163, 20);
            this.RecordingTime_label.TabIndex = 25;
            this.RecordingTime_label.Text = "RecordingTime_label";
            this.RecordingTime_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CreatTime_label
            // 
            this.CreatTime_label.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CreatTime_label.Location = new System.Drawing.Point(537, 120);
            this.CreatTime_label.Name = "CreatTime_label";
            this.CreatTime_label.Size = new System.Drawing.Size(163, 33);
            this.CreatTime_label.TabIndex = 26;
            this.CreatTime_label.Text = "CreatTime_label";
            this.CreatTime_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProgrTime_label
            // 
            this.ProgrTime_label.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProgrTime_label.Location = new System.Drawing.Point(706, 120);
            this.ProgrTime_label.Name = "ProgrTime_label";
            this.ProgrTime_label.Size = new System.Drawing.Size(172, 33);
            this.ProgrTime_label.TabIndex = 27;
            this.ProgrTime_label.Text = "ProgrTime_label";
            this.ProgrTime_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DebugTime_label
            // 
            this.DebugTime_label.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DebugTime_label.Location = new System.Drawing.Point(884, 120);
            this.DebugTime_label.Name = "DebugTime_label";
            this.DebugTime_label.Size = new System.Drawing.Size(171, 33);
            this.DebugTime_label.TabIndex = 28;
            this.DebugTime_label.Text = "DebugTime_label";
            this.DebugTime_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CompDuration_label
            // 
            this.CompDuration_label.Font = new System.Drawing.Font("Segoe UI", 12.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompDuration_label.Location = new System.Drawing.Point(358, 339);
            this.CompDuration_label.Name = "CompDuration_label";
            this.CompDuration_label.Size = new System.Drawing.Size(438, 28);
            this.CompDuration_label.TabIndex = 29;
            this.CompDuration_label.Text = "CompDuration_label";
            // 
            // CompDurationWithPause_label
            // 
            this.CompDurationWithPause_label.Font = new System.Drawing.Font("Segoe UI", 12.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CompDurationWithPause_label.Location = new System.Drawing.Point(358, 450);
            this.CompDurationWithPause_label.Name = "CompDurationWithPause_label";
            this.CompDurationWithPause_label.Size = new System.Drawing.Size(438, 28);
            this.CompDurationWithPause_label.TabIndex = 30;
            this.CompDurationWithPause_label.Text = "CompDurationWithPause_label";
            // 
            // RangeCommit_label
            // 
            this.RangeCommit_label.Location = new System.Drawing.Point(21, 29);
            this.RangeCommit_label.Name = "RangeCommit_label";
            this.RangeCommit_label.Size = new System.Drawing.Size(286, 25);
            this.RangeCommit_label.TabIndex = 31;
            this.RangeCommit_label.Text = "RangeCommit_label";
            this.RangeCommit_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SameDate_label
            // 
            this.SameDate_label.Location = new System.Drawing.Point(655, 9);
            this.SameDate_label.Name = "SameDate_label";
            this.SameDate_label.Size = new System.Drawing.Size(141, 20);
            this.SameDate_label.TabIndex = 32;
            this.SameDate_label.Text = "SameDate_label";
            this.SameDate_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SameDate_checkBox
            // 
            this.SameDate_checkBox.AutoSize = true;
            this.SameDate_checkBox.Location = new System.Drawing.Point(715, 37);
            this.SameDate_checkBox.Name = "SameDate_checkBox";
            this.SameDate_checkBox.Size = new System.Drawing.Size(18, 17);
            this.SameDate_checkBox.TabIndex = 33;
            this.SameDate_checkBox.UseVisualStyleBackColor = true;
            this.SameDate_checkBox.CheckedChanged += new System.EventHandler(this.SameDate_checkBox_CheckedChanged);
            // 
            // Since_dateTimePicker
            // 
            this.Since_dateTimePicker.Location = new System.Drawing.Point(413, 37);
            this.Since_dateTimePicker.Name = "Since_dateTimePicker";
            this.Since_dateTimePicker.Size = new System.Drawing.Size(250, 27);
            this.Since_dateTimePicker.TabIndex = 34;
            this.Since_dateTimePicker.CloseUp += new System.EventHandler(this.Since_dateTimePicker_CloseUp);
            // 
            // RecordSinceText_label
            // 
            this.RecordSinceText_label.Location = new System.Drawing.Point(413, 67);
            this.RecordSinceText_label.Name = "RecordSinceText_label";
            this.RecordSinceText_label.Size = new System.Drawing.Size(132, 20);
            this.RecordSinceText_label.TabIndex = 35;
            this.RecordSinceText_label.Text = "RecordSinceText_label";
            this.RecordSinceText_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // RecordSinceDate_label
            // 
            this.RecordSinceDate_label.Location = new System.Drawing.Point(551, 67);
            this.RecordSinceDate_label.Name = "RecordSinceDate_label";
            this.RecordSinceDate_label.Size = new System.Drawing.Size(110, 20);
            this.RecordSinceDate_label.TabIndex = 36;
            this.RecordSinceDate_label.Text = "RecordSinceDate_label";
            // 
            // RecordUntilText_label
            // 
            this.RecordUntilText_label.Location = new System.Drawing.Point(788, 67);
            this.RecordUntilText_label.Name = "RecordUntilText_label";
            this.RecordUntilText_label.Size = new System.Drawing.Size(135, 20);
            this.RecordUntilText_label.TabIndex = 37;
            this.RecordUntilText_label.Text = "RecordUntilText_label";
            this.RecordUntilText_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // RecordUntilDate_label
            // 
            this.RecordUntilDate_label.Location = new System.Drawing.Point(929, 67);
            this.RecordUntilDate_label.Name = "RecordUntilDate_label";
            this.RecordUntilDate_label.Size = new System.Drawing.Size(109, 20);
            this.RecordUntilDate_label.TabIndex = 38;
            this.RecordUntilDate_label.Text = "RecordUntilDate_label";
            // 
            // Progress_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 495);
            this.Controls.Add(this.RecordUntilDate_label);
            this.Controls.Add(this.RecordUntilText_label);
            this.Controls.Add(this.RecordSinceDate_label);
            this.Controls.Add(this.RecordSinceText_label);
            this.Controls.Add(this.Since_dateTimePicker);
            this.Controls.Add(this.SameDate_checkBox);
            this.Controls.Add(this.SameDate_label);
            this.Controls.Add(this.RangeCommit_label);
            this.Controls.Add(this.CompDurationWithPause_label);
            this.Controls.Add(this.CompDuration_label);
            this.Controls.Add(this.DebugTime_label);
            this.Controls.Add(this.ProgrTime_label);
            this.Controls.Add(this.CreatTime_label);
            this.Controls.Add(this.RecordingTime_label);
            this.Controls.Add(this.RecordingTimeWithPause_label);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.CompDurationWithPauseText_label);
            this.Controls.Add(this.Until_label);
            this.Controls.Add(this.Since_label);
            this.Controls.Add(this.Until_dateTimePicker);
            this.Controls.Add(this.CompDurationText_label);
            this.Controls.Add(this.Commit_vScrollBar);
            this.Controls.Add(this.Commit_richTextBox);
            this.Controls.Add(this.ReturnToMain_button);
            this.Name = "Progress_form";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Progress_form_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ReturnToMain_button;
        private RichTextBox Commit_richTextBox;
        private VScrollBar Commit_vScrollBar;
        private Label CompDurationText_label;
        private DateTimePicker Until_dateTimePicker;
        private Label Since_label;
        private Label Until_label;
        private Label CompDurationWithPauseText_label;
        private Label CreatDuration_label;
        private Label CreatDurationWithPause_label;
        private Label ProgrDuration_label;
        private Label ProgrDurationWithPause_label;
        private Label DebugDuration_label;
        private Label DebugDurationWithPause_label;
        private TableLayoutPanel tableLayoutPanel1;
        private Label RecordingTimeWithPause_label;
        private Label RecordingTime_label;
        private Label CreatTime_label;
        private Label ProgrTime_label;
        private Label DebugTime_label;
        private Label CompDuration_label;
        private Label CompDurationWithPause_label;
        private Label RangeCommit_label;
        private Label SameDate_label;
        private CheckBox SameDate_checkBox;
        private DateTimePicker Since_dateTimePicker;
        private Label RecordSinceText_label;
        private Label RecordSinceDate_label;
        private Label RecordUntilText_label;
        private Label RecordUntilDate_label;
    }
}