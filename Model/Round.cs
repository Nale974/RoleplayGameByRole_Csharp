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
            List<Character> charactersOrderByInitiative = CalculInitiative(characters);

            //Pour chaque personnage du round
            for (int i = 0; i < charactersOrderByInitiative.Count(); i++)
            {
                if (this.CheckWinner() == true) { break; }
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
                        //jusqu'a que le personnage n'a plus d'attaque de disponible
                        while (charactersOrderByInitiative[i].currentAttackNumber > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nC'est au tour de " + charactersOrderByInitiative[i].name + ".");
                            Console.ResetColor();
                            Console.Write("\n");

                            //Choisi un adversaire aléatoirement dans le reste de la liste
                            int idDefender = i;
                            while (idDefender == i)
                            {
                                idDefender = Utils.random.Next(0, charactersOrderByInitiative.Count());
                            }

                            //Initialisation des 2 joueurs d'une attaque
                            Character attacker = charactersOrderByInitiative[i];
                            Character defender = charactersOrderByInitiative[idDefender];

                            attacker.Attack(defender);
                            if (this.CheckWinner() == true){break;}
                        }
                    }
                }
            }

            Console.WriteLine("\nBilan point de vie:");
            charactersOrderByInitiative.ForEach(c => Console.WriteLine(c.name + " : " + c.currentLife));

        }

        private List<Character> CalculInitiative(List<Character> characters)
        {
            Dictionary<int, Character> charactersWithInitiative = new Dictionary<int, Character>();
            Console.WriteLine("DEBUG - Jet d'initiative : ");
            foreach (var character in characters)
            {
                //Console.WriteLine(character.Name + " " + (character.Initiative + random.Next(1, 100)));
                int newInitiative = character.initiative + Utils.random.Next(1, 100);
                while (charactersWithInitiative.ContainsKey(newInitiative))
                {
                    newInitiative = character.initiative + Utils.random.Next(1, 100);
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
