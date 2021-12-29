using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Robot : Character, IPainSensitive, ISpecificNumberForJet, IIncreasedAttack
    {
        public int AttackCapability { get; set; } = -1;
        public int SpecificNumberForJet { get; set; } = 50;
        public double IncreasedAttack { get; set; } = 0.5;

        public Robot(string name) : base(name)
        {
            this.type = "Robot";
            this.name = name + "[" + this.type + "]";
            this.attack = 10;
            this.defense = 100;
            this.initiative = 50;
            this.damages = 50;
            this.maximumLife = 200;
            this.currentLife = 200;
            this.currentAttackNumber = 1;
            this.totalAttackNumber = 1;
            this.characterCategory = Enum.CharacterCategory.Living;
        }
    }
}
