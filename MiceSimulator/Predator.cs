using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiceSimulator
{
    public class Predator
    {
        public Point position;

        public Predator(Point _position)
        {
            this.position = _position;
        }

        public void move(Point dir)
        {
            if (dir.X != 0 && dir.Y != 0)
            {
                this.position.X += dir.X * 7;
                this.position.Y += dir.Y * 7;
            }
            else
            {
                this.position.X += dir.X * 10;
                this.position.Y += dir.Y * 10;
            }
        }
    }
}
