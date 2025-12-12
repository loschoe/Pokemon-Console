using PokemonBattle;
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static string menuArt = @"
___  ___                      _       
|  \/  |                     (_)      
| .  . | __ _  __ _  __ _ ___ _ _ __  
| |\/| |/ _` |/ _` |/ _` / __| | '_ \ 
| |  | | (_| | (_| | (_| \__ \ | | | |
\_|  |_/\__,_|\__, |\__,_|___/_|_| |_|
               __/ |                  
              |___/                   
";

    static string title = @"
______     _                                _____                       _      
| ___ \   | |                              /  __ \                     | |     
| |_/ /__ | | _____ _ __ ___   ___  _ __   | /  \/ ___  _ __  ___  ___ | | ___ 
|  __/ _ \| |/ / _ \ '_ ` _ \ / _ \| '_ \  | |    / _ \| '_ \/ __|/ _ \| |/ _ \
| | | (_) |   <  __/ | | | | | (_) | | | | | \__/\ (_) | | | \__ \ (_) | |  __/
\_|  \___/|_|\_\___|_| |_| |_|\___/|_| |_|  \____/\___/|_| |_|___/\___/|_|\___|
                                                                               
";

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
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(title);
        Console.ResetColor();

        string filePath = "pokedex.csv";
        List<Pokemon> pokemons = PokemonLoader.LoadFromCSV(filePath);

        if (pokemons.Count < 2)
        {
            Console.WriteLine("❌ Pas assez de Pokémon.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\n                                  📜 Pokedex (Y/N) \n");

        ConsoleKeyInfo key = Console.ReadKey(true);
        char pressed = char.ToLower(key.KeyChar);
        if (pressed == 'y')
        {
            Console.Clear();
            for (int i = 0; i < pokemons.Count; i++)
                Console.WriteLine($"{i} - {pokemons[i].Name}");
        }

        Console.Write("\n                          Choisissez votre Pokémon (nom/n°): ");
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
        int money = 300;
        List<IItem> inventory = new List<IItem> { new Pokeball(), new Potion() };

        bool combatTermine = false;
        bool fuite = false;

        while (!playerPokemon.IsKO() && !enemyPokemon.IsKO() && !combatTermine)
        {
            // ---------------- TOUR DU JOUEUR ----------------
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(1000);
            Console.Clear();
            TypeWriterEffect("\n===== TOUR DU JOUEUR =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("🤺 Attaquer");
            Console.WriteLine("🎒 Utiliser un objet");
            Console.WriteLine("🚪 Fuite");
            Console.Write("Choix: \n");
            Console.ResetColor();
            
            key = Console.ReadKey(true);
            pressed = key.KeyChar;

            switch (pressed)

            {
                case '1':
                    playerPokemon.Fight(enemyPokemon);
                    if (!enemyPokemon.IsKO())
                        enemyPokemon.FightAuto(playerPokemon);
                    break;

                case '2':
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(menuArt);
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriterEffect($"💰 Argent : {money}");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    for (int i = 0; i < inventory.Count; i++)
                        Console.WriteLine($"{i + 1}. {inventory[i].Name} ({inventory[i].Cost} ₽)");
                    Console.WriteLine("3. QUITTER");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\nChoisissez un objet à utiliser : ");
                    if (int.TryParse(Console.ReadLine(), out int itemChoice))
                    {
                        itemChoice--; 

                        if (itemChoice >= 0 && itemChoice < inventory.Count)
                        {
                            IItem item = inventory[itemChoice];

                            if (money >= item.Cost)
                            {
                                money -= item.Cost;

                                Console.ForegroundColor = ConsoleColor.Green;
                                TypeWriterEffect($"\n✅ Vous achetez {item.Name} pour {item.Cost}₽ !");
                                Console.ResetColor();

                                if (item is Pokeball pokeball)
                                {
                                    bool captured = pokeball.Use(enemyPokemon);
                                    if (captured)
                                        combatTermine = true;
                                }
                                else if (item is Potion)
                                {
                                    item.Use(playerPokemon);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                TypeWriterEffect("\n❌ Pas assez d'argent !");
                                Console.ResetColor();        
                            }
                        }
                        else
                        {
                            Console.WriteLine("Objet invalide !");
                        }
                    }
                    break;

                case '3':
                    fuite = true;
                    combatTermine = true;
                    break;

                default:
                    Console.WriteLine("Action invalide. Tour sauté.");
                    break;
            }
        }

        // ---------------- FIN DU COMBAT ----------------
        Console.ForegroundColor = ConsoleColor.DarkGray;
        TypeWriterEffect("\n===== FIN DU COMBAT =====");
        Console.ForegroundColor = ConsoleColor.Green;

        if (playerPokemon.IsKO())
            Console.WriteLine($"😭 {enemyPokemon.Name} a gagné !\n");
        //else if (combatTermine && enemyPokemon.IsKO())
        else if (combatTermine && fuite)
            Console.WriteLine($"😭 {enemyPokemon.Name} a gagné par abandon !\n");
        else if (combatTermine)
            Console.WriteLine($"🎉 {enemyPokemon.Name} a été capturé !\n");
        else if (combatTermine && fuite)
            Console.WriteLine($"😭 {enemyPokemon.Name} a gagné par abandon !\n");
        else
            Console.WriteLine($"🏆 {playerPokemon.Name} a gagné !\n");
            Thread.Sleep(1300);
            Console.Clear();
            
        Console.ResetColor();
    }
}
