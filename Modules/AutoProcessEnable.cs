using System.Diagnostics;

namespace Mouse_Mender.Modules;

internal class AutoProcessEnable
{
    private MainForm mainForm;

    // Constructor
    public AutoProcessEnable(MainForm mainFormInst)
    {
        mainForm = mainFormInst;
    }

    // Auto Enable Enabled
    public void EnableAutoEnable()
    {
        // Check if the AutoEnableProcessList has processes
        if (Properties.Settings.Default.AutoEnable && Properties.Settings.Default.AutoEnableProcessList != null && Properties.Settings.Default.AutoEnableProcessList.Count > 0)
        {
            // Start Auto Enable checking
            mainForm.checkProcessTimer.Start();
        }
        else
        {
            // Set Setting back to Disabled
            Properties.Settings.Default.AutoEnable = false;
            Properties.Settings.Default.Save();

            // Set UI back to Disabled
            mainForm.enableAutoEnableToolStripMenuItem.Checked = false;
            mainForm.label11.Text = "Disabled";
            mainForm.label11.ForeColor = Color.DarkRed;

            // Error for no processes in list to check
            MessageBox.Show("To use Auto Enable please add at least 1 process to the process list.", "Mouse Mender - Process List Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // Auto Enable Disabled
    public void DisableAutoEnable()
    {
        if (mainForm.checkProcessTimer != null)
        {
            mainForm.checkProcessTimer.Stop();
        }
    }

    // Restart Auto Enable
    public void RestartAutoEnableTimer()
    {
        if (Properties.Settings.Default.AutoEnable)
        {
            if (mainForm.checkProcessTimer != null)
            {
                mainForm.checkProcessTimer.Stop();
                mainForm.checkProcessTimer.Start(); // Restart the timer
            }
        }
    }

    // Check for Process & Process Closed
    public void CheckForProcesses()
    {
        bool processFound = Properties.Settings.Default.AutoEnableProcessList.Cast<string>().Any(process => Process.GetProcessesByName(process.Replace(".exe", "")).Length > 0);
        if (processFound && !mainForm.isMouseLockedByApp)
        {
            mainForm.ToggleMouseLock();
            mainForm.isMouseLockedByApp = true;
        }
        else if (!processFound && mainForm.isMouseLockedByApp)
        {
            mainForm.ToggleMouseLock();
            mainForm.isMouseLockedByApp = false;
        }
    }
}
