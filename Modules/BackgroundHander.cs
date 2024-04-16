using System.Windows.Forms;

namespace Mouse_Mender.Modules;

public class BackgroundHandler
{
    private RawInput rawInput;
    private Hotkeys hotkeys;
    private MainForm mainForm;
    public BackgroundForm hiddenForm { get; private set; }

    public BackgroundHandler(MainForm mainFormInstance, Hotkeys hotkeysInstance, RawInput rawInputInstance)
    {
        // Assign instances
        mainForm = mainFormInstance;
        hotkeys = hotkeysInstance;
        rawInput = rawInputInstance;

        // Create and Setup the hidden form
        hiddenForm = new BackgroundForm(mainForm, rawInput, hotkeys);
        hiddenForm.Show(); // Show and immediately hide to activate
        hiddenForm.Hide();

        // Set hidden form in MainForm
        mainForm.SetHiddenForm(hiddenForm);

        // Set hidden form in rawInput
        rawInput.SetHiddenForm(hiddenForm);

        // Set hidden form in hotkeys
        hotkeys.SetHiddenForm(hiddenForm);

        // Register mouse for raw input
        rawInput.RegisterMouseForRawInput();
    }
}

// Hidden Form
public class BackgroundForm : Form
{
    private RawInput rawInput;
    private Hotkeys hotkeys;
    private MainForm mainForm;

    public BackgroundForm(MainForm mainForm, RawInput rawInput, Hotkeys hotkeys)
    {
        this.mainForm = mainForm;
        this.rawInput = rawInput;
        this.hotkeys = hotkeys;

        this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        this.ShowInTaskbar = false;
        this.Opacity = 0;
        this.Load += (sender, e) => this.Hide();

        if (!this.IsHandleCreated)
        {
            this.CreateHandle(); // Force the creation of the window handle
        }
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        switch (m.Msg)
        {
            case Hotkeys.WM_HOTKEY:
                if ((int)m.WParam == hotkeys.hotkeyId)
                {
                    if (mainForm.isRunning)
                    {
                        rawInput.unlockMouse();
                    }
                    else
                    {
                        rawInput.SetHiddenForm(this);
                        rawInput.LockMouse();
                    }
                }
                break;

            case RawInput.WM_INPUT:
                rawInput.HandleRawInput(m.LParam);
                break;
        }
    }
}