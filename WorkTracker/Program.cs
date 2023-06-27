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
            Application.Run(main_form);
            Initializer.Execute();
        }

        public static Main_form main_form = new Main_form();
        public static Configure_form configure_form = new Configure_form();
        public static Recording_form recording_form = new Recording_form();
        public static Progress_form progress_form = new Progress_form();
    }
    
    /// <summary>
    /// Inicialize parameters of GUI from text file or set default
    /// </summary>
    internal static class Initializer
    {
        static public void Execute()
        {
            Dictionary<string,string> init_params = InitParamsParser();
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
    }

    internal static class SessionParameterSaver
    {

    }

    internal static class LocalizationManager
    {
        static private string current_lang;
        static public void Initialize(string current_lang)
        {

        }
    }

    internal static class CommitManager
    {
        static private string lastCommitCode;
        static public void Initialize(string lastCommitCode)
        {
            CommitManager.lastCommitCode = lastCommitCode;
        }
    }

    

}