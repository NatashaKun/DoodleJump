using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoodleJump.Classes
{
    public class SpPlatform
    {
        Image img;
        public Transform transform;
        public int sizeX;
        public int sizeY;
        public int plType;
        public SpPlatform(PointF pos, int plType)
        {
            switch (plType)
            {
                case 1:
                    img = Properties.Resources.Platform2;
                    break;
                case 2:
                    img = Properties.Resources.Platform1;
                    break;
            }
            sizeX = 60;
            sizeY = 20;
            transform = new Transform(pos, new Size(sizeX, sizeY));
            this.plType = plType;
        }
        public void Draw(Graphics g) // отрисовка
        {
            g.DrawImage(img, transform.pos.X, transform.pos.Y, transform.size.Width, transform.size.Height);
        }

    }
}
