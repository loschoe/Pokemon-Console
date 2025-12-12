using System;

namespace PokemonBattle
{
    public class HealingAttack : Attack
    {
        public double HealPercent { get; }

        public HealingAttack(string name, TypePokemon type, double percent)
            : base(name, type)
        {
            HealPercent = percent;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            // Soigne un pourcentage du PV max du Pokémon
            int healAmount = (int)Math.Round(attacker.MaxPV * HealPercent);

            attacker.Heal(healAmount, Name);
        }
    }
}
