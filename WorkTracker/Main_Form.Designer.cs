﻿namespace WorkTracker
{
    partial class Main_form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RecordingFormOpening_button = new System.Windows.Forms.Button();
            this.ConfigFormOpening_button = new System.Windows.Forms.Button();
            this.Commit_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Commit_richTextBox = new System.Windows.Forms.RichTextBox();
            this.ChooseCommit_button = new System.Windows.Forms.Button();
            this.TortoiseFileNotSelected_label = new System.Windows.Forms.Label();
            this.ProjNotSelected_label = new System.Windows.Forms.Label();
            this.CurrTrackState_label = new System.Windows.Forms.Label();
            this.Mode_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RecordingFormOpening_button
            // 
            this.RecordingFormOpening_button.Location = new System.Drawing.Point(57, 276);
            this.RecordingFormOpening_button.Name = "RecordingFormOpening_button";
            this.RecordingFormOpening_button.Size = new System.Drawing.Size(94, 50);
            this.RecordingFormOpening_button.TabIndex = 0;
            this.RecordingFormOpening_button.Text = "RecordingFormOpening_Button";
            this.RecordingFormOpening_button.UseVisualStyleBackColor = true;
            this.RecordingFormOpening_button.Click += new System.EventHandler(this.RecordingFormOpening_Button_Click);
            // 
            // ConfigFormOpening_button
            // 
            this.ConfigFormOpening_button.Location = new System.Drawing.Point(57, 562);
            this.ConfigFormOpening_button.Name = "ConfigFormOpening_button";
            this.ConfigFormOpening_button.Size = new System.Drawing.Size(94, 50);
            this.ConfigFormOpening_button.TabIndex = 1;
            this.ConfigFormOpening_button.Text = "ConfigFormOpening_Button";
            this.ConfigFormOpening_button.UseVisualStyleBackColor = true;
            this.ConfigFormOpening_button.Click += new System.EventHandler(this.ConfigFormOpening_Button_Click);
            // 
            // Commit_dateTimePicker
            // 
            this.Commit_dateTimePicker.Checked = false;
            this.Commit_dateTimePicker.Location = new System.Drawing.Point(266, 170);
            this.Commit_dateTimePicker.Name = "Commit_dateTimePicker";
            this.Commit_dateTimePicker.Size = new System.Drawing.Size(259, 27);
            this.Commit_dateTimePicker.TabIndex = 2;
            // 
            // Commit_richTextBox
            // 
            this.Commit_richTextBox.Location = new System.Drawing.Point(297, 249);
            this.Commit_richTextBox.Name = "Commit_richTextBox";
            this.Commit_richTextBox.ReadOnly = true;
            this.Commit_richTextBox.Size = new System.Drawing.Size(204, 441);
            this.Commit_richTextBox.TabIndex = 3;
            this.Commit_richTextBox.Text = "";
            // 
            // ChooseCommit_button
            // 
            this.ChooseCommit_button.Location = new System.Drawing.Point(343, 203);
            this.ChooseCommit_button.Name = "ChooseCommit_button";
            this.ChooseCommit_button.Size = new System.Drawing.Size(91, 40);
            this.ChooseCommit_button.TabIndex = 4;
            this.ChooseCommit_button.Text = "button3";
            this.ChooseCommit_button.UseVisualStyleBackColor = true;
            this.ChooseCommit_button.Click += new System.EventHandler(this.ChooseCommit_Button_Click);
            // 
            // TortoiseFileNotSelected_label
            // 
            this.TortoiseFileNotSelected_label.AutoSize = true;
            this.TortoiseFileNotSelected_label.Location = new System.Drawing.Point(57, 628);
            this.TortoiseFileNotSelected_label.Name = "TortoiseFileNotSelected_label";
            this.TortoiseFileNotSelected_label.Size = new System.Drawing.Size(50, 20);
            this.TortoiseFileNotSelected_label.TabIndex = 7;
            this.TortoiseFileNotSelected_label.Text = "label2";
            // 
            // ProjNotSelected_label
            // 
            this.ProjNotSelected_label.AutoSize = true;
            this.ProjNotSelected_label.Location = new System.Drawing.Point(57, 660);
            this.ProjNotSelected_label.Name = "ProjNotSelected_label";
            this.ProjNotSelected_label.Size = new System.Drawing.Size(50, 20);
            this.ProjNotSelected_label.TabIndex = 8;
            this.ProjNotSelected_label.Text = "label1";
            // 
            // CurrTrackState_label
            // 
            this.CurrTrackState_label.AutoSize = true;
            this.CurrTrackState_label.Location = new System.Drawing.Point(74, 345);
            this.CurrTrackState_label.Name = "CurrTrackState_label";
            this.CurrTrackState_label.Size = new System.Drawing.Size(50, 20);
            this.CurrTrackState_label.TabIndex = 9;
            this.CurrTrackState_label.Text = "label1";
            // 
            // Mode_label
            // 
            this.Mode_label.AutoSize = true;
            this.Mode_label.Location = new System.Drawing.Point(182, 577);
            this.Mode_label.Name = "Mode_label";
            this.Mode_label.Size = new System.Drawing.Size(50, 20);
            this.Mode_label.TabIndex = 10;
            this.Mode_label.Text = "label1";
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 702);
            this.Controls.Add(this.Mode_label);
            this.Controls.Add(this.CurrTrackState_label);
            this.Controls.Add(this.ProjNotSelected_label);
            this.Controls.Add(this.TortoiseFileNotSelected_label);
            this.Controls.Add(this.ChooseCommit_button);
            this.Controls.Add(this.Commit_richTextBox);
            this.Controls.Add(this.Commit_dateTimePicker);
            this.Controls.Add(this.ConfigFormOpening_button);
            this.Controls.Add(this.RecordingFormOpening_button);
            this.Name = "Main_form";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_form_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button RecordingFormOpening_button;
        private Button ConfigFormOpening_button;
        private DateTimePicker Commit_dateTimePicker;
        private RichTextBox Commit_richTextBox;
        private Button ChooseCommit_button;
        private Label TortoiseFileNotSelected_label;
        private Label ProjNotSelected_label;
        private Label CurrTrackState_label;
        private Label Mode_label;
    }
}