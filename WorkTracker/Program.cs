using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Data;
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
            ModesMan.Initialize(init_params["mode"]);
            LocalizationMan.Initialize(init_params["lang"]);
            TortoiseGitMan.Initialize(init_params["tgit_dir"]);
            ProjectMan.Initialize(init_params["last_proj_dir"]);
            CommitMan.Initialize();
        }
    }

    internal static class TortoiseGitMan
    {
        static private string tgit_dir = "";
        static public void Initialize(string init_tgit_dir)
        {
            tgit_dir = init_tgit_dir;
            //TODO: ak je zapnuty repo mod, skontrolovat, ci sa tam vazne tgit nachadza
        }
    }
    internal static class ProjectMan
    {
        static private string proj_dir = "";
        static public void Initialize(string init_proj_dir)
        {
            proj_dir = init_proj_dir;
            //TODO: ak je zapnuty repo, skontrolovat ci sa tam nachadza repo...            
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