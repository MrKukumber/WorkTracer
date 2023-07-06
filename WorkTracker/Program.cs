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
            CommitMan.Initialize();
        }
    }

    internal static class TortoiseGitMan
    {
        static private string tGit_dir = "";
        static public bool IsTGitValid() => ExistsTG();
        static public void Initialize(string init_tGit_dir)
        {
            tGit_dir = init_tGit_dir;
        }

        static private bool ExistsTG() => File.Exists(tGit_dir + "TortoiseGitProc.exe");
        
        //static public void CheckAndProcessTGExistence()
        //{
        //    if (ExistsTG())
        //    {

        //    }
        //}


    }
    internal static class ProjectMan
    {
        static private string proj_dir = "";

        static public void Initialize(string init_proj_dir)
        {
            proj_dir = init_proj_dir;
            //TODO: ak je zapnuty repo, skontrolovat ci sa tam nachadza repo...            
        }
        static public bool IsProjValid()
        {
            if (ModesMan.mode is ModesMan.Modes.local) return File.Exists(proj_dir);
            else return File.Exists(proj_dir) && IsThereRepo();
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

                return output == "true";
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
    }

}