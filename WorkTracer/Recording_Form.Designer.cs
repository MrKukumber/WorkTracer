namespace WorkTracer
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
            Start_roundButton = new RoundButton();
            Pause_roundButton = new RoundButton();
            Stop_roundButton = new RoundButton();
            ReturnToMain_button = new Button();
            ConfigFormOpening_button = new Button();
            PhaseCreat_label = new Label();
            PhaseProgr_label = new Label();
            PhaseDebug_label = new Label();
            Phase_label = new Label();
            Phase_trackBar = new TrackBar();
            CurrTrackState_label = new Label();
            PhaseComment_label = new Label();
            ((System.ComponentModel.ISupportInitialize)Phase_trackBar).BeginInit();
            SuspendLayout();
            // 
            // Start_roundButton
            // 
            Start_roundButton.Anchor = AnchorStyles.Top;
            Start_roundButton.BackgroundImage = Properties.Resources.play_icon;
            Start_roundButton.Location = new Point(33, 24);
            Start_roundButton.Name = "Start_roundButton";
            Start_roundButton.Size = new Size(67, 66);
            Start_roundButton.TabIndex = 0;
            Start_roundButton.UseVisualStyleBackColor = true;
            Start_roundButton.EnabledChanged += Start_roundButton_EnabledChanged;
            Start_roundButton.Click += Start_roundButton_Click;
            // 
            // Pause_roundButton
            // 
            Pause_roundButton.Anchor = AnchorStyles.Top;
            Pause_roundButton.BackgroundImage = Properties.Resources.pause_icon;
            Pause_roundButton.Location = new Point(142, 24);
            Pause_roundButton.Name = "Pause_roundButton";
            Pause_roundButton.Size = new Size(67, 66);
            Pause_roundButton.TabIndex = 1;
            Pause_roundButton.UseVisualStyleBackColor = true;
            Pause_roundButton.EnabledChanged += Pause_roundButton_EnabledChanged;
            Pause_roundButton.Click += Pause_roundButton_Click;
            // 
            // Stop_roundButton
            // 
            Stop_roundButton.Anchor = AnchorStyles.Top;
            Stop_roundButton.BackgroundImage = Properties.Resources.stop_icon;
            Stop_roundButton.Location = new Point(251, 24);
            Stop_roundButton.Name = "Stop_roundButton";
            Stop_roundButton.Size = new Size(67, 66);
            Stop_roundButton.TabIndex = 2;
            Stop_roundButton.UseVisualStyleBackColor = true;
            Stop_roundButton.EnabledChanged += Stop_roundButton_EnabledChanged;
            Stop_roundButton.Click += Stop_roundButton_Click;
            // 
            // ReturnToMain_button
            // 
            ReturnToMain_button.Anchor = AnchorStyles.Top;
            ReturnToMain_button.Location = new Point(208, 145);
            ReturnToMain_button.Name = "ReturnToMain_button";
            ReturnToMain_button.Size = new Size(110, 70);
            ReturnToMain_button.TabIndex = 3;
            ReturnToMain_button.Text = "ReturnToMain_button";
            ReturnToMain_button.UseVisualStyleBackColor = true;
            ReturnToMain_button.Click += ReturnToMain_button_Click;
            // 
            // ConfigFormOpening_button
            // 
            ConfigFormOpening_button.Anchor = AnchorStyles.Top;
            ConfigFormOpening_button.Location = new Point(208, 239);
            ConfigFormOpening_button.Name = "ConfigFormOpening_button";
            ConfigFormOpening_button.Size = new Size(110, 70);
            ConfigFormOpening_button.TabIndex = 4;
            ConfigFormOpening_button.Text = "ConfigFormOpening_Button";
            ConfigFormOpening_button.UseVisualStyleBackColor = true;
            ConfigFormOpening_button.Click += ConfigFormOpening_button_Click;
            // 
            // PhaseCreat_label
            // 
            PhaseCreat_label.Anchor = AnchorStyles.Top;
            PhaseCreat_label.Location = new Point(44, 280);
            PhaseCreat_label.Name = "PhaseCreat_label";
            PhaseCreat_label.Size = new Size(158, 20);
            PhaseCreat_label.TabIndex = 6;
            PhaseCreat_label.Text = "PhaseCreat_label";
            PhaseCreat_label.Click += PhaseCreat_label_Click;
            // 
            // PhaseProgr_label
            // 
            PhaseProgr_label.Anchor = AnchorStyles.Top;
            PhaseProgr_label.Location = new Point(44, 244);
            PhaseProgr_label.Name = "PhaseProgr_label";
            PhaseProgr_label.Size = new Size(158, 20);
            PhaseProgr_label.TabIndex = 7;
            PhaseProgr_label.Text = "PhaseProgr_label";
            // 
            // PhaseDebug_label
            // 
            PhaseDebug_label.Anchor = AnchorStyles.Top;
            PhaseDebug_label.Location = new Point(44, 210);
            PhaseDebug_label.Name = "PhaseDebug_label";
            PhaseDebug_label.Size = new Size(158, 20);
            PhaseDebug_label.TabIndex = 8;
            PhaseDebug_label.Text = "PhaseDebug_label";
            // 
            // Phase_label
            // 
            Phase_label.Anchor = AnchorStyles.Top;
            Phase_label.AutoSize = true;
            Phase_label.Location = new Point(33, 145);
            Phase_label.Name = "Phase_label";
            Phase_label.Size = new Size(86, 20);
            Phase_label.TabIndex = 9;
            Phase_label.Text = "Phase_label";
            // 
            // Phase_trackBar
            // 
            Phase_trackBar.Anchor = AnchorStyles.Top;
            Phase_trackBar.LargeChange = 1;
            Phase_trackBar.Location = new Point(12, 168);
            Phase_trackBar.Maximum = 3;
            Phase_trackBar.Name = "Phase_trackBar";
            Phase_trackBar.Orientation = Orientation.Vertical;
            Phase_trackBar.Size = new Size(56, 141);
            Phase_trackBar.TabIndex = 5;
            Phase_trackBar.Scroll += Phase_trackBar_Scroll;
            // 
            // CurrTrackState_label
            // 
            CurrTrackState_label.Anchor = AnchorStyles.Top;
            CurrTrackState_label.Location = new Point(33, 102);
            CurrTrackState_label.Name = "CurrTrackState_label";
            CurrTrackState_label.Size = new Size(285, 20);
            CurrTrackState_label.TabIndex = 10;
            CurrTrackState_label.Text = "CurrTrackState_label";
            CurrTrackState_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // PhaseComment_label
            // 
            PhaseComment_label.AutoSize = true;
            PhaseComment_label.Location = new Point(44, 176);
            PhaseComment_label.Name = "PhaseComment_label";
            PhaseComment_label.Size = new Size(151, 20);
            PhaseComment_label.TabIndex = 11;
            PhaseComment_label.Text = "PhaseComment_label";
            // 
            // Recording_form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(346, 321);
            Controls.Add(PhaseComment_label);
            Controls.Add(CurrTrackState_label);
            Controls.Add(Phase_label);
            Controls.Add(PhaseDebug_label);
            Controls.Add(PhaseProgr_label);
            Controls.Add(PhaseCreat_label);
            Controls.Add(Phase_trackBar);
            Controls.Add(ConfigFormOpening_button);
            Controls.Add(ReturnToMain_button);
            Controls.Add(Stop_roundButton);
            Controls.Add(Pause_roundButton);
            Controls.Add(Start_roundButton);
            Name = "Recording_form";
            Text = "Form2";
            FormClosing += Recording_form_FormClosing;
            ((System.ComponentModel.ISupportInitialize)Phase_trackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Label PhaseComment_label;
    }
}