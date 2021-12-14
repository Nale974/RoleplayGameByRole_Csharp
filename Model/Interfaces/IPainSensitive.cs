using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface IPainSensitive
    {
        int AttackCapability { get; set; }

        void CalculPainSensitive(int damagesSuffered, int remainingLife)
        {
            //Console.WriteLine("Je suis sensitive de plusieurs rounds");
            //Console.WriteLine("(Dommage subits: " +damagesSuffered+") > (Vie restante: "+remainingLife+") ?");
            if (damagesSuffered > remainingLife)
            {
                int percentLosingAttackAbility = (damagesSuffered - remainingLife) * 2 / (remainingLife + damagesSuffered);
                //Console.WriteLine("Pourcentage de chance d'avoir la DOULEUR: "+percentLosingAttackAbility);
                //Sur une plage de 1 à 100, on tire aléatoirement un nombre.
                //Si le nombre est compris entre 1 et le pourcentage, le personnage perd sa capacité d'attaque. 
                if (Utils.random.Next(1, 101) < percentLosingAttackAbility)
                {
                    AttackCapability = Utils.random.Next(0, 3);
                }
            }
        }
    }
}
