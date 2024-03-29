﻿using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Goule : Character, IScavenger
    {
        public Goule(string name) : base(name)
        {
            this.type = "Goule";
            this.name = name + "[" + this.type + "]";
            this.attack = 50;
            this.defense = 80;
            this.initiative = 120;
            this.damages = 30;
            this.maximumLife = 250;
            this.currentLife = 250;
            this.currentAttackNumber = 5;
            this.totalAttackNumber = 5;
            this.characterCategory = Enum.CharacterCategory.Undead;
            this.characterFeature = Enum.CharacterFeature.Cursed;
        }
    }
}
