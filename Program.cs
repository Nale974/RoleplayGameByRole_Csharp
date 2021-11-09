﻿using LEBON_Nathan_DM_IPI_2021_2022.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEBON_Nathan_DM_IPI_2021_2022
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Duel();
        }
        
        //Exercice 1 : Duel
        private static void Duel()
        {
            Model.Character character1 = new Model.Character("Hector", 75, 75, 75, 30, 200, 200, 2, 2);
            Model.Character character2 = new Model.Character("Simon", 75, 75, 75, 30, 200, 200, 2, 2);
            List<Model.Character> characters = new List<Model.Character>() { character1,character2 };

            //Tant que la vie d'un des personnage est positif, on lance un nouveau round
            var roundNumber = 1;
            var playerWin = false;

            Console.WriteLine("\nBilan point de vie:");
            characters.ForEach(c => Console.WriteLine(c.name + " : " + c.currentLife));

            while (playerWin == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("\n Round " + roundNumber + " :");
                Console.ResetColor();
                Console.ReadLine();
                NewRound(characters);
                foreach (var character in characters)
                {
                    if (character.currentLife <= 0){playerWin = true;}
                }
                roundNumber += 1;
            }
        }

        private static void NewRound(List<Model.Character> characters)
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
                //Jusqu'a que le personnage n'a plus d'attaque de disponible
                while (charactersOrderByInitiative[i].currentAttackNumber > 0)
                {
                    //Choisi un adversaire aléatoirement dans le reste de la liste
                    int idDefender = i;
                    while (idDefender == i)
                    {
                        idDefender = random.Next(0, charactersOrderByInitiative.Count());
                    }

                    //Initialisation des 2 joueurs d'une attaque
                    Character attacker = charactersOrderByInitiative[i];
                    Character defender = charactersOrderByInitiative[idDefender];

                    int numAttack = (attacker.totalAttackNumber + 1) - attacker.currentAttackNumber;
                    Console.WriteLine("\n"+ numAttack + "e attaque de " + attacker.name + ", "+ attacker.name + " attaque " + defender.name + " : ");

                    //Calcul de la marge d'attaque
                    int jetAttack = attacker.CalculJetAttack();
                    int jetDefense = defender.CalculJetDefense();
                    int attackMargin = jetAttack - jetDefense;

                    //Si la marge d'attaque est positif
                    Console.Write("DEBUG - Jet d'attaque de " + attacker.name + " = " + jetAttack + " et jet de défense de " + defender.name + " = " + jetDefense + "\n");
                    if (attackMargin > 0)
                    {
                        int damageSuffered = attacker.Attack(defender, attackMargin);

                        Console.WriteLine("DEBUG - " + attacker.name + " inflige " + attackMargin + "*" + attacker.damages + "/100 , soit " + damageSuffered + " de dommage.");
                        Console.Write(attacker.name + " réussi son attaque, " + defender.name + " perd " + damageSuffered + " point de vie.\n");
                    }
                    else //Si négatif
                    {
                        String response = attacker.name + " à raté son attaque. ";
                        if (defender.currentAttackNumber > 0)
                        {
                            int damageSuffered = defender.CounterAttack(attacker, attackMargin);
                            Console.Write(response + defender.name + " contre-attaque et " + attacker.name + " perd " + damageSuffered + " de point de vie.\n");
                            defender.currentAttackNumber--;
                        }
                        else
                        {
                            Console.Write(response + defender.name + " n'a plus d'attaque disponible pour contre-attaquer.\n");
                        }
                    }

                    //incrément du nombre d'attaques restantes
                    attacker.currentAttackNumber--;
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