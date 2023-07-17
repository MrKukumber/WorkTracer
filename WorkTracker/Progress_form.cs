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
            AppExitMan.ExitApp(e);
        }
        public void Relabel()
        {
            this.Text = Localization.Progress_form_text;
            From_label.Text = Localization.Progress_From_label_text;
            To_label.Text = Localization.Progress_To_label_text;
            Day_label.Text = Localization.Progress_Day_label_text;
            Month_label.Text = Localization.Progress_Month_label_text;
            DayMean_label.Text = Localization.Progress_DayMean_label_text;
            //TODO: doplnit vysledne hodnoty doby prace 
            CompDuration_label.Text = Localization.Progress_CompDuration_label_text;
            CompDurationWithStop_label.Text = Localization.Progress_CompDurationWithStop_label_text;
            ReturnToMain_button.Text = Localization.ReturnToMain_button_text;
            if (ModesMan.modeI is ModesMan.ModesI.local) Commit_richTextBox.Text = Localization.Progress_Commit_richTextBox_local_mode_text;

        }
        public void WriteToCommit_richTextBox(string what) => Commit_richTextBox.Text = what;
        public void SetCommit_dateTimePickerEnabled(bool indicator) => Commit_dateTimePicker.Enabled = indicator;
        
        public void ShowCommit_richTextBox()
        {
            //TODO:
        }
    }
}
