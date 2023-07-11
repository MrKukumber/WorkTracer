using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WorkTracker.Properties;

namespace WorkTracker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Initializer.Execute();
            Application.Run(main_form);

        }

        public static Main_form main_form = new Main_form();
        public static Configure_form configure_form = new Configure_form();
        public static Recording_form recording_form = new Recording_form();
        public static Progress_form progress_form = new Progress_form();
        public static Commit_form commit_form = new Commit_form();
    }
    
    /// <summary>
    /// Inicialize parameters of GUI from text file or set default
    /// </summary>
    internal static class Initializer
    {
        static public void Execute()
        {
            Dictionary<string,string> init_params = InitParamsParser();
            Initialize(init_params);
        }

        static private Dictionary<string, string> InitParamsParser()
        {
            Dictionary<string, string> parameters = new()
            {
                {"lang",""},
                {"mode",""},
                {"tgit_dir",""},
                {"last_proj_dir",""}
            };
            using (StreamReader paramFile = new StreamReader("init_params.txt"))
            {

                while (paramFile.ReadLine() is string line)
                {
                    string[] line_parts = line.Split(" ");
                    if (line_parts.Length == 1) continue;
                    parameters[line_parts[0]] = string.Join(" ", line_parts.Skip(1).ToArray());
                }
            }
            return parameters;
        }

        static private void Initialize (Dictionary<string,string> init_params)
        {
            TortoiseGitMan.Initialize(init_params["tgit_dir"]);
            ProjectMan.Initialize(init_params["last_proj_dir"]);
            ModesMan.Initialize(init_params["mode"]);
            LocalizationMan.Initialize(init_params["lang"]);
            RecordingMan.Initialize();
            CommitMan.Initialize();
        }
    }



    internal static class TortoiseGitMan
    {
        static private string tGit_dir = "";
        static public bool LastTGitValidity { get; private set; }
        static public bool IsTGitValid()
        {
            LastTGitValidity = ExistsTG();
            return LastTGitValidity;
        }
        static public void Initialize(string init_tGit_dir)
        {
            tGit_dir = init_tGit_dir;
        }

        static private bool ExistsTG() => File.Exists(tGit_dir + "\\TortoiseGitProc.exe");

        static public void ChooseTGitFromDialog()
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tGit_dir = openFileDialog.SelectedPath;
                    TortoiseGitMan.CheckAndSetTGit_dir();
                }
                else
                {
                    MessageBox.Show(Localization.SomethingWentWrongTGitDialog);
                }
            }
        }
        static public void CheckAndSetTGit_dir()
        {
            if (IsTGitValid()) SetRightTGit_dir();
            else SetFalseTGit_dir();
        }
        static private void SetRightTGit_dir()
        {
            switch (ModesMan.mode)
            {
                case ModesMan.Modes.repos:
                    Program.configure_form.SetTGitDir_label(tGit_dir);
                    Program.configure_form.SetTGitDir_labelColor(Color.Black);
                    Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
                    break;
                default:
                    Program.configure_form.SetTGitDir_label(tGit_dir);
                    Program.configure_form.SetTGitDir_labelColor(Color.Olive);
                    Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
                    break;
            }
        }

        static private void SetFalseTGit_dir()
        {
            switch (ModesMan.mode)
            {
                case ModesMan.Modes.repos:
                    Program.configure_form.SetTGitDir_label(tGit_dir);
                    Program.configure_form.SetTGitDir_labelColor(Color.Red);
                    Program.main_form.SetTortoiseFileNotSelected_labelVisible(true);
                    break;
                default:
                    Program.configure_form.SetTGitDir_label(tGit_dir);
                    Program.configure_form.SetTGitDir_labelColor(Color.Olive);
                    Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
                    break;
            }
            
        }
    


    }
    internal static class ProjectMan
    {
        static private string proj_dir = "";

        static public void Initialize(string init_proj_dir)
        {
            proj_dir = init_proj_dir;
        }
        static public bool LastProjValidity { get; private set; }
        static public bool IsProjValid()
        {
            switch (ModesMan.mode)
            {
                case ModesMan.Modes.repos:
                    LastProjValidity = Directory.Exists(proj_dir) && IsThereRepo();
                    break;
                default:
                    LastProjValidity = Directory.Exists(proj_dir);
                    break;
            }
            return LastProjValidity;

        }
        static public void ChooseProjectFromDialog()
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    proj_dir = openFileDialog.SelectedPath;
                    if (ModesMan.mode is ModesMan.Modes.repos && Directory.Exists(proj_dir) && !IsThereRepo())
                    {
                        YesNoDialog_form wantToCreateRepoDialog_form = new YesNoDialog_form(Localization.Configure_WantToCreateRepoDialog, Localization.Yes, Localization.No);
                        wantToCreateRepoDialog_form.ShowDialog();
                        if (wantToCreateRepoDialog_form.DialogResult is DialogResult.Yes) CreateRepo();
                    }
                    ProjectMan.CheckAndSetProj_dir();
                }
                else MessageBox.Show(Localization.SomethingWentWrongProjectDialog);
            }
        }

        static public void CheckAndSetProj_dir()
        {
            if (IsProjValid()) SetRightProj_dir();
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

    }



    internal static class CommitMan
    {
        static public string lastCommitCode;
        static public void Initialize()
        {
            //TODO:
        }

        static public string MakeCommit() { return ""; }//TODO: funkcia bude vracat kod commitu recording manageru, ktory ju bude volat
    }

    static internal class RecordingMan
    {
        public enum RecStates { unknown, started, paused, stoped }
        public enum WorkPhase { creating, programing, debuging }
        static public RecStates? recState { get; private set; }
        static public WorkPhase? workPhase { get; private set; }

        static public void Initialize()
        {

        }
        static public void SetStartedRecState()
        {

        }
        static public void SetPausedRecState()
        {

        }
        static public void SetStopedRecState()
        {

        }

        static private void WriteRecordToCsv()
        {

        }
        static private void WriteRecordToCsv(string commit_id)
        {

        }


    }

}