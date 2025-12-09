namespace PokemonBattle
{
    public class HealingAttack : Attack
    {
        public int HealAmount { get; }

        public HealingAttack(string name, TypePokemon type, int amount) : base(name, type)
        {
            HealAmount = amount;
        }

        public override void Use(Pokemon attacker, Pokemon target)
        {
            attacker.Heal(HealAmount, Name);
        }
    }
}
