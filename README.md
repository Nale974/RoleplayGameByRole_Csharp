# LEBON_Nathan_DM_IPI_2021_2022 - Devoir maison C#Init

Mati�re : INET400 - C#Init par Adrien Gerbex

Auteur : Nathan LEBON

## Description
Cet exercice est un jeu textuel dans lequel plusieurs personnages se combattent en utilisant un syst�me de r�gles inspir� des jeux de r�le.

## R�capitulations , interpr�tations et impl�mentation des consignes

- La douleur
	- Personnage sensible -> Interface
	  (peut perd capacit� d'attaquer pendant le tour actuel et 1 ou 2 tours suivants)
		- Gardien, Robot, Kamikaze, Pr�tre, Goule
		- Guerrier : perd capacit� d'attaque seulement pendant le tour actuel
	- Personnage non sensible -> Sans impl�mentation d'interface
		- Berserker
		- Zombie, Liche, Vampire

- D�g�ts sacr�s ou impies, personnages maudits ou b�nis 
-> 2 Enum�rations et une classe statique faisant le lien pour permettre l'impl�mentation de futur type
	- Impies attaque b�nis = d�gats*2
	- Sacr� attaque maudit = d�gats*2
	- Par personnage : 
		- Gardien : inflige sacr�e
		- Liche : inflige impie
		- Pr�tre : inflige sacr�e et est b�ni
		- Morts-vivants (Zombie, Liche, Goule, Vampire) : sont Maudit

- Charognard -> interface
	- Quand un personnage meurt le charognard gagne entre 50 et 100 pdv
	- Liste personnage : Zombie, Goule

- Autres : 
	- Bonus de contre-attaque doubl� (Gardien) -> interface
	- Ajout tous les points de vie perdus aux d�g�ts pour l'attaque (Berserker) -> interface
	- TotalAttackNumber passe � 4 si sa vie est en dessous de 50% (Berserker) -> interface
	- Jet de d�fense toujours �gal � 0 (Zombie) -> interface
	- Peut ne pas contre-attaquer (Zombie, Kamikaze) -> champ dans la classe 'character'
	- Jet avec valeur fixe de 50 (Robot)-> interface
	- Augmente attaque de 50% � chaque tour (Robot) -> interface
	- Soigne de la moiti� des d�g�ts qu�il inflige pendant le tour (Vampire) -> interface
	- Durant une attaque, pour chaque joueur (et l'attaquant), le joueur � 50% de chance d'�tre attaqu� (avec le m�me jet) (Kamikaze) -> interface
	- Impossible de contre-attaquer mon attaque, juste d�fendre (Kamikaze) -> champ dans la classe 'character'
	- Cible en priorit� les morts-vivants (Pr�tre) -> interface
	- Soin de 10% de MaximumLife au d�but de chaque tour (Pr�tre)-> interface

- Cat�gorie de personnage -> champ dans la classe 'character'
	- Vivant : Guerrier, Gardien, Berserker, Robot, Kamikaze, Pr�tre
	- Mort-vivant : Zombie, Liche, Goule, Vampire

## License
Nathan LEBON - 2021/2022

## TODO :
- Formatter nommage percent
- Formatter nommage d'interface