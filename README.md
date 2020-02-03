# game-player

To see these scripts in action, check out my game "Light Polymer" at https://alvarengajulio.itch.io/lightpolymer-demo

The Action class contains the Animator and the Locomotion class contains the Character Controller.

The Animator controls animations (like walking or jumping) while the Character Controller changes the player's location in the world.

The Character class acts as a hub between the two classes, passing values from the Locomotion class to the Action class. For example, the movement animations, like walking and running, change based on player velocity. The Character class grabs the velocity from the Locomotion class and passes it to the Action class so the animations reflect the movement.

