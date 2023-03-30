namespace WorkTracker
{
    public partial class Main_form : Form
    {
        public Main_form()
        {
            InitializeComponent();
        }

        private void RecordingFormOpening_Button_Click(object sender, EventArgs e)
        {
            Program.recording_form.Show();
            this.Hide();

        }
        private void ConfigFormOpening_Button_Click(object sender, EventArgs e)
        {
            Program.configure_form.Show();
            Program.configure_form.previousForm = this;
            this.Hide();

        }


        private void Main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ChooseClosestCommit_button_Click(object sender, EventArgs e)
        {

        }

    }
}