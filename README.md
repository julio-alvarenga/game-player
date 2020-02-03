# game movement and animation

**To see these scripts in action**, check out my game "Light Polymer" at https://alvarengajulio.itch.io/lightpolymer-demo

These 3 classes work together to make an in-game character feel alive. The Locomotion class moves the player, while the Action class animates them. Combine them and you can make a humanoid model move around the world.

For example, the movement animations like walking and running change based on player velocity. The Character class grabs the velocity from the Locomotion class and passes it to the Action class so the animations reflect the movement.

