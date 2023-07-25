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
                CommitMan.CheckAndSetCommitTexts();
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
            CheckAndSetCommitTexts();
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
            TryGetLastCommitCode(out lastCommitCode);
            if (previousCommitCode != lastCommitCode)
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
        static public void CheckAndSetCommitTexts()
        {
            CheckAndSetLastCommitInMain();
            //TODO: aj pre progress form
        }
        static public void CheckAndSetLastCommitInMain()
        {
            ModesMan.mode.VisitForCheckAndSetLastCommitInMain();
        }
        static public void CheckAndSetLastCommitInMain(ModesMan.ReposMode mode)
        {
            if (ResourceControlMan.LastProjValidity)
            {
                TryGetLastCommitCode(out lastCommitCode);
                CommitViewer.ShowCommitInMain();
            }
            else
            {
                Program.main_form.WriteToCommit_richTextBox(Localization.Main_InvalidProjectSelectedCommit_richTextBox);
            }
        }
        static public void CheckAndSetLastCommitInMain(ModesMan.LocalMode mode)
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
        
        static public class CommitViewer
        {
            // pix-6/ 8 - pocet cisiel ktore sa tam vojdu na jeden riadok

            static public void ShowCommitInMain()
            {
                if (TryGetLastCommitText(out string commit))
                {
                    int richTextBoxCharCount = (Program.main_form.GetCommit_richTextBoxWidth() - 6) / 8;
                    string justifiedCommit = TextJustification.Justify(false, richTextBoxCharCount, commit);
                    Program.main_form.WriteToCommit_richTextBox(justifiedCommit);
                }
                else
                {
                    Program.main_form.WriteToCommit_richTextBox(Localization.Main_NoCommitMade_richTextBox_text);
                }

            }
            static public bool TryGetLastCommitText(out string commitText)
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
        }

        static internal class TextJustification
        {

            /// <summary>
            /// writes justified text paragraphs to output file by concatinating input files to one.
            /// the boundary of files serves as not-written word separator.
            /// </summary>
            /// <param name="highlightSpaceIndic"> indicates if in output file should be highlighted white characters as spaces and ends of lines</param>
            /// <param name="input_file_names"></param>
            /// <param name="output_file_name"></param>
            /// <param name="lineLength">given length of one line in output file</param>
            /// <returns>0 if justifiing ran flawlessly and 1 if any error occured. If any error occured, we can't say anything about the output file</returns>
            static public string Justify(bool highlightSpaceIndic, int lineLength, string inputText)
            {
                bool EndOfParagraph;
                bool EndOfFile = false;
                string line;

                string justifiedText = "";
                LineReader LR = new LineReader(inputText, lineLength);
                LineWriter LW = highlightSpaceIndic ? new FilledLineWriter(lineLength) : new WhiteLineWriter(lineLength);
                while (!EndOfFile)
                {
                    (line, EndOfParagraph, EndOfFile) = LR.ReadLine();

                    // if we get end of file and empty line at the same time, it means that LR.ReadLine()
                    //reads only lines full of blank chars, so we dont want to write nothing to output
                    if (EndOfFile && line == "")
                    {
                        return justifiedText;
                    }

                    LW.WriteLine(line, EndOfParagraph, EndOfFile, out string justifiedLine);
                    justifiedText = justifiedText + justifiedLine;
                }
                return justifiedText;
            }

            /// <summary>
            /// just makes StreamWriter on file with given fileName and writes to console about errors
            /// </summary>
            /// <param name="fileName"></param>
            /// <returns>StreamWriter and inidcator of success</returns>
            static (StreamWriter?, bool) GetStreamWriter(string fileName)
            {
                try
                {
                    return (new StreamWriter(fileName), true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("File Error");
                    return (null, false);
                }
            }
            /// <summary>
            /// class for reading lines from file, reading one character form file at the time
            /// </summary>
            class LineReader
            {
                private StreamReader reader;
                private string lastWord;
                private char lastChar;
                private int lineLength;
                private bool endOfFile;
                private char[] blankChars = { ' ', '\n', '\t' };
                public LineReader(string text, int lineLength)
                {
                    reader = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(text)));
                    this.lineLength = lineLength;
                    lastWord = "";
                    lastChar = ' ';
                    endOfFile = false;
                }

                /// <summary>
                /// reads line form file of specific length, replace ale big blank chunks with only one space
                /// and returns it at the output
                /// end of paragraph indicator is set when there occures word, which has blank line in front of it
                /// end of file indicator is set when the end of the file occures after word or some blank string 
                /// no file is set when no openable file was found
                /// success indicator indicates, if reading from file was successful. If it wasnt, then process is immediately terminated
                /// every time we save last word which doesnt fit in the line or is from other paragraph
                /// </summary>
                /// <returns>adapted line, end of paragraph, end of file, and success indicator</returns>
                public (string, bool, bool) ReadLine()
                {
                    string line = lastWord;
                    string word;
                    bool endOfParagrah;

                    if (endOfFile) return (line, true, true);

                    (word, endOfParagrah, endOfFile) = ReadWord();

                    while (line.Length + word.Length + 1 <= lineLength)
                    {
                        if (endOfFile || endOfParagrah)
                        {
                            //if endOfParagraph is true, then word is from the next paragraph, so it doesnt goes in current line
                            //on the other hand, when endOfFile is true, then we read it after the word, so the word belongs to the line
                            if (endOfFile && word != "") line += line == "" ? word : ' ' + word;
                            lastWord = word;
                            return (line, endOfParagrah, endOfFile);
                        }
                        if (word != "") line += line == "" ? word : ' ' + word;

                        (word, endOfParagrah, endOfFile) = ReadWord();
                    }
                    lastWord = word;
                    return (line, endOfParagrah, false);

                }

                /// <summary>
                /// reads and return one word from the file. Firstly goes through blank characters and then loads one word.
                /// end of paragraph is set, when two or more lineFeeds are found after each other
                /// each time we will save last char loaded becaouse of counting of lineFeeds 
                /// </summary>
                /// <returns>loaded word, end of paragraph, end of file and success indicators</returns>
                private (string, bool, bool) ReadWord()
                {
                    string word = "";
                    int charUniCode;
                    int lineFeedCount = lastChar == '\n' ? 1 : 0; //lineFeed from previous word processing

                    charUniCode = reader.Read();
                    //just fly throug blank chars and count lineFeeds
                    while (charUniCode != -1 && blankChars.Contains((char)charUniCode))
                    {
                        if ((char)charUniCode == '\n') ++lineFeedCount;

                        charUniCode = reader.Read();
                    }

                    if (charUniCode != -1) // if end of file doesnt occure
                    {
                        while (charUniCode != -1 && !blankChars.Contains((char)charUniCode))
                        {
                            word += (char)charUniCode;
                            charUniCode = reader.Read();
                        }
                        lastChar = (char)charUniCode;
                        return (word, lineFeedCount > 1, charUniCode == -1);
                    }
                    else
                    {
                        lastChar = (char)charUniCode;
                        return ("", false, true); // end of file comes after blank char, so it is not end of paragraph
                    }
                }
            }

            /// <summary>
            /// class for writing justified lines to file of choose
            /// </summary>
            abstract class LineWriter
            {
                protected bool newParagraph;
                protected int lineLength;

                public LineWriter(int lineLength)
                {
                    this.lineLength = lineLength;
                    newParagraph = false;
                }

                /// <summary>
                /// takes input line, which is in form of text, where words are delimeterated 
                /// by just one single space character or end or start of the line
                /// writes justified line to the file
                /// </summary>
                /// <param name="line">line of properly formated text</param>
                /// <param name="endOfParagraph">indicator of the end of paragraph occurence</param>
                /// <param name="endOfFile">indicator of the end of file occurence</param>
                /// <param name="noFile">indicator if any openable file was found for reading</param>
                /// <returns></returns>
                abstract public void WriteLine(string line, bool endOfParagraph, bool endOfFile, out string justifiedLine);

                /// <summary>
                /// distributes uniformly white spaces between words, such that line has length of lineLength variable
                /// </summary>
                /// <param name="line"></param>
                /// <returns></returns>
                abstract protected string DistributeWhiteSpaces(string line);

            }

            /// <summary>
            /// class for writing justified lines to file of choose by concatinating words with spaces and lines with "end of line" chars
            /// </summary>
            class WhiteLineWriter : LineWriter
            {
                public WhiteLineWriter(int lineLength) : base(lineLength) { }
                override public void WriteLine(string line, bool endOfParagraph, bool endOfFile, out string justifiedLine)
                {
                    justifiedLine = "";
                    // if after previous line was found pargraph delimeter then now starts the new one
                    // so we have to add paragraph delimeter before next line
                    if (newParagraph)
                    {
                        justifiedLine = justifiedLine + ("" + Environment.NewLine);
                        newParagraph = false;
                    }

                    if (line.Split(' ').Length > 1 && !endOfParagraph && !endOfFile)
                        line = DistributeWhiteSpaces(line);

                    if (line != "") justifiedLine = justifiedLine + line + Environment.NewLine;
                    if (endOfParagraph) newParagraph = true; //if the next paragraph occures, we will add blank line for delimetering it
                }

                override protected string DistributeWhiteSpaces(string line)
                {
                    if (line.Length == lineLength) return line;

                    string[] words = line.Split(' ');

                    string newLine = "";
                    int GapsCount = words.Length - 1;
                    int emptySpaceToMaxLength = lineLength - line.Length;
                    int whiteCharsPerGap = emptySpaceToMaxLength / GapsCount;
                    string whiteChars = "";
                    for (int i = 0; i < whiteCharsPerGap; ++i) whiteChars += ' ';
                    for (int i = 0; i < GapsCount; ++i)
                    {
                        newLine += words[i];
                        newLine += whiteChars;
                        //add one extra space character, if white space cant be distributed unifromly
                        if (i < emptySpaceToMaxLength % GapsCount) newLine += ' ';
                        newLine += ' ';// add space chcaracter for previously delimeterating character
                    }
                    newLine += words[words.Length - 1];
                    return newLine;
                }
            }
            /// <summary>
            /// class for writing justified lines to file of choose by concatinating words with '.' and lines with "end of line" chars before which '<-' is written
            /// </summary>
            class FilledLineWriter : LineWriter
            {
                public FilledLineWriter(int lineLength) : base(lineLength) { }

                override public void WriteLine(string line, bool endOfParagraph, bool endOfFile, out string justifiedLine)
                {
                    justifiedLine = "";
                    // if after previous line was found pargraph delimeter then now starts the new one
                    // so we have to add paragraph delimeter before next line
                    if (newParagraph)
                    {
                        justifiedLine = justifiedLine + "<-" + Environment.NewLine;
                        newParagraph = false;
                    }

                    if (line.Split(' ').Length > 1 && !endOfParagraph && !endOfFile)
                        line = DistributeWhiteSpaces(line);
                    else line = line.Replace(' ', '.');

                    if (line != "") justifiedLine = justifiedLine + line + "<-" + Environment.NewLine;
                    if (endOfParagraph) newParagraph = true; //if the next paragraph occures, we will add blank line for delimetering it
                }

                override protected string DistributeWhiteSpaces(string line)
                {
                    if (line.Length == lineLength) return line.Replace(' ', '.');

                    string[] words = line.Split(' ');

                    string newLine = "";
                    int GapsCount = words.Length - 1;
                    int emptySpaceToMaxLength = lineLength - line.Length;
                    int whiteCharsPerGap = emptySpaceToMaxLength / GapsCount;
                    string whiteChars = "";
                    for (int i = 0; i < whiteCharsPerGap; ++i) whiteChars += '.';
                    for (int i = 0; i < GapsCount; ++i)
                    {
                        newLine += words[i];
                        newLine += whiteChars;
                        //add one extra space character, if white space cant be distributed unifromly
                        if (i < emptySpaceToMaxLength % GapsCount) newLine += '.';
                        newLine += '.';// add space chcaracter for previously delimeterating character
                    }
                    newLine += words[words.Length - 1];
                    return newLine;
                }
            }

        }

     
    }
}

