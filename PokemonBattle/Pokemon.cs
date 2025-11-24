using System;
using System.Collections.Generic;
using System.Threading;

namespace PokemonBattle
{
    public class Pokemon
    {
        public string Name { get; private set; }
        public TypePokemon Type { get; private set; }
        public int PV { get; private set; }
        public int MaxPV { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; set; }
        public List<Attack> Attacks { get; set; } = new();

        private bool hasEnteredArena = false;
        private static readonly Random random = new();

        private static readonly Dictionary<TypePokemon, string> TypeEmojis = new()
        {
            { TypePokemon.Électrik, "⚡" },
            { TypePokemon.Feu, "🔥" },
            { TypePokemon.Eau, "💧" },
            { TypePokemon.Plante, "🌿" },
            { TypePokemon.Psy, "🧠" },
            { TypePokemon.Glace, "❄️" },
            { TypePokemon.Ténèbres, "🪦" },
            { TypePokemon.Acier, "🔩" },
            { TypePokemon.Vol, "🪽" },
            { TypePokemon.Sol, "🌍" },
            { TypePokemon.Dragon, "🐉" },
            { TypePokemon.Spectre, "👻" },
            { TypePokemon.Insecte, "🐜" },
            { TypePokemon.Roche, "🪨" },
            { TypePokemon.Poison, "☠️" },
            { TypePokemon.Normal, "⚪" },
            { TypePokemon.Fée, "✨" },
            { TypePokemon.Combat, "👊" }
        };

        public Pokemon(string name, TypePokemon type, int pv, int maxpv, int attack, int defense)
        {
            Name = name;
            Type = type;
            PV = pv;
            MaxPV = maxpv;
            Attack = attack;
            Defense = defense;
        }

        public string GetStyledName()
        {
            return $"{Name}{(TypeEmojis.ContainsKey(Type) ? $" {TypeEmojis[Type]}" : "")}";
        }

        public static void TypeWriterEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public void AfficherInfos()
        {
            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect("\n------- FICHE POKEMON -------");
            TypeWriterEffect($"Nom : {GetStyledName()}");
            TypeWriterEffect($"Type : {Type}");
            TypeWriterEffect($"Points de vie : {PV}/{MaxPV}");
            TypeWriterEffect($"Attaque : {Attack}");
            TypeWriterEffect($"Défense : {Defense}");
            Console.ResetColor();
        }

        public void Fight(Pokemon target)
        {
            if (!hasEnteredArena)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                TypeWriterEffect($"\n{GetStyledName()} de type {Type} est entré dans l'arène");
                TypeWriterEffect($"{target.GetStyledName()} de type {target.Type} est entré dans l'arène");
                Console.ResetColor();

                hasEnteredArena = true;
                target.hasEnteredArena = true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n===== TOUR DE COMBAT =====");
            Console.ResetColor();

            // ✅ Choisir une attaque aléatoire
            var attack = Attacks[random.Next(Attacks.Count)];

            // ✅ Exécuter l’attaque (calcul fait dans Attack.Use)
            attack.Use(this, target);

            // ✅ Vérifier KO une seule fois
            target.CheckStatus();
        }

        public void Damage(string attackName, int damage, double effectiveness)
        {
            PV -= damage;
            if (PV < 0) PV = 0;

            string effMessage = effectiveness switch
            {
                2.0 => "C'est super efficace ! 💥",
                0.5 => "Ce n'est pas très efficace... 😐",
                0.0 => "Cela n’a aucun effet 😶",
                _ => ""
            };

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()} subit {damage} dégâts. {effMessage}");
            TypeWriterEffect($"PV restants : {PV}");
            Console.ResetColor();
        }

        public void Heal(int amount)
        {
            PV += amount;
            if (PV > MaxPV) PV = MaxPV;

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()} récupère {amount} PV ! Il est maintenant à {PV} PV.");
            Console.ResetColor();
        }

        public void CheckStatus()
        {
            Console.ForegroundColor = GetConsoleColor();

            if (PV <= 0)
                TypeWriterEffect($"{GetStyledName()} est KO !");
            else
                TypeWriterEffect($"{GetStyledName()} peut encore se battre !");

            Console.ResetColor();
        }

        public bool IsKO() => PV <= 0;

        private ConsoleColor GetConsoleColor()
        {
            return Type switch
            {
                TypePokemon.Électrik => ConsoleColor.Yellow,
                TypePokemon.Combat => ConsoleColor.Blue,
                TypePokemon.Feu => ConsoleColor.Red,
                TypePokemon.Eau => ConsoleColor.Cyan,
                TypePokemon.Plante => ConsoleColor.Green,
                TypePokemon.Psy => ConsoleColor.Magenta,
                TypePokemon.Glace => ConsoleColor.White,
                TypePokemon.Ténèbres => ConsoleColor.DarkGray,
                TypePokemon.Acier => ConsoleColor.Gray,
                TypePokemon.Vol => ConsoleColor.DarkCyan,
                TypePokemon.Sol => ConsoleColor.DarkYellow,
                TypePokemon.Dragon => ConsoleColor.DarkMagenta,
                TypePokemon.Spectre => ConsoleColor.DarkBlue,
                TypePokemon.Insecte => ConsoleColor.DarkGreen,
                TypePokemon.Roche => ConsoleColor.DarkYellow,
                TypePokemon.Poison => ConsoleColor.DarkMagenta,
                TypePokemon.Normal => ConsoleColor.Gray,
                TypePokemon.Fée => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };
        }
    }
}
