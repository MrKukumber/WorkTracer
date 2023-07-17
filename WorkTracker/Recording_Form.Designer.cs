namespace WorkTracker
{
    partial class Recording_form
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
            this.Start_roundButton = new WorkTracker.RoundButton();
            this.Pause_roundButton = new WorkTracker.RoundButton();
            this.Stop_roundButton = new WorkTracker.RoundButton();
            this.ReturnToMain_button = new System.Windows.Forms.Button();
            this.ConfigFormOpening_button = new System.Windows.Forms.Button();
            this.PhaseCreat_label = new System.Windows.Forms.Label();
            this.PhaseProgr_label = new System.Windows.Forms.Label();
            this.PhaseDebug_label = new System.Windows.Forms.Label();
            this.Phase_label = new System.Windows.Forms.Label();
            this.Phase_trackBar = new System.Windows.Forms.TrackBar();
            this.CurrTrackState_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Phase_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Start_roundButton
            // 
            this.Start_roundButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Start_roundButton.BackgroundImage = global::WorkTracker.Properties.Resources.play_icon;
            this.Start_roundButton.Location = new System.Drawing.Point(33, 24);
            this.Start_roundButton.Name = "Start_roundButton";
            this.Start_roundButton.Size = new System.Drawing.Size(67, 66);
            this.Start_roundButton.TabIndex = 0;
            this.Start_roundButton.UseVisualStyleBackColor = true;
            this.Start_roundButton.EnabledChanged += new System.EventHandler(this.Start_roundButton_EnabledChanged);
            this.Start_roundButton.Click += new System.EventHandler(this.Start_roundButton_Click);
            // 
            // Pause_roundButton
            // 
            this.Pause_roundButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Pause_roundButton.BackgroundImage = global::WorkTracker.Properties.Resources.pause_icon;
            this.Pause_roundButton.Location = new System.Drawing.Point(142, 24);
            this.Pause_roundButton.Name = "Pause_roundButton";
            this.Pause_roundButton.Size = new System.Drawing.Size(67, 66);
            this.Pause_roundButton.TabIndex = 1;
            this.Pause_roundButton.UseVisualStyleBackColor = true;
            this.Pause_roundButton.EnabledChanged += new System.EventHandler(this.Pause_roundButton_EnabledChanged);
            this.Pause_roundButton.Click += new System.EventHandler(this.Pause_roundButton_Click);
            // 
            // Stop_roundButton
            // 
            this.Stop_roundButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Stop_roundButton.BackgroundImage = global::WorkTracker.Properties.Resources.stop_icon;
            this.Stop_roundButton.Location = new System.Drawing.Point(251, 24);
            this.Stop_roundButton.Name = "Stop_roundButton";
            this.Stop_roundButton.Size = new System.Drawing.Size(67, 66);
            this.Stop_roundButton.TabIndex = 2;
            this.Stop_roundButton.UseVisualStyleBackColor = true;
            this.Stop_roundButton.EnabledChanged += new System.EventHandler(this.Stop_roundButton_EnabledChanged);
            this.Stop_roundButton.Click += new System.EventHandler(this.Stop_roundButton_Click);
            // 
            // ReturnToMain_button
            // 
            this.ReturnToMain_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ReturnToMain_button.Location = new System.Drawing.Point(218, 239);
            this.ReturnToMain_button.Name = "ReturnToMain_button";
            this.ReturnToMain_button.Size = new System.Drawing.Size(110, 70);
            this.ReturnToMain_button.TabIndex = 3;
            this.ReturnToMain_button.Text = "ReturnToMain_button";
            this.ReturnToMain_button.UseVisualStyleBackColor = true;
            this.ReturnToMain_button.Click += new System.EventHandler(this.ReturnToMain_button_Click);
            // 
            // ConfigFormOpening_button
            // 
            this.ConfigFormOpening_button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ConfigFormOpening_button.Location = new System.Drawing.Point(218, 143);
            this.ConfigFormOpening_button.Name = "ConfigFormOpening_button";
            this.ConfigFormOpening_button.Size = new System.Drawing.Size(110, 70);
            this.ConfigFormOpening_button.TabIndex = 4;
            this.ConfigFormOpening_button.Text = "ConfigFormOpening_Button";
            this.ConfigFormOpening_button.UseVisualStyleBackColor = true;
            this.ConfigFormOpening_button.Click += new System.EventHandler(this.ConfigFormOpening_button_Click);
            // 
            // PhaseCreat_label
            // 
            this.PhaseCreat_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PhaseCreat_label.Location = new System.Drawing.Point(51, 180);
            this.PhaseCreat_label.Name = "PhaseCreat_label";
            this.PhaseCreat_label.Size = new System.Drawing.Size(158, 20);
            this.PhaseCreat_label.TabIndex = 6;
            this.PhaseCreat_label.Text = "PhaseCreat_label";
            // 
            // PhaseProgr_label
            // 
            this.PhaseProgr_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PhaseProgr_label.Location = new System.Drawing.Point(51, 218);
            this.PhaseProgr_label.Name = "PhaseProgr_label";
            this.PhaseProgr_label.Size = new System.Drawing.Size(158, 20);
            this.PhaseProgr_label.TabIndex = 7;
            this.PhaseProgr_label.Text = "PhaseProgr_label";
            // 
            // PhaseDebug_label
            // 
            this.PhaseDebug_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PhaseDebug_label.Location = new System.Drawing.Point(51, 250);
            this.PhaseDebug_label.Name = "PhaseDebug_label";
            this.PhaseDebug_label.Size = new System.Drawing.Size(158, 20);
            this.PhaseDebug_label.TabIndex = 8;
            this.PhaseDebug_label.Text = "PhaseDebug_label";
            // 
            // Phase_label
            // 
            this.Phase_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Phase_label.AutoSize = true;
            this.Phase_label.Location = new System.Drawing.Point(33, 143);
            this.Phase_label.Name = "Phase_label";
            this.Phase_label.Size = new System.Drawing.Size(86, 20);
            this.Phase_label.TabIndex = 9;
            this.Phase_label.Text = "Phase_label";
            // 
            // Phase_trackBar
            // 
            this.Phase_trackBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Phase_trackBar.LargeChange = 1;
            this.Phase_trackBar.Location = new System.Drawing.Point(9, 175);
            this.Phase_trackBar.Maximum = 2;
            this.Phase_trackBar.Name = "Phase_trackBar";
            this.Phase_trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Phase_trackBar.Size = new System.Drawing.Size(56, 101);
            this.Phase_trackBar.TabIndex = 5;
            this.Phase_trackBar.Scroll += new System.EventHandler(this.Phase_trackBar_Scroll);
            // 
            // CurrTrackState_label
            // 
            this.CurrTrackState_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CurrTrackState_label.Location = new System.Drawing.Point(33, 102);
            this.CurrTrackState_label.Name = "CurrTrackState_label";
            this.CurrTrackState_label.Size = new System.Drawing.Size(285, 20);
            this.CurrTrackState_label.TabIndex = 10;
            this.CurrTrackState_label.Text = "CurrTrackState_label";
            this.CurrTrackState_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Recording_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 321);
            this.Controls.Add(this.CurrTrackState_label);
            this.Controls.Add(this.Phase_label);
            this.Controls.Add(this.PhaseDebug_label);
            this.Controls.Add(this.PhaseProgr_label);
            this.Controls.Add(this.PhaseCreat_label);
            this.Controls.Add(this.Phase_trackBar);
            this.Controls.Add(this.ConfigFormOpening_button);
            this.Controls.Add(this.ReturnToMain_button);
            this.Controls.Add(this.Stop_roundButton);
            this.Controls.Add(this.Pause_roundButton);
            this.Controls.Add(this.Start_roundButton);
            this.Name = "Recording_form";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Recording_form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Phase_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundButton Start_roundButton;
        private RoundButton Pause_roundButton;
        private RoundButton Stop_roundButton;
        private Button ReturnToMain_button;
        private Button ConfigFormOpening_button;
        private Label PhaseCreat_label;
        private Label PhaseProgr_label;
        private Label PhaseDebug_label;
        private Label Phase_label;
        private TrackBar Phase_trackBar;
        private Label CurrTrackState_label;
    }
}