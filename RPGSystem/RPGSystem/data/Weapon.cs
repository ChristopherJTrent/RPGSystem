using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RPGSystem.data
{
    public class Weapon
    {
        public string Name { get; set; }
        public Roller Dice { get; set; }
        public int AttacksPerRound { get; set;}
        public int ArmorPenetration { get; set; }
        public bool updated { get; set; }
        public Weapon(string _name, Roller _dice, int _attacks_per_round, int ArmorPenetration = 0)
        {
            Name = _name;
            Dice = _dice;
            AttacksPerRound = _attacks_per_round;
            this.ArmorPenetration = ArmorPenetration;
            updated = true;
        }

        //defines a secondary constructor that takes a string definition for the dice instead of a created roller object.
        public Weapon(string _name, string dice, int _attacks_per_round, int ArmorPenetration = 0) 
            : this(_name, new Roller(dice), _attacks_per_round, ArmorPenetration) { }
        public int getDamage(Monster m)
        {
            int damage = Dice.Roll();
            int modifiedDefenceRating = Math.Max(m.DamageResistance - ArmorPenetration, 0);
            return damage > modifiedDefenceRating ? damage - modifiedDefenceRating : 0;
        }
    }
}
