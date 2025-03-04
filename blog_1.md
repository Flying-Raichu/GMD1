# Blog for Roll-a-Ball

This was my first time both working with the Unity editor and also game development, so it was a bit difficult to wrap my head around everything. It's not pretty, but at the very least, I wanted to make it functional and have some kind of gameplay loop.

I added a moving platform at the top of a ramp. To make it move in a back and forth pattern, I created empty game objects for the start and stop locations of the platform. In the moving platform's script, I had two Transform fields that held the positions of those game objects, along with a vector to store the direction for the object to move in. In the Start() method, I set it so that the target vector's value was the stop Transform's position. In the Update method, the platform is moved at a fixed speed until it gets close to the target, in which case it sets the next destination to be the start position again, so it moves back in the opposite direction.

On top of this, I also added a few new floating platforms, along with the ability for the player to make the ball jump with the spacebar. This used the OnJump() method. I added a check so that it only allows the jump to actually register if the input is made while the player is touching a solid surface, and that was done by adding a ground tag to all objects that are classified as the ground. I also added another coin at the end of the floating platforms and updated the win condition to incentivize the player to move on those platforms.

I also made a main menu screen before the gameplay actually starts. This was done using a new scene which is placed before the main scene in the build order. It just has the title of the game and a button to play, which just loads the main scene.

Finally, I added a replay button when the player wins/loses, which reloads the scene to a fresh state.