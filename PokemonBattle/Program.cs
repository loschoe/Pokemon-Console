using PokemonBattle;
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    public static void TypeWriterEffect(string text, int delay = 20)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\n⚔️ Bienvenue dans le combat Pokémon !");
        Console.ResetColor();

        string filePath = "pokedex.csv";
        List<Pokemon> pokemons = PokemonLoader.LoadFromCSV(filePath);

        if (pokemons.Count < 2)
        {
            Console.WriteLine("❌ Pas assez de Pokémon.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("📜 Voir le pokédex ? (y/n)");
        string? showPokedex = Console.ReadLine();

        if (showPokedex?.ToLower() == "y")
        {
            for (int i = 0; i < pokemons.Count; i++)
                Console.WriteLine($"{i} - {pokemons[i].Name}");
        }

        Console.Write("\nChoisissez votre Pokémon (nom/n°): ");
        string? input = Console.ReadLine();
        Console.Clear();

        Pokemon? playerPokemon = int.TryParse(input, out int index)
            ? pokemons[index]
            : pokemons.Find(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

        if (playerPokemon == null)
        {
            Console.WriteLine("❌ Pokémon introuvable.");
            return;
        }

        Random rnd = new();
        Pokemon enemyPokemon;
        do
        {
            enemyPokemon = pokemons[rnd.Next(pokemons.Count)];
        } while (enemyPokemon == playerPokemon);

        // Entrée dans l'arène
        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect($"\n{playerPokemon.Name} entre dans l'arène !");
        TypeWriterEffect($"{enemyPokemon.Name} entre dans l'arène !");
        
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\nCombattants :");
        playerPokemon.AfficherInfos();
        enemyPokemon.AfficherInfos();

        // Inventaire du joueur
        List<IItem> inventory = new List<IItem> { new Pokeball(), new Potion() };

        bool combatTermine = false;

        while (!playerPokemon.IsKO() && !enemyPokemon.IsKO() && !combatTermine)
        {
            // ---------------- TOUR DU JOUEUR ----------------
            Console.ForegroundColor = ConsoleColor.Red;
            TypeWriterEffect("\n===== TOUR DU JOUEUR =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("1. Attaquer");
            Console.WriteLine("2. Utiliser un objet");
            Console.Write("Choix: \n");
            Console.ResetColor();
            string? action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    playerPokemon.Fight(enemyPokemon);
                    break;

                case "2":
                    Console.WriteLine("\nInventaire :");
                    for (int i = 0; i < inventory.Count; i++)
                        Console.WriteLine($"{i + 1}. {inventory[i].Name} (Prix : {inventory[i].Cost}) ₽");

                    Console.Write("Choisissez un objet à utiliser : ");
                    if (int.TryParse(Console.ReadLine(), out int itemChoice))
                    {
                        itemChoice--; // 0-based
                        if (itemChoice >= 0 && itemChoice < inventory.Count)
                        {
                            IItem item = inventory[itemChoice];

                            if (item is Pokeball)
                            {
                                item.Use(enemyPokemon);
                                if (enemyPokemon.IsKO())
                                    combatTermine = true; // Capture réussie
                            }
                            else if (item is Potion)
                            {
                                item.Use(playerPokemon);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Objet invalide !");
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Action invalide. Tour sauté.");
                    break;
            }

            // ---------------- TOUR DE L'ENNEMI ----------------
            if (!enemyPokemon.IsKO() && !combatTermine)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWriterEffect("\n===== TOUR DE L'ENNEMI =====");
                Console.ResetColor();
                enemyPokemon.Fight(playerPokemon);
            }
        }

        // ---------------- FIN DU COMBAT ----------------
        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\n===== FIN DU COMBAT =====");
        Console.ForegroundColor = ConsoleColor.Green;

        if (playerPokemon.IsKO())
            Console.WriteLine($"{enemyPokemon.Name} a gagné !");
        else if (combatTermine && enemyPokemon.IsKO())
            Console.WriteLine($"🎉 {enemyPokemon.Name} a été capturé !");
        else
            Console.WriteLine($"{playerPokemon.Name} a gagné !");
            
        Console.ResetColor();
    }
}
