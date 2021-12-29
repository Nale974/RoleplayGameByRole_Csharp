using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Guardian : Character, IPainSensitive, ICounterAttackBonus
    {
        int IPainSensitive.AttackCapability { get; set; } = -1;
        int ICounterAttackBonus.CounterAttackBonus { get; set; } = 2;
        public Guardian(String name) : base(name)
        {
            this.type = "Gardien";
            this.name = name + "[" + this.type + "]";
            this.attack = 50;
            this.defense = 150;
            this.initiative = 50;
            this.damages = 50;
            this.maximumLife = 150;
            this.currentLife = 150;
            this.currentAttackNumber = 3;
            this.totalAttackNumber = 3;
            this.characterCategory = Enum.CharacterCategory.Living;
            this.damageFeature = Enum.DamageFeature.Holy;
        }

    }
}
