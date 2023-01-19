namespace RPGSystem.data
{
    public class Monster
    {
        public int Health { get; private set; }
        public int DamageResistance { get; private set; }

        public Monster(int _health, int _damage_resistance) { 
            Health= _health;
            DamageResistance= _damage_resistance;   
        }
    }
}
