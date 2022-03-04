using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiceSimulator
{
    public partial class Form1 : Form
    {
        public int hoDoMaMice;
        public int heDoMaMice;
        public int hoReMaMice;
        public int hoDoFeMice;
        public int heDoFeMice;
        public int hoReFeMice;
        public int initialPredators;
        public bool predatationON;
        public double chanceOfPredatation;
        public int offSpring;
        public int chanceOfProcreationLight;
        public int chanceOfProcreationDark;
        public List<Mouse> mice;
        public List<Predator> predators;
        public bool goOn;

        public Random rd;

        public Form1()
        {
            InitializeComponent();


        }

        public void cycle()
        {
            giveMiceDirection();
            givePredatorsDirection();
            ageMice();
            predationPhase();
            breedingPhase();
            eliminateOldGeneration();
        }

        public void ageMice()
        {
            foreach(Mouse m in mice)
            {
                m.age++;
            }
        }

        public void giveMiceDirection()
        {
            int xDir;
            int yDir;
            foreach(Mouse m in mice)
            {
                rd = new Random();
                xDir = rd.Next(-1, 1);
                yDir = rd.Next(-1, 1);
                m.move(new Point(xDir, yDir));
            }
        }

        public void givePredatorsDirection()
        {
            int xDir;
            int yDir;
            foreach (Predator p in predators)
            {
                rd = new Random();
                xDir = rd.Next(-1, 1);
                yDir = rd.Next(-1, 1);
                p.move(new Point(xDir, yDir));
            }
        }

        public void predationPhase()
        {
            //predators check if mice near.
            //calculate chance of seeing the mice.
            //eat mice if true, do nothing if false.
        }

        public void breedingPhase()
        {
            Mouse partner;
            bool success;
            foreach (Mouse m in mice)
            {
                if (!m.gender)
                {
                    partner = findEncounter(m);
                    if (partner != null)
                    {
                        rd = new Random();
                        if(partner.genoType[0] == 'A')
                        {
                            success = breeding(chanceOfProcreationDark);
                        }
                        else
                        {
                            success = breeding(chanceOfProcreationLight);
                        }

                        if (success)
                        {
                            procreate(m, partner);
                        }
                    }
                }
            }
        }

        public Mouse findEncounter(Mouse mouse)
        {
            foreach (Mouse m in mice)
            {
                if (m.gender)
                {
                    double distance = Math.Sqrt((Math.Pow(mouse.position.X - m.position.X, 2) + Math.Pow(mouse.position.Y - m.position.Y, 2)));
                    if (distance <= 50)
                        return m;
                }
            }
            return null;
        }

        public bool breeding(int breedingChance) //calculate breeding chance, return true when breeding, false if not.
        {
            rd = new Random();
            if (breedingChance <= rd.Next(1, 100))
            {
                return true;
            }
            else
            {
                return false;
            }
    
        }

        public void procreate(Mouse mouse1, Mouse mouse2)
        {
            Mouse newMouse;
            bool gender;
            string firstAllele;
            string secondAllele;
            
            for (int x = 0; x < offSpring; x++)
            {
                rd = new Random();

                newMouse = new Mouse();
            }
        }


        public void eliminateOldGeneration() //eleminates old generation of mice.
        {
            foreach (Mouse m in mice)
            {
                if(m.age >= 10)
                {
                    rd = new Random();
                    if ( 50 + (m.age - 10) * 5 <= rd.Next(1,100))
                        mice.Remove(m);
                }
            }
        }

        public bool predation(Mouse mouse)
        {
            return true;
        }

        public void eatMouse() //predator eats mouse.
        {

        }

        

    }
}
