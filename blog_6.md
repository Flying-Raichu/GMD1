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
Different enemy types were implemented, but all were of the ranged variety due to the time constraints necessary to develop damage based on the strength of physics collisions. Asteroids were implemented as a hazardous obstacle, but its damage was fixed based on collission, not calculated based on velocities. We also made deliberate use of Unity’s Layer Collision Matrix to prevent projectiles from colliding with other projectiles or same-faction entities, which helped eliminate many unintended interactions and optimized performance.

### Bosses
Bosses were not implemented due to time constraints

# Game modes
Game modes were not implemented due to time constraints

#Misc

### User experience
To give players a sense of responsiveness, we added subtle scaling effects to button presses, screen shake on player hits, and distinct sounds for different events (like levelling up or death). These small touches contributed a lot to the feel of the game.

# Conclusion
In conclusion, as a first project in game development, we consider the game we've created a success. We structured our working process and feature focus in such a manner as to deliver a complete and polished experience, even within tight time constraints. Our roadmap was divided into three practical categories—Foundation, Core, and Enhancement—allowing us to prioritize essential features early on and polish them as we progressed. This helped us reduce crunch and avoid rework during the later stages. We only had two dedicated bug-fixing sessions across the entire project, both of which were relatively short and manageable thanks to our modular approach and incremental testing strategy.

One of the biggest, if not the biggest, challenges we faced was version control in a Unity environment. Our combined inexperience with Unity and Git made us cautious about pushing and merging features, especially early on. We often had to wait on each other to finish major changes to avoid merge conflicts, particularly when working on scenes or shared prefabs. This created friction and some bottlenecks in our workflow. However, by the end of the project, we had a stronger grasp on staging changes responsibly, committing frequently, and resolving conflicts when they arose.

Additionally, we encountered challenges tied to Unity's asset serialization model. Despite using the recommended .gitignore, .meta files and serialized .asset files were often left untracked unless added manually. These files are essential in Unity—deleting or omitting them can sever prefab or component references silently. As a result, we were initially hesitant to do deep rebases or aggressively clean our commit history, leading to a higher number of merge commits than we would’ve preferred. That said, it was an incredibly useful learning experience in managing Unity projects collaboratively through source control.

Another source of friction stemmed from how many of our systems—like UI, player spawning, and event handling—were instantiated at runtime using a factory architecture. This meant we couldn't assign many object references through the Unity Editor's Inspector and instead had to rely on runtime scripts and code-based references. It introduced additional bugs early on due to missing assignments, and slowed down debugging since Unity wouldn't catch reference errors until playmode. While this approach made our game cleaner and more scalable in the long run, it made development slightly more opaque and hard to debug as beginners.

Despite the above, we both feel incredibly proud of the final result. We exceeded our initial scope while maintaining stable builds throughout, and we implemented controller support, multiple enemy types, a level-up system, player feedback loops, and even a leaderboard—all from scratch. What we learned in terms of Unity’s architecture, gameplay design, and code modularity is something that we’ll carry with us into every future project.


