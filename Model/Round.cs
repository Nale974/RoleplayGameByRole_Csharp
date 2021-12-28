using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model
{
    class Round
    {
        public List<Character> characters;
        public Character winner;

        public Round(List<Character> characters)
        {
            this.characters = characters;
            this.winner = null;
        }

        public void Run()
        {
            //Rétablissement des niveaux d'attaque par défaut
            characters.ForEach(c => c.currentAttackNumber = c.totalAttackNumber);

            //Suivi nombre d'attaque
            Console.WriteLine("DEBUG - Nombre d'attaque : ");
            characters.ForEach(c => Console.WriteLine("     " + c.name + " : " + c.currentAttackNumber));
            Console.WriteLine("");

            //Calcul d'initiative
            this.characters = CalculInitiative(characters);

            //Pour chaque personnage du round
            for (int i = 0; i < characters.Count(); i++)
            {
                if (this.CheckWinner() == true) { break; }
                //Si le personnage est en vie
                if (characters[i].currentLife > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nC'est au tour de " + characters[i].name + ".");
                    Console.ResetColor();
                    Console.Write("\n");

                    // Gestion de la caratéristique d'augmentation de l'attaque à chaque tour
                    if (characters[i] is IIncreasedAttack CharacterIncreasedAttack)
                    {
                        CharacterIncreasedAttack.attack = CharacterIncreasedAttack.attack + (int)(CharacterIncreasedAttack.attack * CharacterIncreasedAttack.IncreasedAttack);
                        Console.WriteLine("L'attaque de "+ characters[i].name+" augmente de "+ CharacterIncreasedAttack.IncreasedAttack*100+ "%, il est maintenant de "+ CharacterIncreasedAttack.attack+".");
                    }

                    // Gestion de la caratéristique de la douleur : 
                    // Si le personnage est victime de la douleur
                    if (characters[i] is IPainSensitive characterPainSensitive && characterPainSensitive.AttackCapability >= 0)
                    {
                        Console.WriteLine(characters[i].name + " est bloqué pendant encore " + (characterPainSensitive.AttackCapability + 1) + " tour(s).");
                        characterPainSensitive.AttackCapability -= 1;
                    }
                    else
                    {
                        // Jusqu'a que le personnage n'a plus d'attaque de disponible
                        while (characters[i].currentAttackNumber > 0)
                        {
                            // Choisi un adversaire aléatoirement dans le reste de la liste
                            int idDefender = i;
                            while (idDefender == i)
                            {
                                idDefender = Utils.random.Next(0, characters.Count());
                            }

                            // L'attaquant attaque le defenseur
                            characters[i].Attack(characters[idDefender]);

                            // Gestion de la caratéristique charognard :
                            // Si l'un des personnages est mort pendant l'attaque 
                            if (characters[i].currentLife <= 0 || characters[idDefender].currentLife <= 0)
                            {
                                foreach (var character in characters)
                                {
                                    if (character.currentLife > 0 && character is IScavenger characterScavenger)
                                    {
                                        int lifePointsWon = characterScavenger.LifePointsWon();
                                        character.currentLife += lifePointsWon;
                                        Console.WriteLine(character.name+ " mange le cadavre ce qui lui permet de récupérer "+ lifePointsWon + " points de vie");
                                    }
                                }
                            }
                            
                            // Si il y a un gagnant, on arrête le tour
                            if (this.CheckWinner() == true){break;}
                        }
                    }
                }
            }

            Console.WriteLine("\nBilan point de vie:");
            characters.ForEach(c => Console.WriteLine(c.name + " : " + c.currentLife));

        }

        private List<Character> CalculInitiative(List<Character> characters)
        {
            Dictionary<int, Character> charactersWithInitiative = new Dictionary<int, Character>();
            Console.WriteLine("DEBUG - Jet d'initiative : ");
            foreach (var character in characters)
            {
                //Console.WriteLine(character.Name + " " + (character.Initiative + random.Next(1, 100)));
                int newInitiative = character.CalculJetInitiative();
                while (charactersWithInitiative.ContainsKey(newInitiative))
                {
                    newInitiative = character.CalculJetInitiative();
                }
                charactersWithInitiative.Add(newInitiative, character);
                Console.WriteLine("     " + character.name + " : " + newInitiative);
            }

            List<Character> charactersOrderByInitiative = charactersWithInitiative.OrderByDescending(c => c.Key)
                .Select(c => c.Value)
                .ToList();

            return charactersOrderByInitiative;
        }

        private Boolean CheckWinner()
        {
            Boolean result = false;
            int counterLivingPlayers = 0;
            Character winner = null;

            foreach (var character in characters)
            {
                if (character.currentLife > 0)
                {
                    winner = character;
                    counterLivingPlayers++;
                }
            }

            if (counterLivingPlayers == 1) {
                this.winner = winner;
                result = true; 
            }

            return result;
        }
    }
}
