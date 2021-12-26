using LEBON_Nathan_DM_IPI_2021_2022.Model.Enum;
using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public DamageFeature damageFeature = DamageFeature.None;
        public CharacterFeature characterFeature = CharacterFeature.None;

        public Character(){}

        public Character(string name)
        {
            this.name = name;
        }

        public int CalculJetAttack()
        {
            return this.attack + Utils.random.Next(0, 100);
        }

        public int CalculJetDefense()
        {
            return this.defense+ Utils.random.Next(0, 100);
        }

        public int CalculJetInitiative()
        {
            return this.defense + Utils.random.Next(0, 100);
        }

        public void Attack(Character opponent, int bonus=0, int numberCounterAttack=0)
        {
            String tabulation = String.Concat(Enumerable.Repeat("   ", numberCounterAttack));
            //Vérification utile pour la contre-attaque
            if (this.currentAttackNumber > 0)
            {
                Console.WriteLine(tabulation + "Il reste " + this.currentAttackNumber + " attaque a " + this.name + ", " + this.name + " attaque " + opponent.name + " : ");

                //Calcul de la marge d'attaque
                int jetAttack = this.CalculJetAttack() + (-1 * bonus);
                int jetDefense = opponent.CalculJetDefense();
                int attackMargin = jetAttack - jetDefense;

                //Si la marge d'attaque est positif
                Console.Write(tabulation + "DEBUG - Jet d'attaque de " + this.name + " = " + jetAttack + " et jet de défense de " + opponent.name + " = " + jetDefense + "\n");
                if (attackMargin > 0)
                {
                    //Gestion des types de personnage et d'attaque
                    int criticalDamage = 1;
                    foreach (var weakness in DamageFeatureDictionnary.weaknessList[opponent.characterFeature.ToString()])
                    {
                        if (this.damageFeature.ToString() == weakness) {
                            criticalDamage = 2; 
                            Console.WriteLine(tabulation + opponent.name + " est sensible au type "+ weakness + ". Les dommages sont multipliés par 2 !"); 
                        }
                    }

                    //Gestion de la caractéristique d'ajouter les points de vie perdu aux dégâts
                    int lifelost = 0;
                    if(this is IAllLifeLostForDamage thisCharacterAllLifeLostForDamage) { 
                        lifelost = thisCharacterAllLifeLostForDamage.CalculLifeLost(this.maximumLife, this.currentLife);
                        Console.WriteLine(tabulation + this.name+" ajoute ses "+ lifelost +" points de vie perdu à ses dégats.");
                    }

                    //Calcul de l'attaque et déduction avec pdv de l'adversaires
                    int damagesSuffered = (attackMargin * (this.damages + lifelost) / 100)*criticalDamage;
                    opponent.currentLife -= damagesSuffered;

                    Console.WriteLine(tabulation + "DEBUG - " + this.name + " inflige (" + attackMargin + "*(" + this.damages +"+"+ lifelost+")/100)*"+criticalDamage+" , soit " + damagesSuffered +" de dommage.");
                    Console.Write(tabulation + this.name + " réussi son attaque, " + opponent.name + " perd " + damagesSuffered + " point de vie.\n");

                    if (opponent.currentLife > 0 && opponent is IPainSensitive opponentPainSensitive)
                    {
                        opponentPainSensitive.CalculPainSensitive(damagesSuffered, opponent.currentLife);
                        if (opponentPainSensitive.AttackCapability >= 0)
                        {
                            Console.WriteLine(tabulation + opponent.name + "est sensible à la douleur, il est bloqué pendant " + (opponentPainSensitive.AttackCapability + 1) + " tour(s).");
                        }
                    }

                    if (opponent.currentLife <= 0) { 
                        Console.WriteLine(tabulation + opponent.name+" est K.O."); 
                    }
                    this.currentAttackNumber--;
                }
                else //Si négatif
                {
                    Console.WriteLine(tabulation + this.name + " à raté son attaque. ");
                    Console.WriteLine(tabulation + opponent.name + " tente une contre-attaque.");
                    numberCounterAttack++;

                    this.currentAttackNumber--;

                    // Gestion des bonus de contre-attaque
                    if(opponent is ICounterAttackBonus opponentCounterAttackBonus)
                    {
                        Console.WriteLine(tabulation+opponent.name+" à un bonus de contre-attaque de "+ opponentCounterAttackBonus.CounterAttackBonus + " !");
                        opponent.Attack(this, (attackMargin*opponentCounterAttackBonus.CounterAttackBonus) , numberCounterAttack);
                    }
                    else { opponent.Attack(this, attackMargin, numberCounterAttack); }
                    
                }
            }
            else
            {
                Console.Write(tabulation + this.name + " n'a plus de point d'attaque de disponible.\n");
            }
        }

    }
}
