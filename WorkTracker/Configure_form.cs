﻿using CsvHelper;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;
using static WorkTracker.LocalizationMan;

namespace WorkTracker
{
    public partial class Configure_form : Form
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

        public Form previousForm = new Form();
        public Configure_form()
        {
            InitializeComponent();
        }

        private void Configure_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppExitMan.ExitApp(e);
        }

        private void BackToPrevForm_button_Click(object sender, EventArgs e)
        {
            previousForm.Show();
            this.Hide();
        }

        private void ProjectSelection_button_Click(object sender, EventArgs e)
        {
            ProjectMan.ChooseProjectFromDialog();
        }

        private void Localization_trackBar_Scroll(object sender, EventArgs e)
        {
            LocalizationMan.ChangeLocalization((LocalizationMan.Langs)Localization_trackBar.Value);
        }

        private void ChooseMode_trackBar_Scroll(object sender, EventArgs e)
        {
            ModesMan.ChangeMode((ModesMan.ModesI) ChooseMode_trackBar.Value);
            if (!ResourceControlMan.LastTGitValidity) MessageBox.Show(Localization.NotValidTGitDirChosen);
            if (!ResourceControlMan.LastProjValidity) MessageBox.Show(Localization.NotValidProjectDirSelected);
        }

        private void ChooseTGitDir_button_Click(object sender, EventArgs e)
        {
            TortoiseGitMan.ChooseTGitFromDialog();
        }

        public void Relabel()
        {
            //DONE
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
            Program.configure_form.Relabel();
            Program.main_form.Relabel();
            Program.recording_form.Relabel();
            Program.commit_form.Relabel();
            Program.progress_form.Relabel();
            CommitMan.CheckAndSetCommit_richTextBoxes();
        }
    }

    public static class ModesMan
    {
        public enum ModesI {local = 0, repos = 1};
        static public ModesI modeI { get; private set; }
        static public string[] localizations 
        { get => new string[2] { 
            Localization.Mode_local_text, 
            Localization.Mode_repo_text }; 
        }
        static private Mode[] modes = 
        { 
            new LocalMode(),
            new ReposMode() 
        };
        static private Mode mode;
        static public VisitMode[] visitModes =
        {
            new VisitLocalMode(),
            new VisitReposMode(),
        };
        static public VisitMode visitMode;
        static public void Initialize(string init_mode)
        {
            var a = ModesI.repos.ToString();
            switch (init_mode)
            {
                case "repos":
                    modeI = ModesI.repos;
                    break;
                default:
                    modeI = ModesI.local;
                    break;
            }
            mode = modes[(int)modeI];
            visitMode = visitModes[(int)modeI];
            Program.configure_form.SetChooseMode_trackBar((int)modeI);
            mode.SetMode();
        }
        static public void ChangeMode(ModesI new_mode)
        {
            modeI = new_mode;
            mode = modes[(int)modeI];
            visitMode = visitModes[(int)modeI];
            mode.SetMode();
            TortoiseGitMan.CheckAndSetTGit_dir();
            ProjectMan.CheckAndSetProj_dir();
            RecordingMan.AdaptToEnviromentWithOldProj();
            CommitMan.CheckAndSetCommit_richTextBoxes(0);
        }

        private abstract class Mode
        {
            public abstract void SetMode();
        }

        private class LocalMode : Mode
        {
            public override void SetMode()
            {
                Program.main_form.SetMode_label();
                Program.main_form.WriteToCommit_richTextBox(Localization.Main_Commit_richTextBox_local_mode_text);

                Program.configure_form.SetForeColor_TGitLabelsButtons(Color.Olive);

                Program.progress_form.WriteToCommit_richTextBox(Localization.Progress_Commit_richTextBox_local_mode_text);
            }
        }
        private class ReposMode : Mode
        {
            public override void SetMode()
            {
                Program.main_form.SetMode_label();

                Program.configure_form.SetForeColor_TGitLabelsButtons(Color.Black);

                Program.progress_form.WriteToCommit_richTextBox(""); //TODO:write commit of date in dateTimePicker
            }

        }
        public interface VisitMode
        {
            public bool VisitForIsProjectValid();
            public bool VisitForIsTGitValid();
            public void VisitForSetRightTGit_dir();
            public void VisitForSetFalseTGit_dir();
            public RecordingMan.Record VisitForCreateRecord();
            public void VisitForStop_roundButton_Click(object sender, EventArgs e);
            public void VisitForCheckAndSetCommitInProgress(int? commitIndex);
            public void VisitForCheckAndSetCommitInMain();
        }
        public class VisitLocalMode : VisitMode
        {
            public void VisitForSetRightTGit_dir() => TortoiseGitMan.SetRightTGit_dir(this);
            public void VisitForSetFalseTGit_dir() => TortoiseGitMan.SetFalseTGit_dir(this);
            public bool VisitForIsProjectValid() => ResourceControlMan.IsProjValid(this);
            public bool VisitForIsTGitValid() => ResourceControlMan.IsTGitValid(this);
            public RecordingMan.Record VisitForCreateRecord() => RecordingMan.CreateStopRecord(this);
            public void VisitForStop_roundButton_Click(object sender, EventArgs e) => Program.recording_form.Stop_roundButton_Click(this, sender, e);
            public void VisitForCheckAndSetCommitInProgress(int? commitIndex) => CommitMan.CheckAndSetCommitInProgress(this, commitIndex);
            public void VisitForCheckAndSetCommitInMain() => CommitMan.CheckAndSetCommitInMain(this);
        }
        public class VisitReposMode : VisitMode
        {
            public void VisitForSetRightTGit_dir() => TortoiseGitMan.SetRightTGit_dir(this);
            public void VisitForSetFalseTGit_dir() => TortoiseGitMan.SetFalseTGit_dir(this);
            public bool VisitForIsProjectValid() => ResourceControlMan.IsProjValid(this);
            public bool VisitForIsTGitValid() => ResourceControlMan.IsTGitValid(this);
            public RecordingMan.Record VisitForCreateRecord() => RecordingMan.CreateStopRecord(this);
            public void VisitForStop_roundButton_Click(object sender, EventArgs e) => Program.recording_form.Stop_roundButton_Click(this, sender, e);
            public void VisitForCheckAndSetCommitInProgress(int? commitIndex) => CommitMan.CheckAndSetCommitInProgress(this, commitIndex);
            public void VisitForCheckAndSetCommitInMain() => CommitMan.CheckAndSetCommitInMain(this);
        }
    }
}
