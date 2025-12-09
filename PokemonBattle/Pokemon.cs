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

        private static readonly Random random = new();

        public Pokemon(string name, TypePokemon type, int pv, int maxpv, int attack, int defense)
        {
            Name = name;
            Type = type;
            PV = pv;
            MaxPV = maxpv;
            Attack = attack;
            Defense = defense;
        }

        public string GetStyledName() => $"{Name}";

        public static void TypeWriterEffect(string text, int delay = 20)
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
            Console.WriteLine($"\n------- {GetStyledName()} -------");
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"PV: {PV}/{MaxPV}");
            Console.WriteLine($"Attaque: {Attack}");
            Console.WriteLine($"Défense: {Defense}");
            Console.ResetColor();
        }

        public void Fight(Pokemon target)
        {
            if (Attacks.Count == 0)
            {
                Console.WriteLine($"{GetStyledName()} n'a aucune attaque !");
                return;
            }

            var attack = Attacks[random.Next(Attacks.Count)];
            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()} utilise {attack.Name} !");
            Console.ResetColor();

            attack.Use(this, target);
            target.CheckStatus();
        }

        public void Damage(string attackName, int damage, double effectiveness)
        {
            damage = Math.Max(0, damage - Defense);
            PV -= damage;
            if (PV < 0) PV = 0;

            string eff = effectiveness switch
            {
                >= 2.0 => "C'est super efficace ! 💥",
                0.5 => "Ce n'est pas très efficace... 😐",
                0.0 => "Cela n’a aucun effet 😶",
                _ => ""
            };

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()} subit {damage} dégâts par {attackName} !");
            if (!string.IsNullOrEmpty(eff))
                TypeWriterEffect(eff);
            TypeWriterEffect($"PV restants : {PV}");
            Console.ResetColor();
        }

        public void Heal(int amount, string sourceName = "")
        {
            PV += amount;
            if (PV > MaxPV) PV = MaxPV;

            Console.ForegroundColor = GetConsoleColor();
            if (string.IsNullOrEmpty(sourceName))
                TypeWriterEffect($"{GetStyledName()} récupère {amount} PV");
            else
                TypeWriterEffect($"{GetStyledName()} récupère {amount} PV grâce à {sourceName} !");
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
                TypePokemon.Feu => ConsoleColor.DarkRed,
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
