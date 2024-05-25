using Mouse_Mender.Modules;
using System.Windows.Forms;

namespace Mouse_Mender
{
    public partial class QuickProcessAddForm : Form
    {
        // Declared Variables
        private MainForm mainForm;
        private AutoProcessEnable autoProcessEnable;

        public QuickProcessAddForm()
        {
            InitializeComponent();
            autoProcessEnable = new AutoProcessEnable(mainForm);
        }

        // FormLoad Event
        private void QuickProcessAddForm_Load(object sender, EventArgs e)
        {
            // Set QuickProcessAddForm start location to center of MainForm's current position
            this.Location = CalculateStartPosition();
        }

        // Add Process
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && textBox1.Text != "process.exe")
            {
                String NewProcess;

                // Check if the TextBox1 string ends with ".exe" and add it if it doesn't
                if (!textBox1.Text.EndsWith(".exe"))
                {
                    NewProcess = textBox1.Text.Trim() + ".exe";
                }
                else
                {
                    NewProcess = textBox1.Text.Trim();
                }

                // Check if the process already exists in the listBox1 or AutoEnableProcessList
                bool existsInSettings = Properties.Settings.Default.AutoEnableProcessList != null &&
                                        Properties.Settings.Default.AutoEnableProcessList.Contains(NewProcess);

                if (existsInSettings)
                {
                    // Show a message box if the process already exists
                    MessageBox.Show("The process already exists in the list.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else // Contuine adding the new process
                {

                    // Add the new item to the settings collection
                    if (Properties.Settings.Default.AutoEnableProcessList == null)
                    {
                        Properties.Settings.Default.AutoEnableProcessList = new System.Collections.Specialized.StringCollection();
                    }

                    // Add the New Process (Standardized) to the StringCollection
                    Properties.Settings.Default.AutoEnableProcessList.Add(NewProcess);

                    // Save Process List
                    Properties.Settings.Default.Save();

                    // Reset Auto Enable in MainForm
                    autoProcessEnable.RestartAutoEnableTimer();

                    // Exit QuickProcessAddForm
                    this.Close();
                }
            }
            else
            {
                // Show a message box if the input is invalid
                MessageBox.Show("Please enter a valid process name. Format is 'processname(.exe)'", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Calculate Start Position From MainForm Size & Current Location
        private Point CalculateStartPosition()
        {
            // MainForm dimensions and location
            Point mainFormSavedLocation = Properties.Settings.Default.LastWindowLocation;
            int mainFormWidth = 429;
            int mainFormHeight = 250;

            // QuickProcessAddForm dimensions
            int quickProcessAddFormWidth = 311;
            int quickProcessAddFormHeight = 84;

            // Calculate center position for a child form
            int newX = mainFormSavedLocation.X + (mainFormWidth - quickProcessAddFormWidth) / 2;
            int newY = mainFormSavedLocation.Y + (mainFormHeight - quickProcessAddFormHeight) / 2;

            return new Point(newX, newY);
        }
    }
}
