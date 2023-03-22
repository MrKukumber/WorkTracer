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
    public partial class AreYouSure_form : Form
    {
        public AreYouSure_form()
        {
            InitializeComponent();
        }

        protected virtual void YesSure_button_Click(object sender, EventArgs e)
        {

        }

        protected virtual void NoSure_button_Click(object sender, EventArgs e)
        {

        }
    }

    public class AreYouSureNotCommiting_form : AreYouSure_form 
    {
        protected override void YesSure_button_Click(object sender, EventArgs e)
        {

        }

        protected override void NoSure_button_Click(object sender, EventArgs e)
        {

        }
    }
    public class AreYouSureExitWithoutStop_form : AreYouSure_form
    {
        protected override void YesSure_button_Click(object sender, EventArgs e)
        {

        }

        protected override void NoSure_button_Click(object sender, EventArgs e)
        {

        }
    }
}
