using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Windows.Forms;

namespace DoodleJump.Classes
{
    public class Player
    {
        public Physics physics;
        public Image image, img, shootImg, rocketImg, bubbleImg, bubbleShootImg;

        public Player(int numPl)
        {
            switch (numPl)
            {
                case 1:
                    img = Properties.Resources.Player1;
                    shootImg = Properties.Resources.ShootPl1;
                    rocketImg = Properties.Resources.RocketPl1;
                    bubbleImg = Properties.Resources.BubblePl1;
                    bubbleShootImg = Properties.Resources.BubbleShootPl1;
                    image = img;
                    break;
                case 2:
                    img = Properties.Resources.Player2;
                    shootImg = Properties.Resources.ShootPl2;
                    rocketImg = Properties.Resources.RocketPl2;
                    bubbleImg = Properties.Resources.BubblePl2;
                    bubbleShootImg = Properties.Resources.BubbleShootPl2;
                    image = img;
                    break;
                case 3:
                    img = Properties.Resources.Player3;
                    shootImg = Properties.Resources.ShootPl3;
                    rocketImg = Properties.Resources.RocketPl3;
                    bubbleImg = Properties.Resources.BubblePl3;
                    bubbleShootImg = Properties.Resources.BubbleShootPl3;
                    image = img;
                    break;
                default:
                    img = Properties.Resources.Player1;
                    shootImg = Properties.Resources.ShootPl1;
                    rocketImg = Properties.Resources.RocketPl1;
                    bubbleImg = Properties.Resources.BubblePl1;
                    bubbleShootImg = Properties.Resources.BubbleShootPl1;
                    image = img;
                    break;
            }
            physics = new Physics(new PointF(120, 300), new Size(80, 80));
        }

        public void Draw(Graphics g) // отрисовка
        {
            g.DrawImage(image, physics.transform.pos.X, physics.transform.pos.Y, physics.transform.size.Width, physics.transform.size.Height);
        }
    }
}
