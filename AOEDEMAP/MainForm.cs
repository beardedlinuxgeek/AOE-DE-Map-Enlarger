using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOEDEMAP
{
    public partial class MainForm : Form
    {
        int rectWidth, rectHeight, rectX, rectY;
        int screenNum;

        RedRect redRectForm;
        MapForm mapForm;

        public MainForm()
        {
            InitializeComponent();

            // Create toolbar
            ToolStripButton toolStripMap = new ToolStripButton();
            toolStripMap.Text = "Map";
            toolStripMap.Click += new System.EventHandler(this.toolStripMap_Click);

            ToolStripButton toolStripExit = new ToolStripButton();
            toolStripExit.Text = "Exit";
            toolStripExit.Click += new System.EventHandler(this.toolStripExit_Click);

            ToolStrip toolStrip = new System.Windows.Forms.ToolStrip();
            toolStrip.Items.Add(toolStripMap);
            toolStrip.Items.Add(toolStripExit);

            ToolStripContainer toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
            Controls.Add(toolStripContainer);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rectWidth = Properties.Settings.Default.width;
            rectHeight = Properties.Settings.Default.height;
            rectX = Properties.Settings.Default.x;
            rectY = Properties.Settings.Default.y;
            screenNum = Properties.Settings.Default.screenNum;

            textBoxWidth.Text = rectWidth.ToString();
            textBoxHeight.Text = rectHeight.ToString();
            textBoxX.Text = rectX.ToString();
            textBoxY.Text = rectY.ToString();

            // Populate screen selector
            for(int i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++)
            {
                screenComboBox.Items.Insert(i, "Screen "+i);
            }
            screenComboBox.SelectedIndex = screenNum;
        }

        private void toolStripMap_Click(object sender, EventArgs e)
        {
            if (redRectForm != null)
            {
                redRectForm.Dispose();
            }

            if (!validateInput()) return;

            mapForm = new MapForm(this);
            mapForm.Show();
            this.Hide();
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private bool validateInput()
        {
            int i;

            if (!int.TryParse(textBoxWidth.Text, out i))
            {
                MessageBox.Show("Width is not a number");
                return false;
            }
            rectWidth = i;

            if (!int.TryParse(textBoxHeight.Text, out i))
            {
                MessageBox.Show("Height is not a number");
                return false;
            }
            rectHeight = i;

            if (!int.TryParse(textBoxX.Text, out i))
            {
                MessageBox.Show("X is not a number");
                return false;
            }
            rectX = i;

            if (!int.TryParse(textBoxY.Text, out i))
            {
                MessageBox.Show("Y is not a number");
                return false;
            }
            rectY = i;

            screenNum = screenComboBox.SelectedIndex;

            return true;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if(!validateInput()) return;

            if (redRectForm != null)
            {
                redRectForm.Dispose();
            }
            redRectForm = new RedRect(rectWidth, rectHeight, rectX, rectY, screenNum);
            redRectForm.Show();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.width = rectWidth;
            Properties.Settings.Default.height = rectHeight;
            Properties.Settings.Default.x = rectX;
            Properties.Settings.Default.y = rectY;
            Properties.Settings.Default.screenNum = screenNum;
            Properties.Settings.Default.Save();
        }

        public (int, int, int, int) getCoords()
        {
            return (rectWidth, rectHeight, rectX, rectY);
        }

        public int getScreen()
        {
            return screenNum;
        }
    }
}
