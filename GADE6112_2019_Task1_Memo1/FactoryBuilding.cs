using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peter_Spanos_19013035_Task2
{
    [Serializable]public class FactoryBuilding : Building
    {
        //Declarations of variables
        protected string unitType;
        protected int productionSpeed;
        protected int spawnPoint;

        //Constructor for FactoryBuilding class
        public FactoryBuilding(int x, int y, int h, int f, string sy, bool des, string unT)
        {
            xPos = x;
            yPos = y;
            health = h;
            faction = f;
            symbol = sy;
            isDestroyed = des;
            unitType = unT;
        }

        //Fields
        public int XPos
        {
            get { return base.xPos; }
            set { base.xPos = value; }
        }

        public int YPos
        {
            get { return base.yPos; }
            set { base.yPos = value; }
        }

        public int Health
        {
            get { return base.health; }
            set { base.health = value; }
        }

        public int MaxHealth
        {
            get { return base.health; }
        }

        public int Faction
        {
            get { return base.faction; }
            set { base.faction = value; }
        }

        public string Symbol
        {
            get { return base.symbol; }
            set { base.symbol = value; }
        }

        public bool IsDestroyed { get; set; }


        public int ProductionSpeed
        {
            get { return productionSpeed; }
        }

        //Method for the buildings to generate units 
        public Unit BuildUnit(int factions)
        {
            //Determines Faction Type
            if(faction == 1)
            {
                //Determines what type of unit to generate
                if(unitType == "Melee")
                {
                    //Creates the unit's 
                    MeleeUnit m = new MeleeUnit(xPos,
                                                yPos--,
                                                100,
                                                1,
                                                20,
                                                1,
                                                "M/");
                    //Returns unit
                    return m;
                }
                //Does the same as abow except for ranged units
                else if(unitType == "Ranged")
                {
                    RangedUnit ru = new RangedUnit(xPos,
                                                yPos--,
                                                100,
                                                1,
                                                20,
                                                5,
                                                1,
                                                "R}");
                    return ru;
                }
            }
            //Does the same as above except for the other team
            else if (faction == 0)
            {
                if (unitType == "Melee")
                {
                    MeleeUnit m = new MeleeUnit(xPos,
                                                yPos--,
                                                100,
                                                1,
                                                20,
                                                0,
                                                "M/");
                    return m;
                }
                else if (unitType == "Ranged")
                {
                    RangedUnit ru = new RangedUnit(xPos,
                                                yPos--,
                                                100,
                                                1,
                                                20,
                                                5,
                                                0,
                                                "R}");
                    return ru;
                }
            }
            //Return default
            return null;
        }

        //Method to destroy buildings
        public override void Destruction()
        {
            symbol = ",,,";
            isDestroyed = true;
        }

        //ToString method to handle the information of Factory Buildings
        public override string ToString()
        {
            string temp = "";
            temp += "Factory Building: ";
            temp += " Symbol: {" + symbol + "}";
            temp += " Position: (" + xPos + "," + yPos + ")";
            temp += " Faction: " + faction + " Health: " + health;
            temp += (isDestroyed ? " DESTROYED!" : " STILL STANDING!");
            return temp;
        }

    }
}
