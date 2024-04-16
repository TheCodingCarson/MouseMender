using Mouse_Mender.Modules;

namespace Mouse_Mender
{
    public partial class ProcessEditorForm : Form
    {
        // Declared Variables
        private MainForm mainForm;
        private AutoProcessEnable autoProcessEnable;

        public ProcessEditorForm()
        {
            InitializeComponent();
        }

        // ProcessEditorForm Load Event
        private void ProcessEditorForm_Load(object sender, EventArgs e)
        {
            // Set ProcessEditorForm start location to center of MainForm's current position
            this.Location = CalculateStartPosition();

            // Fill List Box with Saved Processes
            listBox1.Items.Clear();
            if (Properties.Settings.Default.AutoEnableProcessList != null)
            {
                foreach (var process in Properties.Settings.Default.AutoEnableProcessList)
                {
                    listBox1.Items.Add(process);
                }
            }
        }

        // Calculate Start Position From MainForm Size & Current Location
        private Point CalculateStartPosition()
        {
            // MainForm dimensions and location
            Point mainFormSavedLocation = Properties.Settings.Default.LastWindowLocation;
            int mainFormWidth = 429;
            int mainFormHeight = 250;

            // ProcessEditorForm dimensions
            int processEditorFormWidth = 328;
            int processEditorFormHeight = 250;

            // Calculate center position for a child form
            int newX = mainFormSavedLocation.X + (mainFormWidth - processEditorFormWidth) / 2;
            int newY = mainFormSavedLocation.Y + (mainFormHeight - processEditorFormHeight) / 2;

            return new Point(newX, newY);
        }

        // Add Process
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && textBox1.Text != "process.exe")
            {
                // Add item to listbox
                listBox1.Items.Add(textBox1.Text.Trim());

                // Add the new item to the settings collection
                if (Properties.Settings.Default.AutoEnableProcessList == null)
                {
                    Properties.Settings.Default.AutoEnableProcessList = new System.Collections.Specialized.StringCollection();
                }
                Properties.Settings.Default.AutoEnableProcessList.Add(textBox1.Text);
                Properties.Settings.Default.Save();

                // Clear textbox
                textBox1.Clear();

                // Reset Auto Enable in MainForm
                autoProcessEnable.RestartAutoEnableTimer();
            }
            else
            {
                // Show a message box if the input is invalid
                MessageBox.Show("Please enter a valid process name. Format is 'processname.exe'", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete Process
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                // Remove the selected item from the settings collection
                Properties.Settings.Default.AutoEnableProcessList.Remove(listBox1.SelectedItem.ToString());
                Properties.Settings.Default.Save();

                // Remove from the ListBox
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                // Reset Auto Enable in MainForm
                autoProcessEnable.RestartAutoEnableTimer();
            }
        }

        // Move Process Down
        private void button4_Click(object sender, EventArgs e)
        {
            MoveItem(1);
            UpdateSettingsCollection();
        }

        // Move Process Up
        private void button3_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
            UpdateSettingsCollection();
        }

        // Move Listbox Process - Helper Function
        private void MoveItem(int direction)
        {
            // Checking selected item
            if (listBox1.SelectedItem == null || listBox1.SelectedIndex < 0)
                return; // No selected item

            // Calculate new index using direction (-1 for up, 1 for down)
            int newIndex = listBox1.SelectedIndex + direction;

            // Checking bounds of the list
            if (newIndex < 0 || newIndex >= listBox1.Items.Count)
                return; // Index out of range

            // Perform the move
            object selected = listBox1.SelectedItem;
            listBox1.Items.Remove(selected);
            listBox1.Items.Insert(newIndex, selected);
            listBox1.SetSelected(newIndex, true);
        }

        // Update String Collection - Helper Function
        private void UpdateSettingsCollection()
        {
            Properties.Settings.Default.AutoEnableProcessList = new System.Collections.Specialized.StringCollection();
            foreach (var process in listBox1.Items)
            {
                Properties.Settings.Default.AutoEnableProcessList.Add(process.ToString());
            }
            Properties.Settings.Default.Save();
        }
    }
}