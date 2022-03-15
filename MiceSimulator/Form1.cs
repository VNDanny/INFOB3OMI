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
        public int hoDoMaMice = 20;
        public int heDoMaMice = 20;
        public int hoReMaMice = 40;
        public int hoDoFeMice = 20;
        public int heDoFeMice = 20;
        public int hoReFeMice = 40;
        public int initialPredators = 10;


        public bool predatationON = true;
        public int chanceOfPredatationLight = 80;
        public int chanceOfPredatationDark = 60;


        public int offSpring = 4;
        public int chanceOfProcreationLight = 100;
        public int chanceOfProcreationDark = 20;
        public int chanceofProcreationBoth = 50;

        public int breedingCount = 0;
        public int predatationWhite = 0;
        public int predatationBlack = 0;
        public int oldAgeWhite = 0;
        public int oldAgeBlack = 0;
        public int procreationDeathsWhite = 0;
        public int procreationDeathsBlack = 0;


        public List<Mouse> mice;
        public List<Predator> predators;

        public bool goOn = false;
        public int time;
        public Size formSize;

        public Random rd = new Random();
        public Label timeStamp = new Label();

        public Form1()
        {
            this.KeyPreview = true;

            time = 0;
            InitializeComponent();
            InitializeSim();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Size = Screen.PrimaryScreen.Bounds.Size;
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;

            this.BackColor = Color.Green;
            Size buttonSize = new Size(200, 50);

            Button oneCycle = new Button();
            Button manyCycle = new Button();
            Button statistics = new Button();

            oneCycle.Location = new Point(this.Size.Width - 250, this.Size.Height - 75);
            manyCycle.Location = new Point(this.Size.Width - 250, this.Size.Height - 175);
            statistics.Location = new Point(this.Size.Width - 250, this.Size.Height - 275);

            Label mouseAlive = new Label();
            Label predatorCount = new Label();

            timeStamp.Text = time.ToString();
            oneCycle.Text = "Step";
            manyCycle.Text = "Go";
            statistics.Text = "Stats";
            oneCycle.Size = buttonSize;
            manyCycle.Size = buttonSize;
            statistics.Size = buttonSize;
            timeStamp.ForeColor = Color.Red;
            timeStamp.Font = new Font("Arial", 25, FontStyle.Regular);
            timeStamp.AutoSize = true;

            oneCycle.BackColor = Color.DarkGray;
            manyCycle.BackColor = Color.DarkGray;
            statistics.BackColor = Color.DarkGray;

            Controls.Add(oneCycle);
            Controls.Add(manyCycle);
            Controls.Add(timeStamp);
            Controls.Add(statistics);

            oneCycle.Click += new EventHandler(OneCycle);
            manyCycle.Click += new EventHandler(ManyCycle);
            statistics.Click += new EventHandler(openStatistics);

            this.Paint += paintForm;
            this.KeyDown += new KeyEventHandler(buttonPress);
        }

        public void InitializeSim()
        {
            mice = new List<Mouse>();
            predators = new List<Predator>();

            for (int i = 0; i < hoDoMaMice; i++)
            {            
                mice.Add(new Mouse(true, "AA", new Point(rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Width), rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Height))));
            }
            for (int i = 0; i < heDoMaMice; i++)
            {
                mice.Add(new Mouse(true, "Aa", new Point(rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Width), rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Height))));
            }
            for (int i = 0; i < hoReMaMice; i++)
            {
                mice.Add(new Mouse(true, "aa", new Point(rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Width), rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Height))));
            }
            for (int i = 0; i < hoDoFeMice; i++)
            {
                mice.Add(new Mouse(false, "AA", new Point(rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Width), rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Height))));
            }
            for (int i = 0; i < heDoFeMice; i++)
            { 
                mice.Add(new Mouse(false, "Aa", new Point(rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Width), rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Height))));
            }
            for (int i = 0; i < hoReFeMice; i++)
            {
                mice.Add(new Mouse(false, "aa", new Point(rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Width), rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Height))));
            }
            if (predatationON)
            {
                for (int i = 0; i < initialPredators; i++)
                {
                    predators.Add(new Predator(new Point(rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Width), rd.Next(0, Screen.PrimaryScreen.Bounds.Size.Height))));
                }
            }
        }

        public void buttonPress(object o, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void paintForm(object o, PaintEventArgs pea)
        {
            Graphics g = pea.Graphics;
            foreach(Predator p in predators)
            {
                g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(new Point(p.position.X-5, p.position.Y-5), new Size(10,10)));
            }
            foreach(Mouse m in mice)
            {
                Brush b;
                if (m.genoType[0] == 'A')
                {
                    b = new SolidBrush(Color.Black);

                }
                else
                {
                    b = new SolidBrush(Color.White);
                }

                g.FillEllipse(b, new Rectangle(new Point(m.position.X - 4, m.position.Y - 4), new Size(8, 8)));

            }

            timeStamp.Text = time.ToString();
            g.FillRectangle(new SolidBrush(Color.Gray), this.Size.Width - 300, this.Size.Height - 300, 300, 300);
            
        }

        public void openStatistics(object sender, EventArgs e)
        {
            /* public int breedingCount = 0;
        public int predatationWhite = 0;
        public int predatationBlack = 0;
        public int oldAgeWhite = 0;
        public int oldAgeBlack = 0;
        public int procreationDeaths = 0; */
        System.Windows.Forms.MessageBox.Show(
            "Breeding Count: " + breedingCount.ToString() + 
            "\npredatationWhite: " + predatationWhite.ToString() + 
            "\npredatationBlack: " + predatationBlack.ToString() + 
            "\n oldAgeWhite: " + oldAgeWhite.ToString() + 
            "\n oldAgeBlack: " + oldAgeBlack.ToString() + 
            "\n procreationDeathsWhite: " + procreationDeathsWhite.ToString() + 
            "\n procreationDeathsBlack: " + procreationDeathsBlack.ToString());
        }

        public void OneCycle(object sender, EventArgs e)
        {
            cycle();
            this.Invalidate();
        }

        public void ManyCycle(object sender, EventArgs e)
        {
            goOn = !goOn;
            while(goOn)
            {
                cycle();
                this.Invalidate();
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
            }
            
        }

        public void cycle()
        {
            giveMiceDirection();
            ageMice();

            if (predatationON)
            {
                givePredatorsDirection();
                predationPhase();
            }

            breedingPhase();
            eliminateOldGeneration();
            time++;
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
            foreach (Mouse m in mice)
            {              
                xDir = rd.Next(-1, 2);
                yDir = rd.Next(-1, 2);
                m.move(new Point(xDir, yDir), this.Size);
            }
        }

        public void givePredatorsDirection()
        {
            int xDir;
            int yDir;
            foreach (Predator p in predators)
            {              
                xDir = rd.Next(-1, 2);
                yDir = rd.Next(-1, 2);
                p.move(new Point(xDir, yDir), this.Size);
            }
        }

        public void predationPhase()
        {
            foreach(Predator p in predators)
            {
                for (int i = mice.Count - 1; i >= 0; i--)
                {
                    double distance = Math.Sqrt((Math.Pow(p.position.X - mice[i].position.X, 2) + Math.Pow(p.position.Y - mice[i].position.Y, 2)));
                    if (distance <= 50)
                    {
                        if (predation(mice[i]))
                        {
                            if (mice[i].genoType[0] == 'A')
                            {
                                predatationBlack++;
                            }
                            else
                            {
                                predatationWhite++;
                            }
                            mice.Remove(mice[i]);
                        }
                    }
                }
            }
            //predators check if mice near.
            //calculate chance of seeing the mice.
            //eat mice if true, do nothing if false.
        }

        public void breedingPhase()
        {
            Mouse partner;
            bool success;
            for (int i = mice.Count - 1; i >= 0; i--)
            {
                if (!mice[i].gender && mice[i].age > 3)
                {
                    partner = findEncounter(mice[i]);
                    if (partner != null)
                    {
                        if (partner.genoType[0] == 'A' && mice[i].genoType[0] == 'A')
                        {
                            success = breeding(chanceOfProcreationDark);
                        }
                        else if (partner.genoType[0] == 'a' && mice[i].genoType[0] == 'a')
                        {
                            success = breeding(chanceOfProcreationLight);
                        }
                        else
                        {
                            success = breeding(chanceOfProcreationLight);
                        }


                        if (success)
                        {
                            procreate(mice[i], partner);

                            if (mice[i].genoType[0] == 'A')
                            {
                                procreationDeathsBlack++;
                            }
                            else
                            {
                                procreationDeathsWhite++;
                            }

                            if (partner.genoType[0] == 'A')
                            {
                                procreationDeathsBlack++;
                            }
                            else
                            {
                                procreationDeathsWhite++;
                            }

                            mice.RemoveAt(i);
                            mice.Remove(partner);

                            breedingCount = breedingCount + offSpring;
                        }
                    }
                }
            }
        }

        public Mouse findEncounter(Mouse mouse)
        {
            foreach (Mouse m in mice)
            {
                if (m.gender && m.age > 3)
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
            if (breedingChance >= rd.Next(1, 100))
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
            char firstAllele;
            char secondAllele;
            string genoType;

            for (int x = 0; x < offSpring; x++)
            { 
                if (rd.Next(1, 3) == 1)
                    gender = true; //male
                else
                    gender = false; //female

                if(rd.Next(1, 3) == 1)
                    firstAllele = mouse1.genoType[0];
                else
                    firstAllele = mouse1.genoType[1];


                if (rd.Next(1, 3) == 1)
                    secondAllele = mouse2.genoType[0];
                else
                    secondAllele = mouse2.genoType[1];

                if (secondAllele == 'A')
                {
                    secondAllele = firstAllele;
                    firstAllele = 'A';
                }
                genoType = new string(firstAllele, secondAllele);

                newMouse = new Mouse(gender, genoType, mouse1.position);

                mice.Add(newMouse);


            }
        }


        public void eliminateOldGeneration() //eleminates old generation of mice.
        {
            for (int i = mice.Count - 1; i>=0; i--)
            {
                if(mice[i].age >= 10)
                {
                    if (50 + (mice[i].age - 10) * 5 >= rd.Next(1, 100))
                    {
                        if (mice[i].genoType[0] == 'A')
                        {
                            oldAgeBlack++;
                        }
                        else
                        {
                            oldAgeWhite++;
                        }
                        mice.RemoveAt(i);
                    }
                }
            }
        }

        public bool predation(Mouse mouse)
        {
            int predationChance;
            if (mouse.genoType[0] == 'A')
                predationChance = chanceOfPredatationDark;
            else
                predationChance = chanceOfPredatationLight;

            if (predationChance >= rd.Next(1, 100))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
