namespace PokemonBattle;

public class HealingAttack : Attack
{
    public int HealingAmount { get; }

    public HealingAttack(string name, TypePokemon type, int healingAmount) 
        : base(name, type)
    {
        HealingAmount = healingAmount;
    }

    public override void Use(Pokemon user, Pokemon target)
    {
       if (user.IsKO())
       {
           Console.WriteLine($"{user.Name} est KO est ne peut pas utilisé {Name}!");
           return;
       }

        user.Heal(HealingAmount);
        Console.WriteLine($"{user.Name} utilise {Name} et récupère {HealingAmount} PV!");
    }

    public override void GetDescription()
    {
        Console.WriteLine($"- {Name} (Type: {Type}, Vie : {HealingAmount} PV)");
    }
}