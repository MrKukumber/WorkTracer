namespace WorkTracer
{
    partial class Commit_form
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
            this.YesCommit_button = new System.Windows.Forms.Button();
            this.NoCommit_button = new System.Windows.Forms.Button();
            this.WantCommit_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // YesCommit_button
            // 
            this.YesCommit_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.YesCommit_button.Location = new System.Drawing.Point(61, 100);
            this.YesCommit_button.Name = "YesCommit_button";
            this.YesCommit_button.Size = new System.Drawing.Size(94, 29);
            this.YesCommit_button.TabIndex = 0;
            this.YesCommit_button.Text = "YesCommit_button";
            this.YesCommit_button.UseVisualStyleBackColor = true;
            this.YesCommit_button.Click += new System.EventHandler(this.YesCommit_button_Click);
            // 
            // NoCommit_button
            // 
            this.NoCommit_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NoCommit_button.Location = new System.Drawing.Point(248, 100);
            this.NoCommit_button.Name = "NoCommit_button";
            this.NoCommit_button.Size = new System.Drawing.Size(94, 29);
            this.NoCommit_button.TabIndex = 1;
            this.NoCommit_button.Text = "NoCommit_button";
            this.NoCommit_button.UseVisualStyleBackColor = true;
            this.NoCommit_button.Click += new System.EventHandler(this.NoCommit_button_Click);
            // 
            // WantCommit_label
            // 
            this.WantCommit_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WantCommit_label.Location = new System.Drawing.Point(61, 32);
            this.WantCommit_label.Name = "WantCommit_label";
            this.WantCommit_label.Size = new System.Drawing.Size(281, 47);
            this.WantCommit_label.TabIndex = 2;
            this.WantCommit_label.Text = "WantCommit_label";
            this.WantCommit_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Commit_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 163);
            this.Controls.Add(this.WantCommit_label);
            this.Controls.Add(this.NoCommit_button);
            this.Controls.Add(this.YesCommit_button);
            this.Name = "Commit_form";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Commit_form_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Button YesCommit_button;
        private Button NoCommit_button;
        private Label WantCommit_label;
    }
}