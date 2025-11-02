using System;
using System.Collections.Generic;

namespace PokemonBattle
{
    public class Pokemon
    {
        public string Name { get; private set; }
        public TypePokemon Type { get; private set; }
        public int PV { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

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

        public Pokemon(string name, TypePokemon type, int pv, int attack, int defense, int speed)
        {
            Name = name;
            Type = type;
            PV = pv;
            Attack = attack;
            Defense = defense;
            Speed = speed;
        }

        public string GetStyledName()
        {
            string emoji = TypeEmojis.ContainsKey(Type) ? $" {TypeEmojis[Type]}" : "";
            return $"{Name}{emoji}";
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
            TypeWriterEffect($"Points de vie : {PV}");
            TypeWriterEffect($"Points d'attaque : {Attack}");
            TypeWriterEffect($"Défense : {Defense}");
            TypeWriterEffect($"Vitesse : {Speed}");
            Console.ResetColor();
        }

        public void Fight(Pokemon target)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n===== TOUR DE COMBAT =====");
            Console.ResetColor();

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()} attaque {target.GetStyledName()}  et inflige {Attack} points de dégâts !");
            target.Damage(Attack);
            target.CheckStatus();
            Console.ResetColor();
        }

        public void Damage(int damage)
        {
            PV -= damage;
            if (PV < 0) PV = 0;
            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()}  a maintenant {PV} PV.");
        }

        public void CheckStatus()
        {
            Console.ForegroundColor = GetConsoleColor();
            if (PV <= 0)
            {
                TypeWriterEffect($"{GetStyledName()}  est KO !");
            }
            else
            {
                TypeWriterEffect($"{GetStyledName()}  peut encore se battre !");
            }
            Console.ResetColor();
        }

        public bool IsKO()
        {
            return PV <= 0;
        }

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