using WorkTracker.Properties;

namespace WorkTracker
{
    public partial class Main_form : Form
    {
        public Main_form()
        {
            InitializeComponent();
        }

        private void RecordingFormOpening_Button_Click(object sender, EventArgs e)
        {
            Program.recording_form.Show();
            this.Hide();

        }
        private void ConfigFormOpening_Button_Click(object sender, EventArgs e)
        {
            Program.configure_form.Show();
            Program.configure_form.previousForm = this;
            this.Hide();

        }


        private void Main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ProgressFormOpening_button_Click(object sender, EventArgs e)
        {
            Program.progress_form.Show();
            this.Hide();
        }
        public void Relabel()
        {
            Commit_label.Text = Localization.Main_Commit_label_text;
            ConfigFormOpening_button.Text = Localization.Main_ConfigFormOpening_button_text;
            RecordingFormOpening_button.Text = Localization.Main_RecordingFormOpening_Button_text;
            ProgressFormOpening_button.Text = Localization.Main_ProgressFormOpening_button_text;
            ProjNotSelected_label.Text = Localization.Main_ProjNotSelected_label_text;
            TortoiseFileNotSelected_label.Text = Localization.Main_TortoiseFileNotSelected_label_text;
            switch (ModesMan.mode)
            {
                case ModesMan.Modes.local:
                    Mode_label.Text = Localization.Mode_local_text;
                    break;
                case ModesMan.Modes.repos:
                    Mode_label.Text = Localization.Mode_repo_text;
                    break;
                default:
                    Mode_label.Text = ""; //Should not happend
                    break;
            }
            switch (RecordingMan.recState)
            {
                case RecordingMan.RecStates.started:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_play_text;
                    break;
                case RecordingMan.RecStates.paused:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_pause_text;
                    break;
                case RecordingMan.RecStates.stoped:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_stop_text;
                    break;
                default:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_none_text;
                    break;
            }
        }

        private void Mode_label_Click(object sender, EventArgs e)
        {

        }
    }
}