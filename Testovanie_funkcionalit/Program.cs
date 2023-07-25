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
            var since = new DateTime(2023, 7, 15);
            var until = new DateTime(2023, 7, 30);
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = $"{directory}";
                p.StartInfo.FileName = "git";
                p.StartInfo.Arguments = $"log --oneline " +
                    $"--since=\"{since.ToString("yyyy-MM-dd")}\" " +
                    $"--until=\"{until.ToString("yyyy-MM-dd")}\" " +
                     "--pretty=format:\"%C(auto)(%cr)%Creset\n\n%B\u0003\"";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                var output = p.StandardOutput.ReadToEnd() + "\n";
                p.WaitForExit();
                var e = p.ExitCode;
                string[] commitTexts = output.Split("\n\u0003\n");
                commitTexts = commitTexts[0..(commitTexts.Length - 1)];
                Array.Reverse(commitTexts);
                return output;
            }
        }
    }

}