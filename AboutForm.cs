using System.Reflection;

namespace Mouse_Mender
{
    public partial class AboutForm : Form
    {
        // Declared Variables
        private Version version = Assembly.GetExecutingAssembly().GetName().Version;
        private string versionFormatted;

        public AboutForm()
        {
            InitializeComponent();
        }

        // AboutForm Load Event
        private void AboutForm_Load(object sender, EventArgs e)
        {
            // Set AboutForm start location to same position as MainForm currently
            this.Location = Properties.Settings.Default.LastWindowLocation;

            // Format Version String
            versionFormatted = $"{version.Major}.{version.Minor}.{version.Build}";

            // Set Version Label
            label7.Text = "Mouse Mender v" + versionFormatted;
        }

        // Logo Picture Box Clicked
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
