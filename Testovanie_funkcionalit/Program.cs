using System.Diagnostics;

namespace Testovanie_funkcionalit
{
    internal static partial class Program
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

            Application.Run(form1);
        }
        public static Form1 form1 = new Form1();
        public static Form2 form2 = new Form2();
        public static int activationCount = 0;
        public static int OpenTGitCommitForm(string directory)
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = "C:\\Program Files\\TortoiseGit\\bin";
                p.StartInfo.FileName = "TortoiseGitProc.exe";
                p.StartInfo.Arguments =$"/command:commit /path:\"{directory}\"";
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();

                var output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                Console.WriteLine(p.ExitCode);
                return p.ExitCode;
            }
        }

        public static string IsHereRepo(string directory)
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = $"{directory}";
                p.StartInfo.FileName = "git";
                p.StartInfo.Arguments = $"log -1 --pretty=%B";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                var output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                var e = p.ExitCode;
                return output;
            }
        }
    }

}