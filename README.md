# VR-Parkinson-Therapy

This application is a research project and offers patients diagnosed with Parkinson's disease the ability to perform exercises together with a trainer in virtual reality. Thereby, the patient uses an HTC Vive HMD and controllers, whereas the trainer is interacting through an HTC Leap unit. 

The project is built as a network application, where both players are connecting to a server and therefore don't necessarily need to be in the same room.
The current exercises include the following:

## Ball Catcher

The Leap player, i.e. the therapist, creates and transforms balls using a pinch gesture. If they feel the size is appropriate to the patients 
current motor skills, they throw the ball towards the patient, whose aim is to catch it in mid-air.

![alt text](https://github.com/lukasmaxim/VR-Parkinson-Therapy/tree/master/screenshots/ball_game_screenshot.png)

When a ball is created and thrown, gravity is currently off to make it easier for the patient to actually catch the ball. Once one of the controllers
intersects and the Vive player presses the trigger, gravity is activated and the ball can be placed in the crate located on the patient's right side.

## Wire Tracer

Here, the Leap player creates a wire that has to be traced by the patient, using a rod with an attached ring. If the ring touches the
wire, its material is being changed to signal that there was an intersection

![title](https://github.com/lukasmaxim/VR-Parkinson-Therapy/tree/master/screenshots/wire_tracer_screenshot.png)

The Leap player's gesture for creating the wire is to extend only the right index finger, whereupon a wire is spawned according to the
current position of the finger. To stop wire creation, no finger on the right hand may be extended. For deletion, the left thumb, index and middle finger
need to be extended for a period of 1 second.

---

## General Movement
### Global Movement

Both players can move globally by triggering the travel gesture together. For the players to move to the next exercise, the Leap player indicates
the exercise he wants to reach by showing the number of the exercise with their right hand (ball catcher is 1, wire tracer is 2). For the players to actually
change position, the Leap player and the Vive player need to move their hands in reciprocal order in front of them, going up and down. Further,
the Vive player needs to press both triggers to confirm the action.

### Local Movement
For better positioning during the exercises, the Leap player may either extend their left or their right index and middle finger to rotate to the left or right respectively.
If both index and middle finger on both hands are extended, the player moves in forward in the direction of the current view.

## Acknowledgement
This is a research project in collaboration with the Interactive Media Systems Group at TU Wien. 
