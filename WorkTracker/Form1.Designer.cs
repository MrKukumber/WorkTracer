namespace WorkTracker
{
    partial class AreYouSure_form
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
            this.YesSure_button = new System.Windows.Forms.Button();
            this.NoSure_button = new System.Windows.Forms.Button();
            this.AreYouSure_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // YesSure_button
            // 
            this.YesSure_button.Location = new System.Drawing.Point(76, 102);
            this.YesSure_button.Name = "YesSure_button";
            this.YesSure_button.Size = new System.Drawing.Size(94, 29);
            this.YesSure_button.TabIndex = 0;
            this.YesSure_button.Text = "button1";
            this.YesSure_button.UseVisualStyleBackColor = true;
            // 
            // NoSure_button
            // 
            this.NoSure_button.Location = new System.Drawing.Point(268, 103);
            this.NoSure_button.Name = "NoSure_button";
            this.NoSure_button.Size = new System.Drawing.Size(94, 29);
            this.NoSure_button.TabIndex = 1;
            this.NoSure_button.Text = "button2";
            this.NoSure_button.UseVisualStyleBackColor = true;
            // 
            // AreYouSure_label
            // 
            this.AreYouSure_label.AutoSize = true;
            this.AreYouSure_label.Location = new System.Drawing.Point(160, 23);
            this.AreYouSure_label.Name = "AreYouSure_label";
            this.AreYouSure_label.Size = new System.Drawing.Size(50, 20);
            this.AreYouSure_label.TabIndex = 2;
            this.AreYouSure_label.Text = "label1";
            // 
            // AreYouSure_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 167);
            this.Controls.Add(this.AreYouSure_label);
            this.Controls.Add(this.NoSure_button);
            this.Controls.Add(this.YesSure_button);
            this.Name = "AreYouSure_form";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button YesSure_button;
        private Button NoSure_button;
        private Label AreYouSure_label;
    }
}