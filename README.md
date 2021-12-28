- La douleur
	- Personnage sensible -> Interface
	  (Peut perd capacité d'attaquer pendant le tour + 1 ou 2 suivants)
		- Gardien, Robot, Kamikaze, Prêtre, Goule
		- Guerrier = perd capacité d'attaque seulement pendant le tour actuel
	- Personnage non sensible -> Sans implémentation d'interface
		- Berserker
		- Zombie, Liche, Vampire

- Dégâts sacrés ou impies, personnages maudits ou bénis -> 2 Enumérations et une classe statique faisant le lien
	- Impies attaque bénis = dégats*2
	- Sacré attaque maudit = dégats*2
	- Par personnage : 
		- Gardien = inflige sacrée
		- Liche = inflige impie
		- Prêtre = inflige sacrée et est béni
		- Morts-vivants (Zombie, Liche, Goule, Vampire) =  sont Maudit

- Charognard -> interface
	- Quand un autre perso meurt le charognard gagne entre 50 et 100 pdv
	- Liste personnage : Zombie, Goule

Autres : 
	- Bonus de contre-attaque doublé (Gardien)
	- Ajout tous les points de vie perdu aux dégâts pour l'attaque (Berserker)
	- TotalAttackNumber passe à 4 si sa vie est en dessous de 50% (Berserker)
	- Peut pas contre-attaquer (Zombie, Kamikaze)
	- Jet avec valeur fixe de 50 (Robot)
	- Augmente attaque de 50% à chaque tour
	- Soigne de la moitié des dégâts qu’il inflige pendant le tour (Vampire)
	- Durant une attaque, pour chaque joueur (et l'attaquant), le joueur à 50% de chance d'être attaquer (avec le même jet) (Kamikaze)
	- Impossible de contre-attaquer mon attaque, juste defendre (Kamikaze)
	- Cible en priorité mort-vivant (Prêtre)

- Vivant : Guerrier, Gardien, Berserker, Robot, Kamikaze, Prêtre
- Mort-vivant : Zombie, Liche, Goule, Vampire

TODO :
- Formatter les commentaires de caratéristique spéciaux
- Respecter les regles de nommage sur les accesseurs
- Checkwinner en faire un paramètre
- Formatter les percent
- Formatter instaciation d'interface