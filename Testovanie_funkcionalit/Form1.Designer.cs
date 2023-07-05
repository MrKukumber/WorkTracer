namespace Testovanie_funkcionalit
{
    partial class Form1
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
            this.OpenTGitButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.GoTOOtherForm_button = new System.Windows.Forms.Button();
            this.IsHereRepo_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OpenTGitButton
            // 
            this.OpenTGitButton.Location = new System.Drawing.Point(12, 51);
            this.OpenTGitButton.Name = "OpenTGitButton";
            this.OpenTGitButton.Size = new System.Drawing.Size(142, 46);
            this.OpenTGitButton.TabIndex = 0;
            this.OpenTGitButton.Text = "open_TortoiseGit";
            this.OpenTGitButton.UseVisualStyleBackColor = true;
            this.OpenTGitButton.Click += new System.EventHandler(this.OpenTGitButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(245, 93);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(204, 323);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // GoTOOtherForm_button
            // 
            this.GoTOOtherForm_button.Location = new System.Drawing.Point(60, 333);
            this.GoTOOtherForm_button.Name = "GoTOOtherForm_button";
            this.GoTOOtherForm_button.Size = new System.Drawing.Size(94, 29);
            this.GoTOOtherForm_button.TabIndex = 2;
            this.GoTOOtherForm_button.Text = "To other form";
            this.GoTOOtherForm_button.UseVisualStyleBackColor = true;
            this.GoTOOtherForm_button.Click += new System.EventHandler(this.GoTOOtherForm_button_Click);
            // 
            // IsHereRepo_button
            // 
            this.IsHereRepo_button.Location = new System.Drawing.Point(245, 485);
            this.IsHereRepo_button.Name = "IsHereRepo_button";
            this.IsHereRepo_button.Size = new System.Drawing.Size(94, 29);
            this.IsHereRepo_button.TabIndex = 3;
            this.IsHereRepo_button.Text = "Is here repo?";
            this.IsHereRepo_button.UseVisualStyleBackColor = true;
            this.IsHereRepo_button.Click += new System.EventHandler(this.IsHereRepo_button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 559);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "change lang";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 595);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 641);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.IsHereRepo_button);
            this.Controls.Add(this.GoTOOtherForm_button);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.OpenTGitButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button OpenTGitButton;
        private RichTextBox richTextBox1;
        private Button GoTOOtherForm_button;
        private Button IsHereRepo_button;
        private Button button1;
        private Label label1;
    }
}