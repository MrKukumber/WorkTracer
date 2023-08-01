using WorkTracker.Properties;

namespace WorkTracker
{
    public partial class Main_form : Form
    {
        const int WM_ACTIVATEAPP = 0x1C;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ACTIVATEAPP && Form.ActiveForm == this)
            {
                if (m.WParam != IntPtr.Zero)
                {
                    // the application is getting activated
                    Program.CheckAfterActivatingApp(this);
                }
            }
            base.WndProc(ref m);
        }
        public Main_form()
        {
            InitializeComponent();
        }

        private void RecordingFormOpening_Button_Click(object sender, EventArgs e)
        {
            if (!ProjectMan.LastProjValidity)
                if (!TortoiseGitMan.LastTGitValidity) MessageBox.Show(Localization.NotValidProjectDirSelected + "\n" + Localization.NotValidTGitDirChosen);
                else MessageBox.Show(Localization.NotValidProjectDirSelected);
            else if (!TortoiseGitMan.LastTGitValidity) MessageBox.Show(Localization.NotValidTGitDirChosen);
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
            AppExitMan.ExitApp(e);
        }

        private void ProgressFormOpening_button_Click(object sender, EventArgs e)
        {
            ProgressMan.SetAndShowProgression(out bool ableToAccessCSV);
            if(!ableToAccessCSV) MessageBox.Show(Localization.Progress_UnableToAccessCSV);
            Program.progress_form.Show();
            this.Hide();
        }
        public void Relable()
        {
            this.Text = Localization.Main_form_text;
            Commit_label.Text = Localization.Main_Commit_label_text;
            ConfigFormOpening_button.Text = Localization.Main_ConfigFormOpening_button_text;
            RecordingFormOpening_button.Text = Localization.Main_RecordingFormOpening_Button_text;
            ProgressFormOpening_button.Text = Localization.Main_ProgressFormOpening_button_text;
            ProjNotSelected_label.Text = Localization.Main_ProjNotSelected_label_text;
            TortoiseFileNotSelected_label.Text = Localization.Main_TortoiseFileNotSelected_label_text;
            if (ModesMan.ModeI is ModesMan.ModesI.local) Commit_richTextBox.Text = Localization.Main_Commit_richTextBox_local_mode_text;
            CurrTrackState_label.Text = RecordingMan.StatesLocalizations[(int)RecordingMan.recState];            
        }
        //functions for accessing forms objects
        public void SetTortoiseFileNotSelected_labelVisible(bool indicator) => TortoiseFileNotSelected_label.Visible = indicator;
        public void SetProjNotSelected_labelVisible(bool indicator) => ProjNotSelected_label.Visible = indicator;
        public void SetMode_label() => Mode_label.Text = ModesMan.Localizations[(int)ModesMan.ModeI];
        public void SetProgressFormOpening_buttonEnabled(bool indicator) => ProgressFormOpening_button.Enabled = indicator;
        public void WriteToCommit_richTextBox(string what) => Commit_richTextBox.Text = what;
        public void SetCurrTrackState_label() => CurrTrackState_label.Text = RecordingMan.StatesLocalizations[(int)RecordingMan.recState];
        public int GetCommit_richTextBoxWidth() => Commit_richTextBox.Width;
    }
}