using System.Runtime.InteropServices;

namespace Mouse_Mender.Modules;

public class Hotkeys
{
    //Declaring
    private MainForm mainForm;
    private Form hiddenForm;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    public const int WM_HOTKEY = 0x0312;
    public int hotkeyId = 1;
    private const uint MOD_CONTROL = 0x0002;
    private const uint MOD_SHIFT = 0x0004;
    private const uint MOD_ALT = 0x0001;

    // Constructor
    public Hotkeys(MainForm mainFormInst)
    {
        mainForm = mainFormInst;
    }

    // Get Background Form
    public void SetHiddenForm(Form form)
    {
        hiddenForm = form;
    }

    // Save New Hotkey
    public void SaveHotkey()
    {
        Properties.Settings.Default.Hotkey = mainForm.textBox1.Text;
        Properties.Settings.Default.Save();

        // Update Registered Hotkey
        if (Properties.Settings.Default.EnableHotkeys)
        {
            UpdateHotkeyRegistration();
        }

        // Shift focus to prevent re-editing
        mainForm.groupBox1.Focus();
    }

    // Restore Current Hotkey
    public void RestorePreviousHotkey()
    {
        mainForm.textBox1.Text = Properties.Settings.Default.Hotkey;

        // Shift focus to prevent re-editing
        mainForm.groupBox1.Focus();
    }

    // Parsing Hotkey String
    private uint ParseModifiers(string part, out bool isModifier)
    {
        uint modifierKeys = 0;
        isModifier = false;

        if (part.Contains("Ctrl"))
        {
            modifierKeys |= MOD_CONTROL;
            isModifier = true;
        }
        if (part.Contains("Alt"))
        {
            modifierKeys |= MOD_ALT;
            isModifier = true;
        }
        if (part.Contains("Shift"))
        {
            modifierKeys |= MOD_SHIFT;
            isModifier = true;
        }

        return modifierKeys;
    }

    private Keys ParseKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key string is null or whitespace.", nameof(key));

        try
        {
            return (Keys)Enum.Parse(typeof(Keys), key, true); // Case insensitive parsing
        }
        catch (ArgumentException)
        {
            Console.WriteLine($"Invalid key '{key}'. Ensure it matches a valid key name in the Keys enum.");
            throw;
        }
    }

    // Register Hotkey
    public void UpdateHotkeyRegistration()
    {
        string hotkeyText = Properties.Settings.Default.Hotkey;
        var parts = hotkeyText.Split('+').Select(p => p.Trim()).ToArray();
        uint modifiers = 0;
        Keys key = Keys.None;

        if (parts.Length == 1)
        {
            key = ParseKey(parts[0]);
        }
        else if (parts.Length == 2)
        {
            bool isModifier;
            modifiers = ParseModifiers(parts[0], out isModifier);

            if (!isModifier && Enum.IsDefined(typeof(Keys), parts[0]))
            {
                // If the first part is not a modifier but a valid key, parse it as a key
                key = ParseKey(parts[0]);
                // Attempt to parse the second part as a modifier if the first part wasn't
                uint secondPartModifier = ParseModifiers(parts[1], out isModifier);
                if (isModifier)
                {
                    modifiers = secondPartModifier;
                }
                else
                {
                    key = ParseKey(parts[1]); // Second part must be a valid key if the first wasn't a modifier
                }
            }
            else
            {
                // First part was a modifier, parse second part as a key
                key = ParseKey(parts[1]);
            }
        }
        else
        {
            throw new ArgumentException("Hotkey format is incorrect. It must be 'Key' or 'Modifier+Key'.");
        }

        UnregisterHotKey(hiddenForm.Handle, hotkeyId);  // Always unregister before registering a new one
        if (!RegisterHotKey(hiddenForm.Handle, hotkeyId, modifiers, (uint)key))
        {
            Console.WriteLine($"Failed to register hotkey {hotkeyText}. Error code: {Marshal.GetLastWin32Error()}");
        }
    }
}
