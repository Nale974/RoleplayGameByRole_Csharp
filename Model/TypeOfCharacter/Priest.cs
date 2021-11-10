using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.TypeOfCharacter
{
    class Priest : Character
    {
        public Priest(string name)
        {
            this.type = "Prêtre";
            this.name = name;
            this.attack = 50;
            this.defense = 125;
            this.initiative = 20;
            this.damages = 75;
            this.maximumLife = 500;
            this.currentLife = 500;
            this.currentAttackNumber = 6;
            this.totalAttackNumber = 6;
        }
    }
}
