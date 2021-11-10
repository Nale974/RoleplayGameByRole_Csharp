using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.TypeOfCharacter
{
    class Warrior : Character
    {
        public Warrior(string name)
        {
            this.type = "Guerrier";
            this.name = name;
            this.attack = 100;
            this.defense = 100;
            this.initiative = 50;
            this.damages = 100;
            this.maximumLife = 200;
            this.currentLife = 200;
            this.currentAttackNumber = 2;
            this.totalAttackNumber = 2;
        }
    }
}
