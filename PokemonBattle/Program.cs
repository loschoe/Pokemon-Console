using PokemonBattle;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {   // Message de bienvenue 
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n⚔️  Bienvenue dans la console de combat Pokémon !");
        Console.ResetColor();

        // Importation du pokedex 
        string filePath = "pokedex.csv";
        List<Pokemon> pokemons = PokemonLoader.LoadFromCSV(filePath);

        if (pokemons.Count < 2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Le fichier doit contenir au moins deux Pokémon pour lancer un combat.");
            Console.ResetColor();
            return;
        }

        // Accès au pokedex ou poursuite du code 
        Console.WriteLine("📜  Accéder au pokédex (y/n) : ");
        string? choice = Console.ReadLine();
        if (choice != null && choice.ToLower() == "y")
        {
            Console.WriteLine("\nListe des Pokémon disponibles :");
            for (int i = 0; i < pokemons.Count; i++)
            {
                Console.WriteLine($"{i} - {pokemons[i].Name}");
            }
        }
        else
        {
            // Poursuite du code 
        }

        // Demande à l'utilisateur quel pokemon veut-il utiliser 
        Console.WriteLine("\nQuel Pokémon voulez-vous dans votre équipe ? (N° ou nom) : ");
        string? input = Console.ReadLine();
        Console.Clear();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Entrée vide.");
            Console.ResetColor();
            return;
        }

        Pokemon? pokemon1 = null;

        if (int.TryParse(input, out int index))
        {
            if (index >= 0 && index < pokemons.Count)
            {
                pokemon1 = pokemons[index];
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Numéro invalide.");
                Console.ResetColor();
                return;
            }
        }
        else
        {
            pokemon1 = pokemons.Find(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
            if (pokemon1 is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Aucun Pokémon nommé '{input}' trouvé.");
                Console.ResetColor();
                return;
            }
        }

        Pokemon pokemon1Selected = pokemon1;

        // Pokemon ennemi défini aléatoirement 
        Random rnd = new Random();
        Pokemon pokemon2 = pokemons[rnd.Next(pokemons.Count)];

        Console.WriteLine("Les combattants sont :");

        pokemon1Selected.AfficherInfos();
        pokemon2.AfficherInfos();

        // Boucle de combat 
        while (!pokemon1Selected.IsKO() && !pokemon2.IsKO())
        {
            pokemon1Selected.Fight(pokemon2);
            if (pokemon2.IsKO()) break;

            pokemon2.Fight(pokemon1Selected);
        }

        // Fin du jeu !
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n===== FIN DU COMBAT =====");

        if (pokemon1Selected.IsKO())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{pokemon2.Name} a gagné le combat 🏆\n");
        }
        else if (pokemon2.IsKO())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{pokemon1Selected.Name} a gagné le combat 🏆\n");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Match nul !\n");
        }

        Console.ResetColor();
    }
}
