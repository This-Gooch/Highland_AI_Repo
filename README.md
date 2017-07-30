# Highland_AI_Repo
Character Driven Card Game (Unity Project)



# Programming

## Classes

### Unit

The main class of characters. Contains the functionalities. 
Also has a subclass UnitInfo.

#### UnitInfo
Has all the data of the Unit. I.E. attack, defense, health, position in the field, etc...

#### Ability
Contains the functionality of the Unit's abilities. The actual effectors are on a subclass Effect.

##### Effect 
Hold the actual effects an ability/card has.
   `Public Members:  EEffect type;
					int value;
					int originalDuration;
					int duration;`
					
	The types of effects are defines as an enum in NSPGameplay namespace.

## Naming

P1 -> Refers to the player that plays first.
P2 -> Refres to the player that plays last.