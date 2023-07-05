using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;

namespace WorkTracker
{

    public partial class Recording_form : Form
    {
        public Recording_form()
        {
            InitializeComponent();
            Console.WriteLine();
        }

        private void Play_roundButton_Click(object sender, EventArgs e)
        {

        }

        private void Stop_roundButton_Click(object sender, EventArgs e)
        {
            Program.commit_form.Show();
            this.Hide();

        }

        private void ConfigFormOpening_button_Click(object sender, EventArgs e)
        {
            Program.configure_form.Show();
            Program.configure_form.previousForm = this;
            this.Hide();

        }

        private void Recording_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ReturnToMain_button_Click(object sender, EventArgs e)
        {
            Program.main_form.Show();
            this.Hide();
        }
        public void Relabel()
        {
            ConfigFormOpening_button.Text = Localization.Recording_ConfigFormOpening_button_text;
            ReturnToMain_button.Text = Localization.ReturnToMain_button_text;
            Phase_label.Text = Localization.Recording_Faze_label_text;
            PhaseCreat_label.Text = Localization.PhaseCreate_label_text;
            PhaseProgr_label.Text = Localization.PhaseProgr_label_text;
            PhaseDebug_label.Text = Localization.PhaseDebug_label_text;

            switch (RecordingMan.recState)
            {
                case RecordingMan.RecStates.started:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_play_text;
                    break;
                case RecordingMan.RecStates.paused:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_pause_text;
                    break;
                case RecordingMan.RecStates.stoped:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_stop_text;
                    break;
                default:
                    CurrTrackState_label.Text = Localization.CurrTrackState_label_none_text;
                    break;
            }
        }
    }

    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(2, 2, ClientSize.Width - 5, ClientSize.Height - 5);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }

    static internal class RecordingMan 
    {
        public enum RecStates { started, paused, stoped }
        public enum WorkPhase { creating, programing, debuging }
        static public RecStates? recState { get; private set; }
        static public WorkPhase? workPhase { get; private set; }

        //TODO: dodelat funkcionality
    }

}
