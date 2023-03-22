using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Application.Run(new Main_form());
        }

        public static Main_form main_form = new Main_form();
        public static Configure_form configure_form = new Configure_form();
        public static Recording_form recording_form = new Recording_form();
        public static Commit_form commit_form = new Commit_form();
        public static CreateRepo_form createRepo_form = new CreateRepo_form();
        public static AreYouSure_form areYouSureExitWithoutStop_form = new AreYouSureExitWithoutStop_form();
        public static AreYouSure_form areYouSureNotToCommit_form = new AreYouSureNotCommiting_form();
        public static Form previousForm = new Form();

    }
}