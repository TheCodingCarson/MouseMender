using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;

namespace Mouse_Mender
{
    public partial class MainForm : Form
    {
        // Needed For Hotkey
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int WM_HOTKEY = 0x0312;
        private int hotkeyId = 1;
        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;
        private const uint MOD_ALT = 0x0001;

        // Instances & Variables
        private Version version = Assembly.GetExecutingAssembly().GetName().Version;
        private string versionFormatted;
        private AboutForm aboutForm;
        private ProcessEditorForm processEditorForm;
        private QuickProcessAddForm quickProcessAddForm;
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private Screen lockedScreen = null;
        private bool resolutionChange = false;
        private bool isRunning = false;
        private bool forceClose = false;

        // MainForm Initialization
        public MainForm()
        {
            InitializeComponent();
            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged; // Detect Resolution Changes
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

            // Register Hotkey (if enabled)
            if (Properties.Settings.Default.EnableHotkeys)
            {
                UpdateHotkeyRegistration();
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
                UnregisterHotKey(this.Handle, hotkeyId);
                SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;
            }
        }

        // MainForm LocationChanged Event
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.LastWindowLocation = this.Location;
                Properties.Settings.Default.Save();
            }
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

        // ---System Tray---

        // Icon Double Click
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }

        // Exit Button
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            forceClose = true;
            Application.Exit();
        }

        // ---Tool Strip---

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
            }
            else
            {
                Properties.Settings.Default.AutoEnable = false;
                Properties.Settings.Default.Save();

                label11.Text = "Disabled";         // Label
                label11.ForeColor = Color.DarkRed;
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
            if (exitToSystrayToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.CheckForUpdatesonLaunch = true;
                Properties.Settings.Default.Save();
            }
            else
            {
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

        // Enable Hotkeys
        private void enableHotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enableHotkeysToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.EnableHotkeys = true;
                Properties.Settings.Default.Save();

                label9.Text = "Enabled";            // Label
                label9.ForeColor = Color.DarkGreen;
                enableHotkeysToolStripMenuItem1.Checked = true; // Systray

                UpdateHotkeyRegistration(); // Enable Hotkey
            }
            else
            {
                Properties.Settings.Default.EnableHotkeys = false;
                Properties.Settings.Default.Save();

                label9.Text = "Disabled";            // Label
                label9.ForeColor = Color.DarkRed;
                enableHotkeysToolStripMenuItem1.Checked = false; // Systray

                UnregisterHotKey(this.Handle, hotkeyId); // Disable Current Hotkey
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
                aboutForm.Show();

                // Subscribe to the about form FormClosed event
                aboutForm.FormClosed += (s, args) => aboutForm = null;
            }
            else
            {
                // If the about form exists already bring the existing form to the front
                aboutForm.BringToFront();
            }
        }

        // ---Main Functions---

        // Enable & Disable Mouse Lock Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                unlockMouse();
            }
            else
            {
                LockMouse();
            }

            groupBox1.Focus(); // Prevent unwanted focuses
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ClipCursor(ref Rectangle lpRect);

        // Lock Mouse
        private void LockMouse()
        {
            isRunning = true;

            if (!resolutionChange)
            {
                lockedScreen = GetTargetMonitor();

                // Set UI
                label2.Text = "Enabled";
                label2.ForeColor = Color.DarkGreen;

                // Set Systray
                statusDisabledToolStripMenuItem.Text = "Enabled";
                enableMouseMenderToolStripMenuItem.Text = "Disable Mouse Lock";
            }

            // Lock Mouse
            if (lockedScreen == null)
                lockedScreen = GetTargetMonitor();

            if (lockedScreen != null)
            {
                Rectangle bounds = lockedScreen.Bounds;
                ClipCursor(ref bounds); // Lock the cursor within the bounds of the locked screen
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ClipCursor(IntPtr lpRect);

        // Unlock Mouse
        private void unlockMouse()
        {
            isRunning = false;

            if (!resolutionChange)
            {
                lockedScreen = null;

                // Set UI
                label2.Text = "Disabled";
                label2.ForeColor = Color.DarkRed;

                // Set Systray
                statusDisabledToolStripMenuItem.Text = "Disabled";
                enableMouseMenderToolStripMenuItem.Text = "Enable Mouse Lock";
            }           

            // Unlock Mouse
            ClipCursor(IntPtr.Zero);
        }

        // Get Screen Based On Preference
        private Screen GetTargetMonitor()
        {
            var monitorPreference = Properties.Settings.Default.MonitorPreference;
            if (monitorPreference == "CurrentMonitorWithMouse")
            {
                Point cursorPos = Cursor.Position;
                return Screen.FromPoint(cursorPos); // Get the current screen where the mouse cursor is located
            }
            else if (monitorPreference == "PrimaryMonitor")
            {
                return Screen.PrimaryScreen; // Get the primary screen
            }
            return null;
        }

        // Re-apply the ClipCursor To Account For Resolution Change
        private void OnDisplaySettingsChanged(object sender, EventArgs e)
        {
            if (isRunning && lockedScreen != null)
            {
                // Set UI
                label2.Text = "Resetting";
                label2.ForeColor = Color.DarkGoldenrod;

                resolutionChange = true;
                unlockMouse(); // Resetting Locked Screen Settings
                LockMouse();   // Re-locking the mouse cursor to the monitor
                resolutionChange = false;

                // Set UI Back
                label2.Text = "Enabled";
                label2.ForeColor = Color.DarkGreen;
            }
        }

        // ---Hotkeys--

        // HotKey Textbox - Enter
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.KeyDown += textBox1_KeyDown;
            textBox1.KeyUp += textBox1_KeyUp;

            label9.Text = "Editing";
            label9.ForeColor = Color.Black;
        }

        // HotKey Textbox - Leave
        private void textBox1_Leave(object sender, EventArgs e)
        {
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
                SaveHotkey();
                e.SuppressKeyPress = true;  // Suppress further processing
            }
            else if (e.KeyCode == Keys.Escape)
            {
                RestorePreviousHotkey();
                e.SuppressKeyPress = true;  // Suppress further processing
            }
        }

        // HotKet Textbox - KeyUp
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }

        // Save New Hotkey
        private void SaveHotkey()
        {
            Properties.Settings.Default.Hotkey = textBox1.Text;
            Properties.Settings.Default.Save();

            // Update Registered Hotkey
            if (Properties.Settings.Default.EnableHotkeys)
            {
                UpdateHotkeyRegistration();
            }

            // Shift focus to prevent re-editing
            groupBox1.Focus();
        }

        // Restore Current Hotkey
        private void RestorePreviousHotkey()
        {
            textBox1.Text = Properties.Settings.Default.Hotkey;

            // Shift focus to prevent re-editing
            groupBox1.Focus();
        }

        // Parsing Hotkey String
        private uint ParseModifiers(string modifiers)
        {
            uint modifierKeys = 0;
            if (modifiers.Contains("Ctrl"))
                modifierKeys |= MOD_CONTROL;
            if (modifiers.Contains("Alt"))
                modifierKeys |= MOD_ALT;
            if (modifiers.Contains("Shift"))
                modifierKeys |= MOD_SHIFT;
            return modifierKeys;
        }

        private Keys ParseKey(string key)
        {
            return (Keys)Enum.Parse(typeof(Keys), key);
        }

        // Register Hotkey
        private void UpdateHotkeyRegistration()
        {
            string hotkeyText = Properties.Settings.Default.Hotkey;
            var parts = hotkeyText.Split('+').Select(p => p.Trim()).ToArray();
            uint modifiers = ParseModifiers(parts[0]);  // Assuming the first part is always modifiers
            Keys key = ParseKey(parts.Length > 1 ? parts[1] : "");

            // Unregister the previous hotkey if registered
            UnregisterHotKey(this.Handle, hotkeyId);
            // Register the new hotkey
            RegisterHotKey(this.Handle, hotkeyId, modifiers, (uint)key);
        }

        // Hotkey Pressed
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                if ((int)m.WParam == hotkeyId)
                {
                    // Toggle Mouse lock
                    if (isRunning)
                    {
                        unlockMouse();
                    }
                    else
                    {
                        LockMouse();
                    }

                }
            }
        }

        // ---Bottom Info Bar---

        // Click "Made By CodingCarson" label
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
