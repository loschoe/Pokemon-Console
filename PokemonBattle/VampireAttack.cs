using System;

namespace PokemonBattle
{
    public class VampireAttack : Attack
    {
        public int Power { get; }

        public VampireAttack(string name, TypePokemon type, int power) : base(name, type) 
        {
            Power = power;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            int damage = Math.Max(0, Power + attacker.Attack - target.Defense);
            double effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);

            target.Damage(Name, damage, effectiveness);
            attacker.Heal(damage / 2, Name);
        }
    }
}
