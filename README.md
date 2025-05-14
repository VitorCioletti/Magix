# Magix

<p align="center">
    <img width="250" height="250" src="/Documents/Images/logo.png">
</p>

Magix is a strategic 1v1 turn-based game where two wizards duel using elemental spells and environmental interactions. Think chess, but with magic, combos, and unpredictable reactions.

This project was a challenge to myself: build a turn-based game from scratch, solving problems creatively without relying on ready-made solutions. The result? A messy but rewarding codebase that taught me a ton about game architecture and emergent gameplay.

The source code is far from perfect but I am happy with the result.

## Game description

Magix is a 1v1 turn-based duel where each player controls two wizards. On your turn, you can:

- Move across the battlefield.

- Melee attack up close.

- Push enemies to reposition them.

- Cast elemental spells (fire, water, lightning, etc.).

Spells interact with the environment—for example:
⚡ Casting lightning on a water tile electrocutes anyone standing there, dealing damage and slowing them.

Win condition: Defeat both of your opponent’s wizards!

## How to run?

Select `Bootstrapper` scene and run Unity.

## Reading the code

> [!TIP]
> It is recommended start reading from [`BoardController`](Magix/Assets/Scripts/Magix.Controller/Match/Board/BoardController.cs) because it controls the entire board and it is possible to have a slice of the architecture.