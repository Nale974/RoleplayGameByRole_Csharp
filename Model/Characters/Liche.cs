using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Characters
{
    class Liche : Character
    {
        public Liche(string name) : base(name)
        {
            this.type = "Liche";
            this.name = name + "[" + this.type + "]";
            this.attack = 75;
            this.defense = 125;
            this.initiative = 80;
            this.damages = 50;
            this.maximumLife = 125;
            this.currentLife = 125;
            this.currentAttackNumber = 3;
            this.totalAttackNumber = 3;
            this.characterCategory = Enum.CharacterCategory.Undead;
            this.characterFeature = Enum.CharacterFeature.Cursed;
            this.damageFeature = Enum.DamageFeature.Unholy;
        }
    }
}
