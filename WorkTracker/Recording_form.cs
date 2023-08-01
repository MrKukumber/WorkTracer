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
using System.Linq.Expressions;

namespace WorkTracker
{
    public partial class Recording_form : Form
    {
        const int WM_ACTIVATEAPP = 0x1C;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ACTIVATEAPP && Form.ActiveForm == this)
            {
                if (m.WParam != IntPtr.Zero)
                {
                    // the application is getting activated
                    Program.CheckAfterActivatingApp(this);
                }
            }
            base.WndProc(ref m);
        }
        public Recording_form()
        {
            InitializeComponent();
        }
        private void Start_roundButton_Click(object sender, EventArgs e)
        {
            RecordingMan.ProcessNewRecord(RecordingMan.RecStatesI.started);
        }
        private void Stop_roundButton_Click(object sender, EventArgs e)
        {
            ModesMan.VisitMode.VisitForStop_roundButton_Click(sender, e);
        }
        public void Stop_roundButton_Click(ModesMan.VisitLocalMode mode, object sender, EventArgs e)
        {
            RecordingMan.ProcessNewRecord(RecordingMan.RecStatesI.stoped);
        }
        public void Stop_roundButton_Click(ModesMan.VisitReposMode mode, object sender, EventArgs e)
        {
            if (RecordingMan.IsCSVNotLocked())
            {
                Program.commit_form.Show();
                this.Hide();
            }
            else MessageBox.Show(Localization.Recording_UnableToAccessCSV);
        }

        private void Pause_roundButton_Click(object sender, EventArgs e)
        {
            RecordingMan.ProcessNewRecord(RecordingMan.RecStatesI.paused);
        }
        private void Start_roundButton_EnabledChanged(object sender, EventArgs e)
        {
            if (Start_roundButton.Enabled) Start_roundButton.BackgroundImage  = global::WorkTracker.Properties.Resources.play_icon;
            else Start_roundButton.BackgroundImage = null;
        }
        private void Stop_roundButton_EnabledChanged(object sender, EventArgs e)
        {
            if (Stop_roundButton.Enabled) Stop_roundButton.BackgroundImage =  global::WorkTracker.Properties.Resources.stop_icon;
            else Stop_roundButton.BackgroundImage = null;
        }
        private void Pause_roundButton_EnabledChanged(object sender, EventArgs e)
        {
            if (Pause_roundButton.Enabled) Pause_roundButton.BackgroundImage =  global::WorkTracker.Properties.Resources.pause_icon;
            else Pause_roundButton.BackgroundImage = null;
        }
        private void ConfigFormOpening_button_Click(object sender, EventArgs e)
        {
            Program.configure_form.Show();
            Program.configure_form.previousForm = this;
            this.Hide();
        }
        private void Recording_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppExitMan.ExitApp(e);
        }
        private void ReturnToMain_button_Click(object sender, EventArgs e)
        {
            Program.main_form.Show();
            this.Hide();
        }
        private void Phase_trackBar_Scroll(object sender, EventArgs e)
        {
            RecordingMan.ChangeWorkPhase((RecordingMan.WorkPhasesI)Phase_trackBar.Value);
        }
        public void Relable()
        {
            this.Text = Localization.Recording_form_text;
            ConfigFormOpening_button.Text = Localization.Recording_ConfigFormOpening_button_text;
            ReturnToMain_button.Text = Localization.ReturnToMain_button_text;
            Phase_label.Text = Localization.Recording_Faze_label_text;
            PhaseCreat_label.Text = Localization.PhaseCreate_label_text;
            PhaseProgr_label.Text = Localization.PhaseProgr_label_text;
            PhaseDebug_label.Text = Localization.PhaseDebug_label_text;
            CurrTrackState_label.Text = RecordingMan.StatesLocalizations[(int)RecordingMan.recState];
        }
        //functions for accessing forms objects
        public void SetCurrTrackState_label() => CurrTrackState_label.Text = RecordingMan.StatesLocalizations[(int)RecordingMan.recState];
        public void SetStart_roundButtonEnabled(bool indicator) => Start_roundButton.Enabled = indicator;
        public void SetStop_roundButtonEnabled(bool indicator) => Stop_roundButton.Enabled = indicator;
        public void SetPause_roundButtonEnabled(bool indicator) => Pause_roundButton.Enabled = indicator;
        public void SetPhase_trackBarEnabled(bool indicator) => Phase_trackBar.Enabled = indicator;
        public void SetPhase_trackBarValue(int value) => Phase_trackBar.Value = value;

    }
    /// <summary>
    /// class for round shaped buttons
    /// </summary>
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
    /// <summary>
    /// manager of recording, it contains functions for changing recording state and writing records to CSV file
    /// </summary>
    static public class RecordingMan
    {
        public enum RecStatesI { unknown, started, paused, stoped }
        public enum WorkPhasesI { creating, programing, debuging };
        static public RecStatesI recState { get; private set; }
        static public WorkPhasesI workPhase { get; private set; }
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
        // holds last project record
        static public Record? LastRecord { get; private set; }
        /// <summary>
        /// class for record, it is used for writing records into csv file using CSVHelper library
        /// </summary>
        public class Record
        {    
            [Format("yyyy-MM-dd")]
            [Name("date")]
            public DateOnly Date { get; set; }
            [Format("HH:mm:ss")]
            [Name("time")]
            public TimeOnly Time { get; set; }
            [Name("state")]
            public RecStatesI State { get; set; }
            [Name("phase")]
            public WorkPhasesI Phase { get; set; }
            [Name("git")]
            public string? Git { get; set; }
        }
        // configurations for writing into CSV file
        static private readonly CsvConfiguration basicConfig = new(CultureInfo.InvariantCulture) {Delimiter = ",", Comment = '%'};
        static private readonly CsvConfiguration withoutHeaderConfig = new(CultureInfo.InvariantCulture) { Delimiter = ",", Comment = '%', HasHeaderRecord = false };
        static public void Initialize()
        {
            AdaptToEnviromentWithNewProj(out bool ableToAccessCSV);
        }
        /// <summary>
        /// if CSV is accessible, writes new recording to it and changes and sets current state to new one
        /// if it is not accessible, message for user is shown and state is not changed
        /// </summary>
        /// <param name="new_recState"></param>
        static public void ProcessNewRecord(RecStatesI new_recState)
        {
            if (new_recState is not RecStatesI.unknown)
            {
                var newRec = recStates[(int)new_recState].CreateRecord();
                try
                {
                    SaveRecord(newRec);
                    ChangeAndSetRecState(new_recState);
                    LastRecord = newRec;
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show(Localization.Recording_UnableToAccessCSV);
                }
                
            }
        }
        /// <summary>
        /// funcction for testing accessibility of CSV file
        /// </summary>
        /// <returns> true if accessibel</returns>
        static public bool IsCSVNotLocked()
        {
            try
            {
                using (var reader = new StreamReader(ProjectMan.PathToCSVRecordFile))
                {
                    reader.Close();
                }
                return true;
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }
        static public void ChangeWorkPhase(WorkPhasesI new_workPhase)
        {
            workPhase = new_workPhase;
        }
        static private void ChangeAndSetRecState(RecStatesI new_recState)
        {
            recState = new_recState;
            recStates[(int)recState].SetState();
        }
        /// <summary>
        /// adapts recording manager to happend change at which was set new project (directory)
        /// if something is not valid, unknown state is set
        /// reads csv file and sets state and working phase acording to its last recording
        /// if none record is made in project csv file, stoped state is set
        /// </summary>
        /// <param name="ableToAccessCSV"></param>
        static public void AdaptToEnviromentWithNewProj(out bool ableToAccessCSV)
        {
            ableToAccessCSV = true;
            if (ProjectMan.LastProjValidity)
            {
                try
                {
                    LastRecord = GetLastRecordFromCSV();
                    if (TortoiseGitMan.LastTGitValidity)
                    {
                        if (LastRecord is not null)
                        {
                            ChangeAndSetRecState(LastRecord.State);
                            Program.recording_form.SetPhase_trackBarValue((int)LastRecord.Phase);
                        }
                        else
                        {
                            ChangeAndSetRecState(RecStatesI.stoped);
                            Program.recording_form.SetPhase_trackBarValue((int)WorkPhasesI.creating);
                        }
                        return;
                    }
                }
                catch (System.IO.IOException)
                {
                    ableToAccessCSV = false;
                    ChangeAndSetRecState(RecStatesI.unknown);
                    Program.recording_form.SetPhase_trackBarValue((int)WorkPhasesI.creating);
                    return;
                }
            }
            ChangeAndSetRecState(RecStatesI.unknown);
            Program.recording_form.SetPhase_trackBarValue((int)WorkPhasesI.creating);
        }
        /// <summary>
        /// adapts recording manager to happend change at which was not changed project (directory)
        /// if something is not valid, unknown state is set
        /// if none record is made in project, stoped state is set
        /// </summary>
        static public void AdaptToEnviromentWithOldProj()
        {
            if (ProjectMan.LastProjValidity && TortoiseGitMan.LastTGitValidity)
            {
                if (LastRecord is not null)
                {
                    ChangeAndSetRecState(LastRecord.State);
                    Program.recording_form.SetPhase_trackBarValue((int)LastRecord.Phase);
                }
                else
                {
                    ChangeAndSetRecState(RecStatesI.stoped);
                    Program.recording_form.SetPhase_trackBarValue((int)WorkPhasesI.creating);
                }
            }
            else
            {
                ChangeAndSetRecState(RecStatesI.unknown);
                Program.recording_form.SetPhase_trackBarValue((int)WorkPhasesI.creating);
            }
        }

        /// <summary>
        /// function for saving record into CSV file
        /// if csv doesnt stil exists in project (there was made no record in project yet) writes header into file too
        /// </summary>
        /// <param name="record"></param>
        static private void SaveRecord(Record record)
        {
            if (ProjectMan.ExistsRecordCSV())
            {
                AppendRecordToCsv(record);
            }
            else
            {
                WriteRecordToCsv(record);
            }
        }
        /// <summary>
        /// if csv fiel does not exists, returns null, like there was no record to read
        /// </summary>
        /// <returns></returns>
        static private Record? GetLastRecordFromCSV()
        {   
            if(!ProjectMan.ExistsRecordCSV()) return null;
            return ReadLastRecordFromCsv();
        }
        /// <summary>
        /// using CSVHelper library writes header and record into csv 
        /// </summary>
        /// <param name="record"></param>
        static private void WriteRecordToCsv(Record record)
        {
            using (var writer = new StreamWriter(ProjectMan.PathToCSVRecordFile))
            using (var csv = new CsvWriter(writer, basicConfig))
            {
                csv.WriteHeader<Record>();
                csv.NextRecord();
                csv.WriteRecord<Record>(record);
                csv.NextRecord();
            }
        }
        /// <summary>
        /// using CSVHelper library appends record into csv 
        /// true in StreamWriter constructor stands for "append" mode
        /// </summary>
        /// <param name="record"></param>
        static private void AppendRecordToCsv(Record record)
        {
            using (var writer = new StreamWriter(ProjectMan.PathToCSVRecordFile, true))
            using (var csv = new CsvWriter(writer, withoutHeaderConfig))
            {
                csv.WriteRecord(record);
                csv.NextRecord();
            }
        }
        /// <summary>
        /// reads last record from csv by reading all records of csv
        /// </summary>
        /// <returns>last record</returns>
        static private Record? ReadLastRecordFromCsv()
        {
            Record? lastRecord = null;
            using (var reader = new StreamReader(ProjectMan.PathToCSVRecordFile))
            using (var csv = new CsvReader(reader, basicConfig))
            {
                while (csv.Read())
                {
                    lastRecord = csv.GetRecord<Record>();
                }
            }
            return lastRecord;
        }
        
        // functions called by visitingModes in modes manager
        // they call function from StopedRecState class field according to currently set mode
        static public Record CreateStopRecord(ModesMan.VisitLocalMode mode) => new StopedRecState().CreateRecord(mode);
        static public Record CreateStopRecord(ModesMan.VisitReposMode mode) => new StopedRecState().CreateRecord(mode);

        /// <summary>
        /// class representing recording state
        /// </summary>
        private abstract class RecState
        {
            /// <summary>
            /// sets enviroment according to state of recording
            /// </summary>
            public abstract void SetState();
            /// <summary>
            /// </summary>
            /// <returns>instance of Record tailored given state of recording</returns>
            public abstract Record CreateRecord();
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
                Program.recording_form.SetPhase_trackBarEnabled(false);
            }
            public override Record CreateRecord() => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStatesI.started,
                Phase = workPhase,
            };
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
                Program.recording_form.SetPhase_trackBarEnabled(false);
            }
            public override Record CreateRecord() => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStatesI.paused,
                Phase = workPhase,
            };
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
                Program.recording_form.SetPhase_trackBarEnabled(true);
            }
            public override Record CreateRecord()
            {
                return ModesMan.VisitMode.VisitForCreateRecord();
            }
            public Record CreateRecord(ModesMan.VisitLocalMode mode) => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStatesI.stoped,
                Phase = workPhase,
            };
            // if repo mode is active and commit was made, code of commit is saved to the record too
            public Record CreateRecord(ModesMan.VisitReposMode mode) => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStatesI.stoped,
                Phase = workPhase,
                Git = CommitMan.hasBeenCommitted ? CommitMan.lastCommitCode : null
            };
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
                Program.recording_form.SetPhase_trackBarEnabled(false);
            }
            public override Record CreateRecord() => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStatesI.unknown,
                Phase = workPhase,
            };
        }
        /// <summary>
        /// classes inherited from this interace, serve for other managers as instances 
        ///     which they can visit for checking current application recording state and use appropriate version of theirs function
        /// theese classes ensure functionality of all methods dependent of current recording state  
        /// 
        /// currently not used in any way, can be removed from code
        /// </summary>
        public interface IVisitRecState
        {

        }
        public class VisitStartedRecState : IVisitRecState
        {

        }
        public class VisitPausedRecState : IVisitRecState
        {

        }
        public class VisitStopedRecState : IVisitRecState
        {

        }
        public class VisitUnknownRecState : IVisitRecState
        {

        }

        /// <summary>
        /// classes which represent work phases
        /// currently not used in any way, can be removed from code
        /// </summary>
        public abstract class WorkPhase
        {

        }
        public class CreatingWorkPhase : WorkPhase
        {

        }
        public class ProgramingWorkPhase : WorkPhase
        {

        }
        public class DebugingWorkPhase : WorkPhase
        {

        }

        /// <summary>
        /// classes inherited from this interace, serve for other managers as instances 
        ///     which they can visit for checking current application work phase and use appropriate version of theirs function
        /// theese classes ensure functionality of all methods dependent of current work phase  
        /// 
        /// currently not used in any way, can be removed from code
        /// </summary>
        public interface IVisitWorkPhase
        {

        }
        public class VisitCreatingWorkPhase : IVisitWorkPhase
        {

        }
        public class VisitProgramingWorkPhase : IVisitWorkPhase
        {

        }
        public class VisitDebugingWorkPhase : IVisitWorkPhase
        {

        }
    }


}