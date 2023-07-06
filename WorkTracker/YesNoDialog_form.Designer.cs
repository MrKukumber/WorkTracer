namespace WorkTracker
{
    partial class YesNoDialog_form
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
            this.Yes_button = new System.Windows.Forms.Button();
            this.No_button = new System.Windows.Forms.Button();
            this.YesNoDialog_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Yes_button
            // 
            this.Yes_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Yes_button.Location = new System.Drawing.Point(54, 102);
            this.Yes_button.Name = "Yes_button";
            this.Yes_button.Size = new System.Drawing.Size(94, 29);
            this.Yes_button.TabIndex = 0;
            this.Yes_button.Text = "Yes_button";
            this.Yes_button.UseVisualStyleBackColor = true;
            this.Yes_button.Click += new System.EventHandler(this.YesSure_button_Click);
            // 
            // No_button
            // 
            this.No_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.No_button.Location = new System.Drawing.Point(259, 102);
            this.No_button.Name = "No_button";
            this.No_button.Size = new System.Drawing.Size(94, 29);
            this.No_button.TabIndex = 1;
            this.No_button.Text = "No_button";
            this.No_button.UseVisualStyleBackColor = true;
            this.No_button.Click += new System.EventHandler(this.NoSure_button_Click);
            // 
            // YesNoDialog_label
            // 
            this.YesNoDialog_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.YesNoDialog_label.Location = new System.Drawing.Point(54, 23);
            this.YesNoDialog_label.Name = "YesNoDialog_label";
            this.YesNoDialog_label.Size = new System.Drawing.Size(299, 50);
            this.YesNoDialog_label.TabIndex = 2;
            this.YesNoDialog_label.Text = "YesNoDialog_label";
            this.YesNoDialog_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // YesNoDialog_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 158);
            this.Controls.Add(this.YesNoDialog_label);
            this.Controls.Add(this.No_button);
            this.Controls.Add(this.Yes_button);
            this.Name = "YesNoDialog_form";
            this.ResumeLayout(false);

        }

        #endregion

        private Button Yes_button;
        private Button No_button;
        private Label YesNoDialog_label;
    }
}