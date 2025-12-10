using PokemonBattle;

public class Potion : IItem
{
    public string Name => "Rappel";
    public int Cost => 100;

    public bool Use(Pokemon target)
    {
        int healAmount = 25;
        target.Heal(healAmount);
        System.Console.WriteLine($"{Name} récupère {healAmount} PV!");
        return false; // Une potion ne termine pas le combat
    }
}
