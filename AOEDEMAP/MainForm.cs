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

            textBoxWidth.Text = rectWidth.ToString();
            textBoxHeight.Text = rectHeight.ToString();
            textBoxX.Text = rectX.ToString();
            textBoxY.Text = rectY.ToString();
        }

        private void toolStripMap_Click(object sender, EventArgs e)
        {
            if (redRectForm != null)
            {
                redRectForm.Dispose();
            }
            mapForm = new MapForm(this);
            mapForm.Show();
            this.Hide();
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            if(textBoxWidth.Text != "")
            {
                rectWidth = Int32.Parse(textBoxWidth.Text);
            }
        }

        private void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            if (textBoxHeight.Text != "")
            {
                rectHeight = Int32.Parse(textBoxHeight.Text);
            }
        }

        private void textBoxX_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX.Text != "")
            {
                rectX = Int32.Parse(textBoxX.Text);
            }
        }

        private void textBoxY_TextChanged(object sender, EventArgs e)
        {
            if (textBoxY.Text != "")
            {
                rectY = Int32.Parse(textBoxY.Text);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (redRectForm != null)
            {
                redRectForm.Dispose();
            }
            redRectForm = new RedRect(rectWidth, rectHeight, rectX, rectY);
            redRectForm.Show();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.width = rectWidth;
            Properties.Settings.Default.height = rectHeight;
            Properties.Settings.Default.x = rectX;
            Properties.Settings.Default.y = rectY;
            Properties.Settings.Default.Save();
        }

        public (int, int, int, int) getCoords()
        {
            return (rectWidth, rectHeight, rectX, rectY);
        }
    }
}
