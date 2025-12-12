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

        public static void TypeWriterEffect(string text, int delay = 15)
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
            Console.WriteLine($"\n------------ {GetStyledName()} ------------");
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"PV: {PV}/{MaxPV} {HealthBar(PV, MaxPV)}");
            Console.WriteLine($"Attaque/Defence: {Attack}/{Defense}");
            Console.ResetColor();
        }

        static string HealthBar(int pv, int maxpv)
        {
            int size = 20;
            int filled = pv * size / maxpv;
            return "|" + new string('█', filled) + new string('░', size - filled) + "|";
        }

        public void Fight(Pokemon target)
        {
            Thread.Sleep(1500);
            Console.Clear();

            AfficherInfos();
            target.AfficherInfos();

            if (Attacks.Count == 0)
            {
                Console.WriteLine($"{GetStyledName()} n'a aucune attaque !");
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nAttaques disponibles :");

            for (int i = 0; i < Attacks.Count; i++)
            {
                Console.ForegroundColor = GetConsoleColor();
                Attack atk = Attacks[i];

                if (atk is DamageAttack da)
                {
                    int realDamage = (int)Math.Round(Attack * da.Multiplier);
                    Console.WriteLine($"{i+1}. {da.Name} - Dégâts : {realDamage}");
                }
                else if (atk is HealingAttack ha)
                {
                    int realHeal = (int)Math.Round(MaxPV * ha.HealPercent);
                    Console.WriteLine($"{i+1}. {ha.Name} - Soin : {realHeal}");
                }
                else if (atk is VampireAttack va)
                {
                    int realDamage = (int)Math.Round(Attack * 1.0);
                    int realDrain = (int)Math.Round(realDamage * va.DrainPercent);

                    Console.WriteLine($"{i+1}. {va.Name} - Dégâts : {realDamage} | Drain : {realDrain}");
                }
                else
                {
                    Console.WriteLine($"{i+1}. {atk.Name}");
                }
            }


            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nChoisissez une attaque : ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > Attacks.Count)
            {
                Console.WriteLine("❌ Choix invalide !");
                return;
            }

            Thread.Sleep(1000);
            Console.Clear();

            var selectedAttack = Attacks[choice - 1];

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()} utilise {selectedAttack.Name} !\n");

            selectedAttack.Use(this, target);

            Console.WriteLine();
            AfficherInfos();
            target.AfficherInfos();

            target.CheckStatus();
        }

        public void FightAuto(Pokemon target)
        {
            if (Attacks.Count == 0)
            {
                Console.WriteLine($"{GetStyledName()} n'a aucune attaque !");
                return;
            }

            var attack = Attacks[random.Next(Attacks.Count)];

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"\n{GetStyledName()} utilise {attack.Name} !");
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
                >= 2.0 => "c'est super efficace ! 💥",
                0.5 => "ce n'est pas très efficace... 😐",
                0.0 => "cela n’a aucun effet 😶",
                _ => ""
            };

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"{GetStyledName()} subit {damage} dégâts {eff} !");
            TypeWriterEffect($"PV : {PV}/{MaxPV} {HealthBar(PV, MaxPV)}");
            Console.ResetColor();
        }

        public void Heal(int amount, string sourceName = "")
        {
            PV += amount;
            if (PV > MaxPV) PV = MaxPV;

            Console.ForegroundColor = GetConsoleColor();
            TypeWriterEffect($"💊 {GetStyledName()} récupère {amount} PV !");
            TypeWriterEffect($"PV : {PV}/{MaxPV} {HealthBar(PV, MaxPV)}");
            Console.ResetColor();
        }

        public void CheckStatus()
        {
            Console.ForegroundColor = GetConsoleColor();
            if (PV <= 0)
                TypeWriterEffect($"{GetStyledName()} est KO !");
            else
                TypeWriterEffect($"{GetStyledName()} peut encore se battre.");
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
