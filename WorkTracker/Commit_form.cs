using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkTracker
{
    public partial class Commit_form : Form
    {
        public Commit_form()
        {
            InitializeComponent();
        }

        private void NoCommit_button_Click(object sender, EventArgs e)
        {
            YesNoDialog_form areYouSureNotCommiting_Form = new YesNoDialog_form("Are you sure?", "yes", "no");
            areYouSureNotCommiting_Form.ShowDialog();
            if(areYouSureNotCommiting_Form.DialogResult is DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void YesCommit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Commit_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.recording_form.Show();
        }
    }
}
