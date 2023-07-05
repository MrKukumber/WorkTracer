using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;
using static WorkTracker.LocalizationMan;

namespace WorkTracker
{
    public partial class Configure_form : Form
    {
        public Form previousForm = new Form();
        public Configure_form()
        {
            InitializeComponent();
        }


        private void ChooseMode_trackBar_Scroll(object sender, EventArgs e)
        {

        }

        private void Configure_form_Load(object sender, EventArgs e)
        {

        }

        private void Configure_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void BackToPrevForm_button_Click(object sender, EventArgs e)
        {
            previousForm.Show();
            this.Hide();
        }

        private void ProjectSelection_button_Click(object sender, EventArgs e)
        {

        }

        private void Localization_trackBar_Scroll(object sender, EventArgs e)
        {
            LocalizationMan.ChangeLocalization(Localization_trackBar.Value);
        }

        public void Relabel()
        {
            ChooseTGitDir_button.Text = Localization.Select;
            ProjectSelection_button.Text = Localization.Select;
            ChooseProjDir_label.Text = Localization.Config_ChooseProjDir_label_text;
            ChooseLanguge_label.Text = Localization.Config_ChooseLanguge_label_text;
            ChooseTGitDir_label.Text = Localization.Config_ChooseTGitDir_label_text;
            Mode1_label.Text = Localization.Config_Mode1_label_text;
            Mode2_label.Text = Localization.Config_Mode2_label_text;
            BackToPrevForm_button.Text = Localization.Config_BackToPrevForm_button_text;
        }

        public void SetLocalization_trackBar(int value)
        {
            Localization_trackBar.Value = value;
        }
    }

    internal static class LocalizationMan
    {
        public enum Langs { en_GB = 0, sk = 1 };
        static public Langs lang { get; private set; }
        static public void Initialize(string init_lang)
        {
            switch (init_lang)
            {
                case "sk":
                    lang = Langs.sk;
                    break;
                default:
                    lang = Langs.en_GB;
                    break;
            }
            Program.configure_form.SetLocalization_trackBar((int)lang);
            Relabel();

        }
        static public void ChangeLocalization(int new_lang)
        {
            lang = (Langs)new_lang;
            switch (lang)
            {
                case Langs.en_GB:
                    Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
                    break;
                case Langs.sk:
                    Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("sk-SK");
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                    break;

            }
            Relabel();
        }
        static public void Relabel()
        {
            Program.configure_form.Relabel();
            Program.main_form.Relabel();
            Program.recording_form.Relabel();
            Program.commit_form.Relabel();
            Program.progress_form.Relabel();
        }
    }

    internal static class ModesMan
    {
        public enum Modes { repos, local };

        static public Modes mode { get; private set; }

        //TODO: dodelat managera modov
        static public void Initialize(string init_mode)
        {
            switch (init_mode)
            {
                case "repos":
                    mode = Modes.repos;
                    break;
                default:
                    mode = Modes.local;
                    break;
            }
            //TODO:
        }
    }
}
