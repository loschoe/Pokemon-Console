using System;

namespace PokemonBattle
{
    public class DamageAttack : Attack
    {
        public int Damage { get; }

        public DamageAttack(string name, TypePokemon type, int damage)
            : base(name, type)
        {
            Damage = damage;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            Console.WriteLine($"{attacker.Name} utilise {Name}!");
            var effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);
            var degatsFinaux = (int)(Damage * effectiveness);
            if (degatsFinaux < 0) degatsFinaux = 0;

            target.Damage(Name, degatsFinaux, effectiveness);
        }

        public override void GetDescription()
        {
            Console.WriteLine($"- {Name} (Type: {Type}, Damage: {Damage})");
        }
    }
}
