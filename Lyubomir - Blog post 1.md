# Roll-a-Ball: A Beginner’s Journey

The **Roll-a-Ball** tutorial served as my introduction to Unity, guiding me through the process of building a simple game. This post will go over the experience, the challenges I encountered, and the extra features I implemented to expand upon the base tutorial.

## Extra Features
While following the tutorial, I decided to introduce a few additional mechanics to make the game more engaging:
- **Multiple types of pickups** instead of a single collectible type.
- **Jumping mechanic** for the ball.
- **Movable obstacles (Companion Cubes)** that must be pushed into specific areas to satisfy the win condition.

---

## The Development Process

### Moving the Player
Scripting the player's movement was where the first major difficulties arose. The tutorial’s text instructions were somewhat messy, occasionally referencing variables that had not been declared beforehand. This led to some initial confusion, but I was able to work through it.

At this stage, I decided to add a jumping mechanic to the ball. My first approach involved comparing the object’s distance from the ground. However, I quickly realized that this wouldn’t work properly since, in Unity’s world space, the ball’s default Y-position is **0.5**, meaning it technically never touches the ground. To solve this, I used a **raycast** to detect whether the ground was within a certain distance, allowing for more accurate jump detection.

### Moving the Camera
Implementing the camera movement was fairly straightforward. However, I experimented with making the camera **not jump with the ball**, hoping for a smoother effect. Unfortunately, the result was visually unappealing, so I decided to leave the camera behavior as it was.

### Creating Walls
Since the ball can now jump, I had to modify the walls to prevent the player from easily escaping the play area. Instead of making the walls visually larger, I extended their collision boundaries by adding **invisible extensions**. This was done by creating additional wall objects and simply disabling their **Mesh Renderer**, ensuring they functioned as barriers without being seen.

### Creating Collectibles
When implementing collectibles, I took some time to explore the provided function’s variables to understand what each one did. Instead of following the tutorial exactly, I **restructured the code to make it easier to create different types of collectibles**. This allowed for greater flexibility in adding varied gameplay mechanics later on.

### Collision Detection
By default, the tutorial's method for handling collectibles involved **disabling** them rather than removing them from the scene. Since I wasn’t planning to reuse collectibles, I swapped out the **SetActive(false)** logic for **Destroy()**, ensuring that objects were permanently removed upon collection.

### Win Screen
Rather than hardcoding values for the win condition, I created a **pickup counter** that initializes when all `PickUp` objects are created. However, I ran into an issue where the counter **decremented too late**—since the win condition check ran **before** the objects were destroyed, it wasn’t detecting when the last collectible was picked up. Initially, I implemented a **quick and dirty solution** that decremented the counter separately for each type of pickup. A cleaner approach would be to use a **GameManager class** to handle object tracking in a more structured way.

### Enemy AI
Setting up a basic **enemy AI** was mostly straightforward, though I ran into an unexpected problem early on. I mistakenly **scaled the empty GameObject containing the enemy** instead of scaling its visible body, which resulted in some strange behavior—the enemy kept spinning in circles around the player instead of properly chasing them. Once I realized the issue, fixing it was simple.

After getting the AI working, I added **more obstacles, enemies, and an additional type of score mechanic**. I also introduced small obstacle cubes that could be pushed around. A future improvement could be making these cubes essential to progression—perhaps requiring the player to **gather all of them in one spot** to trigger an event.

---

## Conclusion
For a first project, **Roll-a-Ball** was a solid introduction to Unity. The challenges I encountered were relatively minor, and most issues took no more than **15 minutes to troubleshoot**. However, I found that the **general coding practices in the tutorial were not ideal**—it relied on some **bad habits**, and I believe a beginner-focused tutorial should emphasize **clean, maintainable code** from the start. Despite that, it was a fun learning experience, and the extra features I added made the project feel much more engaging.

Moving forward, I’ll continue refining my Unity skills while incorporating **better coding practices** to ensure my projects remain scalable and well-structured.
