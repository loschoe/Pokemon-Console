using System;
using System.Collections.Generic;
using System.Threading;
using PokemonBattle;

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

		private bool hasEnteredArena = false;							// Ne pas réafficher le message d'entrée dans l'arène de combat 

		private static readonly Dictionary<TypePokemon, string> TypeEmojis = new()		// Afficher un émoji à côté du nom du pokemon 
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
			Speed = speed;				// La vitesse ne sert à rien pour l'instant !
		}

		public string GetStyledName()	// Afficher l'émoji
		{
			string emoji = TypeEmojis.ContainsKey(Type) ? $" {TypeEmojis[Type]}" : "";
			return $"{Name}{emoji}";
		}

		public static void TypeWriterEffect(string text, int delay = 30)	// le style machine à écrire 
		{
			foreach (char c in text)
			{
				Console.Write(c);
				Thread.Sleep(delay);
			}
			Console.WriteLine();
		}

		public void AfficherInfos() 		// Les infos du pokemon 
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

		public void Fight(Pokemon target)				// Le système de combat 
		{
			if (!hasEnteredArena)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				TypeWriterEffect($"\n{GetStyledName()} de type {Type} est entré dans l'arène de combat");
				TypeWriterEffect($"{target.GetStyledName()} de type {target.Type} est entré dans l'arène de combat");
				Console.ResetColor();
				hasEnteredArena = true;
				target.hasEnteredArena = true;
			}

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\n===== TOUR DE COMBAT =====");
			Console.ResetColor();

			double multiplicateur = TypeHelper.GetEffectiveness(this.Type, target.Type);
			int degatsFinaux = (int)(Attack * multiplicateur);
			if (degatsFinaux < 0) degatsFinaux = 0;

			string message = multiplicateur switch
			{
				2.0 => $"L'attaque de {GetStyledName()} est très efficace contre {target.GetStyledName()} ! 💥",
				0.5 => $"L'attaque de {GetStyledName()} n'est pas très efficace contre {target.GetStyledName()}... 😐",
				0.0 => $"L'attaque de {GetStyledName()} n’a aucun effet sur {target.GetStyledName()} 😶",
				_ => $"L'attaque de {GetStyledName()} est normale contre {target.GetStyledName()}."
			};

			Console.ForegroundColor = GetConsoleColor();
			TypeWriterEffect(message);
			Console.ResetColor();

			Console.ForegroundColor = GetConsoleColor();
			TypeWriterEffect($"{GetStyledName()} attaque {target.GetStyledName()} et inflige {degatsFinaux} points de dégâts !");
			Console.ResetColor();

			target.Damage(degatsFinaux);
			target.CheckStatus();
		}

		public void Damage(int damage)
		{
			PV -= damage;
			if (PV < 0) PV = 0;

			Console.ForegroundColor = GetConsoleColor();
			TypeWriterEffect($"{GetStyledName()} a maintenant {PV} PV restants.");
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
