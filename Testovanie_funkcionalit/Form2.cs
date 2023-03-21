using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Testovanie_funkcionalit
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BackToMain_button_Click(object sender, EventArgs e)
        {
            Program.form1.Show();
            this.Hide();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form.ActiveForm == this)
                MessageBox.Show("Urcite closing2?");
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Form.ActiveForm == this)
                MessageBox.Show("a ted po druhe closed!");
            System.Windows.Forms.Application.Exit();
        }
    }
}
