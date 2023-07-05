using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            string exit = Program.IsHereRepo("C:\\Users\\matej\\Desktop\\Škola\\VÝŠKA\\5.semester\\Programovani_v_C#\\Zapoctovy_program");
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
    }
}