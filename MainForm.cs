using System.Diagnostics;
using System.Reflection;
using Mouse_Mender.Modules;

namespace Mouse_Mender
{
    public partial class MainForm : Form
    {
        // Instances & Variables
        private string codingcarsonURL = "https://www.youtube.com/@CodingCarson";
        private Version version = Assembly.GetExecutingAssembly().GetName().Version;
        private string versionFormatted;
        private Hotkeys hotkeys;
        private ClipCursorHook clipCursorHook;
        private UpdateChecker updateChecker;
        private AboutForm aboutForm;
        private AutoProcessEnable autoProcessEnable;
        private ProcessEditorForm processEditorForm;
        private QuickProcessAddForm quickProcessAddForm;
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();
        public Screen lockedScreen = null;
        public bool isRunning = false;
        public bool isMouseLockedByApp = false;
        public bool forceClose = false;
        public bool editingHotkey = false;

        // MainForm Initialization
        public MainForm()
        {
            InitializeComponent();
            updateChecker = new UpdateChecker();
            clipCursorHook = new ClipCursorHook(this);
            hotkeys = new Hotkeys(this);
            autoProcessEnable = new AutoProcessEnable(this);
        }

        // MainForm Closed Event
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0); // Ensuring all threads are terminated
        }

        // MainForm Load Event
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Check if start Window Location is visible
            bool isLocationValidAndWithinBounds = Properties.Settings.Default.LastWindowLocation != Point.Empty && Properties.Settings.Default.LastWindowLocation != new Point(0, 0) && IsLocationWithinScreens(Properties.Settings.Default.LastWindowLocation);

            if (!isLocationValidAndWithinBounds)
            {
                // Get Primary screen center location
                Rectangle primaryScreenBounds = Screen.PrimaryScreen.Bounds;
                Point centerPoint = new Point(primaryScreenBounds.Width / 2 - this.Width / 2, primaryScreenBounds.Height / 2 - this.Height / 2);

                // Open in Center
                this.Location = centerPoint;

                // Re-save Location
                Properties.Settings.Default.LastWindowLocation = this.Location;
                Properties.Settings.Default.Save();
            }
            else
            {
                // Default - Open in Last Location
                this.Location = Properties.Settings.Default.LastWindowLocation;
            }

            // Start Auto Enable If Selected
            if (Properties.Settings.Default.AutoEnable && Properties.Settings.Default.AutoEnableProcessList != null && Properties.Settings.Default.AutoEnableProcessList.Count > 0)
            {
                // Start Auto Enable checking
                checkProcessTimer.Start();
            }
            else
            {
                // Set Setting back to Disabled
                Properties.Settings.Default.AutoEnable = false;
                Properties.Settings.Default.Save();

                // Set UI back to Disabled
                enableAutoEnableToolStripMenuItem.Checked = false;
                label11.Text = "Disabled";
                label11.ForeColor = Color.DarkRed;
            }

            // Load UI Properties From Settings

            if (Properties.Settings.Default.AutoEnable) // Auto Enable Status
            {
                label11.Text = "Enabled";            // Label
                label11.ForeColor = Color.DarkGreen;

                enableAutoEnableToolStripMenuItem.Checked = true; // Menu Strip
            }
            else
            {
                label11.Text = "Disabled";         // Label
                label11.ForeColor = Color.DarkRed;

                enableAutoEnableToolStripMenuItem.Checked = false; // Menu Strip
            }

            if (Properties.Settings.Default.MonitorPreference == "CurrentMonitorWithMouse") // Monitor Preference Choice
            {
                currentMonitorWithMouseToolStripMenuItem.Checked = true;
                primaryMonitorToolStripMenuItem.Checked = false;         // Menu Strip
            }
            else
            {
                primaryMonitorToolStripMenuItem.Checked = true;           // Menu Strip
                currentMonitorWithMouseToolStripMenuItem.Checked = false;
            }

            if (Properties.Settings.Default.CheckForUpdatesonLaunch) // Check For Updates On Launch Status
            {
                checkForUpdatesOnLaunchToolStripMenuItem.Checked = true; // Menu Strip
            }
            else
            {
                checkForUpdatesOnLaunchToolStripMenuItem.Checked = false; // SysTray
            }

            if (Properties.Settings.Default.ExittoSystray) // Exit to SysTray Status
            {
                exitToSystrayToolStripMenuItem.Checked = true; // Menu Strip
            }
            else
            {
                exitToSystrayToolStripMenuItem.Checked = false; // SysTray
            }

            if (Properties.Settings.Default.EnableHotkeys) // Hotkeys Enabled Status
            {
                label9.Text = "Enabled";            // Label
                label9.ForeColor = Color.DarkGreen;

                enableHotkeysToolStripMenuItem.Checked = true; // Menu Strip

                enableHotkeysToolStripMenuItem1.Checked = true; // SysTray
            }
            else
            {
                label9.Text = "Disabled";         // Label
                label9.ForeColor = Color.DarkRed;

                enableHotkeysToolStripMenuItem.Checked = false; // Menu Strip

                enableHotkeysToolStripMenuItem1.Checked = false; // SysTray
            }

            textBox1.Text = Properties.Settings.Default.Hotkey; // Hotkey

            versionFormatted = $"{version.Major}.{version.Minor}.{version.Build}"; // Format Version String

            label7.Text = "Mouse Mender v" + versionFormatted; // Version Label
        }

        // MainForm Show Event
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Register Hotkey (if enabled)
            if (Properties.Settings.Default.EnableHotkeys)
            {
                hotkeys.UpdateHotkeyRegistration();
            }

            // Check For Updates (if enabled)
            if (Properties.Settings.Default.CheckForUpdatesonLaunch)
            {
                updateChecker.CheckForUpdates();
            }
        }

        // MainForm FormClosing Event
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close to SysTray
            if (!forceClose && Properties.Settings.Default.ExittoSystray)
            {
                e.Cancel = true;
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else
            {
                // Re-save Location
                Properties.Settings.Default.LastWindowLocation = this.Location;
                Properties.Settings.Default.Save();

                // --Necessary cleanup if closing--
                Hotkeys.UnregisterHotKey(this.Handle, hotkeys.hotkeyId);
            }
        }

        // MainForm ResizeEnd Event - Detect Form Location Change (Ensures the new location is only saved once since resizing is disabled)
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastWindowLocation = this.Location;
            Properties.Settings.Default.Save();
        }

        // Check if loading Window location is visible - Helper Function
        private bool IsLocationWithinScreens(Point location)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Contains(location))
                {
                    return true;
                }
            }
            return false;
        }

        // ---Top Menu Bar---

        // Toggle Mouse Lock
        private void enableMouseMenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleMouseLock();
        }

        // Re-launch Button
        private void relaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0); // Ensuring all threads are terminated
        }

        // Exit Button
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forceClose = true;
            Application.Exit();
        }

        // Enable Auto Enable
        private void enableAutoEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enableAutoEnableToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.AutoEnable = true;
                Properties.Settings.Default.Save();

                label11.Text = "Enabled";         // Label
                label11.ForeColor = Color.DarkGreen;

                autoProcessEnable.EnableAutoEnable();
            }
            else
            {
                Properties.Settings.Default.AutoEnable = false;
                Properties.Settings.Default.Save();

                label11.Text = "Disabled";         // Label
                label11.ForeColor = Color.DarkRed;

                autoProcessEnable.DisableAutoEnable();
            }
        }

        // Open Process Editor
        private void processListEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the process editor form is either not created or has been disposed
            if (processEditorForm == null || processEditorForm.IsDisposed)
            {
                // Re-save MainForm Location
                Properties.Settings.Default.LastWindowLocation = this.Location;
                Properties.Settings.Default.Save();

                // Open ProcessEditorForm
                processEditorForm = new ProcessEditorForm();
                processEditorForm.Show();

                // Subscribe to the process editor form FormClosed event
                processEditorForm.FormClosed += (s, args) => processEditorForm = null;
            }
            else
            {
                // If the process editor form exists already bring the existing form to the front
                processEditorForm.BringToFront();
            }
        }

        // Open Quick Add Process Form
        private void quickAddProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the quick add process form is either not created or has been disposed
            if (quickProcessAddForm == null || quickProcessAddForm.IsDisposed)
            {
                // Re-save MainForm Location
                Properties.Settings.Default.LastWindowLocation = this.Location;
                Properties.Settings.Default.Save();

                // Open QuickProcessAddForm
                quickProcessAddForm = new QuickProcessAddForm();
                quickProcessAddForm.Show();

                // Subscribe to the process editor form FormClosed event
                quickProcessAddForm.FormClosed += (s, args) => quickProcessAddForm = null;
            }
            else
            {
                // If the quick add process form exists already bring the existing form to the front
                quickProcessAddForm.BringToFront();
            }
        }

        // Current Monitor with Mouse Cursor Selection
        private void currentMonitorWithMouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MonitorPreference = "CurrentMonitorWithMouse";
            Properties.Settings.Default.Save();
            primaryMonitorToolStripMenuItem.Checked = false;
        }

        // Primary Monitor Selection
        private void primaryMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MonitorPreference = "PrimaryMonitor";
            Properties.Settings.Default.Save();
            currentMonitorWithMouseToolStripMenuItem.Checked = false;
        }

        // Check for Updates On Launch
        private void checkForUpdatesOnLaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.CheckForUpdatesonLaunch)
            {
                // Set UI
                checkForUpdatesOnLaunchToolStripMenuItem.Checked = true;

                // Save to Settings
                Properties.Settings.Default.CheckForUpdatesonLaunch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                // Set UI
                checkForUpdatesOnLaunchToolStripMenuItem.Checked = false;

                // Save to Settings
                Properties.Settings.Default.CheckForUpdatesonLaunch = false;
                Properties.Settings.Default.Save();
            }
        }

        // Exit to System Tray
        private void exitToSystrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exitToSystrayToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.ExittoSystray = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.ExittoSystray = false;
                Properties.Settings.Default.Save();
            }
        }

        // Toggle Hotkeys
        private void enableHotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.EnableHotkeys)
            {
                Properties.Settings.Default.EnableHotkeys = true;
                Properties.Settings.Default.Save();

                label9.Text = "Enabled";            // Label
                label9.ForeColor = Color.DarkGreen;

                enableHotkeysToolStripMenuItem.Checked = true; // Top bar

                enableHotkeysToolStripMenuItem1.Checked = true; // Systray

                hotkeys.UpdateHotkeyRegistration(); // Enable Hotkey
            }
            else
            {
                Properties.Settings.Default.EnableHotkeys = false;
                Properties.Settings.Default.Save();

                label9.Text = "Disabled";            // Label
                label9.ForeColor = Color.DarkRed;

                enableHotkeysToolStripMenuItem.Checked = false; // Top bar

                enableHotkeysToolStripMenuItem1.Checked = false; // Systray

                Hotkeys.UnregisterHotKey(IntPtr.Zero, hotkeys.hotkeyId); // Disable Current Hotkey
            }
        }

        // Open About Form
        private void aboutMouseMenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the about form is either not created or has been disposed
            if (aboutForm == null || aboutForm.IsDisposed)
            {
                // Re-save MainForm Location
                Properties.Settings.Default.LastWindowLocation = this.Location;
                Properties.Settings.Default.Save();

                // Open AboutForm
                aboutForm = new AboutForm();
                Show();

                // Subscribe to the about form FormClosed event
                FormClosed += (s, args) => aboutForm = null;
            }
            else
            {
                // If the about form exists already bring the existing form to the front
                BringToFront();
            }
        }

        // ---System Tray---

        // Icon Double Click
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        // Toggle Mouse Lock
        private void enableMouseMenderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToggleMouseLock();
        }

        // Toggle Hotkeys
        private void enableHotkeysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.EnableHotkeys)
            {
                Properties.Settings.Default.EnableHotkeys = true;
                Properties.Settings.Default.Save();

                label9.Text = "Enabled";            // Label
                label9.ForeColor = Color.DarkGreen;

                enableHotkeysToolStripMenuItem.Checked = true; // Top bar

                enableHotkeysToolStripMenuItem1.Checked = true; // Systray

                hotkeys.UpdateHotkeyRegistration(); // Enable Hotkey
            }
            else
            {
                Properties.Settings.Default.EnableHotkeys = false;
                Properties.Settings.Default.Save();

                label9.Text = "Disabled";            // Label
                label9.ForeColor = Color.DarkRed;

                enableHotkeysToolStripMenuItem.Checked = false; // Top bar

                enableHotkeysToolStripMenuItem1.Checked = false; // Systray

                Hotkeys.UnregisterHotKey(IntPtr.Zero, hotkeys.hotkeyId); // Disable Current Hotkey
            }
        }

        // Exit Application
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            forceClose = true;
            Application.Exit();
        }

        // ---Form Buttons---

        // Enable & Disable Mouse Lock Button
        private void button1_Click(object sender, EventArgs e)
        {
            ToggleMouseLock();
            groupBox1.Focus(); // Prevent unwanted focuses
        }

        // ---Main Functions---

        // Mouse Lock/Unlock
        public void ToggleMouseLock()
        {
            // Enable Mouse Lock
            if (!isRunning)
            {
                isRunning = true;

                // Set UI
                label2.Text = "Enabled";
                label2.ForeColor = Color.DarkGreen;
                button1.Text = "Disable Mouse Lock";

                // Set Systray
                statusDisabledToolStripMenuItem.Text = "Enabled";
                enableMouseMenderToolStripMenuItem.Text = "Disable Mouse Lock";

                //rawInput.LockMouse();
                clipCursorHook.ClipCursorEnable();
            }
            else if (isRunning) //Disable Mouse Lock
            {
                isRunning = false;

                // Set UI
                label2.Text = "Disabled";
                label2.ForeColor = Color.DarkRed;
                button1.Text = "Enable Mouse Lock";

                // Set Systray
                statusDisabledToolStripMenuItem.Text = "Disabled";
                enableMouseMenderToolStripMenuItem.Text = "Enable Mouse Lock";

                //rawInput.unlockMouse();
                clipCursorHook.ClipCursorDisable();
            }
        }

        // ---Hotkeys--

        // HotKey Textbox - Enter
        private void textBox1_Enter(object sender, EventArgs e)
        {
            editingHotkey = true;
            textBox1.KeyDown += textBox1_KeyDown;
            textBox1.KeyUp += textBox1_KeyUp;

            label9.Text = "Editing";
            label9.ForeColor = Color.Black;
        }

        // HotKey Textbox - Leave
        private void textBox1_Leave(object sender, EventArgs e)
        {
            editingHotkey = false;
            textBox1.KeyDown -= textBox1_KeyDown;
            textBox1.KeyUp -= textBox1_KeyUp;

            if (Properties.Settings.Default.EnableHotkeys)
            {
                label9.Text = "Enabled";
                label9.ForeColor = Color.DarkGreen;
            }
            else
            {
                label9.Text = "Disabled";
                label9.ForeColor = Color.DarkRed;
            }
        }

        // HotKet Textbox - KeyDown
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Avoid processing non-key char like ControlKey alone
            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Menu)
            {
                return;
            }

            // Using ModifierKeys to check for Ctrl, Alt, Shift
            string modifiers = "";
            if ((Control.ModifierKeys & Keys.Control) != 0) modifiers += "Ctrl + ";
            if ((Control.ModifierKeys & Keys.Alt) != 0) modifiers += "Alt + ";
            if ((Control.ModifierKeys & Keys.Shift) != 0) modifiers += "Shift + ";

            // Process other keys
            if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
            {
                textBox1.Text = modifiers + e.KeyCode.ToString();
                e.SuppressKeyPress = true;  // Suppress further processing
            }

            // Handle special cases for Enter and Escape
            if (e.KeyCode == Keys.Return)
            {
                hotkeys.SaveHotkey();
                e.SuppressKeyPress = true;  // Suppress further processing
            }
            else if (e.KeyCode == Keys.Escape)
            {
                hotkeys.RestorePreviousHotkey();
                e.SuppressKeyPress = true;  // Suppress further processing
            }
        }

        // HotKet Textbox - KeyUp
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }

        // Auto Enable Timer Tick
        private void checkProcessTimer_Tick(object sender, EventArgs e)
        {
            autoProcessEnable.CheckForProcesses();
        }

        // ---Bottom Info Bar---

        // Click "Made By CodingCarson" label - Open To CodingCarson Youtube Channel
        private void label6_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {codingcarsonURL.Replace("&", "^&")}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                // Log error
                Debug.WriteLine("ERROR: Could not open the URL: " + ex.Message);
            }
        }
    }
}
