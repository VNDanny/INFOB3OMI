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
        public int hoDoMaMice = 50;
        public int heDoMaMice = 50;
        public int hoReMaMice = 50;
        public int hoDoFeMice = 50;
        public int heDoFeMice = 50;
        public int hoReFeMice = 50;
        public int initialPredators = 10;


        public bool predatationON = true;
        public int chanceOfPredatationLight = 40;
        public int chanceOfPredatationDark = 25;

        public int offSpring = 4;
        public int chanceOfProcreationLight = 90;
        public int chanceOfProcreationDark = 40;
        public int chanceofProcreationBoth = 70;

        public int hoDoMaMiceCount = 0;
        public int heDoMaMiceCount = 0;
        public int hoReMaMiceCount = 0;
        public int hoDoFeMiceCount = 0;
        public int heDoFeMiceCount = 0;
        public int hoReFeMiceCount = 0;

        public int breedingCount = 0;
        public int predationWhite = 0;
        public int predationBlack = 0;
        public int oldAgeWhite = 0;
        public int oldAgeBlack = 0;
        public int procreationDeathsWhite = 0;
        public int procreationDeathsBlack = 0;


        public List<Mouse> mice;
        public List<Predator> predators;

        public bool goOn = false;
        public bool stealth = false;
        public int time;
        public Size formSize;

        public Random rd = new Random();
        public Label timeStamp = new Label();

        public Button manyCycle;
        public Button background;

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
            manyCycle = new Button();
            Button statistics = new Button();
            background = new Button();

            oneCycle.Location = new Point(this.Size.Width - 250, this.Size.Height - 75);
            manyCycle.Location = new Point(this.Size.Width - 250, this.Size.Height - 175);
            statistics.Location = new Point(this.Size.Width - 250, this.Size.Height - 275);
            background.Location = new Point(this.Size.Width - 250, 20);

            Label mouseAlive = new Label();
            Label predatorCount = new Label();

            timeStamp.Text = time.ToString();
            oneCycle.Text = "Step";
            manyCycle.Text = "Go";
            statistics.Text = "Stats";
            background.Text = "Turn into real environment";
            oneCycle.Size = buttonSize;
            manyCycle.Size = buttonSize;
            statistics.Size = buttonSize;
            background.Size = buttonSize;
            timeStamp.ForeColor = Color.Red;
            timeStamp.Font = new Font("Arial", 25, FontStyle.Regular);
            timeStamp.AutoSize = true;

            oneCycle.BackColor = Color.DarkGray;
            manyCycle.BackColor = Color.DarkGray;
            statistics.BackColor = Color.DarkGray;
            background.BackColor = Color.DarkGray;

            Controls.Add(oneCycle);
            Controls.Add(manyCycle);
            Controls.Add(timeStamp);
            Controls.Add(statistics);
            Controls.Add(background);

            oneCycle.Click += new EventHandler(OneCycle);
            manyCycle.Click += new EventHandler(ManyCycle);
            statistics.Click += new EventHandler(openStatistics);
            background.Click += new EventHandler(changeBackground);

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
                "\npredatationWhite: " + predationWhite.ToString() +
                "\npredatationBlack: " + predationBlack.ToString() +
                "\n oldAgeWhite: " + oldAgeWhite.ToString() +
                "\n oldAgeBlack: " + oldAgeBlack.ToString() +
                "\n procreationDeathsWhite: " + procreationDeathsWhite.ToString() +
                "\n procreationDeathsBlack: " + procreationDeathsBlack.ToString() +
                "\n" + miceCountResult());
        }

        public void changeBackground(object sender, EventArgs e)
        {
            stealth = !stealth;

            if (stealth)
            {
                this.BackColor = Color.FromArgb(52, 47, 51);
                background.Text = "Change into grass";
            }
            else 
            {
                this.BackColor = Color.Green;
                background.Text = "Change into real environment";
            }
        }

        public void OneCycle(object sender, EventArgs e)
        {
            cycle();
            this.Invalidate();
        }

        public void ManyCycle(object sender, EventArgs e)
        {
            goOn = !goOn;

            if (goOn)
            {
                manyCycle.Text = "Stop";
            }
            else manyCycle.Text = "Go";

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
           for (int i = mice.Count - 1; i >= 0; i--)
            {
                if (predation(mice[i]))
                {
                    if (mice[i].genoType[0] == 'A')
                    {
                        predationBlack++;
                    }
                    else predationWhite++;

                    mice.RemoveAt(i);
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
                if (!mice[i].gender)
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
            Mouse last = null;
            int maleCount = 0;
            foreach (Mouse m in mice)
            {
                if (m.gender)
                {
                    double distance = Math.Sqrt((Math.Pow(mouse.position.X - m.position.X, 2) + Math.Pow(mouse.position.Y - m.position.Y, 2)));
                    if (distance <= 75)
                    {
                        maleCount++;
                        last = m;
                        if (maleCount > 10)
                        {
                            return null;
                        }
                    }
                }
            }
            return last;
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
                char[] Alleles = { firstAllele, secondAllele };
                genoType = new string(Alleles);

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

        public string miceCountResult()
        {
            int hoDoMa = 0;
            int heDoMa = 0;
            int hoReMa = 0;
            int hoDoFe = 0;
            int heDoFe = 0;
            int hoReFe = 0;
            foreach(Mouse m in mice)
            {
                if (m.gender && m.genoType == "AA")
                    hoDoMa++;
                else if (m.gender && m.genoType == "Aa")
                    heDoMa++;
                else if (m.gender && m.genoType == "aa")
                    hoReMa++;
                else if (!m.gender && m.genoType == "AA")
                    hoDoFe++;
                else if (!m.gender && m.genoType == "Aa")
                    heDoFe++;
                else if (!m.gender && m.genoType == "aa")
                    hoReFe++;
            }
            return hoDoMa.ToString() + " hoDoMa. \n" +
                   heDoMa.ToString() + " heDoMa. \n" +
                   hoReMa.ToString() + " hoReMa. \n" +
                   hoDoFe.ToString() + " hoDoFe. \n" +
                   heDoFe.ToString() + " heDoFe. \n" +
                   hoReFe.ToString() + " hoReFe. ";


        }
    }
}
