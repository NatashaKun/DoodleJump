using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoodleJump.Classes
{
    public class Bonus
    {
        public Transform transform;
        public Image image;
        public int bonusType;

        public Bonus(PointF pos, int bonusType)
        {
            switch (bonusType)
            {
                case 1: // щит
                    image = Properties.Resources.Bubble;
                    transform = new Transform(pos, new Size(40, 40));
                    break;
                case 2: // ракета
                    image = Properties.Resources.Rocket;
                    transform = new Transform(pos, new Size(60, 60));
                    break;
                case 3: // пружина
                    image = Properties.Resources.Spring;
                    transform = new Transform(pos, new Size(20, 20));
                    break;
            }
            this.bonusType = bonusType;
        }

        public void Draw(Graphics g) // отрисовка
        {
            g.DrawImage(image, transform.pos.X, transform.pos.Y, transform.size.Width, transform.size.Height);
        }
    }
}
