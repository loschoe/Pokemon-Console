using System;
using System.Collections.Generic;
using System.IO;
using PokemonBattle;

public static class PokemonLoader
{
    /// <summary>
    /// Charge les Pokémon depuis un fichier CSV et leur assigne des attaques par défaut selon leur type.
    /// </summary>
    public static List<Pokemon> LoadFromCSV(string filePath)
    {
        var pokemons = new List<Pokemon>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"❌ Fichier introuvable : {filePath}");
            return pokemons;
        }

        using var reader = new StreamReader(filePath);
        bool headerSkipped = false;

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            // Ignorer l'en-tête
            if (!headerSkipped)
            {
                headerSkipped = true;
                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
                continue;

            var values = line.Split(',');

            if (values.Length < 6)
                continue;

            string name = values[0];

            if (!Enum.TryParse<TypePokemon>(values[1], true, out var type1))
                continue;

            // Récupération des stats depuis les dernières colonnes
            int pv      = int.Parse(values[^4]);
            int maxpv   = int.Parse(values[^3]);
            int attack  = int.Parse(values[^2]);
            int defense = int.Parse(values[^1]);

            var pokemon = new Pokemon(name, type1, pv, maxpv, attack, defense);

            AddDefaultAttacksByType(pokemon);

            pokemons.Add(pokemon);
        }

        return pokemons;
    }

    /// <summary>
    /// Assigne les attaques par défaut à un Pokémon en fonction de son type.
    /// </summary>
    private static void AddDefaultAttacksByType(Pokemon p)
    {
        switch (p.Type)
        {
            case TypePokemon.Feu:
                p.Attacks.Add(new DamageAttack("Flammèche", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Lance-Flammes", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Cendres chaudes", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Flammes dévorantes", p.Type, 10));
                break;

            case TypePokemon.Eau:
                p.Attacks.Add(new DamageAttack("Pistolet à O", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Cascade", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Pluie régénérante", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Vague vorace", p.Type, 10));
                break;

            case TypePokemon.Plante:
                p.Attacks.Add(new DamageAttack("Fouet Lianes", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Tempête Verte", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Synthèse", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Vampigraine", p.Type, 10));
                break;

            case TypePokemon.Électrik:
                p.Attacks.Add(new DamageAttack("Éclair", p.Type, 30));
                p.Attacks.Add(new DamageAttack("Fatal-Foudre", p.Type, 45));
                p.Attacks.Add(new HealingAttack("Recharge électrique", p.Type, 20));
                p.Attacks.Add(new VampireAttack("Absorption", p.Type, 15));
                break;

            case TypePokemon.Acier:
                p.Attacks.Add(new DamageAttack("Griffe Acier", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Tir Métallique", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Renfort Acier", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Métal", p.Type, 10));
                break;

            case TypePokemon.Combat:
                p.Attacks.Add(new DamageAttack("Poing Éclair", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Coup de Boule", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Focus Vital", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Énergie Volée", p.Type, 10));
                break;

            case TypePokemon.Dragon:
                p.Attacks.Add(new DamageAttack("Draco-Rage", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Dracochoc", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Souffle Draconique", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Dragon", p.Type, 10));
                break;

            case TypePokemon.Fée:
                p.Attacks.Add(new DamageAttack("Éclat Magique", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Voile Féérique", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Soin Enchanté", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Vol de Charme", p.Type, 10));
                break;

            case TypePokemon.Glace:
                p.Attacks.Add(new DamageAttack("Poudreuse", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Blizzard", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Frissons Revigorants", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Givré", p.Type, 10));
                break;

            case TypePokemon.Insecte:
                p.Attacks.Add(new DamageAttack("Dard-Venin", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Fouet Insecte", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Régénération Naturelle", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Parasite", p.Type, 10));
                break;

            case TypePokemon.Normal:
                p.Attacks.Add(new DamageAttack("Charge", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Coup Puissant", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Repos", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Vol de Vie", p.Type, 10));
                break;

            case TypePokemon.Poison:
                p.Attacks.Add(new DamageAttack("Piqûre Toxique", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Nuage Vénéneux", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Soin Acide", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Toxique", p.Type, 10));
                break;

            case TypePokemon.Psy:
                p.Attacks.Add(new DamageAttack("Choc Mental", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Psyko", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Calme Mental", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Psychique", p.Type, 10));
                break;

            case TypePokemon.Roche:
                p.Attacks.Add(new DamageAttack("Éboulement", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Pierre-Volante", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Soin Sablonneux", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Minéral", p.Type, 10));
                break;

            case TypePokemon.Sol:
                p.Attacks.Add(new DamageAttack("Pelle", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Séisme", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Régénération Terrestre", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Terre", p.Type, 10));
                break;

            case TypePokemon.Spectre:
                p.Attacks.Add(new DamageAttack("Griffe Spectrale", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Ombre Nocturne", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Soin Fantomatique", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Spectral", p.Type, 10));
                break;

            case TypePokemon.Ténèbres:
                p.Attacks.Add(new DamageAttack("Morsure", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Griffe Ombre", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Soin Obscur", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Ténébreux", p.Type, 10));
                break;

            case TypePokemon.Vol:
                p.Attacks.Add(new DamageAttack("Aile d’Acier", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Rapace", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Rafale Revigorante", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Drain Ailé", p.Type, 10));
                break;

            default:
                p.Attacks.Add(new DamageAttack("Coup Normal", p.Type, 20));
                p.Attacks.Add(new DamageAttack("Coup Puissant", p.Type, 35));
                p.Attacks.Add(new HealingAttack("Repos", p.Type, 15));
                p.Attacks.Add(new VampireAttack("Vol de Vie", p.Type, 10));
                break;
        }
    }
}
