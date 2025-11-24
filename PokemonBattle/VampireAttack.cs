using System;

namespace PokemonBattle
{
    public class VampireAttack : DamageAttack
    {
        public double VampireCoefficient { get; }

        public VampireAttack(string name, TypePokemon type, int damage, double vampireCoefficient = 0.5)
            : base(name, type, damage)
        {
            VampireCoefficient = vampireCoefficient;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            Console.WriteLine($"{attacker.GetStyledName()} utilise {Name} !");

            // Calcul de l'efficacité selon le type
            var effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);

            // Calcul des dégâts
            int damageDealt = (int)(Damage * effectiveness);
            if (damageDealt < 0) damageDealt = 0;

            // Inflige les dégâts
            target.Damage(Name, damageDealt, effectiveness);

            // Effet vampire : récupère une partie des dégâts
            int healAmount = (int)(damageDealt * VampireCoefficient);
            if (healAmount > 0)
            {
                attacker.Heal(healAmount);
                Console.WriteLine($"{attacker.GetStyledName()} récupère {healAmount} PV grâce à l'effet vampire !");
            }
        }

        public override void GetDescription()
        {
            Console.WriteLine($"- {Name} (Type: {Type}, Dégâts: {Damage}, Récupère {VampireCoefficient * 100}% des dégâts infligés)");
        }
    }
}
