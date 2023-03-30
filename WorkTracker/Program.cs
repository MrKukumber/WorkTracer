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
    }
    
    /// <summary>
    /// Inicialize parameters of GUI from text file or set default
    /// </summary>
    internal static class Inicializer
    {
        
    }

    internal static class SessionParameterSaver
    {

    }

    internal static class LocalizationManager
    {

    }

    

}