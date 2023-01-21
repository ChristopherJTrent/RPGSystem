using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGSystem.data
{
    public static class WeaponDefs
    {
        public static List<Weapon> weapons = new List<Weapon>();
        public static void Init()
        {
            //Ergonomic weapons only roll 1 die, but it's large, and they have a penalty to armor penetration
            //They also make a larger number of attacks per round.
            weapons.Add(new Weapon("Brass Knuckles", new Roller("1d8"), 3, -1));
            weapons.Add(new Weapon("Katars", new Roller("1d10"), 2, -2));
            weapons.Add(new Weapon("Bladed Gauntlet", new Roller("1d10"), 4, -3));
            weapons.Add(new Weapon("Tonfa", new Roller("1d6"), 7, 0));
            //1h Keen -- Keen Weapons have a higher median roll but a larger armor penalty
            weapons.Add(new Weapon("Smallsword", new Roller("3d4"), 2, -2));
            weapons.Add(new Weapon("Dagger", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Whip", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Flail", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Gladius", new Roller("1d6"), 3));
            //2h Keen
            weapons.Add(new Weapon("Estoc", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Spiked Chain", new Roller("4d3"), 3, -1));
            weapons.Add(new Weapon("Elven Thinblade", new Roller("3d6"), 1, -4));
            //Thrown
            weapons.Add(new Weapon("Chakram", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Throwing Dagger", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Throwing Axe", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Dart", new Roller("1d6"), 3));
            //Bows
            weapons.Add(new Weapon("Shortbow", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Longbow", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Great Bow", new Roller("1d6"), 3));
            //Crossbows
            weapons.Add(new Weapon("Crossbow", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Great Crossbow", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Pistol Crossbow", new Roller("1d6"), 3));
            //Guns
            weapons.Add(new Weapon("Blunderbuss", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Long Rifle", new Roller("1d6"), 3));
            weapons.Add(new Weapon("Artillery", new Roller("1d6"), 3));
            //1h Forceful
            weapons.Add(new Weapon("Longsword", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Mace", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Warhammer", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Club", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Wand", new Roller("1d8"), 1, 1));
            //2h Forceful
            weapons.Add(new Weapon("Greatsword", new Roller("1d6"), 1, 2));
            weapons.Add(new Weapon("Maul", new Roller("1d3"), 1, 4));
            //Polearms - Can attack a square further than normal weapons. takes a -1 penalty to hit an enemy adjacent to the wielder
            weapons.Add(new Weapon("Lance", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Spear", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Glaive", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Pole Hammer", new Roller("1d8"), 1, 1));
            weapons.Add(new Weapon("Quarterstaff", new Roller("1d8"), 1, 1));
        }
    }
}
