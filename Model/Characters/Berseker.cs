using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Berseker : Character, IAllLifeLostForDamage, IIncreasedTotalAttackNumber
    {
        public int totalAttackNumberIncrease { get; set; } = 4;
        public double totalAttackNumberLimit { get; set; } = 0.5;

        public Berseker(String name) : base(name)
        {
            this.type = "Berseker";
            this.name = name + "[" + this.type + "]";
            this.attack = 100;
            this.defense = 100;
            this.initiative = 80;
            this.damages = 70;
            this.maximumLife = 300;
            this.currentLife = 300;
            this.currentAttackNumber = 1;
            this.totalAttackNumber = 1;
        }
    }
}
