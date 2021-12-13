using LEBON_Nathan_DM_IPI_2021_2022.Model;
using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEBON_Nathan_DM_IPI_2021_2022
{
    class Program
    {
        public static Random random = new Random();
        static void Main(string[] args)
        {
            //for (int i = 0; i < 10; i++)
            //{
                Tournament();
            //}
        }
        
        private static void Tournament()
        {
            Model.Character character1 = new Model.Characters.Guardian("Hector");
            Model.Character character2 = new Model.Characters.Liche("Simon");
            List<Model.Character> characters = new List<Model.Character>() { character1,character2 };

            //Tant que la vie d'un des personnage est positif, on lance un nouveau round
            var roundNumber = 1;
            var playerWin = false;

            Console.WriteLine("\nBilan point de vie:");
            characters.ForEach(c => Console.WriteLine(c.name + " : " + c.currentLife));

            Character winner = new Character();
            while (playerWin == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n Tour " + roundNumber + " :");
                Console.ResetColor();
                //Console.ReadLine();

                Round(characters);

                //Si il ne reste qu'un personnage en vie, il gagne
                int counterLivingPlayers = 0;
                foreach (var character in characters)
                {
                    if (character.currentLife > 0) {
                        winner = character;
                        counterLivingPlayers ++; 
                    }
                }

                if (counterLivingPlayers==1) {playerWin = true;}

                //Sinon round suivant
                roundNumber += 1;
            }

            //Affichage gagnant 
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nLe gagnant est " + winner.name);
            Console.ResetColor();
        }

        private static void Round(List<Model.Character> characters)
        {
            //Rétablissement des niveaux d'attaque par défaut
            characters.ForEach(c => c.currentAttackNumber = c.totalAttackNumber);

            //Suivi nombre d'attaque
            Console.WriteLine("DEBUG - Nombre d'attaque : ");
            characters.ForEach(c => Console.WriteLine("     "+c.name+" : "+c.currentAttackNumber));
            Console.WriteLine("");

            //Calcul d'initiative
            List<Character> charactersOrderByInitiative = CalculInitiative(characters);
            
            //Pour chaque personnage du round
            for (int i = 0; i < charactersOrderByInitiative.Count(); i++)
            {
                //Si le personnage est en vie
                if (charactersOrderByInitiative[i].currentLife > 0)
                {
                    //Cas où le personnage est sensible et victime de la douleur
                    if (charactersOrderByInitiative[i] is IPainSensitive characterPainSensitive && characterPainSensitive.AttackCapability >= 0)
                    {
                        Console.WriteLine(charactersOrderByInitiative[i].name + " est bloqué pendant encore " + (characterPainSensitive.AttackCapability + 1) + " tour(s).");
                        characterPainSensitive.AttackCapability -= 1;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nC'est au tour de "+ charactersOrderByInitiative[i].name+ ".");
                        Console.ResetColor();
                        //jusqu'a que le personnage n'a plus d'attaque de disponible
                        while (charactersOrderByInitiative[i].currentAttackNumber > 0)
                        {
                            Console.Write("\n");

                            //Choisi un adversaire aléatoirement dans le reste de la liste
                            int idDefender = i;
                            while (idDefender == i)
                            {
                                idDefender = random.Next(0, charactersOrderByInitiative.Count());
                            }

                            //Initialisation des 2 joueurs d'une attaque
                            Character attacker = charactersOrderByInitiative[i];
                            Character defender = charactersOrderByInitiative[idDefender];

                            attacker.Attack(defender);
                        }
                    }
                }
            }

            Console.WriteLine("\nBilan point de vie:");
            charactersOrderByInitiative.ForEach(c => Console.WriteLine(c.name+" : "+c.currentLife));

        }

        private static List<Character> CalculInitiative(List<Model.Character> characters)
        {
            Dictionary<int, Character> charactersWithInitiative = new Dictionary<int, Character>();
            Console.WriteLine("DEBUG - Jet d'initiative : ");
            foreach (var character in characters)
            {
                //Console.WriteLine(character.Name + " " + (character.Initiative + random.Next(1, 100)));
                int newInitiative = character.initiative + random.Next(1, 100);
                while (charactersWithInitiative.ContainsKey(newInitiative))
                {
                    newInitiative = character.initiative + random.Next(1, 100);
                }
                charactersWithInitiative.Add(newInitiative, character);
                Console.WriteLine("     "+character.name+" : "+newInitiative);
            }

            List<Character> charactersOrderByInitiative = charactersWithInitiative.OrderByDescending(c => c.Key)
                .Select(c => c.Value)
                .ToList();

            return charactersOrderByInitiative;
        }

    }
}
