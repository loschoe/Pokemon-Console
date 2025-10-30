namespace PokemonBattle;

public class Pokemon
{
    public string Name;
    public string Type;
    public int PV;
    public int Attack;

    public Pokemon(string name, string type, int pv, int attack)
    {
        Name = name;
        Type = type;
        PV = pv;
        Attack = attack;
    }
    public void AfficherInfos()
    {
        Console.WriteLine($"\n------- FICHE POKEMON -------");
        Console.WriteLine($"Nom : {Name}");
        Console.WriteLine($"Type : {Type}");
        Console.WriteLine($"Points de vie : {PV}");
        Console.WriteLine($"Points d'attaque : {Attack}\n");
    }
    public void Damage(Pokemon cible)
{
    Console.WriteLine($"{Name} attaque {cible.Name} avec {Attack} points de dégâts !");
    cible.PV -= this.Attack;

    if (cible.PV < 0)
        cible.PV = 0;

    Console.WriteLine($"{cible.Name} a maintenant {cible.PV} PV.");
}
}