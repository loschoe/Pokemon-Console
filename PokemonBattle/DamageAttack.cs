using System;

namespace PokemonBattle
{
    public class DamageAttack : Attack
    {
        public int Power { get; }

        public DamageAttack(string name, TypePokemon type, int power) : base(name, type)
        {
            Power = power;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            double effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);

            int damage = (Power + attacker.Attack);
            
            //Console.WriteLine(effectiveness);
            target.Damage(Name, damage, effectiveness);
        }
    }
}
