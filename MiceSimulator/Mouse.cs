using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiceSimulator
{
    // Gender : male = true, female = false.
    // Genotype : "AA", "Aa" or "aa", where "A" is the dark fur and "a" is the light fur. 
    // "A" is dominant while "a" is recessive.

    public class Mouse
    {
        public bool gender;
        public string genoType;
        public Point position;
        public int age;

        public Mouse (bool _gender, string _genoType, Point _position)
        {
            this.gender = _gender;
            this.genoType = _genoType;
            this.position = _position;
            this.age = 0;
        }

        public void move(Point dir)
        {
            if(dir.X != 0 && dir.Y != 0)
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
