using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;
using static WorkTracker.CommitMan;

namespace WorkTracker
{
    public partial class Progress_form : Form
    {
        const int WM_ACTIVATEAPP = 0x1C;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ACTIVATEAPP && Form.ActiveForm == this)
            {
                if (m.WParam != IntPtr.Zero)
                {
                    // the application is getting activated
                    Program.CheckAfterActivatingApp();
                }
            }
            base.WndProc(ref m);
        }
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
            AppExitMan.ExitApp(e);
        }
        private void Commit_vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            CommitMan.ChangeCommitInProgressRichTextBox(Commit_vScrollBar.Value);
        }

        public void Relabel()
        {
            this.Text = Localization.Progress_form_text;
            Since_label.Text = Localization.Progress_From_label_text;
            Until_label.Text = Localization.Progress_To_label_text;
            SameDate_label.Text = Localization.Progress_SameDate_label_text;
            //TODO: doplnit vysledne hodnoty doby prace 
            CompDurationText_label.Text = Localization.Progress_CompDurationText_label_text;
            CompDurationWithPauseText_label.Text = Localization.Progress_CompDurationWithStopText_label_text;
            ReturnToMain_button.Text = Localization.ReturnToMain_button_text;
            if (ModesMan.modeI is ModesMan.ModesI.local) Commit_richTextBox.Text = Localization.Progress_Commit_richTextBox_local_mode_text;

        }
        public void WriteToCommit_richTextBox(string what) => Commit_richTextBox.Text = what;
        public int GetCommit_richTextBoxWidth() => Commit_richTextBox.Width;
        public void EnableCommit_vScrollBar(bool indicator) => Commit_vScrollBar.Enabled = indicator;
        public DateTime GetSince_dateTimePickerDate() => Since_dateTimePicker.Value;
        public DateTime GetUntil_dateTimePickerDate() => Until_dateTimePicker.Value;
        public void SetCommit_vScrollBarMaximum(int maximum) => Commit_vScrollBar.Maximum = maximum;
        public int Commit_vScrollValue { get => Commit_vScrollBar.Value; set => Commit_vScrollBar.Value = value; }

        private void Since_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (SameDate_checkBox.Checked) Until_dateTimePicker.Value = Since_dateTimePicker.Value;
            else Until_dateTimePicker.MinDate = Since_dateTimePicker.Value;
            CommitMan.CheckAndSetCommitInProgress();
        }

        private void Until_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            
            if (SameDate_checkBox.Checked) Since_dateTimePicker.Value = Until_dateTimePicker.Value;
            else Since_dateTimePicker.MaxDate = Until_dateTimePicker.Value;
            CommitMan.CheckAndSetCommitInProgress();
        }

        private void SameDate_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SameDate_checkBox.Checked)
            {
                Since_dateTimePicker.MaxDate = Until_dateTimePicker.MaxDate;
                Until_dateTimePicker.MinDate = Since_dateTimePicker.MinDate;
                Until_dateTimePicker.Value = Since_dateTimePicker.Value;
                Until_dateTimePicker.Enabled = false;
            }
            else
            {
                Since_dateTimePicker.MaxDate = Until_dateTimePicker.Value;
                Until_dateTimePicker.MinDate = Since_dateTimePicker.Value;
                Until_dateTimePicker.Enabled = true;
            }
        }

    }

    static public class ProgressShowingMan
    {
        static public void Initialize()
        {
            //TODO:
        }
    }
    

}
