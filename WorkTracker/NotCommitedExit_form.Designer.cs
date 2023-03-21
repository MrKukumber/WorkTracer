namespace WorkTracker
{
    partial class NotCommitedExit_form
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
            this.YesExit_button = new System.Windows.Forms.Button();
            this.NoExit_button = new System.Windows.Forms.Button();
            this.ExitWithoutCommit_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // YesExit_button
            // 
            this.YesExit_button.Location = new System.Drawing.Point(60, 118);
            this.YesExit_button.Name = "YesExit_button";
            this.YesExit_button.Size = new System.Drawing.Size(94, 29);
            this.YesExit_button.TabIndex = 0;
            this.YesExit_button.Text = "button1";
            this.YesExit_button.UseVisualStyleBackColor = true;
            this.YesExit_button.Click += new System.EventHandler(this.YesExit_Button_Click);
            // 
            // NoExit_button
            // 
            this.NoExit_button.Location = new System.Drawing.Point(247, 118);
            this.NoExit_button.Name = "NoExit_button";
            this.NoExit_button.Size = new System.Drawing.Size(94, 29);
            this.NoExit_button.TabIndex = 1;
            this.NoExit_button.Text = "button2";
            this.NoExit_button.UseVisualStyleBackColor = true;
            // 
            // ExitWithoutCommit_label
            // 
            this.ExitWithoutCommit_label.AutoSize = true;
            this.ExitWithoutCommit_label.Location = new System.Drawing.Point(104, 43);
            this.ExitWithoutCommit_label.Name = "ExitWithoutCommit_label";
            this.ExitWithoutCommit_label.Size = new System.Drawing.Size(50, 20);
            this.ExitWithoutCommit_label.TabIndex = 2;
            this.ExitWithoutCommit_label.Text = "label1";
            // 
            // Not_commited_exit_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 163);
            this.Controls.Add(this.ExitWithoutCommit_label);
            this.Controls.Add(this.NoExit_button);
            this.Controls.Add(this.YesExit_button);
            this.Name = "Not_commited_exit_form";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button YesExit_button;
        private Button NoExit_button;
        private Label ExitWithoutCommit_label;
    }
}