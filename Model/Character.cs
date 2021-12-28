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
        public String type { get; set; }
        public String name { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int initiative { get; set; }
        public int damages { get; set; }
        public int maximumLife { get; set; }
        public int currentLife { get; set; }
        public int currentAttackNumber { get; set; }
        public int totalAttackNumber { get; set; }
        public DamageFeature damageFeature { get; set; }
        public CharacterFeature characterFeature { get; set; }
        public bool myPossibilityToCounterAttacksOfMyOpponent { get; set; }
        public bool thePossibilityThatMyOpponentsCounterMyAttack { get; set; }

        public Character(){}

        public Character(string name)
        {
            this.name = name;
            this.damageFeature = DamageFeature.None;
            this.characterFeature = CharacterFeature.None;
            this.myPossibilityToCounterAttacksOfMyOpponent = true;
            this.thePossibilityThatMyOpponentsCounterMyAttack = true;
        }

        protected int GenerateNumberForJet()
        {
            //Gestion de jet spécifique
            if (this is ISpecificNumberForJet IspecificNumberForJet)
            {
                return IspecificNumberForJet.SpecificNumberForJet;
            }
            else { return Utils.random.Next(0, 100); }
        }

        public int CalculJetAttack()
        {
            return this.attack + GenerateNumberForJet();
        }

        public int CalculJetDefense()
        {
            return this.defense + GenerateNumberForJet();
        }

        public int CalculJetInitiative()
        {
            return this.initiative + GenerateNumberForJet();
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
                    // Gestion de la particularité des types de personnage et d'attaque
                    int criticalDamage = 1;
                    foreach (var weakness in DamageFeatureDictionnary.weaknessList[opponent.characterFeature.ToString()])
                    {
                        if (this.damageFeature.ToString() == weakness) {
                            criticalDamage = 2; 
                            Console.WriteLine(tabulation + opponent.name + " est sensible au type "+ weakness + ". Les dommages sont multipliés par 2 !"); 
                        }
                    }

                    // Gestion de la particularité d'ajouter les points de vie perdu aux dégâts
                    int lifelost = 0;
                    if(this is IAllLifeLostForDamage thisCharacterAllLifeLostForDamage) { 
                        lifelost = thisCharacterAllLifeLostForDamage.CalculLifeLost(this.maximumLife, this.currentLife);
                        Console.WriteLine(tabulation + this.name+" ajoute ses "+ lifelost +" points de vie perdu à ses dégats.");
                    }

                    // Calcul de l'attaque et déduction avec pdv de l'adversaire
                    int damagesSuffered = (attackMargin * (this.damages + lifelost) / 100)*criticalDamage;
                    opponent.currentLife -= damagesSuffered;

                    Console.WriteLine(tabulation + "DEBUG - " + this.name + " inflige (" + attackMargin + "*(" + this.damages +"+"+ lifelost+")/100)*"+criticalDamage+" , soit " + damagesSuffered +" de dommage.");
                    Console.Write(tabulation + this.name + " réussi son attaque, " + opponent.name + " perd " + damagesSuffered + " point de vie.\n");

                    // Gestion de la particularité de la récupération de vie selon les dégats infligés
                    if (this is ICareAccordingToDamageInflicted iCareAccordingToDamageInflicted)
                    {
                        int recoveredLife = (int)(damagesSuffered * iCareAccordingToDamageInflicted.CareAccordingPercent);
                        Console.WriteLine(tabulation+this.name+" récupére "+recoveredLife+" de point de vie en attaquant.");
                        this.currentLife += recoveredLife;
                    }

                    // Gestion de la caratéristique de la douleur : 
                    // Si l'adversaire est sensible à la douleur
                    if (opponent.currentLife > 0 && opponent is IPainSensitive opponentPainSensitive)
                    {
                        opponentPainSensitive.CalculPainSensitive(damagesSuffered, opponent.currentLife);
                        if (opponentPainSensitive.AttackCapability >= 0)
                        {
                            Console.WriteLine(tabulation + opponent.name + "est sensible à la douleur, il est bloqué pendant " + (opponentPainSensitive.AttackCapability + 1) + " tour(s).");
                        }
                    }

                    // Gestion de la particularité de l'augmentation du "TotalAttackNumber" de l'adversaire
                    if (opponent is IIncreasedTotalAttackNumber opponentITAN)
                    {
                        Console.WriteLine(tabulation + "DEBUG - " + opponent.currentLife +" < "+ opponentITAN.totalAttackNumberLimit * opponent.currentLife);
                        if (opponent.currentLife < opponentITAN.totalAttackNumberLimit * opponent.currentLife)
                        {
                            Console.WriteLine("Les points de vie de " + opponent.name + " descendent en dessous des " + opponentITAN.totalAttackNumberLimit * 100 + "%. Son nombre d'attaque total passe alors de " + opponentITAN.totalAttackNumber + " à " + opponentITAN.totalAttackNumberIncrease + " !");
                            opponentITAN.totalAttackNumber = opponentITAN.totalAttackNumberIncrease;
                        }
                    }

                    if (opponent.currentLife <= 0) {Console.WriteLine(tabulation + opponent.name+" est K.O.");}
                    this.currentAttackNumber--;
                }
                else //Si négatif
                {
                    Console.WriteLine(tabulation + this.name + " à raté son attaque. ");
                    this.currentAttackNumber--;

                    // Gestion de la particularité de la possibilité de contrer les attaques de mes adversaire
                    // Et de la particularité que les adversaires puissent contrer mon attaque
                    if (opponent.myPossibilityToCounterAttacksOfMyOpponent == true && this.thePossibilityThatMyOpponentsCounterMyAttack == true)
                    {
                        Console.WriteLine(tabulation + opponent.name + " tente une contre-attaque.");
                        numberCounterAttack++;

                        // Gestion de la particularité des bonus de contre-attaque
                        if (opponent is ICounterAttackBonus opponentCounterAttackBonus)
                        {
                            Console.WriteLine(tabulation + opponent.name + " à un bonus de contre-attaque de " + opponentCounterAttackBonus.CounterAttackBonus + " !");
                            opponent.Attack(this, (attackMargin * opponentCounterAttackBonus.CounterAttackBonus), numberCounterAttack);
                        }
                        else { opponent.Attack(this, attackMargin, numberCounterAttack); }
                    }
                    else { Console.WriteLine( opponent.name + " ne peut pas contre-attaquer."); }
                    
                }
            }
            else
            {
                Console.Write(tabulation + this.name + " n'a plus de point d'attaque de disponible.\n");
            }
        }

    }
}
