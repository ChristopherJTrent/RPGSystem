namespace RPGSystem.data
{
    public class Monster
    {
        public int Health { get; set; }
        public int DamageResistance { get; private set; }

        public Monster(int _health = 10, int _damage_resistance = 3) { 
            Health= _health;
            DamageResistance= _damage_resistance;   
        }
    }
}
