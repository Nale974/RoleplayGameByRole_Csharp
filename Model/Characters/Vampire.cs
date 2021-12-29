using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Vampire : Character, ICareAccordingToDamageInflicted
    {
        public int DamageInflicted { get; set; }
        public double CareAccordingPercent { get; set; }

        public Vampire(string name) : base(name)
        {
            this.type = "Vampire";
            this.name = name + "[" + this.type + "]";
            this.attack = 100;
            this.defense = 100;
            this.initiative = 120;
            this.damages = 50;
            this.maximumLife = 300;
            this.currentLife = 300;
            this.currentAttackNumber = 2;
            this.totalAttackNumber = 2;
            this.characterCategory = Enum.CharacterCategory.Undead;
            this.characterFeature = Enum.CharacterFeature.Cursed;
            this.DamageInflicted = 0;
            this.CareAccordingPercent = 0.50;
        }
    }
}
