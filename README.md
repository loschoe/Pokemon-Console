# 🎮 Pokemon Console - C# 

## 🚀 Présentation   :
Bienvenue dans le dépôt GitHub du **Projet Pokémon**, un jeu en console qui reprend les dynamismes principaux de pokémon. 
On retrouve un pokédex, des combats et bientôt les objets seront codés. 
Ce jeu est développé dans le cadre d'un module à **STRASBOURG Ynov Campus**.

## 📄 Fonctionnalités :
- Accès à un `pokédex` (inspiré des pokémons disponibles sur Pokemon Lune)
- Combat contre l'ordinateur avec des fonctionnalités de calcul de combat intégrées.
- Possibilité de choisir son attaque parmis plusieurs attaques : `DamageAttack`, `VampireAttack`, `HealingAttack`.
- Système monétaire `300 ₽`
- Possibilité d'utiliser une `pokeball` pour **capturer le pokemon** ennemi [L'ajout dans une équipe n'est pas encore codé]
- Possibilité d'utiliser une potion pour **restaurer la vie** 

| Condition de fin de combat        | Description                                                                 | Conséquence                          |
|-----------------------------------|-----------------------------------------------------------------------------|--------------------------------------|
| 🪦 Pokémon KO                     | Le Pokémon du joueur ou de l’ennemi n’a plus de PV                          | Le combat s’arrête, victoire/défaite |
| 🎯 Pokémon ennemi capturé         | Le joueur utilise une Pokéball et réussit la capture                        | Le combat se termine, ennemi capturé |
| 🏃‍♂️ Fuite                        | Le joueur choisit l’option de fuite                                         | Le combat s’arrête immédiatement     |

## 🛠️ Installation et exécution :
1. Cloner le projet : `https://github.com/loschoe/Pokemon-Console.git`
2. Lancer le programme avec la commande : `dotnet run` 
3. Commencer à jouer en suivant les instructions données en console (les inputs donnent toutes les indications)

## 💡 Langages & tech utilisés :
- Backend : `C#`
- Frontend : `Console.ForegroundColor`, `TypeWriterEffect`, `ASCII`
