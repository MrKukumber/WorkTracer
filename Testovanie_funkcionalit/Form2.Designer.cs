namespace Testovanie_funkcionalit
{
    partial class Form2
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
            this.BackToMain_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BackToMain_button
            // 
            this.BackToMain_button.Location = new System.Drawing.Point(355, 92);
            this.BackToMain_button.Name = "BackToMain_button";
            this.BackToMain_button.Size = new System.Drawing.Size(94, 29);
            this.BackToMain_button.TabIndex = 0;
            this.BackToMain_button.Text = "back to main";
            this.BackToMain_button.UseVisualStyleBackColor = true;
            this.BackToMain_button.Click += new System.EventHandler(this.BackToMain_button_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 288);
            this.Controls.Add(this.BackToMain_button);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private Button BackToMain_button;
    }
}