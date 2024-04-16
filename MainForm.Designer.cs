namespace Mouse_Mender
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            topmenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            relaunchToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            autoEnableToolStripMenuItem = new ToolStripMenuItem();
            enableAutoEnableToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            processListEditorToolStripMenuItem = new ToolStripMenuItem();
            quickAddProcessToolStripMenuItem = new ToolStripMenuItem();
            monitorPreferenceToolStripMenuItem = new ToolStripMenuItem();
            currentMonitorWithMouseToolStripMenuItem = new ToolStripMenuItem();
            primaryMonitorToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdatesOnLaunchToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToSystrayToolStripMenuItem = new ToolStripMenuItem();
            enableHotkeysToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            aboutMouseMenderToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            label5 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            button1 = new Button();
            groupBox4 = new GroupBox();
            label7 = new Label();
            label6 = new Label();
            systrayIcon = new NotifyIcon(components);
            systrayMenuStrip = new ContextMenuStrip(components);
            mouseMenderToolStripMenuItem = new ToolStripMenuItem();
            statusDisabledToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            enableMouseMenderToolStripMenuItem = new ToolStripMenuItem();
            enableHotkeysToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            checkProcessTimer = new System.Windows.Forms.Timer(components);
            topmenuStrip.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            systrayMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // topmenuStrip
            // 
            topmenuStrip.BackColor = SystemColors.ControlLight;
            topmenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, autoEnableToolStripMenuItem, monitorPreferenceToolStripMenuItem, settingsToolStripMenuItem });
            topmenuStrip.Location = new Point(0, 0);
            topmenuStrip.Name = "topmenuStrip";
            topmenuStrip.Size = new Size(413, 24);
            topmenuStrip.TabIndex = 0;
            topmenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { relaunchToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // relaunchToolStripMenuItem
            // 
            relaunchToolStripMenuItem.Name = "relaunchToolStripMenuItem";
            relaunchToolStripMenuItem.Size = new Size(128, 22);
            relaunchToolStripMenuItem.Text = "Re-launch";
            relaunchToolStripMenuItem.Click += relaunchToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(125, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(128, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // autoEnableToolStripMenuItem
            // 
            autoEnableToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { enableAutoEnableToolStripMenuItem, toolStripSeparator6, processListEditorToolStripMenuItem, quickAddProcessToolStripMenuItem });
            autoEnableToolStripMenuItem.Name = "autoEnableToolStripMenuItem";
            autoEnableToolStripMenuItem.Size = new Size(83, 20);
            autoEnableToolStripMenuItem.Text = "Auto Enable";
            // 
            // enableAutoEnableToolStripMenuItem
            // 
            enableAutoEnableToolStripMenuItem.CheckOnClick = true;
            enableAutoEnableToolStripMenuItem.Name = "enableAutoEnableToolStripMenuItem";
            enableAutoEnableToolStripMenuItem.Size = new Size(176, 22);
            enableAutoEnableToolStripMenuItem.Text = "Enable Auto Enable";
            enableAutoEnableToolStripMenuItem.Click += enableAutoEnableToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(173, 6);
            // 
            // processListEditorToolStripMenuItem
            // 
            processListEditorToolStripMenuItem.Name = "processListEditorToolStripMenuItem";
            processListEditorToolStripMenuItem.Size = new Size(176, 22);
            processListEditorToolStripMenuItem.Text = "Process List Editor";
            processListEditorToolStripMenuItem.Click += processListEditorToolStripMenuItem_Click;
            // 
            // quickAddProcessToolStripMenuItem
            // 
            quickAddProcessToolStripMenuItem.Name = "quickAddProcessToolStripMenuItem";
            quickAddProcessToolStripMenuItem.Size = new Size(176, 22);
            quickAddProcessToolStripMenuItem.Text = "Quick Add Process";
            quickAddProcessToolStripMenuItem.Click += quickAddProcessToolStripMenuItem_Click;
            // 
            // monitorPreferenceToolStripMenuItem
            // 
            monitorPreferenceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { currentMonitorWithMouseToolStripMenuItem, primaryMonitorToolStripMenuItem });
            monitorPreferenceToolStripMenuItem.Name = "monitorPreferenceToolStripMenuItem";
            monitorPreferenceToolStripMenuItem.Size = new Size(121, 20);
            monitorPreferenceToolStripMenuItem.Text = "Monitor Preference";
            // 
            // currentMonitorWithMouseToolStripMenuItem
            // 
            currentMonitorWithMouseToolStripMenuItem.CheckOnClick = true;
            currentMonitorWithMouseToolStripMenuItem.Name = "currentMonitorWithMouseToolStripMenuItem";
            currentMonitorWithMouseToolStripMenuItem.Size = new Size(227, 22);
            currentMonitorWithMouseToolStripMenuItem.Text = "Current Monitor With Mouse";
            currentMonitorWithMouseToolStripMenuItem.Click += currentMonitorWithMouseToolStripMenuItem_Click;
            // 
            // primaryMonitorToolStripMenuItem
            // 
            primaryMonitorToolStripMenuItem.CheckOnClick = true;
            primaryMonitorToolStripMenuItem.Name = "primaryMonitorToolStripMenuItem";
            primaryMonitorToolStripMenuItem.Size = new Size(227, 22);
            primaryMonitorToolStripMenuItem.Text = "Primary Monitor";
            primaryMonitorToolStripMenuItem.Click += primaryMonitorToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { checkForUpdatesOnLaunchToolStripMenuItem, toolStripSeparator2, exitToSystrayToolStripMenuItem, enableHotkeysToolStripMenuItem, toolStripSeparator5, aboutMouseMenderToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // checkForUpdatesOnLaunchToolStripMenuItem
            // 
            checkForUpdatesOnLaunchToolStripMenuItem.CheckOnClick = true;
            checkForUpdatesOnLaunchToolStripMenuItem.Name = "checkForUpdatesOnLaunchToolStripMenuItem";
            checkForUpdatesOnLaunchToolStripMenuItem.Size = new Size(232, 22);
            checkForUpdatesOnLaunchToolStripMenuItem.Text = "Check For Updates on Launch";
            checkForUpdatesOnLaunchToolStripMenuItem.Click += checkForUpdatesOnLaunchToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(229, 6);
            // 
            // exitToSystrayToolStripMenuItem
            // 
            exitToSystrayToolStripMenuItem.CheckOnClick = true;
            exitToSystrayToolStripMenuItem.Name = "exitToSystrayToolStripMenuItem";
            exitToSystrayToolStripMenuItem.Size = new Size(232, 22);
            exitToSystrayToolStripMenuItem.Text = "Exit to Systray";
            exitToSystrayToolStripMenuItem.Click += exitToSystrayToolStripMenuItem_Click;
            // 
            // enableHotkeysToolStripMenuItem
            // 
            enableHotkeysToolStripMenuItem.CheckOnClick = true;
            enableHotkeysToolStripMenuItem.Name = "enableHotkeysToolStripMenuItem";
            enableHotkeysToolStripMenuItem.Size = new Size(232, 22);
            enableHotkeysToolStripMenuItem.Text = "Enable Hotkeys";
            enableHotkeysToolStripMenuItem.Click += enableHotkeysToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(229, 6);
            // 
            // aboutMouseMenderToolStripMenuItem
            // 
            aboutMouseMenderToolStripMenuItem.Name = "aboutMouseMenderToolStripMenuItem";
            aboutMouseMenderToolStripMenuItem.Size = new Size(232, 22);
            aboutMouseMenderToolStripMenuItem.Text = "About Mouse Mender";
            aboutMouseMenderToolStripMenuItem.Click += aboutMouseMenderToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(13, 19);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 1;
            label1.Text = "Status:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(387, 45);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.Transparent;
            label13.ForeColor = SystemColors.AppWorkspace;
            label13.Location = new Point(229, 18);
            label13.Name = "label13";
            label13.Size = new Size(10, 15);
            label13.TabIndex = 9;
            label13.Text = "|";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.ForeColor = SystemColors.AppWorkspace;
            label12.Location = new Point(114, 18);
            label12.Name = "label12";
            label12.Size = new Size(10, 15);
            label12.TabIndex = 8;
            label12.Text = "|";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.CadetBlue;
            label11.Location = new Point(319, 19);
            label11.Name = "label11";
            label11.Size = new Size(44, 15);
            label11.TabIndex = 7;
            label11.Text = "NotSet";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(239, 19);
            label10.Name = "label10";
            label10.Size = new Size(74, 15);
            label10.TabIndex = 6;
            label10.Text = "Auto Enable:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.CadetBlue;
            label9.Location = new Point(180, 19);
            label9.Name = "label9";
            label9.Size = new Size(44, 15);
            label9.TabIndex = 5;
            label9.Text = "NotSet";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(121, 19);
            label8.Name = "label8";
            label8.Size = new Size(53, 15);
            label8.TabIndex = 4;
            label8.Text = "Hotkeys:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.DarkRed;
            label2.Location = new Point(62, 19);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 3;
            label2.Text = "Disabled";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 78);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(387, 71);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Hotkey Settings";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new Point(275, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(106, 54);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Info";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 32);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 1;
            label5.Text = "Cancel: Escape";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 17);
            label4.Name = "label4";
            label4.Size = new Size(84, 15);
            label4.TabIndex = 0;
            label4.Text = "Confirm: Enter";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Cursor = Cursors.Hand;
            textBox1.Location = new Point(145, 32);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Hotkey";
            textBox1.ReadOnly = true;
            textBox1.ShortcutsEnabled = false;
            textBox1.Size = new Size(124, 23);
            textBox1.TabIndex = 9999;
            textBox1.TabStop = false;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.WordWrap = false;
            textBox1.Enter += textBox1_Enter;
            textBox1.KeyDown += textBox1_KeyDown;
            textBox1.KeyUp += textBox1_KeyUp;
            textBox1.Leave += textBox1_Leave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 35);
            label3.Name = "label3";
            label3.Size = new Size(133, 15);
            label3.TabIndex = 4;
            label3.Text = "Toggle Enable/Disabled:";
            // 
            // button1
            // 
            button1.Location = new Point(12, 155);
            button1.Name = "button1";
            button1.Size = new Size(387, 23);
            button1.TabIndex = 8888;
            button1.TabStop = false;
            button1.Text = "Enable Mouse Lock";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox4
            // 
            groupBox4.BackColor = SystemColors.Control;
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(label6);
            groupBox4.Location = new Point(0, 177);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(413, 35);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(290, 15);
            label7.Name = "label7";
            label7.Size = new Size(120, 15);
            label7.TabIndex = 1;
            label7.Text = "Mouse Mender v0.0.0";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(2, 15);
            label6.Name = "label6";
            label6.Size = new Size(132, 15);
            label6.TabIndex = 0;
            label6.Text = "Made by CodingCarson";
            label6.Click += label6_Click;
            // 
            // systrayIcon
            // 
            systrayIcon.ContextMenuStrip = systrayMenuStrip;
            systrayIcon.Icon = (Icon)resources.GetObject("systrayIcon.Icon");
            systrayIcon.Text = "Mouse Mender";
            systrayIcon.Visible = true;
            systrayIcon.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // systrayMenuStrip
            // 
            systrayMenuStrip.Items.AddRange(new ToolStripItem[] { mouseMenderToolStripMenuItem, statusDisabledToolStripMenuItem, toolStripSeparator3, enableMouseMenderToolStripMenuItem, enableHotkeysToolStripMenuItem1, toolStripSeparator4, exitToolStripMenuItem1 });
            systrayMenuStrip.Name = "systrayMenuStrip";
            systrayMenuStrip.Size = new Size(177, 126);
            // 
            // mouseMenderToolStripMenuItem
            // 
            mouseMenderToolStripMenuItem.Enabled = false;
            mouseMenderToolStripMenuItem.Name = "mouseMenderToolStripMenuItem";
            mouseMenderToolStripMenuItem.Size = new Size(176, 22);
            mouseMenderToolStripMenuItem.Text = "Mouse Mender";
            // 
            // statusDisabledToolStripMenuItem
            // 
            statusDisabledToolStripMenuItem.Enabled = false;
            statusDisabledToolStripMenuItem.Name = "statusDisabledToolStripMenuItem";
            statusDisabledToolStripMenuItem.Size = new Size(176, 22);
            statusDisabledToolStripMenuItem.Text = "Status: Disabled";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(173, 6);
            // 
            // enableMouseMenderToolStripMenuItem
            // 
            enableMouseMenderToolStripMenuItem.Name = "enableMouseMenderToolStripMenuItem";
            enableMouseMenderToolStripMenuItem.Size = new Size(176, 22);
            enableMouseMenderToolStripMenuItem.Text = "Enable Mouse Lock";
            enableMouseMenderToolStripMenuItem.Click += enableMouseMenderToolStripMenuItem1_Click;
            // 
            // enableHotkeysToolStripMenuItem1
            // 
            enableHotkeysToolStripMenuItem1.CheckOnClick = true;
            enableHotkeysToolStripMenuItem1.Name = "enableHotkeysToolStripMenuItem1";
            enableHotkeysToolStripMenuItem1.Size = new Size(176, 22);
            enableHotkeysToolStripMenuItem1.Text = "Enable Hotkeys";
            enableHotkeysToolStripMenuItem1.Click += enableHotkeysToolStripMenuItem1_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(173, 6);
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(176, 22);
            exitToolStripMenuItem1.Text = "Exit";
            // 
            // checkProcessTimer
            // 
            checkProcessTimer.Interval = 1000;
            checkProcessTimer.Tick += checkProcessTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 211);
            Controls.Add(groupBox4);
            Controls.Add(button1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(topmenuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = topmenuStrip;
            MaximizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            Text = "Mouse Mender";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            LocationChanged += MainForm_LocationChanged;
            topmenuStrip.ResumeLayout(false);
            topmenuStrip.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            systrayMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem relaunchToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem monitorPreferenceToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private Label label1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label5;
        private Label label4;
        private Label label3;
        private GroupBox groupBox4;
        private Label label7;
        private Label label6;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem mouseMenderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutMouseMenderToolStripMenuItem;
        private ToolStripMenuItem autoEnableToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem processListEditorToolStripMenuItem;
        private ToolStripMenuItem quickAddProcessToolStripMenuItem;
        private Label label10;
        private Label label8;
        private Label label13;
        private Label label12;
        public ToolStripMenuItem currentMonitorWithMouseToolStripMenuItem;
        public ToolStripMenuItem primaryMonitorToolStripMenuItem;
        public ToolStripMenuItem checkForUpdatesOnLaunchToolStripMenuItem;
        public ToolStripMenuItem exitToSystrayToolStripMenuItem;
        public ToolStripMenuItem enableHotkeysToolStripMenuItem;
        public Label label2;
        public TextBox textBox1;
        public Button button1;
        public ToolStripMenuItem enableAutoEnableToolStripMenuItem;
        public Label label11;
        public Label label9;
        public System.Windows.Forms.Timer checkProcessTimer;
        public MenuStrip topmenuStrip;
        public GroupBox groupBox1;
        public ContextMenuStrip systrayMenuStrip;
        public ToolStripMenuItem enableMouseMenderToolStripMenuItem;
        public ToolStripMenuItem exitToolStripMenuItem1;
        public ToolStripMenuItem statusDisabledToolStripMenuItem;
        public ToolStripMenuItem enableHotkeysToolStripMenuItem1;
        public NotifyIcon systrayIcon;
    }
}
