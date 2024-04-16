using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mouse_Mender.Modules;

public class RawInput
{
    private MainForm mainForm;
    private Form hiddenForm;

    // Raw Input Declare
    [DllImport("user32.dll")]
    static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

    public const int WM_INPUT = 0x00FF;
    private const int RIM_TYPEMOUSE = 0;
    private const int RID_INPUT = 0x10000003;
    private const int RIDEV_INPUTSINK = 0x00000100;

    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTHEADER
    {
        public uint dwType;
        public uint dwSize;
        public IntPtr hDevice;
        public IntPtr wParam;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct RAWMOUSE
    {
        [FieldOffset(0)] public ushort usFlags;
        [FieldOffset(4)] public uint ulButtons;
        [FieldOffset(4)] public ushort usButtonFlags;
        [FieldOffset(6)] public ushort usButtonData;
        [FieldOffset(8)] public uint ulRawButtons;
        [FieldOffset(12)] public int lLastX;
        [FieldOffset(16)] public int lLastY;
        [FieldOffset(20)] public uint ulExtraInformation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUT
    {
        public RAWINPUTHEADER header;
        public RAWMOUSE mouse;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RAWINPUTDEVICE
    {
        public ushort usUsagePage;
        public ushort usUsage;
        public uint dwFlags;
        public IntPtr hwndTarget;
    }
    // ---

    public RawInput(MainForm form)
    {
        mainForm = form;
    }

    public void SetHiddenForm(Form form)
    {
        hiddenForm = form;
    }

    // Lock Mouse
    public void LockMouse()
    {
        mainForm.isRunning = true;
        mainForm.lockedScreen = GetTargetMonitor();

        // Set UI
        mainForm.label2.Text = "Enabled";
        mainForm.label2.ForeColor = Color.DarkGreen;
        mainForm.button1.Text = "Disable Mouse Lock";

        // Set Systray
        mainForm.statusDisabledToolStripMenuItem.Text = "Enabled";
        mainForm.enableMouseMenderToolStripMenuItem.Text = "Disable Mouse Lock";

        // Lock Mouse
        RegisterMouseForRawInput();  // Start capturing raw input

    }

    // Unlock Mouse
    public void unlockMouse()
    {
        mainForm.isRunning = false;
        mainForm.lockedScreen = null;

        // Set UI
        mainForm.label2.Text = "Disabled";
        mainForm.label2.ForeColor = Color.DarkRed;
        mainForm.button1.Text = "Enable Mouse Lock";

        // Set Systray
        mainForm.statusDisabledToolStripMenuItem.Text = "Disabled";
        mainForm.enableMouseMenderToolStripMenuItem.Text = "Enable Mouse Lock";

        // Unlock Mouse
        UnregisterMouseForRawInput();  // Stop capturing raw input
    }

    // Register Raw Inputs
    public void RegisterMouseForRawInput()
    {
        RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];
        rid[0].usUsagePage = 0x01; // Usage page for Generic Desktop Controls
        rid[0].usUsage = 0x02; // Usage for Mouse
        rid[0].dwFlags = RIDEV_INPUTSINK; // Receive system-wide events
        rid[0].hwndTarget = hiddenForm.Handle; // Follows keyboard focus

        if (!RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
        {
            int error = Marshal.GetLastWin32Error();
            throw new ApplicationException("Failed to register raw input device(s). Error: " + error);
        }
    }

    // Unregister Raw Inputs
    private void UnregisterMouseForRawInput()
    {
        RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];
        rid[0].usUsagePage = 0x01;  // Usage page for Generic Desktop Controls
        rid[0].usUsage = 0x02;      // Usage for Mouse
        rid[0].dwFlags = 0x00000001; // RIDEV_REMOVE: Specifies the topmost removal of the device
        rid[0].hwndTarget = IntPtr.Zero;

        if (!RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
        {
            throw new ApplicationException("Failed to unregister raw input device(s).");
        }
    }

    // Handle Raw Inputs
    [DllImport("user32.dll")]
    static extern int GetRawInputData(IntPtr hRawInput, uint uiCommand, out RAWINPUT pData, ref uint pcbSize, uint cbSizeHeader);
    public void HandleRawInput(IntPtr lParam)
    {
        uint dwSize = 0;
        GetRawInputData(lParam, RID_INPUT, out RAWINPUT input, ref dwSize, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

        if (input.header.dwType == RIM_TYPEMOUSE && mainForm.isRunning)
        {
            Point currentPos = Cursor.Position;
            Point newPos = new Point(currentPos.X + input.mouse.lLastX, currentPos.Y + input.mouse.lLastY);

            // Check if the new position is within the bounds of the locked screen
            if (!IsCursorWithinTargetMonitor(newPos))
            {
                Cursor.Position = ClampToScreen(newPos, mainForm.lockedScreen.Bounds);
            }
        }
    }

    // Check if Cursor is in monitor
    private bool IsCursorWithinTargetMonitor(Point position)
    {
        Screen targetMonitor = GetTargetMonitor();
        return targetMonitor.Bounds.Contains(position);
    }

    // Helper method to clamp the cursor position within the screen bounds
    private Point ClampToScreen(Point position, Rectangle bounds)
    {
        int x = Math.Max(bounds.Left, Math.Min(bounds.Right - 1, position.X));
        int y = Math.Max(bounds.Top, Math.Min(bounds.Bottom - 1, position.Y));
        return new Point(x, y);
    }

    // Get Screen Based On Preference
    public Screen GetTargetMonitor()
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
}