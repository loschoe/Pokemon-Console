using PokemonBattle;
using System;

public class Pokeball : IItem
{
    public string Name => "Pokéball";
    public int Cost => 50;

    private static Random chance = new Random();
    private int catchRate = 50; // Pourcentage de chance de capture

    public void Use(Pokemon target)
    {
        int roll = chance.Next(0, 100);
        if (roll < catchRate)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n🎉 Félicitations ! Vous avez capturé {target.Name} !");
            Console.ResetColor();

            // Mettre les PV à 0 pour terminer le combat
            target.Heal(-target.PV);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n😢 {target.Name} a échappé à la Pokéball !");
            Console.ResetColor();
        }
    }
}
