using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Peter_Spanos_19013035_Task2
{
    [Serializable] public class Map
    {
        //Declares list to hold the units and buildings
        public List<Unit> units = new List<Unit>();
        public List<Building> buildings = new List<Building>();

        //Declares random class and other useful variables
        Random r = new Random();
        public int numUnits = 0;
        public int numBuildings = 0;
        TextBox txtInfo;

        //Fields 
        public List<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }

        public List<Building> Buildings
        {
            get { return buildings; }
            set { buildings = value; }
        }

        //Constructor
        public Map(int n, TextBox txt, int noBuilds)
        {
            units = new List<Unit>();
            buildings = new List<Building>();
            numUnits = n;
            txtInfo = txt;
            numBuildings = noBuilds;
        }

        //Handles generation of the units
        public void Generate()
        {
            for(int i = 0; i < numUnits; i++)
            {
               if(r.Next(0,2) == 0) //Generate Melee Unit
                {
                    //creates the unit's stats
                    MeleeUnit m = new MeleeUnit(r.Next(0, 10),
                                                r.Next(0, 10),
                                                100,
                                                1,
                                                20,
                                                (i % 2 == 0 ? 1 : 0),
                                                "M/");
                    units.Add(m);
                }
               else // Generate Ranged Unit
                {
                    //creates the unit's stats
                    RangedUnit ru = new RangedUnit(r.Next(0, 10),
                                                r.Next(0, 10),
                                                100,
                                                1,
                                                20,
                                                5,
                                                (i % 2 == 0 ? 1 : 0),
                                                "R}");
                    units.Add(ru);
                }
            }

            for(int k = 0; k < numBuildings; k++)
            {
                if(r.Next(0,2) == 0) //Generate Resource Building
                {
                    //creates the Building's stats
                    ResourceBuilding rb = new ResourceBuilding(r.Next(0, 10),
                                                               r.Next(0, 10),
                                                               150,
                                                               (k % 2 == 0 ? 1 : 0),
                                                               "[G]",
                                                               false);
                     buildings.Add(rb);
                }
                else //Generate Unit Building
                {
                    //creates the Building's stats
                    FactoryBuilding fb = new FactoryBuilding(r.Next(0, 10),
                                                             r.Next(0, 10),
                                                             200,
                                                             (k % 2 == 0 ? 1 : 0),
                                                             "[F]",
                                                             false,
                                                             (r.Next(0, 2) == 1 ? "Melee" : "Ranged"));

                    buildings.Add(fb);
                }
            }
        }

        //Displays the units onto the form
        public void Display(GroupBox groupBox)
        {
            //Clears form to prevent multiple instances of buttons
            groupBox.Controls.Clear();
            

            //Adding Units
            foreach(Unit u in units)
            {
                Button b = new Button();
                //Determines what type of unit to create
                if (u is MeleeUnit)
                {
                    //Creates the physical properties of the unit
                    MeleeUnit mu = (MeleeUnit)u;
                    b.Size = new Size(30, 30);
                    b.Location = new Point(mu.XPos * 30, mu.YPos * 30);
                    b.Text = mu.Symbol;
                    if (mu.Faction == 0)
                    {
                        b.ForeColor = Color.HotPink;
                    }
                    else
                    {
                        b.ForeColor = Color.Blue ;
                    }
                }
                else
                {
                    //Creates the physical properties of the unit
                    RangedUnit ru = (RangedUnit)u;
                    b.Size = new Size(30, 30);
                    b.Location = new Point(ru.XPos * 30, ru.YPos * 30);
                    b.Text = ru.Symbol;
                    if (ru.Faction == 0)
                    {
                        b.ForeColor = Color.HotPink;
                    }
                    else
                    {
                        b.ForeColor = Color.Blue;
                    }
                }
                //adds the unit's stats to the Unit_Click method
                b.Click += Unit_Click;
                //Adds the units to the groupbox
                groupBox.Controls.Add(b);
            }

            //Adding Buildings
            foreach (Building bud in buildings)
            {
                Button b = new Button();
                //Determines what type of building to create
                if (bud is ResourceBuilding)
                {
                    //Creates its physical properties
                    ResourceBuilding rb = (ResourceBuilding)bud;

                    b.Size = new Size(30, 30);
                    b.Location = new Point(rb.XPos * 30, rb.YPos * 30);
                    b.Text = rb.Symbol;
                    if (rb.Faction == 0)
                    {
                        b.ForeColor = Color.HotPink;
                    }
                    else
                    {
                        b.ForeColor = Color.Blue;
                    }
                }
                else
                {
                    FactoryBuilding fb = (FactoryBuilding)bud;

                    b.Size = new Size(30, 30);
                    b.Location = new Point(fb.XPos * 30, fb.YPos * 30);
                    b.Text = fb.Symbol;
                    if (fb.Faction == 0)
                    {
                        b.ForeColor = Color.HotPink;
                    }
                    else
                    {
                        b.ForeColor = Color.Blue;
                    }

                }
                //Adds the stats to the Building_Click method
                b.Click += Building_Click;
                //Adds the buildings to the groupbox
                groupBox.Controls.Add(b);
            }
        }

        //Adds a unit's info to the ToString
        public void Unit_Click(object sender, EventArgs e)
        {
            //Determines the button's position
            int x, y;
            Button b = (Button)sender;
            x = b.Location.X / 30;
            y = b.Location.Y / 30;

            //Does this for every unit on the map
            foreach(Unit u in units)
            {
                //Determines type of unit
                if (u is RangedUnit)
                {
                    //Calls the ToString method to display the stats
                    RangedUnit ru = (RangedUnit)u;
                    if (ru.XPos == x && ru.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = ru.ToString();
                    }
                }
                else if (u is MeleeUnit)
                {
                    //Calls the ToString method to display the stats
                    MeleeUnit mu = (MeleeUnit)u;
                    if (mu.XPos == x && mu.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = mu.ToString();
                    }
                }
            }
        }

        //Adds a building's info to the ToString
        public void Building_Click(object sender, EventArgs e)
        {
            int x, y;

            Button b = (Button)sender;
            //Determines the button's position
            x = b.Location.X / 30;
            y = b.Location.Y / 30;

            //Does this for every building on the map
            foreach (Building bud in buildings)
            {
                //Determines type of building
                if (bud is ResourceBuilding)
                {
                    //Calls the ToString method to display the stats
                    ResourceBuilding rb = (ResourceBuilding)bud;
                    if (rb.XPos == x && rb.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = rb.ToString();
                    }
                }
                else if (bud is FactoryBuilding)
                {
                    //Calls the ToString method to display the stats
                    FactoryBuilding fb = (FactoryBuilding)bud;
                    if (fb.XPos == x && fb.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = fb.ToString();
                    }
                }
            }
        }
    }
}
