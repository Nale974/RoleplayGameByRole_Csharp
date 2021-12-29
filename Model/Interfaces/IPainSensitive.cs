using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface IPainSensitive
    {
        int AttackCapability { get; set; }
        int currentLife { get; set; }

        void CalculPainSensitive(int damagesSuffered)
        {
            if (damagesSuffered > this.currentLife)
            {
                int percentLosingAttackAbility = (damagesSuffered - this.currentLife) * 2 / (this.currentLife + damagesSuffered);
                //Sur une plage de 1 à 100, on tire aléatoirement un nombre.
                //Si le nombre est compris entre 1 et le pourcentage, le personnage perd sa capacité d'attaque. 
                if (Utils.random.Next(0, 100) < percentLosingAttackAbility)
                {
                    AttackCapability = Utils.random.Next(0, 3);
                }
            }
        }
    }
}
