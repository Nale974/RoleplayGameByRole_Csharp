using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Zombie : Character, IScavenger
    {
        public Zombie(string name) : base(name)
        {
            this.type = "Zombie";
            this.name = name + "[" + this.type + "]";
            this.attack = 100;
            this.defense = 0;
            this.initiative = 20;
            this.damages = 60;
            this.maximumLife = 1000;
            this.currentLife = 1000;
            this.currentAttackNumber = 1;
            this.totalAttackNumber = 1;
            this.characterCategory = Enum.CharacterCategory.Undead;
            this.characterFeature = Enum.CharacterFeature.Cursed;
            this.myPossibilityToCounterAttacksOfMyOpponent = false;
        }
    }
}
