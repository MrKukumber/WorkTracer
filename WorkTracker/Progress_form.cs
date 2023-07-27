using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;
using static WorkTracker.CommitMan;

namespace WorkTracker
{
    public partial class Progress_form : Form
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
        public Progress_form()
        {
            InitializeComponent();
        }

        private void MainFormOpening_button_Click(object sender, EventArgs e)
        {
            Program.main_form.Show();
            this.Hide();
        }

        private void Progress_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppExitMan.ExitApp(e);
        }
        private void Commit_vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            CommitMan.ChangeCommitInProgressRichTextBox(Commit_vScrollBar.Value);
        }
        private void Since_dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            if (SameDate_checkBox.Checked) Until_dateTimePicker.Value = Since_dateTimePicker.Value;
            else Until_dateTimePicker.MinDate = Since_dateTimePicker.Value;
            CommitMan.CheckAndSetCommitInProgress();
            ProgressShowingMan.SetAndShowProgression(out bool ableToAccessCSV);
            if (!ableToAccessCSV) MessageBox.Show(Localization.Progress_UnableToAccessCSV);
        }

        private void Until_dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            if (SameDate_checkBox.Checked) Since_dateTimePicker.Value = Until_dateTimePicker.Value;
            else Since_dateTimePicker.MaxDate = Until_dateTimePicker.Value;
            CommitMan.CheckAndSetCommitInProgress();
            ProgressShowingMan.SetAndShowProgression(out bool ableToAccessCSV);
            if(!ableToAccessCSV) MessageBox.Show(Localization.Progress_UnableToAccessCSV);
        }
        private void SameDate_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SameDate_checkBox.Checked)
            {
                Since_dateTimePicker.MaxDate = Until_dateTimePicker.MaxDate;
                Until_dateTimePicker.MinDate = Since_dateTimePicker.MinDate;
                Until_dateTimePicker.Value = Since_dateTimePicker.Value;
                Until_dateTimePicker.Enabled = false;
            }
            else
            {
                Since_dateTimePicker.MaxDate = Until_dateTimePicker.Value;
                Until_dateTimePicker.MinDate = Since_dateTimePicker.Value;
                Until_dateTimePicker.Enabled = true;
            }
            CommitMan.CheckAndSetCommitInProgress();
            ProgressShowingMan.SetAndShowProgression(out bool ableToAccessCSV);
        }

        public void Relabel()
        {
            this.Text = Localization.Progress_form_text;
            Since_label.Text = Localization.Progress_From_label_text;
            Until_label.Text = Localization.Progress_To_label_text;
            RecordSinceText_label.Text = Localization.Progress_RecordSinceText_label_text;
            RecordUntilText_label.Text = Localization.Progress_RecordUntilText_label_text;
            SameDate_label.Text = Localization.Progress_SameDate_label_text;
            //TODO: doplnit vysledne hodnoty doby prace 
            RecordingTime_label.Text = Localization.Progress_RecordingTime_label_text;
            RecordingTimeWithPause_label.Text = Localization.Progress_RecordingTimeWithPause_label_text;
            CreatTime_label.Text = Localization.Progress_CreatTime_label_text;
            ProgrTime_label.Text = Localization.Progress_ProgrTime_label_text;
            DebugTime_label.Text = Localization.Progress_DebugTime_label_text;
            CompDurationText_label.Text = Localization.Progress_CompDurationText_label_text;
            CompDurationWithPauseText_label.Text = Localization.Progress_CompDurationWithStopText_label_text;
            ReturnToMain_button.Text = Localization.ReturnToMain_button_text;
            RangeCommit_label.Text = Localization.Progress_RangeCommit_label_text;
            if (ModesMan.modeI is ModesMan.ModesI.local) Commit_richTextBox.Text = Localization.Progress_Commit_richTextBox_local_mode_text;

        }
        public void SetCompDuration_labelText(string text) => CompDuration_label.Text = text;
        public void SetCompDurationWithPause_labelText(string text) => CompDurationWithPause_label.Text = text;
        public void SetCreatDuration_labelText(string text) => CreatDuration_label.Text = text;
        public void SetCreatDurationWithPause_labelText(string text) => CreatDurationWithPause_label.Text = text;
        public void SetProgrDuration_labelText(string text) => ProgrDuration_label.Text = text;
        public void SetProgrDurationWithPause_labelText(string text) => ProgrDurationWithPause_label.Text = text;
        public void SetDebugDuration_labelText(string text) => DebugDuration_label.Text = text;
        public void SetDebugDurationWithPause_labelText(string text) => DebugDurationWithPause_label.Text = text;

        public void WriteToCommit_richTextBox(string what) => Commit_richTextBox.Text = what;
        public int GetCommit_richTextBoxWidth() => Commit_richTextBox.Width;
        public void EnableCommit_vScrollBar(bool indicator) => Commit_vScrollBar.Enabled = indicator;
        public DateTime GetFullSince_dateTimePickerDate() => Since_dateTimePicker.Value.Date.Add(new TimeSpan(0, 0, 0));
        public DateTime GetFullUntil_dateTimePickerDate() => Until_dateTimePicker.Value.Date.Add(new TimeSpan(23, 59, 59));
        public void SetUntil_dateTimePickerMinDate(DateTime minDate) => Until_dateTimePicker.MinDate = minDate;
        public void SetSince_dateTimePickerMaxDate(DateTime maxDate) => Since_dateTimePicker.MaxDate = maxDate;
        public void SetSince_dateTimePickerValue(DateTime value) => Since_dateTimePicker.Value = value;
        public void SetUntil_dateTimePickerValue(DateTime value) => Until_dateTimePicker.Value = value;
        public void SetRecordSinceDate_labelText(string date) => RecordSinceDate_label.Text = date;
        public void SetRecordUntilDate_labelText(string date) => RecordUntilDate_label.Text = date;
        public void SetCommit_vScrollBarMaximum(int maximum) => Commit_vScrollBar.Maximum = maximum;
        public int Commit_vScrollValue { get => Commit_vScrollBar.Value; set => Commit_vScrollBar.Value = value; }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    static public class ProgressShowingMan
    {
        static private ComputedValue[] computedValues;
        static public void Initialize()
        {
            computedValues = new ComputedValue[]
            {
                new CompleteComputedValue(),
                new CompleteWithPauseComputedValue(),
                new CreatingComputedValue(),
                new CreatingWithStopsComputedValue(),
                new ProgramingComputedValue(),
                new ProgramingWithStopsComputedValue(),
                new DebugingComputedValue(),
                new DebugingWithStopsComputedValue(),
            };
            //TODO:
            CheckAndSetDateTimePickersInProgress(true, out _);
        }
        static private CsvConfiguration basicConfig = new(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Comment = '%',
        };

        static public void CheckAndSetDateTimePickersInProgress(bool ResetValues, out bool ableToAccessCSV)
        {
            ableToAccessCSV = true;
            if (ResourceControlMan.LastProjValidity && ProjectMan.ExistsRecordCSV())
            {
                try
                {
                    (RecordingMan.Record? first, RecordingMan.Record? last) = ReadFirstAndLastRecordFromCsv();
                    if (first is not null && last is not null)
                    {
                        DateTime firstDateTime = first.Date.ToDateTime(first.Time);
                        DateTime lastDateTime = last.Date.ToDateTime(last.Time);
                        if (ResetValues || Program.progress_form.GetFullSince_dateTimePickerDate() < firstDateTime)
                        {
                            Program.progress_form.SetSince_dateTimePickerMaxDate(lastDateTime);
                            Program.progress_form.SetSince_dateTimePickerValue(firstDateTime);
                            Program.progress_form.SetRecordSinceDate_labelText(firstDateTime.ToString("dd.MM.yyyy"));
                        }
                        if (ResetValues || Program.progress_form.GetFullUntil_dateTimePickerDate() > lastDateTime)
                        {
                            Program.progress_form.SetUntil_dateTimePickerMinDate(firstDateTime);
                            Program.progress_form.SetUntil_dateTimePickerValue(lastDateTime);
                            Program.progress_form.SetRecordUntilDate_labelText(lastDateTime.ToString("dd.MM.yyyy"));

                        }
                    }
                }
                catch (System.IO.IOException)
                {
                    ableToAccessCSV = false;
                }
            }
        }

        static public void SetAndShowProgression(out bool ableToAccessCSV)
        {
            DateTime since = Program.progress_form.GetFullSince_dateTimePickerDate();
            DateTime until = Program.progress_form.GetFullUntil_dateTimePickerDate();
            ComputeProgressFromCsvInRange(since, until, out ableToAccessCSV);
            ShowComputedValuesInProgress();
        }
        static public void ShowComputedValuesInProgress()
        {
            Program.progress_form.SetCompDuration_labelText(computedValues[0].CompleteTime.ToString());
            Program.progress_form.SetCompDurationWithPause_labelText(computedValues[1].CompleteTime.ToString());
            Program.progress_form.SetCreatDuration_labelText(computedValues[2].CompleteTime.ToString());
            Program.progress_form.SetCreatDurationWithPause_labelText(computedValues[3].CompleteTime.ToString());
            Program.progress_form.SetProgrDuration_labelText(computedValues[4].CompleteTime.ToString());
            Program.progress_form.SetProgrDurationWithPause_labelText(computedValues[5].CompleteTime.ToString());
            Program.progress_form.SetDebugDuration_labelText(computedValues[6].CompleteTime.ToString());
            Program.progress_form.SetDebugDurationWithPause_labelText(computedValues[7].CompleteTime.ToString());
        }

        static public void ComputeProgressFromCsvInRange(DateTime since, DateTime until, out bool ableToAccessCSV)
        {
            ableToAccessCSV = true;
            ResetComputedValues();
            try
            {
                if (ResourceControlMan.LastProjValidity && ProjectMan.ExistsRecordCSV())
                {
                    using (var reader = new StreamReader(ProjectMan.PathToCSVRecordFile))
                    using (var csv = new CsvReader(reader, basicConfig))
                    {
                        while (TryProcessNextRecordInRangeFrom(csv, since, until)) ;
                    }
                }
            }
            catch (System.IO.IOException)
            {
                ableToAccessCSV = false;
            }
        }

        private static void ResetComputedValues()
        {
            foreach (var cv in computedValues) cv.Reset();
        }

        private static bool TryProcessNextRecordInRangeFrom(CsvReader csv, DateTime since, DateTime until)
        {
            if (TryReadRecord(csv, out RecordingMan.Record? record) &&
                record is not null &&
                record.Date.ToDateTime(record.Time) is var recordDateTime &&
                recordDateTime <= until)
            {
                if (recordDateTime >= since)
                    foreach (var cv in computedValues) cv.ProcessRecord(record.State, record.Phase, recordDateTime);
                return true;
            }
            return false;
        }

        private static bool TryReadRecord(CsvReader csv, out RecordingMan.Record? record)
        {
            if (csv.Read())
            {
                record = csv.GetRecord<RecordingMan.Record>();
            }
            else
            {
                record = null;
            }
            return (record is not null);
        }
        static private (RecordingMan.Record?, RecordingMan.Record?) ReadFirstAndLastRecordFromCsv()
        {
            RecordingMan.Record? lastRecord = null;
            RecordingMan.Record? firstRecord = null;
            using (var reader = new StreamReader(ProjectMan.PathToCSVRecordFile))
            using (var csv = new CsvReader(reader, basicConfig))
            {
                csv.Read();
                firstRecord = csv.GetRecord<RecordingMan.Record>();
                lastRecord = firstRecord;
                while (csv.Read())
                {
                    lastRecord = csv.GetRecord<RecordingMan.Record>();
                }
            }
            return (firstRecord, lastRecord);
        }

        private abstract class ComputedValue
        {
            public ComputedValue()
            {
                Reset();
            }
            public TimeSpan CompleteTime { get; protected set; }
            public DateTime? InitialClosure { get; protected set; }

            public virtual void Reset()
            {
                CompleteTime = new TimeSpan(0, 0, 0, 0);
                InitialClosure = null;
            }
            public abstract void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime);

            protected enum Processes { nothing, initialize, add }
            protected Processes[,,] whatTodo =
            {   /*Unknown state*/
                { /*  Complete,             CompleteWithPauses,   Creating,             CreatingWithPauses,   Programing,           ProgramingWithPauses, Debuging,             DebugingWithPauses  */
 /*creat phase*/    { Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing    },
 /*progr phase*/    { Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing    },
 /*debug phase*/    { Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing    }
                },
                /*Started state*/
                { /*  Complete,             CompleteWithPauses,   Creating,             CreatingWithPauses,   Programing,           ProgramingWithPauses, Debuging,             DebugingWithPauses  */
 /*creat phase*/    { Processes.initialize, Processes.initialize, Processes.initialize, Processes.initialize, Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing    },
 /*progr phase*/    { Processes.initialize, Processes.initialize, Processes.nothing,    Processes.nothing,    Processes.initialize, Processes.initialize, Processes.nothing,    Processes.nothing    },
 /*debug phase*/    { Processes.initialize, Processes.initialize, Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.initialize, Processes.initialize },
                },
                /*Pused state*/
                { /*  Complete,             CompleteWithPauses,   Creating,             CreatingWithPauses,   Programing,           ProgramingWithPauses, Debuging,             DebugingWithPauses  */
 /*creat phase*/    { Processes.add,        Processes.nothing,    Processes.add,        Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing    },
 /*progr phase*/    { Processes.add,        Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.add,        Processes.nothing,    Processes.nothing,    Processes.nothing    },
 /*debug phase*/    { Processes.add,        Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.add,        Processes.nothing    }
                },
                /*Stoped state*/
                { /*  Complete,             CompleteWithPauses,   Creating,             CreatingWithPauses,   Programing,           ProgramingWithPauses, Debuging,             DebugingWithPauses  */
 /*creat phase*/    { Processes.add,        Processes.add,        Processes.add,        Processes.add,        Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing    },
 /*progr phase*/    { Processes.add,        Processes.add,        Processes.nothing,    Processes.nothing,    Processes.add,        Processes.add,        Processes.nothing,    Processes.nothing    },
 /*debug phase*/    { Processes.add,        Processes.add,        Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.nothing,    Processes.add,        Processes.add        }
                }

            };
            protected void Process(Processes process, DateTime datetime)
            {
                switch (process)
                {
                    case Processes.initialize:
                        InitialClosure = datetime;
                        break;
                    case Processes.add:
                        if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
                        InitialClosure = null;
                        break;
                    default:
                        break;
                }
            }
        }

        private class CompleteComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 0], datetime);
            }

        }
        private class CompleteWithPauseComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 1], datetime);
            }
        }
        private class CreatingComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 2], datetime);
            }
        }
        private class CreatingWithStopsComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 3], datetime);
            }
        }
        private class ProgramingComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 4], datetime);
            }
        }
        private class ProgramingWithStopsComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 5], datetime);
            }
        }
        private class DebugingComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 6], datetime);
            }
        }
        private class DebugingWithStopsComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 7], datetime);
            }
        }

    }




}
























//public virtual void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitUnknownRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitUnknownRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitUnknownRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }








//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) 
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}

//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;

//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}





//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}

//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}





//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}





//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
