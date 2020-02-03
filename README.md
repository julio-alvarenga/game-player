# game-player

Character Class for Unity 3D

The Character class can be used by any humanoid-rigged model, as long as an Animator and Character Controller component are attached.

The Action class contains the Animator and the Locomotion class contains the Character Controller.

The Animator controls animations (like walking or jumping) while the Character Controller changes the player's location in the world.

The Character class acts as a hub between the two, passing values from the Locomotion class to the Action class. For example, the grounded movement animations like walking and running change based on player velocity. The Character class grabs the velocity from Locomotion and passes it to Action so the animations will reflect the way the player is moving.

To see these scripts in action, check out my game "Light Polymer" at https://alvarengajulio.itch.io/lightpolymer-demo

