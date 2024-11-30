# Classbreak

Yet another version for GP3.

## Quick facts

- Genre: Puzzle, walking simulator, psychological horror, adventure
- First person

## Explain the idea with one pargraph

You're a university student rotating between classes, but as soon as you enter the next classroom, the time is reset to the end of the previous class and your schedule changes to another classroom.
You find yourself stuck inside a gloomy teaching hall as in _Groundhog Day_.
While you're moving to another classroom, weird things would happen on the way.
They are different each time, so every time you'd face new challenges.

## Core mechanisms

- Level loop: The game naturally forms a loop of levels.

	Beside that the first level starts right at the beginning of the game, all other levels starts at the moment that the player steps out of the current classroom they're in.

	All levels ends at the moment the player steps into the destination classroom (with potential additional requirements fulfilled).

	Each level would have some changes to the scene.
	The changes would be applied when its level starts, and unloaded/reset when its level ends.

	Each level has a time limit of the duration of a classbreak.
	If the player didn't manage to get to the destination classroom in time, they will "die" and respawn in the previous classroom; the time will also be reset.

- UI

	- Mobile phone: Prompts notification to tell the player the location of the next classroom.

	- Timer: An on-screen clock to indicate the time left till the next class.
		It shall blink in red when the time's about to run out.

	- Pause UI (optional)

	- Interaction indicator: Shows on an interactable object when the player is looking at it.

- Player

	- Basic control: WASD movement + mouse orientation + shift to sprint (optional).

	- Inventory (optional): Some objects might be picked up for later usage.
		Whether this is required depends on specific level designs.

## What makes it interesting

- Emotional connection and unique premise

	The relatable setting of being a university student could create an emotional investment in the character's plight, but then the time loop mechanic offers an interesting twist on familiar themes.

- Exploration and discovery

	Players might get stuck on expected way to the next classroom.
	They need to explore unvisited areas like unrelated classrooms or the restrooms to find their way to the destination, and discovering clues and solving puzzles is a delightful experience.
	This gives players movitation to keep going on upcoming cycles.

- Potential for humor

	The absurdity of the situation could lend itself well to comedic elements.

## MDA analysis

\[To be done\]

## Potential production-side difficulties

- Content creation
	- Designing unique rooms and challenges for each day.
	- Balancing the difficulty between each level (because the duration of the class break should be the same for each level).

- Gameplay mechanisms
	- Designing the level contents.
	- Implementing the level-switching logic.
	- Potential requirement of an inventory system.

- Aesthetics
	- Preparing assets for and building the interior environment of the teaching hall.
		- Modelling the interior space (the hallway & the rooms).
		- Finding PBR textures.
		- Modelling furnitures.
		- Adding detailed objects.
	- Creating an convincing acoustic environment.
		- The background noise to render the gloominess and upsetting ambient.
		- Walking sound effects.
		- Door opening/closing effects, and also the door knobs.

## Visual settings

The game takes place on a floor of an old teaching hall.
The hall is portrayed to be dim, narrow and unpleasant;
you could imagine a mixture of _The Backrooms_ and Nightingale Hall’s 5th floor or Holmes Hall’s ground floor.
All exits to the outer world are somehow blocked:
The elevators are out-of-use, there is no entry to staircases, etc.

## Example floor map

![](./example%20floor%20map.png)

## Puzzle design ideas

1. (Tutorial) The first cycle shall be plain—just let the player go freely from one classroom to another, to show them the basic goal of the game.

1. (Advanced tutorial) Still the same, but the destination is harder to find.
	The player may have to rely on the floor map shown on the board in front of the reception desk to find the classroom.

1. (Fence gate) A fence gate appeared in the middle of the hallway.
	The player has to either press a button to open the gate or take another route.

1. (Pure humor) The destination classroom number doesn't exist on the floor map.
	The player has to visit every room in turn just to find out that it is the restrooms.

1. (Hidden passageway) A persistent road block is blocking the destination classroom's door.
	The player could find an unobvious passageway in the adjacent classroom to enter.

1. (Callback to the floor map) The numbers on the doors are missing, also the room number shown in the mobile notification is weird.
	The player has to refer to the floor map to find the correct room.

1. (Black out) The light's out. It's impossible to read the numbers on the doors nor the floor map. The player has to reply on their memory.

## Text walkthrough

1. The player spawns (wakes up?) in an empty classroom. The class-ending bell is ringing.

1. A cell phone notification pops up in the bottom-right corner of the screen, showing the time & the location of the next class, which is right after 10 minutes.

1. Also a clock would be shown in the bottom-left corner of the screen, with a mark showing the remaining time to the next class.
The player could recognize two things:

	- They are a student in a classroom where a previous class has just ended.
	- The next class is starting in a short time and they need to rush to the classroom.

1. Being aware of this, the player gets out of the room to find the classroom. While they’re moving, the clock and the cell phone UI stay in place for them to keep track of the remaining time and the location. Luckily, according to the schedule, it’s right on the same floor of the same building.

1. By reading the floor map and the door numbers, the player managed to find the classroom quickly. As they enter the room, the clock and the cell phone UI disappears.

1. A few seconds later, the class-ending bell rings again. The player must find it weird: How come the bell rings again right after the class just started?

1. They might wait in the classroom for a while, but no one is showing up. So they eventually decide to leave the room as there is nothing better to do.

1. But at the moment they step out of the door, the cell phone notification shows up again: The same class, at the same time, but in a different classroom! The clock also appears, but the time is reset to 10 minutes ago—a groundhog day!

1. So the player tries to go to the next classroom, except that this time, something in the hallway has changed: Some doors are closed, some lights are broken, a janitor cart appears on the way… Something’s wrong and it’s spooky.

1. They reach the classroom, but the same thing happens again, except that more weird things are happening in the hallway, blocking and delaying the player’s pace.

	- If the player failed to arrive on time, the clock would ring an ominous alarm, a black fog would rush towards the camera, and the player would be reset to the end of the previous class, just at the moment the class-ending bell starts to ring.

1. The cycle continues. The weird things are different each time, making it harder and harder to reach the classroom on time. The player may have to give multiple tries over some cycles.

1. Eventually, there’s a time when they leave the classroom again, there are no more future classes. All classes have ended and the player can finally go home.
