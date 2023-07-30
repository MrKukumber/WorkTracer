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
        // when user choose new starting or ending date, all result values and commit rich text box are adjusted to new range
        private void Since_dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            if (SameDate_checkBox.Checked) Until_dateTimePicker.Value = Since_dateTimePicker.Value;
            else Until_dateTimePicker.MinDate = Since_dateTimePicker.Value;
            CommitMan.CheckAndSetCommitInProgress();
            ProgressMan.SetAndShowProgression(out bool ableToAccessCSV);
            if (!ableToAccessCSV) MessageBox.Show(Localization.Progress_UnableToAccessCSV);
        }

        private void Until_dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            if (SameDate_checkBox.Checked) Since_dateTimePicker.Value = Until_dateTimePicker.Value;
            else Since_dateTimePicker.MaxDate = Until_dateTimePicker.Value;
            CommitMan.CheckAndSetCommitInProgress();
            ProgressMan.SetAndShowProgression(out bool ableToAccessCSV);
            if(!ableToAccessCSV) MessageBox.Show(Localization.Progress_UnableToAccessCSV);
        }
        // when this check box is checked, the date time pickers are functioning as one
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
            ProgressMan.SetAndShowProgression(out bool ableToAccessCSV);
        }

        public void Relable()
        {
            this.Text = Localization.Progress_form_text;
            Since_label.Text = Localization.Progress_From_label_text;
            Until_label.Text = Localization.Progress_To_label_text;
            RecordSinceText_label.Text = Localization.Progress_RecordSinceText_label_text;
            RecordUntilText_label.Text = Localization.Progress_RecordUntilText_label_text;
            SameDate_label.Text = Localization.Progress_SameDate_label_text;
            RecordingTime_label.Text = Localization.Progress_RecordingTime_label_text;
            RecordingTimeWithPause_label.Text = Localization.Progress_RecordingTimeWithPause_label_text;
            CreatTime_label.Text = Localization.Progress_CreatTime_label_text;
            ProgrTime_label.Text = Localization.Progress_ProgrTime_label_text;
            DebugTime_label.Text = Localization.Progress_DebugTime_label_text;
            CompDurationText_label.Text = Localization.Progress_CompDurationText_label_text;
            CompDurationWithPauseText_label.Text = Localization.Progress_CompDurationWithStopText_label_text;
            ReturnToMain_button.Text = Localization.ReturnToMain_button_text;
            RangeCommit_label.Text = Localization.Progress_RangeCommit_label_text;
            if (ModesMan.ModeI is ModesMan.ModesI.local) Commit_richTextBox.Text = Localization.Progress_Commit_richTextBox_local_mode_text;

        }
        //functions for accessing forms objects
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
    }
    /// <summary>
    /// computes resulting progress values and displays them
    /// </summary>
    static public class ProgressMan
    {
        //all values that are computed, initialized in Initialize function
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
            CheckAndSetDateTimePickersInProgress(true, out _);
        }

        // configurationfor writing into CSV file
        static private CsvConfiguration basicConfig = new(CultureInfo.InvariantCulture) { Delimiter = ",", Comment = '%' };

        /// <summary>
        /// sets datetime pickers in progress form
        /// if current datetimes are out of project recordings bounds, their are adjusted to theese bounds
        /// </summary>
        /// <param name="ResetValues">if it is set, datetimes are adjusted to bounderies of range of project recording</param>
        /// <param name="ableToAccessCSV">false, if csv wasnt accessible</param>
        static public void CheckAndSetDateTimePickersInProgress(bool ResetValues, out bool ableToAccessCSV)
        {
            ableToAccessCSV = true;
            if (ProjectMan.LastProjValidity && ProjectMan.ExistsRecordCSV())
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
        /// <summary>
        /// gets bounderies of range given by dateTime pickers, computes resulting values of work progression and shows them in progression form
        /// </summary>
        /// <param name="ableToAccessCSV"></param>
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
        /// <summary>
        /// at first resets the computed values, 
        /// then opens up csv file and computes progress in range record by record
        /// when last record or last record within boundaries is read, computation is stoped
        /// </summary>
        /// <param name="since"></param>
        /// <param name="until"></param>
        /// <param name="ableToAccessCSV"></param>
        static public void ComputeProgressFromCsvInRange(DateTime since, DateTime until, out bool ableToAccessCSV)
        {
            ableToAccessCSV = true;
            ResetComputedValues();
            try
            {
                if (ProjectMan.LastProjValidity && ProjectMan.ExistsRecordCSV())
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
        /// <summary>
        /// processing of next record from file
        /// if record is out of boundaries, it is not accounted to the result
        /// </summary>
        /// <param name="csv"></param>
        /// <param name="since"></param>
        /// <param name="until"></param>
        /// <returns>false, when last valid record was read</returns>
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
        /// <summary>
        /// reads record from csv file
        /// </summary>
        /// <param name="csv"></param>
        /// <param name="record"></param>
        /// <returns>false, when no other record can be read</returns>
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
        /// <summary>
        /// gets and returns first and last record of csv record file
        /// </summary>
        /// <returns>first and last record of csv, if there are some </returns>
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

        /// <summary>
        /// inner abbstract class, that holds computed values
        /// its successors compute results based on whtaTodo "grid"
        /// </summary>
        private abstract class ComputedValue
        {
            public ComputedValue()
            {
                Reset();
            }
            // resulting computed time
            public TimeSpan CompleteTime { get; protected set; }
            //initial date and time of segment of result
            //after finding ending record, the diference of this record date and time and of InitialClosure is added to Complete Time
            public DateTime? InitialClosure { get; protected set; }

            public virtual void Reset()
            {
                CompleteTime = new TimeSpan(0, 0, 0, 0);
                InitialClosure = null;
            }
            /// <summary>
            /// every succesor overrides this method and call Process function with appropriate argument to the computed valued
            /// </summary>
            /// <param name="recStateI">recording state of record</param>
            /// <param name="workPhaseI">work phase of record</param>
            /// <param name="datetime">date and time of the record</param>
            public abstract void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime);

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
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 0], datetime);
            }

        }
        private class CompleteWithPauseComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 1], datetime);
            }
        }
        private class CreatingComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 2], datetime);
            }
        }
        private class CreatingWithStopsComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 3], datetime);
            }
        }
        private class ProgramingComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 4], datetime);
            }
        }
        private class ProgramingWithStopsComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 5], datetime);
            }
        }
        private class DebugingComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 6], datetime);
            }
        }
        private class DebugingWithStopsComputedValue : ComputedValue
        {
            public override void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime)
            {
                Process(whatTodo[(int)recStateI, (int)workPhaseI, 7], datetime);
            }
        }
    }
}