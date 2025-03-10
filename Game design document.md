# Game design document
Anna Pomerantz (331501) and Lyubomir Veselinov (331166)

Working title: **Galactic Siege**
## Game Concept
A modern Space Invaders-inspired game with roguelite mechanics. Players face waves of enemies, level up, and choose from randomized power-ups and modules to shape their playstyle. The game includes bosses with unique mechanics, encouraging replayability.

The target audience are retro game enthusiasts, people who enjoy roguelite elements and replayability with some score chasing mixed in.

## Visual Style
Retro-futuristic with neon colors, inspired by Tron. The player's ship and enemies change color and design based on chosen weapons and power-ups. The GUI will be sleek to reflect the space theme of the game.

## End Goal
Achieve the highest score across various game modes. After each session, players can see how their score was achieved, showcasing chosen power-ups and modules.

## Core Mechanics
- Movement: The ship moves with thrust towards the mouse or analog stick, with momentum carried across screen edges.
- Levels & Power-ups: Randomized power-ups and modules are chosen as the player levels up. Power-ups are re-rollable and obtained with currency received throughout the level.
- Health System: The player has health, shields (regen or delayed), and armor (damage absorption). Health is lost if shields deplete.
- Weapons: Various weapons are unlocked, each with unique stats (damage, rate of fire, range, etc.). Some require charging, overheating, or have limited uses.
### Enemies & Bosses
- Enemy Types: Melee, ranged, and engineer enemies with varied behaviors.
- Bosses: Appear at intervals, testing the player's movement, spacing, and endurance. They have unique mechanics and may return as regular enemies in later waves.

## Game Modes
- Classic: Standard mode with waves and bosses.
- Endless: After completing Classic, endless waves increase in difficulty.
- Extreme: High-speed, high-aggression enemies with higher scores.
- Sandbox: A free mode to experiment with power-ups, modules, and ship builds.

## Roadmap for Implementation
- Foundation: Core mechanics (movement, weapons, enemy AI, shield/health system, score system).
- Core: Additional weapons, enemies, levelling system, and power-ups.
- Enhancements: Boss mechanics, armor, and additional game modes.

## Concept art
![image](https://github.com/user-attachments/assets/0908c6e3-4a56-48a1-95cc-39a3031fd022)










