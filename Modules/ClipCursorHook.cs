using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mouse_Mender.Modules;

public class ClipCursorHook : IDisposable
{
    private MainForm mainForm;

    private static bool clipped = false;
    private static RECT rc;
    private static IntPtr hMouseHook = IntPtr.Zero;
    private static LowLevelMouseProc _proc = MouseEvent;

    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool ClipCursor(ref RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool ClipCursor(IntPtr lpRect);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    private const int WH_MOUSE_LL = 14;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    // Constructor
    public ClipCursorHook(MainForm form)
    {
        mainForm = form;
        SystemEvents.DisplaySettingsChanged += DisplayResolutionChanged;
    }

    // IDisposable
    public void Dispose()
    {
        // Unsubscribe from the event
        SystemEvents.DisplaySettingsChanged -= DisplayResolutionChanged;

        // Disable cursor clipping if enabled
        if (clipped)
        {
            ClipCursorDisable();
        }
    }

    // Lock Mouse
    public void ClipCursorEnable()
    {
        // Lock Mouse to screen (if not already locked)
        if (!clipped)
        {
            mainForm.lockedScreen = GetTargetMonitor();
            clipped = true;

            // Create Rectangle from Screen Bounds
            rc = new RECT
            {
                Left = mainForm.lockedScreen.Bounds.Left,
                Top = mainForm.lockedScreen.Bounds.Top,
                Right = mainForm.lockedScreen.Bounds.Right,
                Bottom = mainForm.lockedScreen.Bounds.Bottom
            };

            // Hook Mouse
            if (hMouseHook == IntPtr.Zero)
            {
                using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
                using (var curModule = curProcess.MainModule)
                {
                    hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, _proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
        else // If already running when called reset
        {
            // Disable Currently Running Lock
            ClipCursorDisable();

            // -Continue Re-locking Mouse Cursor to Screen-
            mainForm.lockedScreen = GetTargetMonitor();
            clipped = true;

            // Create Rectangle from Screen Bounds
            rc = new RECT
            {
                Left = mainForm.lockedScreen.Bounds.Left,
                Top = mainForm.lockedScreen.Bounds.Top,
                Right = mainForm.lockedScreen.Bounds.Right,
                Bottom = mainForm.lockedScreen.Bounds.Bottom
            };

            // Hook Mouse
            if (hMouseHook == IntPtr.Zero)
            {
                using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
                using (var curModule = curProcess.MainModule)
                {
                    hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, _proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
    }

    // Unlock Mouse
    public void ClipCursorDisable()
    {
        mainForm.lockedScreen = null;
        clipped = false;

        // Unhook Mouse
        if (hMouseHook != IntPtr.Zero)
        {
            UnhookWindowsHookEx(hMouseHook);
            hMouseHook = IntPtr.Zero;
        }

        // Release the cursor clipping
        ClipCursor(IntPtr.Zero);
    }

    private static IntPtr MouseEvent(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && clipped)
        {
            ClipCursor(ref rc);
        }

        return CallNextHookEx(hMouseHook, nCode, wParam, lParam);
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

    // Event handler for display resolution change
    private void DisplayResolutionChanged(object sender, EventArgs e)
    {
        if (clipped)
        {
            // Reset Cursor Clip
            ClipCursorEnable();
        }
    }
}