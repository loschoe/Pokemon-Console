namespace PokemonBattle;

public interface IItem
{
    string Name { get; }
    int Cost { get; }

    bool Use(Pokemon target);
}
