using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.TypeOfCharacter
{
    class Robot : Character
    {
        public Robot(string name)
        {
            this.type = "Robot";
            this.name = name;
            this.attack = 75;
            this.defense = 125;
            this.initiative = 80;
            this.damages = 50;
            this.maximumLife = 125;
            this.currentLife = 125;
            this.currentAttackNumber = 3;
            this.totalAttackNumber = 3;
        }
    }
}
