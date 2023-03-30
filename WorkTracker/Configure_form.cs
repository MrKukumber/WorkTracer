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
    public partial class Configure_form : Form
    {
        public Form previousForm = new Form();
        public Configure_form()
        {
            InitializeComponent();
        }


        private void ChooseMode_trackBar_Scroll(object sender, EventArgs e)
        {

        }

        private void Configure_form_Load(object sender, EventArgs e)
        {

        }

        private void Configure_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void BackToPrevForm_button_Click(object sender, EventArgs e)
        {
            previousForm.Show();
            this.Hide();
        }

        private void ProjectSelection_button_Click(object sender, EventArgs e)
        {

        }
    }
}
