using System.Diagnostics.Contracts;
using PokemonBattle;
class Program
{
    static void Main(string[] args)
    {
        Pokemon pikachu = new Pokemon("Pikachu", "Électrique", 500, 200);
        Pokemon ratata = new Pokemon("Ratata", "Normal", 400, 100);

        pikachu.AfficherInfos();
        Console.WriteLine("! COMBAT !");
        ratata.AfficherInfos();
        
        {
            Console.WriteLine("Démarrer le combat");

            pikachu.Damage(ratata);
            ratata.Damage(pikachu);
            pikachu.Damage(ratata);
            ratata.Damage(pikachu);
        }
    }
}
