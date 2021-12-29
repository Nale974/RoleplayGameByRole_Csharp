using LEBON_Nathan_DM_IPI_2021_2022.Model.Enum;
using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Priest : Character, IPainSensitive, ITargetsPriorityCategory
    {
        public int AttackCapability { get; set; } = -1;
        public CharacterCategory targetsPriorityCategory { get; set; }
        public Priest(string name) : base(name)
        {
            this.type = "Prêtre";
            this.name = name + "[" + this.type + "]";
            this.attack = 75;
            this.defense = 125;
            this.initiative = 50;
            this.damages = 50;
            this.maximumLife = 150;
            this.currentLife = 150;
            this.currentAttackNumber = 1;
            this.totalAttackNumber = 1;
            this.characterCategory = Enum.CharacterCategory.Living;
            this.damageFeature = Enum.DamageFeature.Holy;
            this.characterFeature = Enum.CharacterFeature.Blessed;
            this.targetsPriorityCategory = CharacterCategory.Undead;
        }
    }
}
