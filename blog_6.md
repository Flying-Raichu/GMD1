This blog post will outline the final POC of Galactic Siege and all the established features when compared to our game design document.
# Visual style
Thanks to Anna's efforts, the game's visual foundation was established. The player, enemies, projectiles and background follow a retro-futuristic design:

### The player
![image](https://github.com/user-attachments/assets/ff2f16af-4506-4c6c-b311-142b5c4c02b7)

### The different enemies
![image](https://github.com/user-attachments/assets/529d2170-4f84-4bf6-b32a-9decbc3e9abe)

### Projectiles
![image](https://github.com/user-attachments/assets/261c139b-776a-4aa9-a6ad-eec3ec3d04d1)

# Core mechanics
### Movement
Movement was fully established based on the concept outlined in the game design document, the player character zooms around the screen using physics-based movement.

### Levels and power-ups
Levels were established, the full XP and levelling system implemented with an algorithm for XP earning upscaling. Sadly, we did not have time to create power-ups.

### Health System
The player's health system was established, minus the armor mechanic. Both the shields and health work as expected, the shield regenerating when no damage is taken for X time, the health passively ticking up.

# Enemies & Bosses
### Enemy types
Different enemy types were implemented, but all were of the ranged variety due to the time constraints necessary to develop damage based on the strength of physics collisions. Asteroids were implemented as a hazardous obstacle, but its damage was fixed based on collission, not calculated based on velocities.

### Bosses
Bosses were not implemented due to time constraints

# Game modes
Game modes were not implemented due to time constraints

# Conclusion
In conclusion, as a first project in game development, we consider the game we've created as a success. We structured our working process and feature focus in such a manner so as to be able to deliver a full, polished experience of the game regardless of time constraints, the roadmap being based on three categories (Foundation, Core and Enhancement) helping us keep track of when to develop what and hwo to ensure we have a polished deliverable in regards to implemented features. We only had 2 bug fixing sessions in total and both were decently short and manageble.

We also want to mention what a cumulative challenge version control was for this project. Our overall inexeperience with Unity when combined with Github did result in us being a bit hesitant in terms of pushing and having to merge features, both of us having to wait for the other to finish features before we could start our own, especially near the beginning.


