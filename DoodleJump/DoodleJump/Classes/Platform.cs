using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoodleJump.Classes
{
    public class Platform
    {

        Image img;
        public Transform transform;
        public int sizeX;
        public int sizeY;
        public bool isTouched; // было ли касание

        public Platform(PointF pos)
        {
            img = Properties.Resources.Platform1;
            sizeX = 60;
            sizeY = 20;
            transform = new Transform(pos, new Size(sizeX, sizeY));
            isTouched = false;
        }

        public void Draw(Graphics g) // отрисовка
        {
            g.DrawImage(img, transform.pos.X, transform.pos.Y, transform.size.Width, transform.size.Height);
        }
    }
}
