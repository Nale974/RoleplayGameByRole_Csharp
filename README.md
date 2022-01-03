# LEBON_Nathan_DM_IPI_2021_2022 - Devoir maison C#Init

Matière : INET400 - C#Init par Adrien Gerbex

Auteur : Nathan LEBON

## Description
Cet exercice est un jeu textuel dans lequel plusieurs personnages se combattent en utilisant un système de règles inspiré des jeux de rôle.

## Récapitulations , interprétations et implémentation des consignes

- La douleur
	- Personnage sensible -> Interface
	  (peut perd capacité d'attaquer pendant le tour actuel et 1 ou 2 tours suivants)
		- Gardien, Robot, Kamikaze, Prêtre, Goule
		- Guerrier : perd capacité d'attaque seulement pendant le tour actuel
	- Personnage non sensible -> Sans implémentation d'interface
		- Berserker
		- Zombie, Liche, Vampire

- Dégâts sacrés ou impies, personnages maudits ou bénis 
-> 2 Enumérations et une classe statique faisant le lien pour permettre l'implémentation de futur type
	- Impies attaque bénis = dégats*2
	- Sacré attaque maudit = dégats*2
	- Par personnage : 
		- Gardien : inflige sacrée
		- Liche : inflige impie
		- Prêtre : inflige sacrée et est béni
		- Morts-vivants (Zombie, Liche, Goule, Vampire) : sont Maudit

- Charognard -> interface
	- Quand un personnage meurt le charognard gagne entre 50 et 100 pdv
	- Liste personnage : Zombie, Goule

- Autres : 
	- Bonus de contre-attaque doublé (Gardien) -> interface
	- Ajout tous les points de vie perdus aux dégâts pour l'attaque (Berserker) -> interface
	- TotalAttackNumber passe à 4 si sa vie est en dessous de 50% (Berserker) -> interface
	- Jet de défense toujours égal à 0 (Zombie) -> interface
	- Peut ne pas contre-attaquer (Zombie, Kamikaze) -> champ dans la classe 'character'
	- Jet avec valeur fixe de 50 (Robot)-> interface
	- Augmente attaque de 50% à chaque tour (Robot) -> interface
	- Soigne de la moitié des dégâts qu’il inflige pendant le tour (Vampire) -> interface
	- Durant une attaque, pour chaque joueur (et l'attaquant), le joueur à 50% de chance d'être attaqué (avec le même jet) (Kamikaze) -> interface
	- Impossible de contre-attaquer mon attaque, juste défendre (Kamikaze) -> champ dans la classe 'character'
	- Cible en priorité les morts-vivants (Prêtre) -> interface
	- Soin de 10% de MaximumLife au début de chaque tour (Prêtre)-> interface

- Catégorie de personnage -> champ dans la classe 'character'
	- Vivant : Guerrier, Gardien, Berserker, Robot, Kamikaze, Prêtre
	- Mort-vivant : Zombie, Liche, Goule, Vampire

## License
Nathan LEBON - 2021/2022

## TODO :
- Formatter nommage percent
- Formatter nommage d'interface