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
    public partial class RedRect : Form
    {
        private int widthProp;
        private int heightProp;

        public RedRect(int width, int height, int x, int y)
        {
            widthProp = width;
            heightProp = height;

            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;

            Width = width;
            Height = height;

            StartPosition = FormStartPosition.Manual;
            Location = new Point(x, y);
        }

        private void RedRect_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaintBackground(PaintEventArgs e) { /* Ignore */ }
        protected override void OnPaint(PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Red, 5);
            Rectangle rect = new Rectangle(0, 0, widthProp, heightProp);
            e.Graphics.DrawRectangle(blackPen, rect);

        }
    }
}
