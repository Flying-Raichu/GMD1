# Blog 5

More stuff:
- Enemies now have points associated with them. On death, these points are added to the player's score.
- Menu buttons have some little animations and a sound effect when selecting. When clicked, it shrinks for a little while and expands, and the color changes slightly. The call to do whatever functionality was needed had to be placed inside the coroutine in order for the animation and sound to not be cut off in the middle (example: AnimateButton in StartStop.cs)
- Enemies now explode upon death with a sound and an animation where a white circle sprite expands and fades using linear interpolation (found in ExplosionEffect.cs).
- Two new weapon types were added and the weapon classes were refactored with inheritance in mind. One is a scatter shot that shoots three mini pellets in a cone shape (ScatterWeapon.cs), and the other is a bomb that blinks white for a countdown and then explodes, checking collision within a certain radius and damaging all enemies in the radius (BombWeapon + BombProjectile.cs). The explosion animation reuses the one made for enemies exploding.
- Sounds from player shots and enemy damage were tweaked in Weapon.cs so that the pitch would be randomized within a certain range so that it wasn't repetitive.
- Bugs were addressed. After the factory pattern refactoring and what we think was a rebase that overwrote some changes, a few things broke:
    - The pause menu no longer worked, since it lost its reference to the player.
    - Enemies lost their reference for the player's position, so only aimed at the player's starting location and did not rotate
    - Anything colliding with an asteroid would disappear (should only damage the player when hit) and the asteroid broke instantly instead of being damaged over time
    - Player bullets collided with enemy bullets and appeared to drift around freely. This just meant changing layer interactions.
    - The player's score kept increasing after a game over. We had to add an event listener to stop the coroutine on death (shown in ScoreManager.cs)
    - Since most of the development was done with a KB+M (but still had the mapping for controller), we did a controller test and adjusted the event system to work with our GUI buttons.
- Extra enemy types.
    - Two new enemy types were created, a grenade enemy that slowly fires projectiles that do hefty damage, as well as a scatter shot enemy that fires projectiles in a shotgun-like pattern
