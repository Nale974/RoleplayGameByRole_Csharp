﻿using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Kamikaze : Character, IPainSensitive
    {
        public int AttackCapability { get; set; } = -1;
        public Kamikaze(string name) : base(name)
        {
            this.type = "Kamikaze";
            this.name = name + "[" + this.type + "]";
            this.attack = 50;
            this.defense = 125;
            this.initiative = 20;
            this.damages = 75;
            this.maximumLife = 500;
            this.currentLife = 500;
            this.currentAttackNumber = 6;
            this.totalAttackNumber = 6;
            this.characterCategory = Enum.CharacterCategory.Living;
            this.myPossibilityToCounterAttacksOfMyOpponent = false;
            this.thePossibilityThatMyOpponentsCounterMyAttack = false;
            this.multipleAttack = true;
        }
    }
}
