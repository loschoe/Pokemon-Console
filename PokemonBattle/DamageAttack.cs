using System;

namespace PokemonBattle
{
    public class DamageAttack : Attack
    {
        public double Multiplier { get; }

        public DamageAttack(string name, TypePokemon type, double multiplier)
            : base(name, type)
        {
            Multiplier = multiplier;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            double effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);

            // Calcul des dégâts basés sur la stat d'attaque du Pokémon
            int damage = (int)Math.Round(attacker.Attack * Multiplier);

            target.Damage(Name, damage, effectiveness);
        }
    }
}
