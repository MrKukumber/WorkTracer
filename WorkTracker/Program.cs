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

        // forms used in program
        public static Main_form main_form = new Main_form();
        public static Configure_form configure_form = new Configure_form();
        public static Recording_form recording_form = new Recording_form();
        public static Progress_form progress_form = new Progress_form();
        public static Commit_form commit_form = new Commit_form();


        private static bool messageAlreadyShown = false;
        /// <summary>
        /// after aplication is made active, this function is called to check and set those things, 
        /// that could be affected by any activity outside of the aplication
        /// </summary>
        /// <param name="form">form, which was activated after aplication activation</param>
        static public void CheckAfterActivatingApp(Form form)
        {
            bool projValidityBeforeCheck = ProjectMan.LastProjValidity;
            bool tGitValidityBeforeCheck = TortoiseGitMan.LastTGitValidity;
            TortoiseGitMan.CheckAndSetTGit_dir();
            ProjectMan.CheckAndSetProj_dir();
            RecordingMan.AdaptToEnviromentWithNewProj(out bool ableToAccessCSV);
            ProgressMan.CheckAndSetDateTimePickersInProgress(false, out _);
            CommitMan.GetCheckAndSetCommit_richTextBoxes();
            ProgressMan.SetAndShowProgression(out _);
            if (!ProjectMan.LastProjValidity && projValidityBeforeCheck)
                if (!TortoiseGitMan.LastTGitValidity && tGitValidityBeforeCheck) MessageBox.Show(Localization.NonValidChangeOfProject_dir + "\n" + Localization.NonValidChangeOfTGit_dir);
                else MessageBox.Show(Localization.NonValidChangeOfProject_dir);
            else if (!TortoiseGitMan.LastTGitValidity && tGitValidityBeforeCheck) MessageBox.Show(Localization.NonValidChangeOfTGit_dir);
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
        /// <summary>
        /// reads fiel with saved parameters
        /// if file is not accessible, default params are used
        /// </summary>
        /// <returns>parameters</returns>
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
        /// <summary>
        /// calls Initialize() methods for all managers
        /// </summary>
        /// <param name="init_params">parameters from initialization file</param>
        static private void Initialize (Dictionary<string,string> init_params)
        {
            ModesMan.Initialize(init_params["mode"]);
            TortoiseGitMan.Initialize(init_params["tgit_dir"]);
            ProjectMan.Initialize(init_params["last_proj_dir"]);
            LocalizationMan.Initialize(init_params["lang"]);
            RecordingMan.Initialize();
            ProgressMan.Initialize();
            CommitMan.Initialize();
        } 
    }


    /// <summary>
    /// guarantees correct exiting of aplication
    /// </summary>
    public static class AppExitMan
    {
        /// <summary>
        /// called whenever closing of aplication is called
        /// if user decides not to exit application after dialog window shows, it cancels closing of application
        /// </summary>
        /// <param name="e"></param>
        public static void ExitApp(FormClosingEventArgs e)
        {

            if (e.CloseReason is CloseReason.UserClosing) 
            { 
                if (RecordingMan.recState is (RecordingMan.RecStatesI.started or RecordingMan.RecStatesI.paused))
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
        }
        /// <summary>
        /// function, which saves parameter to initialization file for future use
        /// </summary>
        private static void SaveParameters()
        {
            using (StreamWriter paramFile = new StreamWriter("init_params.txt"))
            {
                paramFile.WriteLine("lang " + LocalizationMan.Lang);
                paramFile.WriteLine("mode " + ModesMan.ModeI);
                TortoiseGitMan.WriteTGit_dirTo(paramFile);
                ProjectMan.WriteProj_dirTo(paramFile);
            }
        }
    }
    /// <summary>
    /// class for justifying text
    /// it is adapted implementation from one of my others works, originally it worked with files and fileStreams
    /// some of the implementation decisions are therefore litle bit strange
    /// </summary>
    static internal class TextJustification
    {
        static public string Justify(bool highlightSpaceIndic, int lineLength, string inputText)
        {
            bool EndOfParagraph;
            bool EndOfString = false;
            string line;

            string justifiedText = "";
            LineReader LR = new LineReader(inputText, lineLength);
            LineWriter LW = highlightSpaceIndic ? new FilledLineWriter(lineLength) : new WhiteLineWriter(lineLength);
            while (!EndOfString)
            {
                (line, EndOfParagraph, EndOfString) = LR.ReadLine();

                // if we get end of string and empty line at the same time, it means that LR.ReadLine()
                //reads only lines full of blank chars, so we dont want to write nothing to output
                if (EndOfString && line == "")
                {
                    return justifiedText;
                }

                LW.WriteLine(line, EndOfParagraph, EndOfString, out string justifiedLine);
                justifiedText = justifiedText + justifiedLine;
            }
            return justifiedText;
        }

        /// <summary>
        /// class for reading lines from string, reading one character form string at the time
        /// </summary>
        class LineReader
        {
            private StreamReader reader;
            private string lastWord;
            private char lastChar;
            private int lineLength;
            private bool endOfStream;
            private char[] blankChars = { ' ', '\n', '\t' };
            public LineReader(string text, int lineLength)
            {
                reader = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(text)));
                this.lineLength = lineLength;
                lastWord = "";
                lastChar = ' ';
                endOfStream = false;
            }

            /// <summary>
            /// reads line of specific length, replace ale big blank chunks with only one space
            /// and returns it at the output
            /// end of paragraph indicator is set when there occures word, which has blank line in front of it
            /// end of stream indicator is set when the end of the stream occures after word or some blank string 
            /// every time we save last word which doesnt fit in the line or is from other paragraph
            /// </summary>
            /// <returns>adapted line, end of paragraph, end of string</returns>
            public (string, bool, bool) ReadLine()
            {
                string line = lastWord;
                string word;
                bool endOfParagrah;

                if (endOfStream) return (line, true, true);

                (word, endOfParagrah, endOfStream) = ReadWord();

                while (line.Length + word.Length + 1 <= lineLength)
                {
                    if (endOfStream || endOfParagrah)
                    {
                        //if endOfParagraph is true, then word is from the next paragraph, so it doesnt goes in current line
                        //on the other hand, when endOfStream is true, then we read it after the word, so the word belongs to the line
                        if (endOfStream && word != "") line += line == "" ? word : ' ' + word;
                        lastWord = word;
                        return (line, endOfParagrah, endOfStream);
                    }
                    if (word != "") line += line == "" ? word : ' ' + word;

                    (word, endOfParagrah, endOfStream) = ReadWord();
                }
                lastWord = word;
                return (line, endOfParagrah, false);

            }

            /// <summary>
            /// reads and return one word from the string. Firstly goes through blank characters and then loads one word.
            /// end of paragraph is set, when two or more lineFeeds are found after each other
            /// each time we will save last char loaded becaouse of counting of lineFeeds 
            /// </summary>
            /// <returns>loaded word, end of paragraph, end of string</returns>
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

                if (charUniCode != -1) // if end of stream doesnt occure
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
                    return ("", false, true); // end of stream comes after blank char, so it is not end of paragraph
                }
            }
        }

        /// <summary>
        /// class that returns justified lines of strings
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
            /// </summary>
            /// <param name="line">line of properly formated text</param>
            /// <param name="endOfParagraph">indicator of the end of paragraph occurence</param>
            /// <param name="endOfStream">indicator of the end of stream occurence</param>
            /// <param name="justifiedLine"></param>
            abstract public void WriteLine(string line, bool endOfParagraph, bool endOfStream, out string justifiedLine);

            /// <summary>
            /// distributes uniformly white spaces between words, such that line has length of lineLength variable
            /// </summary>
            /// <param name="line"></param>
            /// <returns></returns>
            abstract protected string DistributeWhiteSpaces(string line);

        }

        /// <summary>
        /// class that returns justified lines of strings by concatinating words with spaces and lines with "end of line" chars
        /// </summary>
        class WhiteLineWriter : LineWriter
        {
            public WhiteLineWriter(int lineLength) : base(lineLength) { }
            override public void WriteLine(string line, bool endOfParagraph, bool endOfStream, out string justifiedLine)
            {
                justifiedLine = "";
                // if after previous line was found pargraph delimeter then now starts the new one
                // so we have to add paragraph delimeter before next line
                if (newParagraph)
                {
                    justifiedLine = justifiedLine + ("" + Environment.NewLine);
                    newParagraph = false;
                }

                if (line.Split(' ').Length > 1 && !endOfParagraph && !endOfStream)
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
        /// class that returns justified lines of strings by concatinating words with '.' and lines with "end of line" chars before which '<-' is written
        /// </summary>
        class FilledLineWriter : LineWriter
        {
            public FilledLineWriter(int lineLength) : base(lineLength) { }

            override public void WriteLine(string line, bool endOfParagraph, bool endOfStream, out string justifiedLine)
            {
                justifiedLine = "";
                // if after previous line was found pargraph delimeter then now starts the new one
                // so we have to add paragraph delimeter before next line
                if (newParagraph)
                {
                    justifiedLine = justifiedLine + "<-" + Environment.NewLine;
                    newParagraph = false;
                }

                if (line.Split(' ').Length > 1 && !endOfParagraph && !endOfStream)
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