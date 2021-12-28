- La douleur
	- Personnage sensible -> Interface
	  (Peut perd capacit� d'attaquer pendant le tour + 1 ou 2 suivants)
		- Gardien, Robot, Kamikaze, Pr�tre, Goule
		- Guerrier = perd capacit� d'attaque seulement pendant le tour actuel
	- Personnage non sensible -> Sans impl�mentation d'interface
		- Berserker
		- Zombie, Liche, Vampire

- D�g�ts sacr�s ou impies, personnages maudits ou b�nis -> 2 Enum�rations et une classe statique faisant le lien
	- Impies attaque b�nis = d�gats*2
	- Sacr� attaque maudit = d�gats*2
	- Par personnage : 
		- Gardien = inflige sacr�e
		- Liche = inflige impie
		- Pr�tre = inflige sacr�e et est b�ni
		- Morts-vivants (Zombie, Liche, Goule, Vampire) =  sont Maudit

- Charognard -> interface
	- Quand un autre perso meurt le charognard gagne entre 50 et 100 pdv
	- Liste personnage : Zombie, Goule

Autres : 
	- Bonus de contre-attaque doubl� (Gardien)
	- Ajout tous les points de vie perdu aux d�g�ts pour l'attaque (Berserker)
	- TotalAttackNumber passe � 4 si sa vie est en dessous de 50% (Berserker)
	- Peut pas contre-attaquer (Zombie, Kamikaze)
	- Jet avec valeur fixe de 50 (Robot)
	- Augmente attaque de 50% � chaque tour
	- Soigne de la moiti� des d�g�ts qu�il inflige pendant le tour (Vampire)
	- Durant une attaque, pour chaque joueur (et l'attaquant), le joueur � 50% de chance d'�tre attaquer (avec le m�me jet) (Kamikaze)
	- Impossible de contre-attaquer mon attaque, juste defendre (Kamikaze)
	- Cible en priorit� mort-vivant (Pr�tre)

- Vivant : Guerrier, Gardien, Berserker, Robot, Kamikaze, Pr�tre
- Mort-vivant : Zombie, Liche, Goule, Vampire

TODO :
- Formatter les commentaires de carat�ristique sp�ciaux
- Respecter les regles de nommage sur les accesseurs
- Checkwinner en faire un param�tre
- Formatter les percent
- Formatter instaciation d'interface