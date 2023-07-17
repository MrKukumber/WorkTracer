﻿using System;
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
            ModesMan.mode.VisitForStop_roundButton_Click(sender, e);
        }
        public void Stop_roundButton_Click(ModesMan.LocalMode mode, object sender, EventArgs e)
        {
            RecordingMan.ProcessNewRecord(RecordingMan.RecStates.stoped);
        }
        public void Stop_roundButton_Click(ModesMan.ReposMode mode, object sender, EventArgs e)
        {
            if (RecordingMan.IsCSVNotLocked())
            {
                Program.commit_form.Show();
                this.Hide();
                RecordingMan.ProcessNewRecord(RecordingMan.RecStates.stoped);
            }
            else MessageBox.Show(Localization.Recording_UnableToAccessCSV);
        }

        private void Pause_roundButton_Click(object sender, EventArgs e)
        {
            RecordingMan.ProcessNewRecord(RecordingMan.RecStates.paused);
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
        public void SetPhase_trackBarEnabled(bool indicator) => Phase_trackBar.Enabled = indicator;
        public void SetPhase_trackBarValue(int value) => Phase_trackBar.Value = value;

        private void Phase_trackBar_Scroll(object sender, EventArgs e)
        {
            RecordingMan.ChangeWorkPhase((RecordingMan.WorkPhase)Phase_trackBar.Value);
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
    static public class RecordingMan
    {
        private const string csvRecordFileName = ".workTracer_recordings.csv";
        public enum RecStates { unknown =  0, started, paused, stoped }
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
            if (new_recState is not RecStates.unknown)
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
        static public bool IsCSVNotLocked()
        {
            try
            {
                using (var reader = new StreamReader(ProjectMan.Proj_dir + "\\" + csvRecordFileName))
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
        static public void ChangeWorkPhase(WorkPhase new_workPhase)
        {
            workPhase = new_workPhase;
        }
        static private void ChangeAndSetRecState(RecStates new_recState)
        {
            recState = new_recState;
            recStates[(int)recState].SetState();
        }
        static public void AdaptToEnviroment(bool isNewProj)
        {
            if (isNewProj)
                if (ProjectMan.LastProjValidity)
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
                                ChangeAndSetRecState(RecStates.stoped);
                                Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
                            }
                            return;
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show(Localization.Config_UnableToAccessCSV);
                        ChangeAndSetRecState(RecStates.unknown);
                        Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
                        return;
                    }
            else
                if (ProjectMan.LastProjValidity && TortoiseGitMan.LastTGitValidity)
                {
                    if (LastRecord is not null)
                    {
                        ChangeAndSetRecState(LastRecord.State);
                        Program.recording_form.SetPhase_trackBarValue((int)LastRecord.Phase);
                    }
                    else
                    {
                        ChangeAndSetRecState(RecStates.stoped);
                        Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
                    }
                    return;
                }
            ChangeAndSetRecState(RecStates.unknown);
            Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
        }
        static private void SaveRecord(Record record)
        {
            if (ExistsRecordCSV(ProjectMan.Proj_dir))
            {
                AppendRecordToCsv(ProjectMan.Proj_dir, record);
            }
            else
            {
                WriteRecordToCsv(ProjectMan.Proj_dir, record);
            }
        }

        static private Record? GetLastRecordFromCSV()
        {   
            if(!ExistsRecordCSV(ProjectMan.Proj_dir)) return null;
            return ReadLastRecordFromCsv(ProjectMan.Proj_dir);
        }
        static private bool ExistsRecordCSV(string proj_dir) => File.Exists(proj_dir + "\\" + csvRecordFileName);
        static private void WriteRecordToCsv(string proj_dir, Record record)
        {
            using (var writer = new StreamWriter(proj_dir + "\\" + csvRecordFileName))
            using (var csv = new CsvWriter(writer, basicConfig))
            {
                csv.WriteHeader<Record>();
                csv.NextRecord();
                csv.WriteRecord<Record>(record);
                csv.NextRecord();
            }
        }
        static private void AppendRecordToCsv(string proj_dir, Record record)
        {
            using (var stream = File.Open(proj_dir + "\\" + csvRecordFileName, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, withoutHeaderConfig))
            {
                csv.WriteRecord(record);
                csv.NextRecord();
            }
        }
        static private Record? ReadLastRecordFromCsv(string proj_dir)
        {
            Record? lastRecord = null;
            using (var reader = new StreamReader(proj_dir + "\\" + csvRecordFileName))
            using (var csv = new CsvReader(reader, basicConfig))
            {
                while (csv.Read())
                {
                    lastRecord = csv.GetRecord<Record>();
                }
            }
            return lastRecord;

        }

        public abstract class RecState
        {
            public abstract void SetState();
            public abstract Record CreateRecord();
        }

        public class StartedRecState : RecState
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
                State = RecStates.started,
                Phase = workPhase,
            };
        }
        public class PausedRecState : RecState
        {
            public override void SetState()
            {
                Program.main_form.SetCurrTrackState_label();
                Program.recording_form.SetCurrTrackState_label();
                Program.recording_form.SetStart_roundButtonEnabled(true);
                Program.recording_form.SetPause_roundButtonEnabled(false);
                Program.recording_form.SetStop_roundButtonEnabled(true);
                Program.recording_form.SetPhase_trackBarEnabled(true);
            }
            public override Record CreateRecord() => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStates.paused,
                Phase = workPhase,
            };
        }
        public class StopedRecState : RecState
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
                return ModesMan.mode.VisitForCreateRecord(this);
            }
            public Record CreateRecord(ModesMan.LocalMode mode) => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStates.stoped,
                Phase = workPhase,
            };
            public Record CreateRecord(ModesMan.ReposMode mode) => new Record
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                State = RecStates.stoped,
                Phase = workPhase,
                Git = CommitMan.lastCommitCode
            };
        }
        public class UnknownRecState : RecState
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
                State = RecStates.unknown,
                Phase = workPhase,
            };
        }
    }
    internal static class CommitMan
    {
        static public string lastCommitCode;
        static public void Initialize()
        {
            //TODO:
        }

        static public string MakeCommit() { return ""; }//TODO: funkcia bude vracat kod commitu recording manageru, ktory ju bude volat
    }
}















//if (ProjectMan.LastProjValidity && TortoiseGitMan.LastTGitValidity)
//{
//    if (isNewProj)
//    {
//        try
//        {
//            LastRecord = GetLastRecordFromCSV();
//            if (LastRecord != null)
//            {
//                ChangeAndSetRecState(LastRecord.State);
//                Program.recording_form.SetPhase_trackBarValue((int)LastRecord.Phase);
//            }
//            else
//            {
//                ChangeAndSetRecState(RecStates.stoped);
//                Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
//            }
//        }
//        catch (System.IO.IOException)
//        {
//            MessageBox.Show(Localization.Config_UnableToAccessCSV);
//            ChangeAndSetRecState(RecStates.unknown);
//            Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
//        }
//    }
//    else
//    {
//        if (LastRecord != null)
//        {
//            ChangeAndSetRecState(LastRecord.State);
//            Program.recording_form.SetPhase_trackBarValue((int)LastRecord.Phase);
//        }
//        else
//        {
//            ChangeAndSetRecState(RecStates.stoped);
//            Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
//        }
//    }
//}
//else
//{
//    ChangeAndSetRecState(RecStates.unknown);
//    Program.recording_form.SetPhase_trackBarValue((int)RecordingMan.WorkPhase.creating);
//}
