using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DoodleJump.Classes
{
    public class Transform
    {
        public PointF pos; // координата
        public Size size; // размер

        public Transform(PointF pos, Size size)
        {
            this.pos = pos;
            this.size = size;
        }
    }
}
