# Magix

<p align="center">
    <img width="300" height="150" src="/Documents/Images/logo.png">
</p>

Magix is a 1v1 turn based game in which a player controls 2 wizards and  have to eliminate.

This is a fun little project of mine to write a turn based game from scratch without googling much
solutions. The goal of it was to solve problems (and also create a few) so I can be able to design better solutions to
complex requirements.

The source code is far from perfect but I am happy with the result.

## Game description

Each player controls 2 wizards and must execute actions with both of them. Wizards can melee, move, push a wizard and cast nature elements spells.
Spells combine with existing elements in a tile, creating new effects. For instance, thunder spell will electrocute a pool of water andcause damage and decrease movement of any wizard inside.  Wins who defeats both oponent`s wizards.

## How to run?

Select `Bootstrapper` scene and run Unity.

## Reading the code

> [!TIP]
> It is recommended start reading from [`BoardController`](Magix/Assets/Scripts/Magix.Controller/Match/Board/BoardController.cs) because it controls the entire board and it is possible to have a slice of the architecture.