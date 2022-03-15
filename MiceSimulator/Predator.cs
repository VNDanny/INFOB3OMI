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

        public void move(Point dir, Size sz)
        {
            if (dir.X != 0 && dir.Y != 0)
            {
                this.position.X += dir.X * 71;
                this.position.Y += dir.Y * 71;
            }
            else
            {
                this.position.X += dir.X * 100;
                this.position.Y += dir.Y * 100;
            }

            if (this.position.X < 0)
                this.position.X = 0;
            else if (this.position.X > sz.Width)
                this.position.X = sz.Width;
            if (this.position.Y < 0)
                this.position.Y = 0;
            else if (this.position.Y > sz.Height)
                this.position.Y = sz.Height;
        }
    }
}
