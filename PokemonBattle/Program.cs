using PokemonBattle;

class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n⚔️  Bienvenue dans la console de combat Pokémon !");

        Pokemon pokemon1 = new Pokemon("⚡ Pikachu", TypePokemon.Electrik, 100, 50);
        Pokemon pokemon2 = new Pokemon("👊 Machoc", TypePokemon.Combat, 75, 40);

        pokemon1.AfficherInfos();
        pokemon2.AfficherInfos();

        while (!pokemon1.IsKO() && !pokemon2.IsKO())
        {
            pokemon1.Fight(pokemon2);
            if (pokemon2.IsKO()) break;

            pokemon2.Fight(pokemon1);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n===== FIN DU COMBAT =====");

        if (pokemon1.IsKO())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{pokemon2.Name} a gagné le combat 🏆\n");
        }
        else if (pokemon2.IsKO())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{pokemon1.Name} a gagné le combat 🏆\n");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Match nul !\n");
        }

        Console.ResetColor();
    }
}
