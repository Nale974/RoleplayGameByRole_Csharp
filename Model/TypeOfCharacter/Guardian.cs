using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.TypeOfCharacter
{
    class Guardian : Character
    {
        public Guardian(string name)
        {
            this.type = "Gardien";
            this.name = name;
            this.attack = 50;
            this.defense = 150;
            this.initiative = 50;
            this.damages = 50;
            this.maximumLife = 150;
            this.currentLife = 150;
            this.currentAttackNumber = 3;
            this.totalAttackNumber = 3;
        }
    }
}
