using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WorkTracker.Properties;

namespace WorkTracker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Initializer.Execute();
            Application.Run(main_form);
        }

        public static Main_form main_form = new Main_form();
        public static Configure_form configure_form = new Configure_form();
        public static Recording_form recording_form = new Recording_form();
        public static Progress_form progress_form = new Progress_form();
        public static Commit_form commit_form = new Commit_form();

        private static bool messageAlreadyShown = false;
        static public void CheckAfterActivatingApp(Form form)
        {
            ResourceControlMan.CheckAndSetResources(out bool ableToAccessCSV);
            ProgressShowingMan.CheckAndSetDateTimePickersInProgress(false, out _);
            CommitMan.CheckAndSetCommit_richTextBoxes();
            ProgressShowingMan.SetAndShowProgression(out _);
            if (!ableToAccessCSV && !messageAlreadyShown)
            {
                if (form is Progress_form) MessageBox.Show(Localization.Progress_UnableToAccessCSV);
                else MessageBox.Show(Localization.Config_UnableToAccessCSV);
                messageAlreadyShown = true;
            }
            else
            {
                messageAlreadyShown = false;
            }
        }
    }
    
    /// <summary>
    /// Inicialize parameters of GUI from text file or set default
    /// </summary>
    internal static class Initializer
    {
        static public void Execute()
        {
            Dictionary<string,string> init_params = InitParamsParser();
            Initialize(init_params);
        }

        static private Dictionary<string, string> InitParamsParser()
        {
            Dictionary<string, string> parameters = new()
            {
                {"lang",""},
                {"mode",""},
                {"tgit_dir",""},
                {"last_proj_dir",""}
            };
            try
            {
                using (StreamReader paramFile = new StreamReader("init_params.txt"))
                {
                    while (paramFile.ReadLine() is string line)
                    {
                        string[] line_parts = line.Split(" ");
                        if (line_parts.Length == 1) continue;
                        parameters[line_parts[0]] = string.Join(" ", line_parts.Skip(1).ToArray());
                    }
                }
            }
            catch (System.IO.IOException)
            {
                parameters["lang"] = "en_GB";
                parameters["mode"] = "local";
                parameters["tgit_dir"] = "";
                parameters["last_proj_dir"] = "";
            }
            return parameters;
        }

        static private void Initialize (Dictionary<string,string> init_params)
        {
            ModesMan.Initialize(init_params["mode"]);
            TortoiseGitMan.Initialize(init_params["tgit_dir"]);
            ProjectMan.Initialize(init_params["last_proj_dir"]);
            LocalizationMan.Initialize(init_params["lang"]);
            RecordingMan.Initialize();
            ProgressShowingMan.Initialize();
            CommitMan.Initialize();
        }

 
    }





    public static class ResourceControlMan
    {
        static public bool LastProjValidity { get; private set; }
        static public bool LastTGitValidity { get; private set; }
        static public bool IsProjValid() => ModesMan.visitMode.VisitForIsProjectValid();
        static public bool IsTGitValid() => ModesMan.visitMode.VisitForIsTGitValid();
        public static void CheckAndSetResources(out bool ableToAccessCSV)
        {
            bool projValidityBeforeCheck = LastProjValidity;
            bool tGitValidityBeforeCheck = LastTGitValidity;
            TortoiseGitMan.CheckAndSetTGit_dir();

            ProjectMan.CheckAndSetProj_dir();

            if (!LastProjValidity && projValidityBeforeCheck)
                if (!LastTGitValidity && tGitValidityBeforeCheck) MessageBox.Show(Localization.NonValidChangeOfProject_dir + "\n" + Localization.NonValidChangeOfTGit_dir);
                else MessageBox.Show(Localization.NonValidChangeOfProject_dir);
            else if (!LastTGitValidity && tGitValidityBeforeCheck) MessageBox.Show(Localization.NonValidChangeOfTGit_dir);

            RecordingMan.AdaptToEnviromentWithNewProj(out ableToAccessCSV);
        }
        static public bool IsProjValid(ModesMan.VisitLocalMode mode)
        {
            LastProjValidity = ProjectMan.ExistsProjDir();
            return LastProjValidity;
        }
        static public bool IsProjValid(ModesMan.VisitReposMode mode)
        {
            LastProjValidity = ProjectMan.ExistsProjDir() && ProjectMan.IsThereRepo();
            return LastProjValidity;
        }
        static public bool IsTGitValid(ModesMan.VisitLocalMode mode)
        {
            LastTGitValidity = true;
            return LastTGitValidity;
        }
        static public bool IsTGitValid(ModesMan.VisitReposMode mode)
        {
            LastTGitValidity = TortoiseGitMan.ExistsTG();
            return LastTGitValidity;
        }


    }
    public static class AppExitMan
    {
        public static void ExitApp(FormClosingEventArgs e)
        {

            if (e.CloseReason is CloseReason.UserClosing && RecordingMan.recState is (RecordingMan.RecStatesI.started or RecordingMan.RecStatesI.paused))
            {
                YesNoDialog_form areYouSureYouWantToExit_form = new YesNoDialog_form(Localization.NotStopedExit_YesNoDialog_label_text, Localization.Yes, Localization.No);
                areYouSureYouWantToExit_form.ShowDialog();
                if (areYouSureYouWantToExit_form.DialogResult is DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            SaveParameters();
            System.Windows.Forms.Application.Exit();
        }

        private static void SaveParameters()
        {
            using (StreamWriter paramFile = new StreamWriter("init_params.txt"))
            {
                paramFile.WriteLine("lang " + LocalizationMan.lang);
                paramFile.WriteLine("mode " + ModesMan.modeI);
                TortoiseGitMan.WriteTGit_dirTo(paramFile);
                ProjectMan.WriteProj_dirTo(paramFile);
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