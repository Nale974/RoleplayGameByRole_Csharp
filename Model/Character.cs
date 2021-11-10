using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model
{
    class Character
    {
        public String type;
        public String name;
        public int attack;
        public int defense;
        public int initiative;
        public int damages;
        public int maximumLife;
        public int currentLife;
        public int currentAttackNumber;
        public int totalAttackNumber;

        private readonly Random random = new Random();

        public Character()
        {
        }

        public Character(string name, int attack, int defense, int initiative, int damages, int maximumLife, int currentLife, int currentAttackNumber, int totalAttackNumber)
        {
            this.name = name;
            this.attack = attack;
            this.defense = defense;
            this.initiative = initiative;
            this.damages = damages;
            this.maximumLife = maximumLife;
            this.currentLife = currentLife;
            this.currentAttackNumber = currentAttackNumber;
            this.totalAttackNumber = totalAttackNumber;
        }

        public int CalculJetAttack()
        {
            return this.attack + random.Next(0, 100);
        }

        public int CalculJetDefense()
        {
            return this.defense+ random.Next(0, 100);
        }

        public int CalculJetInitiative()
        {
            return this.defense + random.Next(0, 100);
        }

        public int Attack(Character opponent, int attackMargin)
        {
            int damageSuffered = attackMargin * this.damages / 100;
            opponent.currentLife -= damageSuffered;

            return damageSuffered;
        }

        public int CounterAttack(Character opponent, int attackMargin)
        {
            int damageSuffered = this.damages + random.Next(0, 100) + (attackMargin * -1);
            opponent.currentLife -= damageSuffered;

            return damageSuffered;
        }
    }
}
