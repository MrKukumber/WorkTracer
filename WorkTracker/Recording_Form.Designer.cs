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
            this.Play_roundButton = new WorkTracker.RoundButton();
            this.Pause_roundButton = new WorkTracker.RoundButton();
            this.Stop_roundButton = new WorkTracker.RoundButton();
            this.ReturnToMain_button = new System.Windows.Forms.Button();
            this.ConfigFormOpening_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Play_roundButton
            // 
            this.Play_roundButton.Location = new System.Drawing.Point(27, 32);
            this.Play_roundButton.Name = "Play_roundButton";
            this.Play_roundButton.Size = new System.Drawing.Size(67, 66);
            this.Play_roundButton.TabIndex = 0;
            this.Play_roundButton.Text = "roundButton1";
            this.Play_roundButton.UseVisualStyleBackColor = true;
            this.Play_roundButton.Click += new System.EventHandler(this.Play_roundButton_Click);
            // 
            // Pause_roundButton
            // 
            this.Pause_roundButton.Location = new System.Drawing.Point(136, 32);
            this.Pause_roundButton.Name = "Pause_roundButton";
            this.Pause_roundButton.Size = new System.Drawing.Size(67, 66);
            this.Pause_roundButton.TabIndex = 1;
            this.Pause_roundButton.Text = "roundButton2";
            this.Pause_roundButton.UseVisualStyleBackColor = true;
            // 
            // Stop_roundButton
            // 
            this.Stop_roundButton.Location = new System.Drawing.Point(245, 32);
            this.Stop_roundButton.Name = "Stop_roundButton";
            this.Stop_roundButton.Size = new System.Drawing.Size(67, 66);
            this.Stop_roundButton.TabIndex = 2;
            this.Stop_roundButton.Text = "roundButton3";
            this.Stop_roundButton.UseVisualStyleBackColor = true;
            this.Stop_roundButton.Click += new System.EventHandler(this.Stop_roundButton_Click);
            // 
            // ReturnToMain_button
            // 
            this.ReturnToMain_button.Location = new System.Drawing.Point(196, 115);
            this.ReturnToMain_button.Name = "ReturnToMain_button";
            this.ReturnToMain_button.Size = new System.Drawing.Size(116, 29);
            this.ReturnToMain_button.TabIndex = 3;
            this.ReturnToMain_button.Text = "ReturnToMain_button";
            this.ReturnToMain_button.UseVisualStyleBackColor = true;
            this.ReturnToMain_button.Click += new System.EventHandler(this.ReturnToMain_button_Click);
            // 
            // ConfigFormOpening_button
            // 
            this.ConfigFormOpening_button.Location = new System.Drawing.Point(52, 104);
            this.ConfigFormOpening_button.Name = "ConfigFormOpening_button";
            this.ConfigFormOpening_button.Size = new System.Drawing.Size(94, 50);
            this.ConfigFormOpening_button.TabIndex = 4;
            this.ConfigFormOpening_button.Text = "ConfigFormOpening_Button";
            this.ConfigFormOpening_button.UseVisualStyleBackColor = true;
            this.ConfigFormOpening_button.Click += new System.EventHandler(this.ConfigFormOpening_button_Click);
            // 
            // Recording_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 175);
            this.Controls.Add(this.ConfigFormOpening_button);
            this.Controls.Add(this.ReturnToMain_button);
            this.Controls.Add(this.Stop_roundButton);
            this.Controls.Add(this.Pause_roundButton);
            this.Controls.Add(this.Play_roundButton);
            this.Name = "Recording_form";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Recording_form_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundButton Play_roundButton;
        private RoundButton Pause_roundButton;
        private RoundButton Stop_roundButton;
        private Button ReturnToMain_button;
        private Button ConfigFormOpening_button;
    }
}