using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using Microsoft.VisualBasic;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WorkTracker
{

    public partial class Recording_form : Form
    {
        public Recording_form()
        {
            InitializeComponent();
        }
        private void Start_roundButton_Click(object sender, EventArgs e)
        {
            RecordingMan.ProcessNewRecord(RecordingMan.RecStates.started);
        }

        private void Stop_roundButton_Click(object sender, EventArgs e)
        {
            if (ModesMan.modeI == ModesMan.ModesI.repos)
            {
                Program.commit_form.Show();
                this.Hide();
                //TODO: dorobit commitovanie
            }
            else RecordingMan.ProcessNewRecord(RecordingMan.RecStates.stoped);
        }

        private void Pause_roundButton_Click(object sender, EventArgs e)
        {
            RecordingMan.ProcessNewRecord(RecordingMan.RecStates.paused);
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
            //DONE
            this.Text = Localization.Recording_form_text;
            ConfigFormOpening_button.Text = Localization.Recording_ConfigFormOpening_button_text;
            ReturnToMain_button.Text = Localization.ReturnToMain_button_text;
            Phase_label.Text = Localization.Recording_Faze_label_text;
            PhaseCreat_label.Text = Localization.PhaseCreate_label_text;
            PhaseProgr_label.Text = Localization.PhaseProgr_label_text;
            PhaseDebug_label.Text = Localization.PhaseDebug_label_text;
            CurrTrackState_label.Text = RecordingMan.StatesLocalizations[(int)RecordingMan.recState];
        }

        public void SetCurrTrackState_label() => CurrTrackState_label.Text = RecordingMan.StatesLocalizations[(int)RecordingMan.recState];
        public void SetStart_roundButtonEnabled(bool indicator) => Start_roundButton.Enabled = indicator;
        public void SetStop_roundButtonEnabled(bool indicator) => Stop_roundButton.Enabled = indicator;
        public void SetPause_roundButtonEnabled(bool indicator) => Pause_roundButton.Enabled = indicator;

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
        private const string csvRecordFileName = ".workTracer_recordings.csv";
        public enum RecStates { unknown, started, paused, stoped }
        public enum WorkPhase { creating, programing, debuging }
        static public RecStates recState { get; private set; }
        static public WorkPhase workPhase { get; private set; }
        static public string[] StatesLocalizations
        {
            get => new string[4]{
            Localization.CurrTrackState_label_none_text,
            Localization.CurrTrackState_label_start_text,
            Localization.CurrTrackState_label_pause_text,
            Localization.CurrTrackState_label_stop_text };
        }
        static private RecState[] recStates = 
        { 
            new UnknownRecState(), 
            new StartedRecState(), 
            new PausedRecState(), 
            new StopedRecState() 
        };

        static public Record? LastRecord { get; private set; }
        public class Record
        {    
            [Format("yyyy-MM-dd")]
            [Name("date")]
            public DateOnly Date { get; set; }
            [Format("HH:mm:ss")]
            [Name("time")]
            public TimeOnly Time { get; set; }
            [Name("state")]
            public RecStates State { get; set; }
            [Name("phase")]
            public WorkPhase Phase { get; set; }
            [Name("git")]
            public string? Git { get; set; }
        }
        static private CsvConfiguration basicConfig = new(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Comment = '%',
        };
        static private CsvConfiguration withoutHeaderConfig = new(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Comment = '%',
            HasHeaderRecord = false
        };
        static public void Initialize()
        {
            AdaptToEnviroment(true);
        }

        static public void ProcessNewRecord(RecStates new_recState)
        {
            ChangeAndSetRecState(new_recState);
            if (recState is not RecStates.unknown)
            {
                var newRec = recStates[(int)recState].CreateRecord();
                LastRecord = newRec;
                SaveLastRecord();
            }
        }
        static private void ChangeAndSetRecState(RecStates new_recState)
        {
            recState = new_recState;
            recStates[(int)recState].SetState();
        }
        static public void AdaptToEnviroment(bool isNewProj) => ModesMan.mode.VisitForAdaptToEnviroment(isNewProj);
        static public void AdaptToEnviroment(ModesMan.LocalMode mode, bool isNewProj)
        {
            if (ProjectMan.LastProjValidity)
            {
                if (isNewProj)
                {
                    LastRecord = GetLastRecordFromCSV();
                    if (LastRecord != null) ChangeAndSetRecState(LastRecord.State);
                    else ChangeAndSetRecState(RecStates.stoped);
                }
            }
            else {
                LastRecord = null;
                ChangeAndSetRecState(RecStates.unknown);
            }
        }
        static public void AdaptToEnviroment(ModesMan.ReposMode mode, bool isNewProj)
        {
            if (ProjectMan.LastProjValidity && TortoiseGitMan.LastTGitValidity)
            {
                if (isNewProj)
                {
                    LastRecord = GetLastRecordFromCSV();
                    if (LastRecord != null) ChangeAndSetRecState(LastRecord.State);
                    else ChangeAndSetRecState(RecStates.stoped);
                }
            }
            else
            {
                LastRecord = null;
                ChangeAndSetRecState(RecStates.unknown);
            }
        }
        static private void SaveLastRecord()
        {
            if (ExistsRecordCSV(ProjectMan.Proj_dir))
            {
                AppendLastRecordToCsv(ProjectMan.Proj_dir);
            }
            else
            {
                WriteLastRecordToCsv(ProjectMan.Proj_dir);
            }
        }

        static private Record? GetLastRecordFromCSV()
        {   
            if(!ExistsRecordCSV(ProjectMan.Proj_dir)) return null;
            return ReadLastRecordFromCsv(ProjectMan.Proj_dir);
        }
        static private bool ExistsRecordCSV(string proj_dir) => File.Exists(proj_dir + "\\" + csvRecordFileName);
        static private void WriteLastRecordToCsv(string proj_dir)
        {
            using (var writer = new StreamWriter(proj_dir + "\\" + csvRecordFileName))
            using (var csv = new CsvWriter(writer, basicConfig))
            {
                csv.WriteHeader<Record>();
                csv.NextRecord();
                csv.WriteRecord<Record>(LastRecord);
                csv.NextRecord();

            }
        }
        static private void AppendLastRecordToCsv(string proj_dir)
        {
            using (var stream = File.Open(proj_dir + "\\" + csvRecordFileName, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, withoutHeaderConfig))
            {
                csv.WriteRecord(LastRecord);
                csv.NextRecord();
            }
        }
        static private Record? ReadLastRecordFromCsv(string proj_dir)
        {
            Record? lastRecord = null;
            using (var reader = new StreamReader(proj_dir + "\\" + csvRecordFileName))
            using (var csv = new CsvReader(reader, basicConfig))
            {
                csv.Read();
                var record = csv.GetRecord<Record>();
                while (csv.Read())
                {
                    lastRecord = csv.GetRecord<Record>();
                }
            }
            return lastRecord;

        }

        private abstract class RecState
        {
            public abstract void SetState();
            public virtual Record CreateRecord() => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = recState,
                Phase = workPhase,
            };
        }

        private class StartedRecState : RecState
        {
            public override void SetState()
            {
                Program.main_form.SetCurrTrackState_label();
                Program.recording_form.SetCurrTrackState_label();
                Program.recording_form.SetStart_roundButtonEnabled(false);
                Program.recording_form.SetPause_roundButtonEnabled(true);
                Program.recording_form.SetStop_roundButtonEnabled(true);
            }
        }
        private class PausedRecState : RecState
        {
            public override void SetState()
            {
                Program.main_form.SetCurrTrackState_label();
                Program.recording_form.SetCurrTrackState_label();
                Program.recording_form.SetStart_roundButtonEnabled(true);
                Program.recording_form.SetPause_roundButtonEnabled(false);
                Program.recording_form.SetStop_roundButtonEnabled(true);

            }
        }
        private class StopedRecState : RecState
        {
            public override void SetState()
            {
                Program.main_form.SetCurrTrackState_label();
                Program.recording_form.SetCurrTrackState_label();
                Program.recording_form.SetStart_roundButtonEnabled(true);
                Program.recording_form.SetPause_roundButtonEnabled(false);
                Program.recording_form.SetStop_roundButtonEnabled(false);
            }
            public override Record CreateRecord()
            {
                //TODO: vytvaram az po tom, co commitnem alebo necommitnem, treba dorobit
                throw new NotImplementedException();
            }
        }
        private class UnknownRecState : RecState
        {
            public override void SetState()
            {
                Program.main_form.SetCurrTrackState_label();
                Program.recording_form.SetCurrTrackState_label();
                Program.recording_form.SetStart_roundButtonEnabled(false);
                Program.recording_form.SetPause_roundButtonEnabled(false);
                Program.recording_form.SetStop_roundButtonEnabled(false);
            }
        }



    }
}
