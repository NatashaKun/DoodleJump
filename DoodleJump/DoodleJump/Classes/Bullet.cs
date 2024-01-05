using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoodleJump.Classes
{
    public class Bullet
    {
        public readonly Transform transform;
        public readonly Image img;

        
        public Bullet(PointF pos)
        {
            img = Properties.Resources.Bullet;
            transform = new Transform(pos, new Size(15, 15));
        }

        public void Move()// для движения пули
        {
            transform.pos.Y -= 15;
        }

        public void Draw(Graphics g) // отрисовка
        {
            g.DrawImage(img, transform.pos.X, transform.pos.Y, transform.size.Width, transform.size.Height);
        }
    }
}
