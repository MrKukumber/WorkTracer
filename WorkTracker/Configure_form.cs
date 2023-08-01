using CsvHelper;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        // variable, that saves from which form we entered configure form so we could go back after closing configure form
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
            if (!TortoiseGitMan.LastTGitValidity) MessageBox.Show(Localization.NotValidTGitDirChosen);
            if (!ProjectMan.LastProjValidity) MessageBox.Show(Localization.NotValidProjectDirSelected);
        }
        private void ChooseTGitDir_button_Click(object sender, EventArgs e)
        {
            TortoiseGitMan.ChooseTGitFromDialog();
        }

        public void Relable()
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

        //functions for accessing forms objects
        public void SetLocalization_trackBar(int value) => Localization_trackBar.Value = value;
        public void SetChooseMode_trackBar(int value) => ChooseMode_trackBar.Value = value;
        public void SetProjDir_label(string dir) =>ProjDir_label.Text = dir;
        public void SetProjDir_labelColor(System.Drawing.Color color) => ProjDir_label.ForeColor = color;
        public void SetTGitDir_label(string dir) => TGitDir_label.Text = dir;
        public void SetTGitDir_labelColor(System.Drawing.Color color) => TGitDir_label.ForeColor = color;
        // changes color of labels and button associated with TGit
        public void SetForeColor_TGitLabelsButtons(System.Drawing.Color color)
        {
            TGitDir_label.ForeColor = color;
            ChooseTGitDir_label.ForeColor = color;
            ChooseTGitDir_button.ForeColor = color;
        }

    }
    /// <summary>
    /// manages localization of application
    /// </summary>
    internal static class LocalizationMan
    {
        public enum Langs { en_GB = 0, sk = 1 };
        static public Langs Lang { get; private set; }
        /// <summary>
        /// takes parameter of initial file and changes land according to it, then sets localization of aplication.
        /// </summary>
        /// <param name="init_lang"></param>
        static public void Initialize(string init_lang)
        {
            switch (init_lang)
            {
                case "sk":
                    Lang = Langs.sk;
                    break;
                default:
                    Lang = Langs.en_GB;
                    break;
            }
            Program.configure_form.SetLocalization_trackBar((int)Lang);
            ChangeLocalization(Lang);

        }
        static public void ChangeLocalization(Langs new_lang)
        {
            Lang = new_lang;
            switch (Lang)
            {
                case Langs.sk:
                    Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("sk-SK");
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
                    break;

            }
            Relable();
        }
        /// <summary>
        /// calls Relable functions in all forms. These functions rewrites all labels in those forms.
        /// </summary>
        static public void Relable()
        {
            Program.configure_form.Relable();
            Program.main_form.Relable();
            Program.recording_form.Relable();
            Program.commit_form.Relable();
            Program.progress_form.Relable();
            CommitMan.GetCheckAndSetCommit_richTextBoxes();
        }
    }
    /// <summary>
    /// class for managing modes
    /// </summary>
    public static class ModesMan
    {
        public enum ModesI {local = 0, repos};
        static public ModesI ModeI { get; private set; }
        static public string[] Localizations 
        { get => new string[2] { 
            Localization.Mode_local_text, 
            Localization.Mode_repo_text }; 
        }
        static private readonly Mode[] modes = 
        { 
            new LocalMode(),
            new ReposMode() 
        };
        static private Mode mode; // initialized in Initialize function
        static public readonly IVisitMode[] visitModes =
        {
            new VisitLocalMode(),
            new VisitReposMode(),
        };
        static public IVisitMode VisitMode { get; private set; } // field for visiting current mode, initialized in Initialize function
        /// <summary>
        /// takes prameter from initial file and sets mode according to it
        /// </summary>
        /// <param name="init_mode"></param>
        static public void Initialize(string init_mode)
        {
            switch (init_mode)
            {
                case "repos":
                    ModeI = ModesI.repos;
                    break;
                default:
                    ModeI = ModesI.local;
                    break;
            }
            mode = modes[(int)ModeI];
            VisitMode = visitModes[(int)ModeI];
            Program.configure_form.SetChooseMode_trackBar((int)ModeI);
            mode.SetMode();
        }
        /// <summary>
        /// takes new mode, sets it and calls functions of other managers which could ve affected by change
        /// </summary>
        /// <param name="new_mode">new mode to be set</param>
        static public void ChangeMode(ModesI new_mode)
        {
            ModeI = new_mode;
            mode = modes[(int)ModeI];
            VisitMode = visitModes[(int)ModeI];
            mode.SetMode();
            TortoiseGitMan.CheckAndSetTGit_dir();
            ProjectMan.CheckAndSetProj_dir();
            RecordingMan.AdaptToEnviromentWithOldProj();
            CommitMan.GetCheckAndSetCommit_richTextBoxes(0);
        }

        /// <summary>
        /// represents modes of aplication
        /// </summary>
        private abstract class Mode
        {
            /// <summary>
            /// sets the enviroment according to current mode
            /// </summary>
            public abstract void SetMode();
        }
        private class LocalMode : Mode
        {
            public override void SetMode()
            {
                Program.main_form.SetMode_label();
                Program.configure_form.SetForeColor_TGitLabelsButtons(Color.Olive);
            }
        }
        private class ReposMode : Mode
        {
            public override void SetMode()
            {
                Program.main_form.SetMode_label();
                Program.configure_form.SetForeColor_TGitLabelsButtons(Color.Black);
            }
        }
        /// <summary>
        /// classes inherited from this interace, serve for other managers as instances 
        /// which they can visit for checking current application mode and use appropriate version of their function
        /// theese classes ensure functionality of all methods dependent of current mode  
        /// </summary>
        public interface IVisitMode
        {
            public bool VisitForIsProjectValid();
            public bool VisitForIsTGitValid();
            public void VisitForSetRightTGit_dir();
            public void VisitForSetFalseTGit_dir();
            public void VisitForDoYouWnatToCreateRepoQuestion();
            public RecordingMan.Record VisitForCreateRecord();
            public void VisitForStop_roundButton_Click(object sender, EventArgs e);
            public void VisitForCheckAndSetCommitInProgress(int? commitIndex);
            public void VisitForCheckAndSetCommitInMain();
        }
        public class VisitLocalMode : IVisitMode
        {
            public bool VisitForIsProjectValid() => ProjectMan.IsProjValid(this);
            public bool VisitForIsTGitValid() => TortoiseGitMan.IsTGitValid(this);
            public void VisitForSetRightTGit_dir() => TortoiseGitMan.SetRightTGit_dir(this);
            public void VisitForSetFalseTGit_dir() => TortoiseGitMan.SetFalseTGit_dir(this);
            public void VisitForDoYouWnatToCreateRepoQuestion() => ProjectMan.DoYouWnatToCreateRepoQuestion(this);
            public RecordingMan.Record VisitForCreateRecord() => RecordingMan.CreateStopRecord(this);
            public void VisitForStop_roundButton_Click(object sender, EventArgs e) => Program.recording_form.Stop_roundButton_Click(this, sender, e);
            public void VisitForCheckAndSetCommitInProgress(int? commitIndex) => CommitMan.GetCheckAndSetCommitInProgress(this, commitIndex);
            public void VisitForCheckAndSetCommitInMain() => CommitMan.GetCheckAndSetCommitInMain(this);
        }
        public class VisitReposMode : IVisitMode
        {
            public bool VisitForIsProjectValid() => ProjectMan.IsProjValid(this);
            public bool VisitForIsTGitValid() => TortoiseGitMan.IsTGitValid(this);
            public void VisitForSetRightTGit_dir() => TortoiseGitMan.SetRightTGit_dir(this);
            public void VisitForSetFalseTGit_dir() => TortoiseGitMan.SetFalseTGit_dir(this);
            public void VisitForDoYouWnatToCreateRepoQuestion() => ProjectMan.DoYouWnatToCreateRepoQuestion(this);
            public RecordingMan.Record VisitForCreateRecord() => RecordingMan.CreateStopRecord(this);
            public void VisitForStop_roundButton_Click(object sender, EventArgs e) => Program.recording_form.Stop_roundButton_Click(this, sender, e);
            public void VisitForCheckAndSetCommitInProgress(int? commitIndex) => CommitMan.GeCheckAndSetCommitInProgress(this, commitIndex);
            public void VisitForCheckAndSetCommitInMain() => CommitMan.GetCheckAndSetCommitInMain(this);
        }
    }

    /// <summary>
    /// class for managing tortoise git direcotry and calling torotise git actions
    /// </summary>
    internal static class TortoiseGitMan
    {
        static public string TGit_dir { get => tGit_dir; }
        static private string tGit_dir = "";
        static public bool LastTGitValidity { get; private set; }// properties set by calling function IsTGitValid() 

        /// <summary>
        /// intitialize TGit directory to saved one and checks, if it is valid
        /// </summary>
        /// <param name="init_tGit_dir">saved direcotry from initial file</param>
        static public void Initialize(string init_tGit_dir)
        {
            tGit_dir = init_tGit_dir;
            CheckAndSetTGit_dir();
        }
        /// <summary>
        /// opens up the folder browser dialog, where user can choose drectory of TGit
        /// if directory is not valid, it shows relevant message
        /// after choosing a directory, it lets recording manager adapt enviroment according to validity of chosen file
        /// </summary>
        static public void ChooseTGitFromDialog()
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tGit_dir = openFileDialog.SelectedPath;
                    CheckAndSetTGit_dir();
                    if (!TortoiseGitMan.LastTGitValidity) MessageBox.Show(Localization.NotValidTGitDirChosen);
                    RecordingMan.AdaptToEnviromentWithOldProj();
                }
                else
                {
                    MessageBox.Show(Localization.SomethingWentWrongTGitDialog);
                }
            }
        }
        /// <summary>
        /// test for existence of executable file of Tortoise Git in tGit_dir directory
        /// </summary>
        /// <returns></returns>
        static public bool ExistsTG() => File.Exists(tGit_dir + "\\TortoiseGitProc.exe");
        /// <summary>
        /// Checks if TGit directory is valid by visiting mode in ModesMan
        /// </summary>
        /// <returns>validity</returns>
        static public bool IsTGitValid() => ModesMan.VisitMode.VisitForIsTGitValid();
        static public bool IsTGitValid(ModesMan.VisitLocalMode mode)
        {
            LastTGitValidity = true;
            return LastTGitValidity;
        }
        static public bool IsTGitValid(ModesMan.VisitReposMode mode)
        {
            LastTGitValidity = ExistsTG();
            return LastTGitValidity;
        }
        /// <summary>
        /// function that sets appropriate changes of enviroment according to TGit dir validity
        /// </summary>
        static public void CheckAndSetTGit_dir()
        {
            if (TortoiseGitMan.IsTGitValid()) SetRightTGit_dir();
            else SetFalseTGit_dir();
        }
        static private void SetRightTGit_dir() => ModesMan.VisitMode.VisitForSetRightTGit_dir(); //visits mode for correct adjustments
        static public void SetRightTGit_dir(ModesMan.VisitLocalMode _)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Olive);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
        }
        static public void SetRightTGit_dir(ModesMan.VisitReposMode _)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Black);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
        }
        static private void SetFalseTGit_dir() => ModesMan.VisitMode.VisitForSetFalseTGit_dir(); //visits mode for correct adjustments
        static public void SetFalseTGit_dir(ModesMan.VisitLocalMode _)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Olive);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
        }
        static public void SetFalseTGit_dir(ModesMan.VisitReposMode _)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Red);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(true);
        }
        static public void WriteTGit_dirTo(StreamWriter file) => file.WriteLine("tgit_dir " + tGit_dir);
    }

    /// <summary>
    /// class for maging project directory, provides functions for checking, if it is valid
    /// </summary>
    internal static class ProjectMan
    {
        private const string csvRecordFileName = ".workTracer_recordings.csv";
        static public string Proj_dir { get => proj_dir; }
        static private string proj_dir = "";
        static public bool LastProjValidity { get; private set; }// properties set by calling function IsProjValid() 

        /// intitialize project directory to saved one and checks, if it is valid
        static public void Initialize(string init_proj_dir)
        {
            proj_dir = init_proj_dir;
            CheckAndSetProj_dir();
        }
        static public string PathToCSVRecordFile { get => proj_dir + "\\" + csvRecordFileName; }
        static public bool ExistsRecordCSV() => File.Exists(proj_dir + "\\" + csvRecordFileName);
        /// <summary>
        /// opens up a folder browser dialog for choosing new project (directory)
        /// if directory is not valid, it shows relevant message
        /// after choosing a directory, it calls functions of other managers which could ve affected by change
        /// if repo mode is active it and chosen directory doesnt contains repository, it asks user, if he wants to create one
        /// </summary>
        static public void ChooseProjectFromDialog()
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    proj_dir = openFileDialog.SelectedPath;

                    DoYouWnatToCreateRepoQuestion();

                    CheckAndSetProj_dir();
                    if (!ProjectMan.LastProjValidity) MessageBox.Show(Localization.NotValidProjectDirSelected);
                    RecordingMan.AdaptToEnviromentWithNewProj(out bool ableToAccessCSV);
                    ProgressMan.CheckAndSetDateTimePickersInProgress(true, out _);
                    CommitMan.GetCheckAndSetCommit_richTextBoxes(0);
                    if (!ableToAccessCSV) MessageBox.Show(Localization.Config_UnableToAccessCSV);
                }
                else MessageBox.Show(Localization.SomethingWentWrongProjectDialog);
            }
        }
        /// <summary>
        /// funcion for asking, if user wnats to create repository according to selected mode
        /// </summary>
        static private void DoYouWnatToCreateRepoQuestion() => ModesMan.VisitMode.VisitForDoYouWnatToCreateRepoQuestion();
        static public void DoYouWnatToCreateRepoQuestion(ModesMan.VisitLocalMode mode) { }
        static public void DoYouWnatToCreateRepoQuestion(ModesMan.VisitReposMode mode)
        {
            if (Directory.Exists(proj_dir) && !IsThereRepo())
            {
                YesNoDialog_form wantToCreateRepoDialog_form = new YesNoDialog_form(Localization.Configure_WantToCreateRepoDialog, Localization.Yes, Localization.No);
                wantToCreateRepoDialog_form.ShowDialog();
                if (wantToCreateRepoDialog_form.DialogResult is DialogResult.Yes) CreateRepo();
            }
        }

        public static bool ExistsProjDir() => Directory.Exists(proj_dir);
        /// <summary>
        /// testing, if given project directory is part of git repository ba calling git command
        /// </summary>
        /// <returns>return true, if project is part of repository</returns>
        public static bool IsThereRepo()
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = proj_dir;
                p.StartInfo.FileName = "git";
                p.StartInfo.Arguments = "rev-parse --is-inside-work-tree";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                var output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                return output == "true\n";
            }
        }
        /// <summary>
        /// Checks if project directory is valid by visiting mode in ModesMan
        /// </summary>
        /// <returns>validity</returns>
        static public bool IsProjValid() => ModesMan.VisitMode.VisitForIsProjectValid();


        static public bool IsProjValid(ModesMan.VisitLocalMode mode)
        {
            LastProjValidity = ExistsProjDir();
            return LastProjValidity;
        }
        static public bool IsProjValid(ModesMan.VisitReposMode mode)
        {
            LastProjValidity = ExistsProjDir() && IsThereRepo();
            return LastProjValidity;
        }
        /// <summary>
        /// function that sets appropriate changes of enviroment according to project dir validity
        /// </summary>
        static public void CheckAndSetProj_dir()
        {
            if (ProjectMan.IsProjValid()) SetRightProj_dir();
            else SetFalseProj_dir();

        }
        static private void SetRightProj_dir()
        {
            Program.configure_form.SetProjDir_label(proj_dir);
            Program.configure_form.SetProjDir_labelColor(Color.Black);

            Program.main_form.SetProjNotSelected_labelVisible(false);
            Program.main_form.SetProgressFormOpening_buttonEnabled(true);
        }

        static private void SetFalseProj_dir()
        {
            Program.configure_form.SetProjDir_label(proj_dir);
            Program.configure_form.SetProjDir_labelColor(Color.Red);

            Program.main_form.SetProjNotSelected_labelVisible(true);
            Program.main_form.SetProgressFormOpening_buttonEnabled(false);
        }

        /// <summary>
        /// creates repository in project directory
        /// </summary>
        private static void CreateRepo()
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = proj_dir;
                p.StartInfo.FileName = "git";
                p.StartInfo.Arguments = "init";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            }
        }
        public static void WriteProj_dirTo(StreamWriter file) => file.WriteLine("last_proj_dir " + proj_dir);

    }
}
