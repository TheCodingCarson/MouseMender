namespace Mouse_Mender
{
    partial class ProcessEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessEditorForm));
            listBox1 = new ListBox();
            groupBox1 = new GroupBox();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label13 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(2, 39);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(308, 169);
            listBox1.TabIndex = 4444;
            listBox1.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlLight;
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label13);
            groupBox1.Location = new Point(0, -8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(318, 44);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(220, 15);
            button4.Name = "button4";
            button4.Size = new Size(26, 23);
            button4.TabIndex = 7777;
            button4.TabStop = false;
            button4.Text = "↓";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(252, 15);
            button3.Name = "button3";
            button3.Size = new Size(26, 23);
            button3.TabIndex = 6666;
            button3.TabStop = false;
            button3.Text = "↑";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(284, 15);
            button2.Name = "button2";
            button2.Size = new Size(26, 23);
            button2.TabIndex = 5555;
            button2.TabStop = false;
            button2.Text = "🗑️";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(179, 15);
            button1.Name = "button1";
            button1.Size = new Size(26, 23);
            button1.TabIndex = 8888;
            button1.TabStop = false;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(4, 15);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "process.exe";
            textBox1.Size = new Size(169, 23);
            textBox1.TabIndex = 9999;
            textBox1.TabStop = false;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.Transparent;
            label13.ForeColor = SystemColors.AppWorkspace;
            label13.Location = new Point(207, 18);
            label13.Name = "label13";
            label13.Size = new Size(10, 15);
            label13.TabIndex = 10;
            label13.Text = "|";
            // 
            // ProcessEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 211);
            Controls.Add(groupBox1);
            Controls.Add(listBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProcessEditorForm";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Mouse Mender - Process Editor";
            Load += ProcessEditorForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private GroupBox groupBox1;
        private Button button1;
        private TextBox textBox1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label label13;
    }
}