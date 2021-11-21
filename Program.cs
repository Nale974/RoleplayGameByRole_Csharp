using LEBON_Nathan_DM_IPI_2021_2022.Model;
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
            Tournament();
        }
        
        private static void Tournament()
        {
            Model.Character character1 = new Model.Character("Hector", 75, 75, 400, 30, 200, 200, 5, 5);
            Model.Character character2 = new Model.Character("Simon", 75, 75, 75, 30, 200, 200, 5, 5);
            List<Model.Character> characters = new List<Model.Character>() { character1,character2 };

            //Tant que la vie d'un des personnage est positif, on lance un nouveau round
            var roundNumber = 1;
            var playerWin = false;

            Console.WriteLine("\nBilan point de vie:");
            characters.ForEach(c => Console.WriteLine(c.name + " : " + c.currentLife));

            while (playerWin == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n Round " + roundNumber + " :");
                Console.ResetColor();
                Console.ReadLine();
                Round(characters);
                foreach (var character in characters)
                {
                    if (character.currentLife <= 0){playerWin = true;}
                }
                roundNumber += 1;
            }

            //Affichage gagnant 
            Character winner = new Character();
            foreach (var character in characters)
            {
                if (character.currentLife > 0) { winner = character; }
            }
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
                        Counter(defender,attacker,attackMargin);
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

        private static void Counter(Character counterAttacker, Character counterDefender, int bonus)
        {
            String response = counterDefender.name + " à raté son attaque. ";
            
            if (counterAttacker.currentAttackNumber > 0)
            {
                int numCounterAttack = (counterAttacker.totalAttackNumber + 1) - counterAttacker.currentAttackNumber;
                Console.WriteLine("\n" + response + "Il reste " + counterAttacker.currentAttackNumber + " attaque à " + counterAttacker.name + ", " + counterAttacker.name + " contre attaque " + counterDefender.name + " : ");

                //Calcul de la marge d'attaque
                int counterJetAttack = counterAttacker.CalculJetAttack() +(-1 * bonus);
                int counterJetDefense = counterDefender.CalculJetDefense();
                int counterAttackMargin = counterJetAttack - counterJetDefense;

                //Si la marge d'attaque est positif
                Console.Write("DEBUG - Jet d'attaque de " + counterAttacker.name + " = " + counterJetAttack + " et jet de défense de " + counterDefender.name + " = " + counterJetDefense + "\n");
                Console.WriteLine("Le bonus est de "+bonus);
                if (counterAttackMargin > 0)
                {
                    int damageSuffered = counterAttacker.Attack(counterDefender, counterAttackMargin);

                    Console.WriteLine("DEBUG - " + counterAttacker.name + " inflige " + counterAttackMargin + "*" + counterAttacker.damages + "/100 , soit " + damageSuffered + " de dommage.");
                    Console.Write(counterAttacker.name + " réussi sa contre-attaque, " + counterDefender.name + " perd " + damageSuffered + " point de vie.\n");
                }
                else
                {
                    Counter(counterDefender, counterAttacker, counterAttackMargin);
                    Console.WriteLine("ContreAttaque raté");
                }
                //int damageSuffered = defender.CounterAttack(attacker, attackMargin);
                //Console.Write(response + defender.name + " contre-attaque et " + attacker.name + " perd " + damageSuffered + " de point de vie.\n");
                counterAttacker.currentAttackNumber--;
            }
            else
            {
                Console.Write(response + counterAttacker.name + " n'a plus d'attaque disponible pour contre-attaquer.\n");
            }
        }
    }
}
