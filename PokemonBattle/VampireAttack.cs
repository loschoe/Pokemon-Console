namespace PokemonBattle
{
    public class VampireAttack : Attack
    {
        public int DrainAmount { get; }

        public VampireAttack(string name, TypePokemon type, int drainAmount)
            : base(name, type)
        {
            DrainAmount = drainAmount;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            int damage = Math.Max(0, DrainAmount + attacker.Attack - target.Defense);
            double effectiveness = TypeHelper.GetEffectiveness(Type, target.Type);

            target.Damage(Name, damage, effectiveness);
            attacker.Heal(damage / 2, Name);
        }
    }
}