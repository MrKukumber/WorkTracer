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
                    Program.CheckAfterActivatingApp();
                }
            }
            base.WndProc(ref m);
        }
        public Commit_form()
        {
            InitializeComponent();
        }

        private void NoCommit_button_Click(object sender, EventArgs e)
        {
            YesNoDialog_form areYouSureNotCommiting_Form = new YesNoDialog_form( Localization.Commit_NotCommiting_YesNoDialog_label_text, Localization.Yes, Localization.No);
            areYouSureNotCommiting_Form.ShowDialog();
            if (areYouSureNotCommiting_Form.DialogResult is DialogResult.Yes)
            {
                Program.recording_form.Show();
                this.Hide();
                CommitMan.hasBeenCommited = false;
                RecordingMan.ProcessNewRecord(RecordingMan.RecStates.stoped);
            }
        }

        private void YesCommit_button_Click(object sender, EventArgs e)
        {
            if (CommitMan.TryCallTGitAndMakeCommit())
            {
                Program.recording_form.Show();
                this.Hide();
                CommitMan.CheckAndSetCommit_richTextBoxes(0);
                RecordingMan.ProcessNewRecord(RecordingMan.RecStates.stoped);
            }
        }

        private void Commit_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                YesNoDialog_form areYouSureNotCommiting_Form = new YesNoDialog_form(Localization.Commit_NotCommiting_YesNoDialog_label_text, Localization.Yes, Localization.No);
                areYouSureNotCommiting_Form.ShowDialog();
                if (areYouSureNotCommiting_Form.DialogResult is DialogResult.Yes)
                {
                    Program.recording_form.Show();
                    this.Hide();
                    CommitMan.hasBeenCommited = false;
                    RecordingMan.ProcessNewRecord(RecordingMan.RecStates.stoped);
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
        static public bool hasBeenCommited;
        static public void Initialize()
        {
            CheckAndSetCommit_richTextBoxes(0);
            hasBeenCommited = false;
        }

        //return false, when user should be given oportunity to try to make commit again
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
                hasBeenCommited = false;
                return false;
            }
            TryGetLastCommitCode(out string newCommitCode);
            if (previousCommitCode != newCommitCode)
            {
                hasBeenCommited = true;
                return true;
            }
            else
            {
                hasBeenCommited = false;
                YesNoDialog_form noCommitMade_Form = new YesNoDialog_form(Localization.Commit_NoCommitMade_YesNoDialog_text, Localization.Yes, Localization.No);
                noCommitMade_Form.ShowDialog();
                if (noCommitMade_Form.DialogResult is DialogResult.Yes)
                {
                    return true;
                }
                return false;
            }
        }

        static public void ChangeCommitInProgressRichTextBox(int index) => CommitPresenter.ShowCommitInProgress(index);

        static public void CheckAndSetCommit_richTextBoxes()
        {
            CheckAndSetCommitInMain();
            CheckAndSetCommitInProgress();
        }
        static public void CheckAndSetCommit_richTextBoxes(int commitIndexInProgress)
        {
            CheckAndSetCommitInMain();
            CheckAndSetCommitInProgress(commitIndexInProgress);
        }
        static public void CheckAndSetCommitInProgress()
        {
            ModesMan.mode.VisitForCheckAndSetCommitInProgress(null);
        }

        static public void CheckAndSetCommitInProgress(int commitIndex)
        {
            ModesMan.mode.VisitForCheckAndSetCommitInProgress(commitIndex);
        }

        static public void CheckAndSetCommitInProgress(ModesMan.ReposMode mode, int? commitIndex)
        {
            if (commitIndex is not null) Program.progress_form.Commit_vScrollValue = (int)commitIndex;
            if (ResourceControlMan.LastProjValidity)
            {
                CommitPresenter.GetCommitsAndShowCurrentOneInProgress();
            }
            else
            {
                Program.progress_form.WriteToCommit_richTextBox(Localization.Progress_InvalidProjectSelectedCommit_richTextBox);
                Program.progress_form.EnableCommit_vScrollBar(false);
            }
        }
        static public void CheckAndSetCommitInProgress(ModesMan.LocalMode mode, int? commitIndex)
        {
            Program.progress_form.WriteToCommit_richTextBox(Localization.Progress_Commit_richTextBox_local_mode_text);
            Program.progress_form.EnableCommit_vScrollBar(false);
        }
        static public void CheckAndSetCommitInMain()
        {
            ModesMan.mode.VisitForCheckAndSetCommitInMain();
        }

        static public void CheckAndSetCommitInMain(ModesMan.ReposMode mode)
        {
            if (ResourceControlMan.LastProjValidity)
            {
                CommitPresenter.GetAndShowLastCommitInMain();
            }
            else
            {
                Program.main_form.WriteToCommit_richTextBox(Localization.Main_InvalidProjectSelectedCommit_richTextBox);
            }
        }
        static public void CheckAndSetCommitInMain(ModesMan.LocalMode mode)
        {
            Program.main_form.WriteToCommit_richTextBox(Localization.Main_Commit_richTextBox_local_mode_text);

        }

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
        static private class CommitPresenter
        {
            static private string[] commitsFromRangeInProgress = { };
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
            static public bool TryGetAndSetCommitsFromRangeInProgress()
            {
                DateTime since = Program.progress_form.GetSince_dateTimePickerDate();
                DateTime until = Program.progress_form.GetUntil_dateTimePickerDate();
                since = since.Date.Add(new TimeSpan(0, 0, 0));
                until = until.Date.Add(new TimeSpan(23, 59, 59));
                return TryGetCommitTextsFromRange(since, until, out commitsFromRangeInProgress);
            }

            static public void ShowCommitInProgress(int index)
            {
                if (index < commitsFromRangeInProgress.Length) JustifyAndShowCommitIn(Program.progress_form, commitsFromRangeInProgress[index]);
                else JustifyAndShowCommitIn(Program.progress_form, commitsFromRangeInProgress[commitsFromRangeInProgress.Length - 1]);
            }

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
            static private void JustifyAndShowCommitIn(Main_form main_form, string commit)
            {
                int richTextBoxCharCount = (main_form.GetCommit_richTextBoxWidth() - 6) / 8;
                string justifiedCommit = TextJustification.Justify(false, richTextBoxCharCount, commit);
                main_form.WriteToCommit_richTextBox(justifiedCommit);
            }

            static private void JustifyAndShowCommitIn(Progress_form progress_form, string commit)
            {
                int richTextBoxCharCount = (progress_form.GetCommit_richTextBoxWidth() - 6) / 8;
                string justifiedCommit = TextJustification.Justify(false, richTextBoxCharCount, commit);
                progress_form.WriteToCommit_richTextBox(justifiedCommit);
            }
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
            static private bool TryGetCommitTextsFromRange(DateTime since, DateTime until, out string[] commitTexts)
            {
                using (Process p = new Process())
                {
                    p.StartInfo.WorkingDirectory = ProjectMan.Proj_dir;
                    p.StartInfo.FileName = "git";
                    p.StartInfo.Arguments = "log --oneline " +
                        $"--since=\"{since.ToString("yyyy-MM-dd HH:mm")}\" " +
                        $"--until=\"{until.ToString("yyyy-MM-dd HH:mm")}\" " +
                         "--pretty=format:\"%C(auto)(%cr)%Creset\n\n%B\u0003\"";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();

                    string commits = p.StandardOutput.ReadToEnd() + "\n";
                    commitTexts = commits.Split("\n\u0003\n");
                    if (commitTexts.Length > 0) commitTexts = commitTexts[0..(commitTexts.Length - 1)];
                    //Array.Reverse(commitTexts);
                    return commitTexts.Length > 0 ? true : false;
                }
            }
        }
    }
}

