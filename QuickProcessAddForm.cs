using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mouse_Mender
{
    public partial class QuickProcessAddForm : Form
    {
        public QuickProcessAddForm()
        {
            InitializeComponent();
        }

        // Add Process
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && textBox1.Text != "process.exe")
            {

                // Add the new item to the settings collection
                if (Properties.Settings.Default.AutoEnableProcessList == null)
                {
                    Properties.Settings.Default.AutoEnableProcessList = new System.Collections.Specialized.StringCollection();
                }
                Properties.Settings.Default.AutoEnableProcessList.Add(textBox1.Text);
                Properties.Settings.Default.Save();

                // Close Form
                this.Close();
            }
            else
            {
                // Show a message box if the input is invalid
                MessageBox.Show("Please enter a valid process name. Format is 'processname.exe'", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
