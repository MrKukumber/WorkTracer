using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;
using static WorkTracker.LocalizationMan;

namespace WorkTracker
{
    public partial class Configure_form : Form
    {
        public Form previousForm = new Form();
        public Configure_form()
        {
            InitializeComponent();
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
            ProjectMan.ChooseProjectFromDialog();
            if (!ProjectMan.LastProjValidity) MessageBox.Show(Localization.NotValidProjectDirSelected);

        }

        private void Localization_trackBar_Scroll(object sender, EventArgs e)
        {
            LocalizationMan.ChangeLocalization((LocalizationMan.Langs)Localization_trackBar.Value);
        }

        private void ChooseMode_trackBar_Scroll(object sender, EventArgs e)
        {
            ModesMan.ChangeMode((ModesMan.Modes) ChooseMode_trackBar.Value);
        }

        private void ChooseTGitDir_button_Click(object sender, EventArgs e)
        {
            TortoiseGitMan.ChooseTGitFromDialog();
            if (!TortoiseGitMan.LastTGitValidity) MessageBox.Show(Localization.NotValidTGitDirChosen);

        }
        public void Relabel()
        {
            this.Text = Localization.Configure_form_text;
            ChooseTGitDir_button.Text = Localization.Select;
            ProjectSelection_button.Text = Localization.Select;
            ChooseProjDir_label.Text = Localization.Config_ChooseProjDir_label_text;
            ChooseLanguge_label.Text = Localization.Config_ChooseLanguge_label_text;
            ChooseTGitDir_label.Text = Localization.Config_ChooseTGitDir_label_text;
            Mode1_label.Text = Localization.Config_Mode1_label_text;
            Mode2_label.Text = Localization.Config_Mode2_label_text;
            BackToPrevForm_button.Text = Localization.Config_BackToPrevForm_button_text;
        }

        public void SetLocalization_trackBar(int value)
        {
            Localization_trackBar.Value = value;
        }

        public void SetChooseMode_trackBar(int value) => ChooseMode_trackBar.Value = value;
        public void SetProjDir_label(string dir) =>ProjDir_label.Text = dir;
        public void SetProjDir_labelColor(System.Drawing.Color color) => ProjDir_label.ForeColor = color;
        public void SetTGitDir_label(string dir) => TGitDir_label.Text = dir;
        public void SetTGitDir_labelColor(System.Drawing.Color color) => TGitDir_label.ForeColor = color;
        public void SetForeColor_TGitLabelsButtons(System.Drawing.Color color)
        {
            TGitDir_label.ForeColor = color;
            ChooseTGitDir_label.ForeColor = color;
            ChooseTGitDir_button.ForeColor = color;
        }


    }

    internal static class LocalizationMan
    {
        public enum Langs { en_GB = 0, sk = 1 };
        static public Langs lang { get; private set; }
        static public void Initialize(string init_lang)
        {
            switch (init_lang)
            {
                case "sk":
                    lang = Langs.sk;
                    break;
                default:
                    lang = Langs.en_GB;
                    break;
            }
            Program.configure_form.SetLocalization_trackBar((int)lang);
            ChangeLocalization(lang);

        }
        static public void ChangeLocalization(Langs new_lang)
        {
            lang = new_lang;
            switch (lang)
            {
                case Langs.sk:
                    Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("sk-SK");
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
                    break;

            }
            Relabel();
        }
        static public void Relabel()
        {
            ModesMan.Relabel();//je dolezite aby zostalo pred Relable funkciami formularov
            //TODO: to iste budeme musiet spravit aj pre tie fazy a  typy nahravania
            Program.configure_form.Relabel();
            Program.main_form.Relabel();
            Program.recording_form.Relabel();
            Program.commit_form.Relabel();
            Program.progress_form.Relabel();
        }
    }

    internal static class ModesMan
    {
        public enum Modes {local = 0, repos = 1};
        static public Modes mode { get; private set; }
        static public string[] localizations = { Localization.Mode_local_text, Localization.Mode_repo_text };
        static private Mode[] modes = { new LocalMode(), new ReposMode() };

        //TODO: dodelat managera modov
        static public void Initialize(string init_mode)
        {
            switch (init_mode)
            {
                case "repos":
                    mode = Modes.repos;
                    break;
                default:
                    mode = Modes.local;
                    break;
            }
            Program.configure_form.SetChooseMode_trackBar((int)mode);
            modes[(int)mode].SetMode();
        }
        static public void Relabel() => localizations = new string[] { Localization.Mode_local_text, Localization.Mode_repo_text };
        static public void ChangeMode(Modes new_mode)
        {
            mode = new_mode;
            modes[(int)mode].SetMode();
        }

        private abstract class Mode
        {
            public abstract void SetMode();
        }

        private class LocalMode : Mode
        {
            public override void SetMode()
            {
                //Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
                //if (ProjectMan.IsProjValid())
                //{
                //    Program.main_form.SetProjNotSelected_labelVisible(false);
                //    Program.main_form.SetProgressFormOpening_buttonEnabled(true);
                //}
                //else
                //{
                //    Program.main_form.SetProjNotSelected_labelVisible(true);
                //    Program.main_form.SetProgressFormOpening_buttonEnabled(false);
                //}
                Program.main_form.SetMode_label();
                Program.main_form.WriteToCommit_richTextBox(Localization.Main_Commit_richTextBox_local_mode_text);

                Program.configure_form.SetForeColor_TGitLabelsButtons(Color.Olive);

                Program.progress_form.SetCommit_dateTimePickerEnabled(false);
                Program.progress_form.WriteToCommit_richTextBox(Localization.Progress_Commit_richTextBox_local_mode_text);

                TortoiseGitMan.CheckAndSetTGit_dir();
                ProjectMan.CheckAndSetProj_dir();
            }
        }
        private class ReposMode : Mode
        {
            public override void SetMode()
            {
                //if (TortoiseGitMan.IsTGitValid()) Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
                //else Program.main_form.SetTortoiseFileNotSelected_labelVisible(true);
                //if (ProjectMan.IsProjValid())
                //{
                //    Program.main_form.SetProjNotSelected_labelVisible(false);
                //    Program.main_form.SetProgressFormOpening_buttonEnabled(true);
                //}
                //else
                //{
                //    Program.main_form.SetProjNotSelected_labelVisible(true);
                //    Program.main_form.SetProgressFormOpening_buttonEnabled(false);
                //}
                Program.main_form.WriteToCommit_richTextBox("");//TODO: write last commit
                Program.main_form.SetMode_label();

                Program.configure_form.SetForeColor_TGitLabelsButtons(Color.Black);

                Program.progress_form.WriteToCommit_richTextBox(""); //TODO:write commit of date in dateTimePicker
                Program.progress_form.SetCommit_dateTimePickerEnabled(true);

                TortoiseGitMan.CheckAndSetTGit_dir();
                ProjectMan.CheckAndSetProj_dir();
            }
        }
    }
}
