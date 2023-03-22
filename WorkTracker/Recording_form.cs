using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkTracker
{

    public partial class Recording_form : Form
    {
        public Recording_form()
        {
            InitializeComponent();
            Console.WriteLine();
        }


        private void Play_roundButton_Click(object sender, EventArgs e)
        {

        }

        private void Pause_roundButton_Click(object sender, EventArgs e)
        {

        }

        private void Stop_roundButton_Click(object sender, EventArgs e)
        {
            Program.commit_form.Show();
            this.Enabled = false;
        }

        private void ReturnToMain_Button_Click(object sender, EventArgs e)
        {
            Program.main_form.Show();
            this.Hide();
        }

        private void ConfigFormOpening_button_Click(object sender, EventArgs e)
        {
            Program.configure_form.Show();
            Program.configure_form.previousForm = this;
            this.Hide();

        }

        private void Recording_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }





    }

    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(2, 2, ClientSize.Width - 5, ClientSize.Height - 5);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
