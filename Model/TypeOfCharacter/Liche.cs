using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.TypeOfCharacter
{
    class Liche : Character
    {
        public Liche(string name)
        {
            this.type = "Goule";
            this.name = name;
            this.attack = 100;
            this.defense = 100;
            this.initiative = 120;
            this.damages = 50;
            this.maximumLife = 300;
            this.currentLife = 300;
            this.currentAttackNumber = 2;
            this.totalAttackNumber = 2;
        }
    }
}
