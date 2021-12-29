using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model
{
    class Tournament
    {
        public List<Character> characters = new List<Character>();

        public Tournament(List<Character> characters)
        {
            this.characters = characters;
        }

        public void Run()
        {
            //this.characters.AddRange(new List<Character> { new Characters.Zombie("Hector"), new Characters.Berseker("Simon") });

            // Tant que la vie d'un des personnage est positif, on lance un nouveau round
            var roundNumber = 1;
            var playerWin = false;

            Console.WriteLine("\nBilan point de vie:");
            characters.ForEach(c => Console.WriteLine(c.name + " : " + c.currentLife));

            Character winner = new Character();
            while (playerWin == false)
            {
                Console.WriteLine("\nTapez sur le bouton entrée pour continuez.");
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n Tour " + roundNumber + " :");
                Console.ResetColor();

                Round round = new Round(this.characters);
                round.Run();

                //Si il ne reste qu'un personnage en vie, il gagne
                if(round.winner != null) { 
                    winner = round.winner;
                    break;
                }

                //Sinon round suivant
                this.characters = round.characters;
                roundNumber += 1;
            }

            //Affichage gagnant 
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nLe gagnant est " + winner.name);
            Console.ResetColor();
        }
    }
}
