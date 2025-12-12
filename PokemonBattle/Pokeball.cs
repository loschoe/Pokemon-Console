using PokemonBattle;
using System;
using System.Threading;

public class Pokeball : IItem
{
    public string Name => "Pokéball";
    public int Cost => 100;

    private static Random chance = new Random();
    private int catchRate = 50;

    public bool Use(Pokemon target)
    {
        int roll = chance.Next(0, 100);

        if (roll < catchRate)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n🎉 Félicitations ! Vous avez capturé {target.Name} !");
            Console.ResetColor();

            return true; // ✅ capture réussie
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n😢 {target.Name} a échappé à la Pokéball !");
            Console.ResetColor();

            Thread.Sleep(1000);
            Console.Clear();

            return false; // ❌ échec
        }
    }
}
