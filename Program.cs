using LEBON_Nathan_DM_IPI_2021_2022.Model;
using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEBON_Nathan_DM_IPI_2021_2022
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue sur le rendu de mon devoir maison 'LEBON_Nathan_DM_IPI_2021_2022'");
            Console.WriteLine("Matière : INET400 - C#Init par Adrien Gerbex");
            Console.WriteLine("Auteur : Nathan LEBON");
            Console.WriteLine("\n----------------------------------------------------------\n");
            Console.WriteLine("Cet exercice est un jeu textuel dans lequel plusieurs personnages se combattent en utilisant un système de règles inspiré des jeux de rôle.\n");
            Console.WriteLine("Choisi une option pour lancer une simulation de celui-ci:");
            Console.WriteLine("1) Duel (2 personnes)");
            Console.WriteLine("2) Battle royal (10 personnes)");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Tournament duel = new Tournament(new List<Character>{ new Model.Characters.Zombie("Hector"), new Model.Characters.Berseker("Simon") });
                    duel.Run();
                    Console.ReadLine();
                    return true;
                case "2":
                    Tournament battleRoyal = new Tournament(new List<Character> { 
                        new Model.Characters.Berseker("Hector"), 
                        new Model.Characters.Goule("Simon"),
                        new Model.Characters.Guardian("Adele"),
                        new Model.Characters.Kamikaze("Elodie"),
                        new Model.Characters.Liche("Guillaume"),
                        new Model.Characters.Priest("Claire"),
                        new Model.Characters.Robot("Tony"),
                        new Model.Characters.Vampire("Marion"),
                        new Model.Characters.Warrior("Thimotée"),
                        new Model.Characters.Zombie("Elise"),
                    });
                    battleRoyal.Run();
                    Console.ReadLine();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }
    }
}
