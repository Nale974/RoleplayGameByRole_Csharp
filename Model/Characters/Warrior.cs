﻿using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Warrior : Character, IPainSensitive
    {
        public int AttackCapability { get; set; } = -1;
        public Warrior(string name) : base(name)
        {
            this.type = "Guerrier";
            this.name = name + "[" + this.type + "]";
            this.attack = 100;
            this.defense = 100;
            this.initiative = 50;
            this.damages = 100;
            this.maximumLife = 200;
            this.currentLife = 200;
            this.currentAttackNumber = 2;
            this.totalAttackNumber = 2;
            this.characterCategory = Enum.CharacterCategory.Living;
        }
        void IPainSensitive.CalculPainSensitive(int damagesSuffered){
            //Gestion de calcul de la douleur spécifique au guerrier
            //(Si ce trait de personnage devient plus commun il faudrait en faire une autre interface)
            if (damagesSuffered > this.currentLife)
            {
                int percentLosingAttackAbility = (damagesSuffered - currentLife) * 2 / (currentLife + damagesSuffered);
                if (Utils.random.Next(1, 101) < percentLosingAttackAbility)
                {
                    AttackCapability = 0;
                }
            }
        }
    }
}
