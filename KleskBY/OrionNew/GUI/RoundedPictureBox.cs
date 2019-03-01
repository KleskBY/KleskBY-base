using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Orion.GUI
{
    public partial class RoundedPictureBox : PictureBox
    {
        public RoundedPictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(2, 2);
            InitializeComponent();
        //    this.BackColor = Color.DarkGray;
            this.Controls.Add(pictureBox);
          //  this.Controls.Add(PictureBox);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                gp.CloseFigure();
                this.Region = new Region(gp);
            }
        }
    }
}
