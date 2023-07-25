using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

        static public void CheckAfterActivatingApp()
        {
            ResourceControlMan.CheckAndSetResources();
            CommitMan.CheckAndSetCommitTexts();
        }
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
            try
            {
                using (StreamReader paramFile = new StreamReader("init_params.txt"))
                {
                    while (paramFile.ReadLine() is string line)
                    {
                        string[] line_parts = line.Split(" ");
                        if (line_parts.Length == 1) continue;
                        parameters[line_parts[0]] = string.Join(" ", line_parts.Skip(1).ToArray());
                    }
                }
            }
            catch (System.IO.IOException)
            {
                parameters["lang"] = "en_GB";
                parameters["mode"] = "local";
                parameters["tgit_dir"] = "";
                parameters["last_proj_dir"] = "";
            }
            return parameters;
        }

        static private void Initialize (Dictionary<string,string> init_params)
        {
            ModesMan.Initialize(init_params["mode"]);
            TortoiseGitMan.Initialize(init_params["tgit_dir"]);
            ProjectMan.Initialize(init_params["last_proj_dir"]);
            LocalizationMan.Initialize(init_params["lang"]);
            RecordingMan.Initialize();
            CommitMan.Initialize();
        }

 
    }



    internal static class TortoiseGitMan
    {
        static public string TGit_dir { get => tGit_dir; }
        static private string tGit_dir = "";
        static public void Initialize(string init_tGit_dir)
        {
            tGit_dir = init_tGit_dir;
            CheckAndSetTGit_dir();
        }
        static public void ChooseTGitFromDialog()
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tGit_dir = openFileDialog.SelectedPath;
                    CheckAndSetTGit_dir();
                    if (!ResourceControlMan.LastTGitValidity) MessageBox.Show(Localization.NotValidTGitDirChosen);
                    RecordingMan.AdaptToEnviromentWithOldProj();
                }
                else
                {
                    MessageBox.Show(Localization.SomethingWentWrongTGitDialog);
                }
            }
        }
        static public bool ExistsTG() => File.Exists(tGit_dir + "\\TortoiseGitProc.exe");
        static public void CheckAndSetTGit_dir()
        {
            if (ResourceControlMan.IsTGitValid()) SetRightTGit_dir();
            else SetFalseTGit_dir();
        }
        static private void SetRightTGit_dir() => ModesMan.mode.VisitForSetRightTGit_dir();
        static public void SetRightTGit_dir(ModesMan.LocalMode mode)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Olive);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
        }
        static public void SetRightTGit_dir(ModesMan.ReposMode mode)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Black);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
        }
        static private void SetFalseTGit_dir() => ModesMan.mode.VisitForSetFalseTGit_dir();
        static public void SetFalseTGit_dir(ModesMan.LocalMode mode)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Olive);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(false);
        }
        static public void SetFalseTGit_dir(ModesMan.ReposMode mode)
        {
            Program.configure_form.SetTGitDir_label(tGit_dir);
            Program.configure_form.SetTGitDir_labelColor(Color.Red);
            Program.main_form.SetTortoiseFileNotSelected_labelVisible(true);
        }
        
        static public void WriteTGit_dirTo(StreamWriter file) => file.WriteLine("tgit_dir " + tGit_dir);



    }
    internal static class ProjectMan
    {
        private const string csvRecordFileName = ".workTracer_recordings.csv";
        static public string Proj_dir { get => proj_dir; }
        static private string proj_dir = "";
        static public void Initialize(string init_proj_dir)
        {
            proj_dir = init_proj_dir;
            CheckAndSetProj_dir();
        }
        static public string PathToCSVRecordFile { get => proj_dir + "\\" + csvRecordFileName; }
        static public bool ExistsRecordCSV() => File.Exists(proj_dir + "\\" + csvRecordFileName);
        static public void ChooseProjectFromDialog()
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    proj_dir = openFileDialog.SelectedPath;
                    if (ModesMan.modeI is ModesMan.ModesI.repos && Directory.Exists(proj_dir) && !IsThereRepo())
                    {
                        YesNoDialog_form wantToCreateRepoDialog_form = new YesNoDialog_form(Localization.Configure_WantToCreateRepoDialog, Localization.Yes, Localization.No);
                        wantToCreateRepoDialog_form.ShowDialog();
                        if (wantToCreateRepoDialog_form.DialogResult is DialogResult.Yes) CreateRepo();
                    }
                    CheckAndSetProj_dir();
                    if (!ResourceControlMan.LastProjValidity) MessageBox.Show(Localization.NotValidProjectDirSelected);
                    RecordingMan.AdaptToEnviromentWithNewProj(out bool ableToAccessCSV);
                    CommitMan.CheckAndSetCommitTexts();
                    if(!ableToAccessCSV) MessageBox.Show(Localization.Config_UnableToAccessCSV);
                }
                else MessageBox.Show(Localization.SomethingWentWrongProjectDialog);
            }
        }
        public static bool ExistsProjDir() => Directory.Exists(proj_dir);
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
        public static void WriteProj_dirTo(StreamWriter file) => file.WriteLine("last_proj_dir " + proj_dir);

        static public void CheckAndSetProj_dir()
        {
            if (ResourceControlMan.IsProjValid()) SetRightProj_dir();
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

    public static class ResourceControlMan
    {
        static public bool LastProjValidity { get; private set; }
        static public bool LastTGitValidity { get; private set; }
        static public bool IsProjValid() => ModesMan.mode.VisitForIsProjectValid();
        static public bool IsTGitValid() => ModesMan.mode.VisitForIsTGitValid();
        private static bool messageAlreadyShown = false;
        public static void CheckAndSetResources()
        {
            bool projValidityBeforeCheck = LastProjValidity;
            bool tGitValidityBeforeCheck = LastTGitValidity;
            TortoiseGitMan.CheckAndSetTGit_dir();

            ProjectMan.CheckAndSetProj_dir();

            if (!LastProjValidity && projValidityBeforeCheck)
                if (!LastTGitValidity && tGitValidityBeforeCheck) MessageBox.Show(Localization.NonValidChangeOfProject_dir + "\n" + Localization.NonValidChangeOfTGit_dir);
                else MessageBox.Show(Localization.NonValidChangeOfProject_dir);
            else if (!LastTGitValidity && tGitValidityBeforeCheck) MessageBox.Show(Localization.NonValidChangeOfTGit_dir);

            RecordingMan.AdaptToEnviromentWithNewProj(out bool ableToAccessCSV);

            if (!ableToAccessCSV && !messageAlreadyShown)
            {
                MessageBox.Show(Localization.Config_UnableToAccessCSV);
                messageAlreadyShown = true;
            }
            else
            {
                messageAlreadyShown = false;
            }
        }
        static public bool IsProjValid(ModesMan.LocalMode mode)
        {
            LastProjValidity = ProjectMan.ExistsProjDir();
            return LastProjValidity;
        }
        static public bool IsProjValid(ModesMan.ReposMode mode)
        {
            LastProjValidity = ProjectMan.ExistsProjDir() && ProjectMan.IsThereRepo();
            return LastProjValidity;
        }
        static public bool IsTGitValid(ModesMan.LocalMode mode)
        {
            LastTGitValidity = true;
            return LastTGitValidity;
        }
        static public bool IsTGitValid(ModesMan.ReposMode mode)
        {
            LastTGitValidity = TortoiseGitMan.ExistsTG();
            return LastTGitValidity;
        }


    }


}