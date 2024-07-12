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
            ReturnToMain_button = new Button();
            Commit_richTextBox = new RichTextBox();
            Commit_vScrollBar = new VScrollBar();
            CompDurationText_label = new Label();
            Until_dateTimePicker = new DateTimePicker();
            Since_label = new Label();
            Until_label = new Label();
            CompDurationWithPauseText_label = new Label();
            CreatDuration_label = new Label();
            CreatDurationWithPause_label = new Label();
            ProgrDuration_label = new Label();
            ProgrDurationWithPause_label = new Label();
            DebugDuration_label = new Label();
            DebugDurationWithPause_label = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            CommeDurationWithPause_label = new Label();
            CommeDuration_label = new Label();
            RecordingTimeWithPause_label = new Label();
            RecordingTime_label = new Label();
            CreatTime_label = new Label();
            ProgrTime_label = new Label();
            DebugTime_label = new Label();
            CompDuration_label = new Label();
            CompDurationWithPause_label = new Label();
            RangeCommit_label = new Label();
            SameDate_label = new Label();
            SameDate_checkBox = new CheckBox();
            Since_dateTimePicker = new DateTimePicker();
            RecordSinceText_label = new Label();
            RecordSinceDate_label = new Label();
            RecordUntilText_label = new Label();
            RecordUntilDate_label = new Label();
            CommeTime_label = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // ReturnToMain_button
            // 
            ReturnToMain_button.Anchor = AnchorStyles.None;
            ReturnToMain_button.Location = new Point(1120, 408);
            ReturnToMain_button.Name = "ReturnToMain_button";
            ReturnToMain_button.Size = new Size(110, 70);
            ReturnToMain_button.TabIndex = 0;
            ReturnToMain_button.Text = "ReturnToMain_button";
            ReturnToMain_button.UseVisualStyleBackColor = true;
            ReturnToMain_button.Click += MainFormOpening_button_Click;
            // 
            // Commit_richTextBox
            // 
            Commit_richTextBox.Anchor = AnchorStyles.None;
            Commit_richTextBox.BackColor = SystemColors.MenuBar;
            Commit_richTextBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Commit_richTextBox.Location = new Point(12, 55);
            Commit_richTextBox.Name = "Commit_richTextBox";
            Commit_richTextBox.Size = new Size(286, 423);
            Commit_richTextBox.TabIndex = 6;
            Commit_richTextBox.Text = "1234567890123456789012345678901234567890";
            // 
            // Commit_vScrollBar
            // 
            Commit_vScrollBar.Anchor = AnchorStyles.None;
            Commit_vScrollBar.LargeChange = 1;
            Commit_vScrollBar.Location = new Point(320, 55);
            Commit_vScrollBar.Maximum = 5;
            Commit_vScrollBar.Name = "Commit_vScrollBar";
            Commit_vScrollBar.Size = new Size(26, 394);
            Commit_vScrollBar.TabIndex = 8;
            Commit_vScrollBar.Scroll += Commit_vScrollBar_Scroll;
            // 
            // CompDurationText_label
            // 
            CompDurationText_label.Anchor = AnchorStyles.None;
            CompDurationText_label.Font = new Font("Segoe UI", 12.8F, FontStyle.Bold, GraphicsUnit.Point);
            CompDurationText_label.Location = new Point(349, 305);
            CompDurationText_label.Name = "CompDurationText_label";
            CompDurationText_label.Size = new Size(438, 35);
            CompDurationText_label.TabIndex = 11;
            CompDurationText_label.Text = "CompDurationText_label";
            // 
            // Until_dateTimePicker
            // 
            Until_dateTimePicker.Anchor = AnchorStyles.None;
            Until_dateTimePicker.Location = new Point(927, 37);
            Until_dateTimePicker.Name = "Until_dateTimePicker";
            Until_dateTimePicker.Size = new Size(250, 27);
            Until_dateTimePicker.TabIndex = 13;
            Until_dateTimePicker.CloseUp += Until_dateTimePicker_CloseUp;
            // 
            // Since_label
            // 
            Since_label.Anchor = AnchorStyles.None;
            Since_label.Location = new Point(440, 9);
            Since_label.Name = "Since_label";
            Since_label.Size = new Size(248, 20);
            Since_label.TabIndex = 14;
            Since_label.Text = "Since_label";
            Since_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // Until_label
            // 
            Until_label.Anchor = AnchorStyles.None;
            Until_label.Location = new Point(927, 9);
            Until_label.Name = "Until_label";
            Until_label.Size = new Size(250, 20);
            Until_label.TabIndex = 15;
            Until_label.Text = "Until_label";
            Until_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // CompDurationWithPauseText_label
            // 
            CompDurationWithPauseText_label.Anchor = AnchorStyles.None;
            CompDurationWithPauseText_label.Font = new Font("Segoe UI", 12.8F, FontStyle.Bold, GraphicsUnit.Point);
            CompDurationWithPauseText_label.Location = new Point(349, 385);
            CompDurationWithPauseText_label.Name = "CompDurationWithPauseText_label";
            CompDurationWithPauseText_label.Size = new Size(438, 70);
            CompDurationWithPauseText_label.TabIndex = 16;
            CompDurationWithPauseText_label.Text = "CompDurationWithPauseText_label";
            // 
            // CreatDuration_label
            // 
            CreatDuration_label.Anchor = AnchorStyles.Top;
            CreatDuration_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CreatDuration_label.Location = new Point(7, 0);
            CreatDuration_label.Name = "CreatDuration_label";
            CreatDuration_label.Size = new Size(163, 25);
            CreatDuration_label.TabIndex = 17;
            CreatDuration_label.Text = "CreatDuration_label";
            CreatDuration_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // CreatDurationWithPause_label
            // 
            CreatDurationWithPause_label.Anchor = AnchorStyles.Top;
            CreatDurationWithPause_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CreatDurationWithPause_label.Location = new Point(7, 62);
            CreatDurationWithPause_label.Name = "CreatDurationWithPause_label";
            CreatDurationWithPause_label.Size = new Size(163, 25);
            CreatDurationWithPause_label.TabIndex = 18;
            CreatDurationWithPause_label.Text = "CreatDurationWithPause_label";
            CreatDurationWithPause_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // ProgrDuration_label
            // 
            ProgrDuration_label.Anchor = AnchorStyles.Top;
            ProgrDuration_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ProgrDuration_label.Location = new Point(181, 0);
            ProgrDuration_label.Name = "ProgrDuration_label";
            ProgrDuration_label.Size = new Size(172, 25);
            ProgrDuration_label.TabIndex = 19;
            ProgrDuration_label.Text = "ProgrDuration_label";
            ProgrDuration_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // ProgrDurationWithPause_label
            // 
            ProgrDurationWithPause_label.Anchor = AnchorStyles.Top;
            ProgrDurationWithPause_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ProgrDurationWithPause_label.Location = new Point(181, 62);
            ProgrDurationWithPause_label.Name = "ProgrDurationWithPause_label";
            ProgrDurationWithPause_label.Size = new Size(172, 25);
            ProgrDurationWithPause_label.TabIndex = 20;
            ProgrDurationWithPause_label.Text = "ProgrDurationWithPause_label";
            ProgrDurationWithPause_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // DebugDuration_label
            // 
            DebugDuration_label.Anchor = AnchorStyles.Top;
            DebugDuration_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            DebugDuration_label.Location = new Point(361, 0);
            DebugDuration_label.Name = "DebugDuration_label";
            DebugDuration_label.Size = new Size(168, 25);
            DebugDuration_label.TabIndex = 21;
            DebugDuration_label.Text = "DebugDuration_label";
            DebugDuration_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // DebugDurationWithPause_label
            // 
            DebugDurationWithPause_label.Anchor = AnchorStyles.Top;
            DebugDurationWithPause_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            DebugDurationWithPause_label.Location = new Point(361, 62);
            DebugDurationWithPause_label.Name = "DebugDurationWithPause_label";
            DebugDurationWithPause_label.Size = new Size(168, 25);
            DebugDurationWithPause_label.TabIndex = 22;
            DebugDurationWithPause_label.Text = "DebugDurationWithPause_label";
            DebugDurationWithPause_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(CommeDurationWithPause_label, 3, 1);
            tableLayoutPanel1.Controls.Add(CommeDuration_label, 3, 0);
            tableLayoutPanel1.Controls.Add(DebugDurationWithPause_label, 2, 1);
            tableLayoutPanel1.Controls.Add(DebugDuration_label, 2, 0);
            tableLayoutPanel1.Controls.Add(ProgrDuration_label, 1, 0);
            tableLayoutPanel1.Controls.Add(CreatDurationWithPause_label, 0, 1);
            tableLayoutPanel1.Controls.Add(ProgrDurationWithPause_label, 1, 1);
            tableLayoutPanel1.Controls.Add(CreatDuration_label, 0, 0);
            tableLayoutPanel1.Location = new Point(518, 154);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(712, 125);
            tableLayoutPanel1.TabIndex = 23;
            // 
            // CommeDurationWithPause_label
            // 
            CommeDurationWithPause_label.Anchor = AnchorStyles.Top;
            CommeDurationWithPause_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CommeDurationWithPause_label.Location = new Point(541, 62);
            CommeDurationWithPause_label.Name = "CommeDurationWithPause_label";
            CommeDurationWithPause_label.Size = new Size(163, 25);
            CommeDurationWithPause_label.TabIndex = 39;
            CommeDurationWithPause_label.Text = "CommeDurationWithPause_label";
            CommeDurationWithPause_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // CommeDuration_label
            // 
            CommeDuration_label.Anchor = AnchorStyles.Top;
            CommeDuration_label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CommeDuration_label.Location = new Point(541, 0);
            CommeDuration_label.Name = "CommeDuration_label";
            CommeDuration_label.Size = new Size(163, 25);
            CommeDuration_label.TabIndex = 39;
            CommeDuration_label.Text = "CommeDuration_label";
            CommeDuration_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // RecordingTimeWithPause_label
            // 
            RecordingTimeWithPause_label.Anchor = AnchorStyles.None;
            RecordingTimeWithPause_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            RecordingTimeWithPause_label.Location = new Point(356, 216);
            RecordingTimeWithPause_label.Name = "RecordingTimeWithPause_label";
            RecordingTimeWithPause_label.Size = new Size(163, 44);
            RecordingTimeWithPause_label.TabIndex = 24;
            RecordingTimeWithPause_label.Text = "RecordingTimeWithPause_label";
            RecordingTimeWithPause_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // RecordingTime_label
            // 
            RecordingTime_label.Anchor = AnchorStyles.None;
            RecordingTime_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            RecordingTime_label.Location = new Point(356, 161);
            RecordingTime_label.Name = "RecordingTime_label";
            RecordingTime_label.Size = new Size(163, 20);
            RecordingTime_label.TabIndex = 25;
            RecordingTime_label.Text = "RecordingTime_label";
            RecordingTime_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // CreatTime_label
            // 
            CreatTime_label.Anchor = AnchorStyles.None;
            CreatTime_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            CreatTime_label.Location = new Point(528, 118);
            CreatTime_label.Name = "CreatTime_label";
            CreatTime_label.Size = new Size(163, 33);
            CreatTime_label.TabIndex = 26;
            CreatTime_label.Text = "CreatTime_label";
            CreatTime_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // ProgrTime_label
            // 
            ProgrTime_label.Anchor = AnchorStyles.None;
            ProgrTime_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            ProgrTime_label.Location = new Point(697, 118);
            ProgrTime_label.Name = "ProgrTime_label";
            ProgrTime_label.Size = new Size(172, 33);
            ProgrTime_label.TabIndex = 27;
            ProgrTime_label.Text = "ProgrTime_label";
            ProgrTime_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // DebugTime_label
            // 
            DebugTime_label.Anchor = AnchorStyles.None;
            DebugTime_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            DebugTime_label.Location = new Point(875, 118);
            DebugTime_label.Name = "DebugTime_label";
            DebugTime_label.Size = new Size(171, 33);
            DebugTime_label.TabIndex = 28;
            DebugTime_label.Text = "DebugTime_label";
            DebugTime_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // CompDuration_label
            // 
            CompDuration_label.Anchor = AnchorStyles.None;
            CompDuration_label.Font = new Font("Segoe UI", 12.8F, FontStyle.Bold, GraphicsUnit.Point);
            CompDuration_label.Location = new Point(349, 337);
            CompDuration_label.Name = "CompDuration_label";
            CompDuration_label.Size = new Size(438, 28);
            CompDuration_label.TabIndex = 29;
            CompDuration_label.Text = "CompDuration_label";
            // 
            // CompDurationWithPause_label
            // 
            CompDurationWithPause_label.Anchor = AnchorStyles.None;
            CompDurationWithPause_label.Font = new Font("Segoe UI", 12.8F, FontStyle.Bold, GraphicsUnit.Point);
            CompDurationWithPause_label.Location = new Point(349, 448);
            CompDurationWithPause_label.Name = "CompDurationWithPause_label";
            CompDurationWithPause_label.Size = new Size(438, 28);
            CompDurationWithPause_label.TabIndex = 30;
            CompDurationWithPause_label.Text = "CompDurationWithPause_label";
            // 
            // RangeCommit_label
            // 
            RangeCommit_label.Anchor = AnchorStyles.None;
            RangeCommit_label.Location = new Point(12, 27);
            RangeCommit_label.Name = "RangeCommit_label";
            RangeCommit_label.Size = new Size(286, 25);
            RangeCommit_label.TabIndex = 31;
            RangeCommit_label.Text = "RangeCommit_label";
            RangeCommit_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // SameDate_label
            // 
            SameDate_label.Anchor = AnchorStyles.None;
            SameDate_label.Location = new Point(739, 9);
            SameDate_label.Name = "SameDate_label";
            SameDate_label.Size = new Size(141, 20);
            SameDate_label.TabIndex = 32;
            SameDate_label.Text = "SameDate_label";
            SameDate_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // SameDate_checkBox
            // 
            SameDate_checkBox.Anchor = AnchorStyles.None;
            SameDate_checkBox.AutoSize = true;
            SameDate_checkBox.Location = new Point(799, 37);
            SameDate_checkBox.Name = "SameDate_checkBox";
            SameDate_checkBox.Size = new Size(18, 17);
            SameDate_checkBox.TabIndex = 33;
            SameDate_checkBox.UseVisualStyleBackColor = true;
            SameDate_checkBox.CheckedChanged += SameDate_checkBox_CheckedChanged;
            // 
            // Since_dateTimePicker
            // 
            Since_dateTimePicker.Anchor = AnchorStyles.None;
            Since_dateTimePicker.Location = new Point(440, 37);
            Since_dateTimePicker.Name = "Since_dateTimePicker";
            Since_dateTimePicker.Size = new Size(250, 27);
            Since_dateTimePicker.TabIndex = 34;
            Since_dateTimePicker.CloseUp += Since_dateTimePicker_CloseUp;
            // 
            // RecordSinceText_label
            // 
            RecordSinceText_label.Anchor = AnchorStyles.None;
            RecordSinceText_label.Location = new Point(440, 67);
            RecordSinceText_label.Name = "RecordSinceText_label";
            RecordSinceText_label.Size = new Size(132, 20);
            RecordSinceText_label.TabIndex = 35;
            RecordSinceText_label.Text = "RecordSinceText_label";
            RecordSinceText_label.TextAlign = ContentAlignment.TopRight;
            // 
            // RecordSinceDate_label
            // 
            RecordSinceDate_label.Anchor = AnchorStyles.None;
            RecordSinceDate_label.Location = new Point(578, 67);
            RecordSinceDate_label.Name = "RecordSinceDate_label";
            RecordSinceDate_label.Size = new Size(110, 20);
            RecordSinceDate_label.TabIndex = 36;
            RecordSinceDate_label.Text = "RecordSinceDate_label";
            // 
            // RecordUntilText_label
            // 
            RecordUntilText_label.Anchor = AnchorStyles.None;
            RecordUntilText_label.Location = new Point(927, 67);
            RecordUntilText_label.Name = "RecordUntilText_label";
            RecordUntilText_label.Size = new Size(135, 20);
            RecordUntilText_label.TabIndex = 37;
            RecordUntilText_label.Text = "RecordUntilText_label";
            RecordUntilText_label.TextAlign = ContentAlignment.TopRight;
            // 
            // RecordUntilDate_label
            // 
            RecordUntilDate_label.Anchor = AnchorStyles.None;
            RecordUntilDate_label.Location = new Point(1068, 67);
            RecordUntilDate_label.Name = "RecordUntilDate_label";
            RecordUntilDate_label.Size = new Size(109, 20);
            RecordUntilDate_label.TabIndex = 38;
            RecordUntilDate_label.Text = "RecordUntilDate_label";
            // 
            // CommeTime_label
            // 
            CommeTime_label.Anchor = AnchorStyles.None;
            CommeTime_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            CommeTime_label.Location = new Point(1051, 118);
            CommeTime_label.Name = "CommeTime_label";
            CommeTime_label.Size = new Size(171, 33);
            CommeTime_label.TabIndex = 39;
            CommeTime_label.Text = "CommeTime_label";
            CommeTime_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // Progress_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 495);
            Controls.Add(CommeTime_label);
            Controls.Add(RecordUntilDate_label);
            Controls.Add(RecordUntilText_label);
            Controls.Add(RecordSinceDate_label);
            Controls.Add(RecordSinceText_label);
            Controls.Add(Since_dateTimePicker);
            Controls.Add(SameDate_checkBox);
            Controls.Add(SameDate_label);
            Controls.Add(RangeCommit_label);
            Controls.Add(CompDurationWithPause_label);
            Controls.Add(CompDuration_label);
            Controls.Add(DebugTime_label);
            Controls.Add(ProgrTime_label);
            Controls.Add(CreatTime_label);
            Controls.Add(RecordingTime_label);
            Controls.Add(RecordingTimeWithPause_label);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(CompDurationWithPauseText_label);
            Controls.Add(Until_label);
            Controls.Add(Since_label);
            Controls.Add(Until_dateTimePicker);
            Controls.Add(CompDurationText_label);
            Controls.Add(Commit_vScrollBar);
            Controls.Add(Commit_richTextBox);
            Controls.Add(ReturnToMain_button);
            Name = "Progress_form";
            Text = "Form1";
            FormClosing += Progress_form_FormClosing;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private Label CommeDurationWithPause_label;
        private Label CommeDuration_label;
        private Label CommeTime_label;
    }
}