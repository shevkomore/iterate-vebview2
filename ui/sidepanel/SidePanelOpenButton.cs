using iterate.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iterate
{
    public partial class SidePanelOpenButton : Form
    {
        bool drag = false;
        Point relativeMousePos;
        Point startDragPos;
        IterateApplicationContext context;
        public SidePanelOpenButton(IterateApplicationContext context)
        {
            this.context = context;
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            relativeMousePos = Point.Subtract(MousePosition, new Size(Location));
            startDragPos = MousePosition;
            drag = true;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            if(Math.Abs(startDragPos.X - MousePosition.X + startDragPos.Y - MousePosition.Y) < Settings.Default.DragThreshold)
                context.OpenSidePanel();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Location = Point.Subtract(MousePosition, new Size(relativeMousePos));
            }
        }
    }
}
