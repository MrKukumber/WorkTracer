using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using System.Globalization;
using System.DirectoryServices;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Testovanie_funkcionalit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = Loc.Commit_WantCommit_label_text;
        }
        int a = 0;
        private void OpenTGitButton_Click(object sender, EventArgs e)
        {
            int exitCode = Program.OpenTGitCommitForm("C:\\Users\\matej\\Desktop\\Škola\\VÝŠKA\\5.semester\\Programovani_v_C#\\Zapoctovy_program\\skusam tortoise");
            //C:\\Users\\matej\\Desktop\\Škola\\VÝŠKA\\5.semester\\Programovani_v_C#\\Zapoctovy_program\\WorkTracker
            richTextBox1.Text = exitCode.ToString();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Program.activationCount++;
            a++;
            richTextBox1.Text = $"aktivoval som sa {Program.activationCount}-krat";
        }

        private void GoTOOtherForm_button_Click(object sender, EventArgs e)
        {
            Program.form2.Show();
            this.Hide();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Form.ActiveForm == this)
                MessageBox.Show("a ted!");
            System.Windows.Forms.Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Form.ActiveForm == this)
            {
                var window = MessageBox.Show("Close the window?", "Are you sure?", MessageBoxButtons.YesNo);
                e.Cancel = (window == DialogResult.No);
            }

        }

        private void IsHereRepo_button_Click(object sender, EventArgs e)
        {
            string exit = Program.IsHereRepo("C:\\Users\\matej\\Desktop\\Škola\\VÝŠKA\\5.semester\\Programovani_v_C#\\Zapoctovy_program\\WorkTracker");
            //C:\\Users\\matej\\Desktop\\Škola\\VÝŠKA\\5.semester\\Programovani_v_C#\\Zapoctovy_program\\WorkTracker
            richTextBox1.Text = exit;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture == System.Globalization.CultureInfo.GetCultureInfo("en")) 
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("sk");
            else Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en");
            label1.Text = Loc.Commit_WantCommit_label_text;

        }

        private void readFromCsv_button_Click(object sender, EventArgs e)
        {
            Record? lastRecord = null;
            using (var reader = new StreamReader("C:\\Users\\matej\\Desktop\\Škola\\VÝŠKA\\5.semester\\Programovani_v_C#\\Zapoctovy_program\\skusam tortoise" + "\\" + ".workTracer_recordings.csv"))
            using (var csv = new CsvReader(reader, basicConfig))
            {
                csv.Read();
                var record = csv.GetRecord<Record>();
                return;
            }
        }
        static private CsvConfiguration basicConfig = new(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Comment = '%',
        };

        public enum RecStates { unknown, started, paused, stoped }
        public enum WorkPhase { creating, programing, debuging }
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

        private void writeToCsv_button_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open("C:\\Users\\matej\\Desktop\\Škola\\VÝŠKA\\5.semester\\Programovani_v_C#\\Zapoctovy_program\\skusam tortoise" + "\\" + ".workTracer_recordings.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, basicConfig))
            {
                //csv.WriteRecords(new List<Record>(){new Record
                //{
                //    Date = new DateOnly(2001, 7, 13),
                //    Time = new TimeOnly(20, 20, 20),
                //    State = RecStates.started,
                //    Phase = WorkPhase.creating,
                //    Git = "asfdasf"
                //} });
                //csv.WriteHeader<Record>();
                //csv.NextRecord();
                //csv.WriteRecord<Record>(new Record 
                //    {
                //    Date = new DateOnly(2001, 7, 13),
                //    Time = new TimeOnly(20, 20, 20),
                //    State = RecStates.started,
                //    Phase = WorkPhase.creating,
                //    Git = "asfdasf"
                //} );
                //csv.NextRecord();
                //csv.WriteRecords(new List<Record>(){new Record
                //{
                //    Date = new DateOnly(2001, 7, 13),
                //    Time = new TimeOnly(20, 20, 20),
                //    State = RecStates.started,
                //    Phase = WorkPhase.creating,
                //    Git = "asfdasf"
                //} });
                csv.WriteRecord<Record>(new Record
                {
                    Date = new DateOnly(2001, 7, 13),
                    Time = new TimeOnly(20, 20, 20),
                    State = RecStates.started,
                    Phase = WorkPhase.creating,
                    Git = "asfdasf"
                });
                csv.NextRecord();

            }
        }
    }
}