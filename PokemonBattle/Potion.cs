using PokemonBattle;

public class Potion : IItem
{
    public string Name => "Rappel";
    public int Cost => 100;

    public void Use(Pokemon target)
    {
        int healAmount = 25;
        target.Heal(healAmount);
        System.Console.ForegroundColor = System.ConsoleColor.Green;
        System.Console.WriteLine($"\n💊 {target.Name} récupère {healAmount} PV !");
        System.Console.ResetColor();
    }
}
