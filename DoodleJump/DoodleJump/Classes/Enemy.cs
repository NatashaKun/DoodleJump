using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoodleJump.Classes
{
    public class Enemy
    {
        public Transform transform;
        public Image image;

        public Enemy(PointF pos, int enemyType)
        {
            switch (enemyType) // тип
            {
                case 1:
                    image = Properties.Resources.Enemy1;
                    break;
                case 2:
                    image = Properties.Resources.Enemy2;
                    break;
                case 3:
                    image = Properties.Resources.Enemy3;
                    break;
                case 4:
                    image = Properties.Resources.Enemy4;
                    break;
            }
            transform = new Transform(pos, new Size(80, 80));
        }

        public void Draw(Graphics g) // отрисовка
        {
            g.DrawImage(image, transform.pos.X, transform.pos.Y, transform.size.Width, transform.size.Height);
        }

        public bool StandartCollide() // взаимодействие пули и монстра
        {
            for (int i = 0; i < Controller.bullets.Count; i++)
            {
                var bullet = Controller.bullets[i];
                PointF delta = new PointF();
                delta.X = (transform.pos.X + transform.size.Width / 2) - (bullet.transform.pos.X + bullet.transform.size.Width / 2);
                delta.Y = (transform.pos.Y + transform.size.Height / 2) - (bullet.transform.pos.Y + bullet.transform.size.Height / 2);
                if (Math.Abs(delta.X) + 10 <= transform.size.Width / 2 + bullet.transform.size.Width / 2) // если разность середин по х меньше чем сумма половин по х
                {
                    if (Math.Abs(delta.Y) <= transform.size.Height / 2 + bullet.transform.size.Height / 2) // аналогично по y
                    {
                        Controller.RemoveBullet(i); // удаление пули, которая попала
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
