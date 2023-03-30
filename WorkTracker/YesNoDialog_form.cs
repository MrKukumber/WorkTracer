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
    public partial class YesNoDialog_form : Form
    {
        public YesNoDialog_form(String label, String yesButtonLabel, String noButtonLabel)
        {
            InitializeComponent();
            YesNoDialog_label.Text = label;
            Yes_button.Text = yesButtonLabel;
            No_button.Text = noButtonLabel;
        }

        private void YesSure_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void NoSure_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

    }
}
