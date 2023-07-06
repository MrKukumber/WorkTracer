using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;

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
            YesNoDialog_form areYouSureNotCommiting_Form = new YesNoDialog_form( Localization.NotCommiting_YesNoDialog_label_text, Localization.Yes, Localization.No);
            areYouSureNotCommiting_Form.ShowDialog();
            if (areYouSureNotCommiting_Form.DialogResult is DialogResult.Yes)
            {
                Program.recording_form.Show();
                this.Hide();
            }
        }

        private void YesCommit_button_Click(object sender, EventArgs e)
        {
            //TODO: zavolat tortoisegit, commitnut, na zaklade vysledku commitu zavriet alebo
            //nechat otvorene a oznamit uzivatelovi ze nekommitol a ci to vazne chce tak nechat,
            //ak che vratit ho do recordingu ak nie znova otvorit commit okno
            Program.recording_form.Show();
            this.Hide();
        }

        private void Commit_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                YesNoDialog_form areYouSureNotCommiting_Form = new YesNoDialog_form(Localization.NotCommiting_YesNoDialog_label_text, Localization.Yes, Localization.No);
                areYouSureNotCommiting_Form.ShowDialog();
                if (areYouSureNotCommiting_Form.DialogResult is DialogResult.Yes)
                {
                    Program.recording_form.Show();
                    this.Hide();
                }
            }
            else System.Windows.Forms.Application.Exit();
        }
        public void Relabel()
        {
            this.Text = Localization.Commit_form_text;
            WantCommit_label.Text = Localization.Commit_WantCommit_label_text;
            YesCommit_button.Text = Localization.Yes;
            NoCommit_button.Text = Localization.No;
        }
    }
}
