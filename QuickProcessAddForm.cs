namespace Mouse_Mender
{
    public partial class QuickProcessAddForm : Form
    {
        public QuickProcessAddForm()
        {
            InitializeComponent();
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

                // Add the new item to the settings collection
                if (Properties.Settings.Default.AutoEnableProcessList == null)
                {
                    Properties.Settings.Default.AutoEnableProcessList = new System.Collections.Specialized.StringCollection();
                }
                Properties.Settings.Default.AutoEnableProcessList.Add(textBox1.Text);
                Properties.Settings.Default.Save();

                // Close Form
                this.Close();
            }
            else
            {
                // Show a message box if the input is invalid
                MessageBox.Show("Please enter a valid process name. Format is 'processname.exe'", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
