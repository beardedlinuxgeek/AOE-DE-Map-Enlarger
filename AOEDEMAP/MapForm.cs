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
    public partial class MapForm : Form
    {
        MainForm mainForm;
        int rectWidth, rectHeight, rectX, rectY;
        Rectangle rect;

        private Timer imageLoadTimer;
        private Image mScreenImage = null;

        public MapForm(MainForm mainForm)
        {
            InitializeComponent();

            DoubleBuffered = true;

            this.mainForm = mainForm;
            (rectWidth, rectHeight, rectX, rectY) = this.mainForm.getCoords();
            rect = new Rectangle(rectX, rectY, rectWidth, rectHeight);

            imageLoadTimer = new Timer();
            imageLoadTimer.Enabled = true;
            imageLoadTimer.Interval = 200;
            imageLoadTimer.Tick += new EventHandler(HandleTimer);

            Width = rectWidth *2;
            Height = rectHeight *2;
        }

        private void MapForm_Load(object sender, EventArgs e)
        {

        }

        void HandleTimer(object sender, EventArgs e)
        {
            if (mScreenImage != null)
            {
                mScreenImage.Dispose();
            }

            int screenTop = SystemInformation.VirtualScreen.Top;
            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;
            mScreenImage = new Bitmap(screenWidth, screenHeight);
            Graphics g = Graphics.FromImage(mScreenImage);
            g.CopyFromScreen(Screen.AllScreens[this.mainForm.getScreen()].Bounds.X, screenTop, 0, 0, mScreenImage.Size);
            g.Dispose();

            Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g;
            g = e.Graphics;

            if (mScreenImage != null)
            {
                Rectangle dest = new Rectangle(0, 0, Width, Height);
                int w = rectWidth;
                int h = rectHeight;
                int x = rectX;
                int y = rectY;

                g.DrawImage(
                    mScreenImage,
                    dest,
                    x, y,
                    w, h,
                    GraphicsUnit.Pixel);
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            mainForm.Show();
        }
    }
}
