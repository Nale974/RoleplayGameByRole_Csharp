using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Robot : Character, IPainSensitive, ISpecificNumberForJet
    {
        public int AttackCapability { get; set; } = -1;
        public int SpecificNumberForJet { get; set; } = 50;
        public Robot(string name)
        {
            this.type = "Robot";
            this.name = name + "[" + this.type + "]";
            this.attack = 75;
            this.defense = 125;
            this.initiative = 80;
            this.damages = 50;
            this.maximumLife = 125;
            this.currentLife = 125;
            this.currentAttackNumber = 3;
            this.totalAttackNumber = 3;
        }
    }
}
