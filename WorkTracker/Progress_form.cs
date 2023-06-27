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
    public partial class Progress_form : Form
    {
        public Progress_form()
        {
            InitializeComponent();
        }

        private void MainFormOpening_button_Click(object sender, EventArgs e)
        {
            Program.main_form.Show();
            this.Hide();
        }

        private void Progress_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
