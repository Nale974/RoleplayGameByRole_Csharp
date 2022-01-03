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
            // Rétablissement du nombre d'attaques par défaut par personnages
            characters.ForEach(c => c.currentAttackNumber = c.totalAttackNumber);

            // Suivi du nombre d'attaques
            /*Console.WriteLine("DEBUG - Nombre d'attaque : ");
            characters.ForEach(c => Console.WriteLine(c.name + " : " + c.currentAttackNumber));
            Console.WriteLine("");*/

            // Calcul d'initiative
            this.characters = CalculInitiative(characters);
            Console.WriteLine("");

            // Gestion des particularités de début de tour
            foreach (Character character in characters)
            {
                // Gestion de la particularité d'augmentation de l'attaque à chaque tour
                if (character.currentLife > 0 && character is IIncreasedAttack characterIncreasedAttack)
                {
                    characterIncreasedAttack.attack += (int)(characterIncreasedAttack.attack * characterIncreasedAttack.IncreasedAttack);
                    Console.WriteLine("L'attaque de " + character.name + " augmente de " + characterIncreasedAttack.IncreasedAttack * 100 + "%, il est maintenant de " + characterIncreasedAttack.attack + ".");
                }

                // Gestion de la particularité de soins récurrent
                if (character.currentLife > 0 && character is IRecurrentCare characterRecurrentCare)
                {
                    characterRecurrentCare.Care();
                    Console.WriteLine(character.name + " regagne " + characterRecurrentCare.recurrentCarePercent * 100 + "% de sa vie maximum, il a maintenant " + character.currentLife + " pdv.");
                }
            }

            // Pour chaque personnage du round
            for (int i = 0; i < characters.Count(); i++)
            {
                //Si il y a un gagnant, on arrête le tour
                if (this.CheckWinner() == true) { break; }

                // Si le personnage est en vie
                if (characters[i].currentLife > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nC'est au tour de " + characters[i].name + ".");
                    Console.ResetColor();
                    Console.Write("\n");

                    // Gestion de la particularité de la douleur : 
                    // Si le personnage est victime de la douleur
                    if (characters[i] is IPainSensitive characterPainSensitive && characterPainSensitive.AttackCapability >= 0)
                    {
                        Console.WriteLine(characters[i].name + " est bloqué pendant encore " + (characterPainSensitive.AttackCapability + 1) + " tour(s).");
                        characterPainSensitive.AttackCapability -= 1;
                    }
                    else
                    {
                        if (characters[i].currentAttackNumber == 0) { Console.WriteLine(characters[i].name+" n'a plus d'attaque disponible."); }
                        // Jusqu'a que le personnage n'a plus d'attaque disponible
                        while (characters[i].currentAttackNumber > 0)
                        {
                            List<Character> defenders = new List<Character>();

                            // Gestion de la particularité des attaques multiples
                            if (characters[i].multipleAttack == true)
                            {
                                foreach (Character character in characters)
                                {
                                    if (character.currentLife > 0 && Utils.random.Next(0, 100) < 50)
                                    {
                                        defenders.Add(character);
                                    }
                                }
                            }
                            else // Cas classique
                            {
                                // Gestion de la particularité des attaques ciblés
                                if (characters[i] is ITargetsPriorityCategory characterTargetsPriorityCategory)
                                {
                                    int idDefenderPriorityCategory;
                                    List<Character> charactersAliveTargetsPriorityCategory = characterTargetsPriorityCategory.CharactersAliveTargetsPriorityCategory(this.characters);
                                    if (charactersAliveTargetsPriorityCategory.Count() != 0)
                                    {
                                        idDefenderPriorityCategory = Utils.random.Next(0, charactersAliveTargetsPriorityCategory.Count());
                                        defenders.Add(charactersAliveTargetsPriorityCategory[idDefenderPriorityCategory]);
                                    }
                                }

                                // Cas classique
                                if (defenders.Count() == 0)
                                {
                                    // Choisi un adversaire aléatoirement dans le reste de la liste
                                    int idDefender = i;
                                    while (idDefender == i || characters[idDefender].currentLife < 0)
                                    {
                                        idDefender = Utils.random.Next(0, characters.Count());
                                    }

                                    defenders.Add(characters[idDefender]);
                                }
                                
                            }

                            // L'attaquant attaque les defenseurs
                            characters[i].Attack(defenders);

                            //Gestion de la particularité de la douleur
                            if (characters[i] is IPainSensitive characterPainSensitiveSecure && characterPainSensitiveSecure.AttackCapability >= 0) { break;}

                            // Gestion de la particularité du charognard :
                            // Si l'un ou plusieurs des personnages est mort pendant l'attaque
                            int deaths = 0;
                            if (characters[i].currentLife <= 0){ deaths++; }
                            foreach (Character defender in defenders)
                            {
                                if (defender != characters[i] && defender.currentLife <= 0)
                                {
                                    deaths++;
                                }
                            }
                            foreach (Character character in characters)
                            {
                                if (character.currentLife > 0 && character is IScavenger characterScavenger)
                                {
                                    int lifePointsWon = characterScavenger.LifePointsWon(deaths);
                                    if (lifePointsWon>0) { Console.WriteLine(character.name + " mange le(s) cadavre(s) ce qui lui permet de récupérer " + lifePointsWon + " points de vie"); }
                                }
                            }

                            // Si il y a un gagnant, on arrête le tour
                            if (this.CheckWinner() == true){break;}
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nBilan point de vie:");
            Console.ResetColor();
            characters.ForEach(c => Console.WriteLine(c.name + " : " + c.currentLife));

        }

        private List<Character> CalculInitiative(List<Character> characters)
        {
            Dictionary<int, Character> charactersWithInitiative = new Dictionary<int, Character>();
            Console.WriteLine("Jet d'initiative : ");
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
