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
            weapons.Add(new Weapon("Ergonomic", "1d8", 3, -1));
            //weapons.Add(new Weapon("Katars", "1d10", 2, -2));
            //weapons.Add(new Weapon("Bladed Gauntlet", "1d10", 4, -3));
            //weapons.Add(new Weapon("Tonfa", "1d6", 7, 0));
            //1h Keen -- Keen Weapons have a higher median roll but a larger armor penalty
            weapons.Add(new Weapon("1h Keen", "2d6", 2, -1));
            //weapons.Add(new Weapon("Dagger", "1d6", 3));
            //weapons.Add(new Weapon("Whip", "1d6", 3));
            //weapons.Add(new Weapon("Flail", "1d6", 3));
            //weapons.Add(new Weapon("Gladius", "1d6", 3));
            //2h Keen
            weapons.Add(new Weapon("2h Keen", "1d6", 3));
            //weapons.Add(new Weapon("Spiked Chain", "4d3", 3, -2));
            //weapons.Add(new Weapon("Elven Thinblade", "3d6", 1, -4));
            //Thrown
            weapons.Add(new Weapon("Thrown", "1d6", 3));
            //weapons.Add(new Weapon("Throwing Dagger", "1d6", 3));
            //weapons.Add(new Weapon("Throwing Axe", "1d6", 3));
            //weapons.Add(new Weapon("Dart", "1d6", 3));
            //Bows
            weapons.Add(new Weapon("Bows", "1d6", 3));
            //weapons.Add(new Weapon("Longbow", "1d6", 3));
            //weapons.Add(new Weapon("Great Bow", "1d6", 3));
            //Crossbows
            weapons.Add(new Weapon("Crossbows", "1d6", 3));
            //weapons.Add(new Weapon("Great Crossbow", "1d6", 3));
            //weapons.Add(new Weapon("Pistol Crossbow", "1d6", 3));
            //Guns
            weapons.Add(new Weapon("Guns", "1d6", 3));
            //weapons.Add(new Weapon("Long Rifle", "1d6", 3));
            //weapons.Add(new Weapon("Artillery", "1d6", 3));
            //1h Forceful
            weapons.Add(new Weapon("1h Forceful", "1d8", 1, 1));
            //weapons.Add(new Weapon("Mace", "1d8", 1, 1));
            //weapons.Add(new Weapon("Warhammer", "1d8", 1, 1));
            //weapons.Add(new Weapon("Club", "1d8", 1, 1));
            //weapons.Add(new Weapon("Wand", "1d8", 1, 1));
            //2h Forceful
            weapons.Add(new Weapon("2h Forceful", "1d6", 1, 2));
            //weapons.Add(new Weapon("Maul", "1d3", 1, 4));
            //Polearms - Can attack a square further than normal weapons. takes a -1 penalty to hit an enemy adjacent to the wielder
            weapons.Add(new Weapon("Polearms", "1d8", 1, 1));
            //weapons.Add(new Weapon("Spear", "1d8", 1, 1));
            //weapons.Add(new Weapon("Glaive", "1d8", 1, 1));
            //weapons.Add(new Weapon("Pole Hammer", "1d8", 1, 1));
            //weapons.Add(new Weapon("Quarterstaff", "1d8", 1, 1));
        }
    }
}
