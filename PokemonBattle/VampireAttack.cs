using System;

namespace PokemonBattle
{
    public class VampireAttack : Attack
    {
        public double DrainPercent { get; }

        public VampireAttack(string name, TypePokemon type, double drainPercent)
            : base(name, type)
        {
            DrainPercent = drainPercent;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            // Dégâts calculés comme une attaque simple
            int damage = (int)Math.Round(attacker.Attack * 1.0);

            double effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);

            target.Damage(Name, damage, effectiveness);

            // Le Pokémon récupère un pourcentage des dégâts infligés
            int healAmount = (int)Math.Round(damage * DrainPercent);

            attacker.Heal(healAmount, Name);
        }
    }
}
