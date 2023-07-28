using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkTracker.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace WorkTracker
{
    public partial class Commit_form : Form
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
        public Commit_form()
        {
            InitializeComponent();
        }
        
        // if user decides not to commit, dialog is shown to him, asking if he is sure
        private void NoCommit_button_Click(object sender, EventArgs e)
        {
            YesNoDialog_form areYouSureNotCommiting_Form = new YesNoDialog_form( Localization.Commit_NotCommiting_YesNoDialog_label_text, Localization.Yes, Localization.No);
            areYouSureNotCommiting_Form.ShowDialog();
            if (areYouSureNotCommiting_Form.DialogResult is DialogResult.Yes)
            {
                Program.recording_form.Show();
                this.Hide();
                CommitMan.hasBeenCommitted = false;
                RecordingMan.ProcessNewRecord(RecordingMan.RecStatesI.stoped);
            }
        }
        // if user says yes, tortoise git is called so he could make commit
        // if commit is successfull or user decides during run of tortoise git, that he does not wnat to commit, commit will be hidden and recording form shown
        // if user does not commmit, dialog will be shown, asking if he is sure to not commit
        private void YesCommit_button_Click(object sender, EventArgs e)
        {
            if (CommitMan.TryCallTGitAndMakeCommit())
            {
                BackFromCommitToRecordingForm();
            }
            else
            {
                YesNoDialog_form noCommitMade_Form = new YesNoDialog_form(Localization.Commit_NoCommitMade_YesNoDialog_text, Localization.Yes, Localization.No);
                noCommitMade_Form.ShowDialog();
                if (noCommitMade_Form.DialogResult is DialogResult.Yes)
                {
                    BackFromCommitToRecordingForm();
                }
            }
        }
        // function for returning to recording form from commit one
        // sets commit forms and lets process stoped recording state
        private void BackFromCommitToRecordingForm()
        {
            Program.recording_form.Show();
            this.Hide();
            CommitMan.CheckAndSetCommit_richTextBoxes(0);
            RecordingMan.ProcessNewRecord(RecordingMan.RecStatesI.stoped);
        }
        // if user close the commit form, same procedure as when he does not wnat to commit is made
        private void Commit_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //depends, if it is closed by user or system
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                YesNoDialog_form areYouSureNotCommiting_Form = new YesNoDialog_form(Localization.Commit_NotCommiting_YesNoDialog_label_text, Localization.Yes, Localization.No);
                areYouSureNotCommiting_Form.ShowDialog();
                if (areYouSureNotCommiting_Form.DialogResult is DialogResult.Yes)
                {
                    Program.recording_form.Show();
                    this.Hide();
                    CommitMan.hasBeenCommitted = false;
                    RecordingMan.ProcessNewRecord(RecordingMan.RecStatesI.stoped);
                }
            }
            else System.Windows.Forms.Application.Exit();
        }
        public void Relabel()
        {
            this.Text = Localization.Commit_form_text;
            WantCommit_label.Text = Localization.Commit_WantCommit_label_text;
            YesCommit_button.Text = Localization.Yes;
            NoCommit_button.Text = Localization.No;
        }

    }
    public static class CommitMan
    {
        static public string? lastCommitCode;
        static public bool hasBeenCommitted;

        static public void Initialize()
        {
            CheckAndSetCommit_richTextBoxes(0);
            hasBeenCommitted = false;
        }

        /// <summary>
        /// calls tortoise git
        /// if somethings goes wrong when running it, message is shown and lets user to make adjusments that it would be possible
        /// if code of last commit is not changed after exiting tortoise git, it means, that new commit was not made
        /// </summary>
        /// <returns>false, when user should be given oportunity to try to make commit again</returns>
        static public bool TryCallTGitAndMakeCommit()
        {
            string? previousCommitCode = lastCommitCode;
            try
            {
                RunTortoiseGitCommitCommand();
            }
            catch (Win32Exception)
            {
                MessageBox.Show(Localization.RunTortoiseGitFailure);
                hasBeenCommitted = false;
                return false;
            }
            TryGetLastCommitCode(out string newCommitCode);
            if (previousCommitCode != newCommitCode)
            {
                hasBeenCommitted = true;
                return true;
            }
            else
            {
                hasBeenCommitted = false;
                return false;
            }
        }

        static public void ChangeCommitInProgressRichTextBox(int index) => CommitPresenter.ShowCommitInProgress(index);

        /// <summary>
        /// function that sets commits in rich text boxes in both main and progress forms
        /// index of showed commit in progress form is preserved
        /// </summary>
        static public void CheckAndSetCommit_richTextBoxes()
        {
            CheckAndSetCommitInMain();
            CheckAndSetCommitInProgress();
        }
        /// <summary>
        /// function that sets commits in rich text boxes in both main and progress forms
        /// index of showed commit in progress form is set to commitIndexInProgress
        /// </summary>
        static public void CheckAndSetCommit_richTextBoxes(int commitIndexInProgress)
        {
            CheckAndSetCommitInMain();
            CheckAndSetCommitInProgress(commitIndexInProgress);
        }
        /// <summary>
        /// sets commit rich text box in progress form, preserving showed commit index in process
        /// taking current mode of project into concideration
        /// </summary>
        static public void CheckAndSetCommitInProgress()
        {
            ModesMan.VisitMode.VisitForCheckAndSetCommitInProgress(null);
        }
        /// <summary>
        /// sets commit rich text box in progress form, setting shown commit accroding to commitIndex
        /// taking current mode of project into concideration
        /// </summary>
        static public void CheckAndSetCommitInProgress(int commitIndex)
        {
            ModesMan.VisitMode.VisitForCheckAndSetCommitInProgress(commitIndex);
        }
        static public void CheckAndSetCommitInProgress(ModesMan.VisitReposMode mode, int? commitIndex)
        {
            if (commitIndex is not null) Program.progress_form.Commit_vScrollValue = (int)commitIndex;
            if (ResourceControlMan.LastProjValidity) // if project is not valid, respective message is shown in commit rich text box
            {
                CommitPresenter.GetCommitsAndShowCurrentOneInProgress();
            }
            else
            {
                Program.progress_form.WriteToCommit_richTextBox(Localization.Progress_InvalidProjectSelectedCommit_richTextBox);
                Program.progress_form.EnableCommit_vScrollBar(false);
            }
        }
        // when local mod is active, respective message is shown in commit rich text box
        static public void CheckAndSetCommitInProgress(ModesMan.VisitLocalMode mode, int? commitIndex)
        {
            Program.progress_form.WriteToCommit_richTextBox(Localization.Progress_Commit_richTextBox_local_mode_text);
            Program.progress_form.EnableCommit_vScrollBar(false);
        }
        /// <summary>
        /// sets commit rich text box in main form, showing last commit of project
        /// </summary>
        static public void CheckAndSetCommitInMain()
        {
            ModesMan.VisitMode.VisitForCheckAndSetCommitInMain();
        }

        static public void CheckAndSetCommitInMain(ModesMan.VisitReposMode mode)
        {
            if (ResourceControlMan.LastProjValidity)// if project is not valid, respective message is shown in commit rich text box
            {
                CommitPresenter.GetAndShowLastCommitInMain();
            }
            else
            {
                Program.main_form.WriteToCommit_richTextBox(Localization.Main_InvalidProjectSelectedCommit_richTextBox);
            }
        }
        // when local mod is active, respective message is shown in commit rich text box
        static public void CheckAndSetCommitInMain(ModesMan.VisitLocalMode mode)
        {
            Program.main_form.WriteToCommit_richTextBox(Localization.Main_Commit_richTextBox_local_mode_text);
        }
        /// <summary>
        /// function, that gets last commit code
        /// </summary>
        /// <param name="commitCode"></param>
        /// <returns>false if there is no code to retrieve, or cant be from othere reasons retrieved</returns>
        static private bool TryGetLastCommitCode(out string commitCode)
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = ProjectMan.Proj_dir;
                p.StartInfo.FileName = "git";
                p.StartInfo.Arguments = "log --format=\"%H\" -n 1";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
                commitCode = p.StandardOutput.ReadToEnd();
                return p.ExitCode is 0 ? true : false;
            }
        }
        static public void RunTortoiseGitCommitCommand()
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = TortoiseGitMan.TGit_dir;
                p.StartInfo.FileName = "TortoiseGitProc.exe";
                p.StartInfo.Arguments = $"/command:commit /path:\"{ProjectMan.Proj_dir}\"";
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                p.WaitForExit();
            }
        }
        /// <summary>
        /// inner class, that process showing of commits in rich text boxes
        /// </summary>
        static private class CommitPresenter
        {
            const int richTextBoxCharSize = 8;
            const int richTextBoxBias = 6;
            static private string[] commitsFromRangeInProgress = { };
            /// <summary>
            /// gets commits in range of date time pickers in progress form, saves them and shows the one according to value of scroll bar in progress form
            /// if no commit is found, respective message is writen into commit rich text box
            /// </summary>
            static public void GetCommitsAndShowCurrentOneInProgress()
            {
                if (TryGetAndSetCommitsFromRangeInProgress())
                {
                    Program.progress_form.SetCommit_vScrollBarMaximum(commitsFromRangeInProgress.Length - 1);
                    Program.progress_form.EnableCommit_vScrollBar(true);
                    ShowCommitInProgress(Program.progress_form.Commit_vScrollValue);
                }
                else
                {
                    Program.progress_form.EnableCommit_vScrollBar(false);
                    Program.progress_form.WriteToCommit_richTextBox(Localization.Progress_NoCommitFound_richTextBox_text);
                }
            }
            /// <summary>
            /// gets range of dates and returns commits in that range
            /// </summary>
            static public bool TryGetAndSetCommitsFromRangeInProgress()
            {
                DateTime since = Program.progress_form.GetFullSince_dateTimePickerDate();
                DateTime until = Program.progress_form.GetFullUntil_dateTimePickerDate();
                return TryGetCommitTextsFromRange(since, until, out commitsFromRangeInProgress);
            }
            /// <summary>
            /// if given index is greater than count of acquired commits, last commit is shown instead
            /// </summary>
            /// <param name="index">index of commit to be shown</param>
            static public void ShowCommitInProgress(int index)
            {
                if (index < commitsFromRangeInProgress.Length) JustifyAndShowCommitIn(Program.progress_form, commitsFromRangeInProgress[index]);
                else JustifyAndShowCommitIn(Program.progress_form, commitsFromRangeInProgress[commitsFromRangeInProgress.Length - 1]);
            }
            /// <summary>
            /// gets last commit and show it in the main form
            /// if no commit is found, writes respective message to commit rich text box
            /// </summary>
            static public void GetAndShowLastCommitInMain()
            {
                if (TryGetLastCommitText(out string commit))
                {
                    JustifyAndShowCommitIn(Program.main_form, commit);
                }
                else
                {
                    Program.main_form.WriteToCommit_richTextBox(Localization.Main_NoCommitMade_richTextBox_text);
                }

            }
            /// <summary>
            /// takes commit text, justifies it and shows it in the main form
            /// </summary>
            static private void JustifyAndShowCommitIn(Main_form main_form, string commit)
            {
                int richTextBoxCharCount = (main_form.GetCommit_richTextBoxWidth() - richTextBoxBias) / richTextBoxCharSize;
                string justifiedCommit = TextJustification.Justify(false, richTextBoxCharCount, commit);
                main_form.WriteToCommit_richTextBox(justifiedCommit);
            }
            /// <summary>
            /// takes commit text, justifies it and show it in the progress form
            /// </summary>
            static private void JustifyAndShowCommitIn(Progress_form progress_form, string commit)
            {
                int richTextBoxCharCount = (progress_form.GetCommit_richTextBoxWidth() - richTextBoxBias) / richTextBoxCharSize;
                string justifiedCommit = TextJustification.Justify(false, richTextBoxCharCount, commit);
                progress_form.WriteToCommit_richTextBox(justifiedCommit);
            }
            /// <summary>
            /// gets last commit text
            /// </summary>
            /// <param name="commitText"></param>
            /// <returns>false, if there is none commit or can't be from other reasons retrieved </returns>
            static private bool TryGetLastCommitText(out string commitText)
            {
                using (Process p = new Process())
                {
                    p.StartInfo.WorkingDirectory = ProjectMan.Proj_dir;
                    p.StartInfo.FileName = "git";
                    p.StartInfo.Arguments = "log -1 --pretty=%B";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    commitText = p.StandardOutput.ReadToEnd();
                    return p.ExitCode is 0 ? true : false;
                }
            }
            /// <summary>
            /// reads and parse commits from given range
            /// </summary>
            /// <param name="since">start of range</param>
            /// <param name="until">end of range</param> 
            /// <param name="commitTexts">variable in which commits will be returned</param>
            /// <returns>false, if no commits where retrieved</returns>
            static private bool TryGetCommitTextsFromRange(DateTime since, DateTime until, out string[] commitTexts)
            {
                using (Process p = new Process())
                {
                    p.StartInfo.WorkingDirectory = ProjectMan.Proj_dir;
                    p.StartInfo.FileName = "git";
                    p.StartInfo.Arguments = "log --oneline " +
                        $"--since=\"{since.ToString("yyyy-MM-dd HH:mm")}\" " +
                        $"--until=\"{until.ToString("yyyy-MM-dd HH:mm")}\" " +
                         "--pretty=format:\"%C(auto)(%cr)%Creset\n\n%B\u0003\""; //added unwritable symbol \u0003 enabling spliting of returned commits
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();

                    string commits = p.StandardOutput.ReadToEnd() + "\n";
                    commitTexts = commits.Split("\n\u0003\n");
                    if (commitTexts.Length > 0) commitTexts = commitTexts[0..(commitTexts.Length - 1)]; //last memeber of array is blank string
                    return commitTexts.Length > 0 ? true : false;
                }
            }
        }
    }
}

