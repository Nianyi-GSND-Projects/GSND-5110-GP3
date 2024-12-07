/* Definitions */

#let flex = (..data, maxcolumn: 3, _align: center, gutter: 1em) => {
	set align(_align);
	grid(
		columns: calc.min(maxcolumn, data.pos().len()),
		align: center,
		column-gutter: gutter,
		..data,
	);
}

/* Preambles */

#set page(paper: "us-letter",
	margin: (x: 1in, y: 1in),
)
#set par(justify: true, linebreaks: "optimized")
#show text.where(lang: "zh"): set text(font: "KaiTi")
#set heading(numbering: "1.1.1")
#show heading.where(level: 1): it => {
	set align(center);
	set text(size: 16pt);
	v(0.5em);
	it;
	v(0.5em);
}
#show heading.where(level: 3): set heading(outlined: false)

/* Content */

#let gameName = [Classroom 404]

#let title = (title: "", subtitle: "") => {
	set align(center);
	show par: set block(below: 0.8em);
	par(text(title,
		size: 1.8em,
		weight: "bold",
	));
	par(text(subtitle,
		style: "italic",
		size: 1.3em,
		font: "Book Antiqua",
	));
}
#title(
	title: [Concept Document of _#(gameName)_],
	subtitle: [The Duo]
)

#let member(name: "", localname: "", mail: "") = {
	show link: set text(font: "Consolas");
	set align(center);

	text()[#name (#localname)];
	linebreak();
	link("mailto:" + mail)[<#mail>];
}
#flex(
	member(
		name: "Nian'yi Wang",
		localname: text(lang: "zh")[王念一],
		mail: "wang.nian@northeastern.edu"
	),
	member(
		name: "Sadaf Nezameddini",
		localname: text(lang: "fa")[صدف نظام‌الدینی],
		mail: "nezameddini.s@northeastern.edu"
	),
)

#let mlink = (url, txt) => {
	show link: set text(fill: blue);
	link(url)[#underline(txt)];
}

#flex(
	maxcolumn: 5,
	mlink("https://trello.com/invite/b/672edd5f7abed9fb9f9fa34c/ATTI5d6206837aa4d4bc65d880d861dc1842BA71A0AA/gsnd-5110-gp3-the-duo")[Trello Board],
	mlink("https://github.com/Nianyi-GSND-Projects/GSND-5110-GP3")[GitHub Repository],
	mlink("https://github.com/Nianyi-GSND-Projects/GSND-5110-GP3/blob/master/Team%20Log/Team%20Log.md")[Team Log],
	mlink("https://github.com/Nianyi-GSND-Projects/GSND-5110-GP3/releases")[Releases],
)

#outline(indent: 1em)

#pagebreak()
= Quick Facts

#((..data) => {
	show table.cell.where(x: 0): set text(weight: "bold");
	set align(center);
	table(..data,
		columns: (1in, 1fr),
		align: left,
		stroke: none,
	);
})(
	[Genre], [Puzzle, First-person, Walking simulator],
	[Platform], [PC],
	[Audience], [
		- Students---The player will play the role of a student who needs to get to class on time. Any student should be able to relate to this experience.

		- General audience---The game is all-population friendly. Although having a bit spooky vibe, there is no actual horror elements at all. Everybody could enjoy the gameplay.
	],
)

= Overview

Reach classroom 404 before the next class starts---but every time you arrive, the room's location changes, and the clock rewinds. You're stuck in a time loop! In each loop, new obstacles and eerie surprises block your way, try to solve them with your intelligence! Can you ever reach the reality of the true classroom 404 and break the time loop?

_#(gameName)_ is a puzzle game that captures the familiar intense experience of running between classes: rushing to classes.
This relatable setting could create an emotional connection with our target audiences---students, but then the time loop mechanic offers an interesting twist on familiar themes.
Players would also constantly get stuck on expected way to the next classroom, so they need to explore unvisited areas to find their way to the destination, and discovering clues and solving puzzles is a delightful experience.
Under this setting, we could even stuff some absurdity into the puzzle designs, bringing unexpected comedic effects.

= Player Experience

== Scene

=== Visual setting/background

The game takes place on a floor of an old teaching hall.
The hall is portrayed to be dim, narrow and unpleasant;
you could imagine a mixture of _The Backroom_ and Nightingale Hall's 5th floor or Holmes Hall’s ground floor (@fig:scene-interior).
All exits to the outer world are somehow blocked:
The elevators are out-of-use, there is no entry to staircases, etc (@fig:scene-overview).

#figure(
	caption: [A snapshot from the scene's interior space.],
	image("./assets/scene-interior.png",
		height: 12em,
	),
) <fig:scene-interior>

#figure(
	caption: [
		An overview of the scene.
		The exit door on the left-most corner is hidden throughout the game;
		it only shows up in the last level.
	],
	image("./assets/scene-overview.png",
		height: 16em,
	),
) <fig:scene-overview>

=== Classrooms

The teaching hall consists of many classrooms, which make up the main element of this game.
The classrooms are the departure and destination of the puzzle levels.
The player should be able to tell the room numbers of the classrooms easily (unless made difficult intentionally by level design).
Therefore, there is a door plate beside every door (@fig:door-plates).

#figure(
	caption: [Each classroom door has its own door plate showing the room number.],
	image("./assets/door-plates.png",
		height: 14em,
	),
) <fig:door-plates>

To enter/exit a classroom, the player could interact with the door knob to open/close the door.

=== Floor map

Another important element is the floor map.
It is a guidance that could tell the player where is where without them having to navigate the entire scene (@fig:floor-map-clean).
Many of our level designs are taking advantage of the floor map.

#figure(
	caption: [The clean floor map texture used in the project.],
	image("./assets/Floor Map (clean).png",
		height: 12em,
	),
) <fig:floor-map-clean>

When used in game, it is labelled with room numbers and highlighted with a spotlight on the wall (@fig:floor-map-in-game).

#figure(
	caption: [The in-game floor map.],
	image("./assets/floor-map-in-scene.png",
		height: 14em,
	),
) <fig:floor-map-in-game>

== UI

=== HUD

There are two HUD elements: the mobile notification and the timer (@fig:hud).

#figure(
	caption: [The in-game HUD.],
	image("./assets/hud.png",
		height: 16em,
	),
) <fig:hud>

In each level, the mobile notification pops up first to indicate the start of a new level, then the timer appears and starts counting down.
The mobile notification soon hides as it's not providing important information in later gameplay.

=== Interaction

The game uses UI indicators to teach players how to control the protagonist.

At the beginning, we teach the basic character control by showing icons representing the input actions (@fig:starting-input-guidance).
These guidance icons will disappear soon as the player leaves the spawning classroom.

#figure(
	caption: [Input guidance at start.],
	image("./assets/starting-input-guidance.png",
		height: 16em,
	)
) <fig:starting-input-guidance>

Some scene objects are able to interact with.
To do that, the player needs to look at the object and press LMB.
When they're looking at an interactable object, a mouse icon would show on it (@fig:interaction-indicator).

#figure(
	caption: [Interaction indicator when focusing on interactable objects.],
	image("./assets/interaction-indicator.png",
		height: 12em,
	)
) <fig:interaction-indicator>

== Acoustic experience
<sec:acoustic-experience>

=== Ambient sound

To render the gloomy, unpleasant vibe of the teaching hall, we applied a subtle humming noise as a global ambient sound.

=== Footsteps

While walking, the sound of footsteps could be heard periodically.

=== Door knobs
<par:experience-door-knobs>

When interacting with door knobs, a light opening sound is played at the position of the knob.

=== Bell

When a level start, a bell indicating the dismissal of the last class would be played.

=== Timer

The timer has a periodical tick-tock sound effect, which is nearly inaudible at the beginning, but will gradually increase its volume as the time approaches the scheduled class-starting time.
We hope that this could achieve a hurrying and gradually panicking experience.

=== Level failing
<par:experience-level-failing>

When the player failed to arrive at classroom 404 on time, the level is failed.
An ominous sound would be played to indicate the failure.

#pagebreak()
= Gameplay

== Walkthrough

@fig:cycle shows the flow of the entire gameplay.
Please refer to it whenever you're uncertain in the upcoming description.

#figure(
	image("./assets/Classroom 404 Game Cycle.svg",
		height: 40em,
	),
	caption: [A flow chart showing the flow of the entire gameplay.],
) <fig:cycle>

#[
=== Game starts

- The player wakes up in an empty classroom.
	The class-dismissing bell is ringing.

- A mobile notification pops up in the bottom-right corner of the screen, showing the time & the location of the next class, which is in classroom 404 right after 1 minute.
	Also a clock would be shown in the bottom-left corner of the screen, with a mark showing the remaining time to the next class.
	The player could recognize two things:

	- They are playing as a student, just woke up in a classroom where the previous class just ended.
	- The next class is starting in a short time and they need to rush to the classroom.

=== In-level

- Being aware of this, the player goes out to find the classroom.
	The mobile notification would hide automatically, but the clock stays to remind the player of the remaining time.

- By reading the floor map and the door numbers, the player managed to find the classroom quickly.
	As they enter the room, the UI disappears, marking the success of the level.

- A few seconds later, the lights in the classroom go off, the class-dismissing bell rings again.
	The player must find it weird: How come the bell rings again right after the class just started?

- The mobile nofitication pops up again: The same room 404, the same one-minute-before-class-starts time.
	The player goes out only to find that the room they are in is not 404 anymore---they need to again find classroom 404.

- So the player tries to go to the next classroom, except that this time, something in the hallway has changed: Some doors are closed, some lights are broken, a janitor cart appears on the way...
	Something’s wrong, and it’s spooky.

=== End of level

- They reach the classroom, but the same thing happens again, except that more weird things are happening in the hallway, blocking and delaying the player’s pace.

	- If the player failed to arrive on time, the clock would ring an ominous alarm, a black fog would rush towards the camera, and the player would be reset to the last starting classroom, just at the moment the bell starts ringing.

- The cycle continues.
	The weird things are different each time, making it harder and harder to reach the classroom on time.
	The player may have to give multiple tries over some cycles.

=== Game ends

- Eventually, there’s a true ending level.
	There are no more future classes, and when the player opens the door, they could see blue sky and go home (@fig:escape).

	#figure(
		image("./assets/escape.png",
			height: 16em,
		),
		caption: [The final escape.],
	) <fig:escape>
]

== Puzzle-solving

Being a puzzle game, the puzzle-solving happens in the main part of each cycle.
In each cycle, the player needs to navigate to classroom 404 at a new, unknown location.
On their way finding the room, the player will encounter unexpected obstacles, different in each level.

== Attributes

=== Exploration

The main goal of this game is to find classroom 404, so the player needs to explore the map actively.

=== Indefinite time loop

The player is passively advancing to upcoming levels, without knowing how many levels there will be ahead.

=== Identifying mechanics

Each level has its own special mechanics.
In order to pass the levels, the player must try to identify their mechanics and overcome the obstacles.

#{
	show ref: it => link(it.target, [#it.element.body]);
	[To read more in detail of the level designs, see @sec:level-design.];
}
== Progression

When a level is passed, the next level automatically starts in a short time.
When the last level is passed, the game will roll the ending animation and go to the ending screen.

= Controls

This game is targeted for PC players.

The actions in this game are very simple.
There are only 3 (1 if you don't count the basic character controls), as shown in @table:action-map.

#{
	show table.cell.where(y: 0): set text(weight: "bold");
	[#figure(
		caption: [The action map of the game.],
		table(
			columns: 4,
			align: left,
			stroke: none,
			table.hline(stroke: 1pt),
			table.header[Action][Meaning][PC][Console#footnote[
				Theoretically, it could be compatible to console players for free, as the interactions in this game are all very simple and naturally matches with console's input schemes, but due to the limited project life span and lacking of contributors, we didn't have a chance to play-test the game with consoles.
				Although the input mappings are already set for consoles in the project, just for the sake of credibility, we'll avoid saying that our game is console-compatible.
			]],
			table.hline(stroke: 0.5pt),
			[Movement], [Move around in the scene.], [W/A/S/D], [Right stick],
			[Orientation], [Look around.], [Mouse], [Left stick],
			[Interaction], [Interact with objects and trigger logics.], [LMB], [South button],
			table.hline(stroke: 1pt),
		),
	) <table:action-map>];
}

= Game Aesthetics

In cooperation with the background setting of being stuck in a time loop, we wanted to achieve a liminal, spooky, unpleasant aesthetics.
Several designs are applied in the game.

== Environment

The environment is mostly monotonous, mimicking the interior space of a gloomy teaching hall.
There are absolutely no figure-like characters appearing in the entirely of the game, all that the player could see is just the empty hallway, the lights, the doors, and some furnitures.
Not only this could create a vibe of loneliness, it also makes any changes in environment made by the levels instantly eye-catching (e.g. @fig:abrupt-blockade).

#figure(
	image("./assets/abrupt-blockade.png",
		height: 16em,
	),
	caption: [An abrupt blockade in the hallway.],
) <fig:abrupt-blockade>

We have proposed an alternative style of design at an early stage of the project, that is to make the scene super realistic.
Unfortunately, that would require an abundant amount of efforts put into adding and polishing the details.
Due to the limitation of the time span of the project and our range of ability, we chose to abandon this approach.
But if it were successfully excuted, we think it'd render the liminal atmosphere even better.

== Immersiveness and realism

=== Acoustics

Many acoustic designs mentioned in @sec:acoustic-experience are applied for realism as well as helping render the atmosphere.

What we could improve on the this side is to add spatial reverbs.
It could make the acoustic environment more realistic and render the eerie vibe even better.

=== Visual composition

The scene architectures are applied with realistic marble and wooden materials.

There are lights on the hallway and room ceiling, and also on the floor (to show the way in the "Black-out" level).
They are making the alternating lit-dark pattern on the walls.

Spot lights are also used on the floor map as a visual clue to guide the player to read it.

=== Post-process

Post-process filters are applied to enhance the visual style (@fig:post-process-comparison).

#figure(
	caption: [A comparison of the visual appearances with and without post-effect of the same view.],
	grid(
		columns: 2,
		column-gutter: 0.5em,
		image("./assets/without-post-effects.png"),
		image("./assets/with-post-effects.png"),
	)
) <fig:post-process-comparison>

== Responsiveness

Our game tries to avoid using explicit guidances, so to make sure that the players won't feel lost, some elements are responsive.
When the player makes some actions, they could get instant responses.

=== Level failing

When a level is failed, not only the ominous sound effect mentioned in @par:experience-level-failing would be played, also a black fog would quickly approach the camera (@fig:black-fog).
This could give the player a panicking feeling, as well as a strong-enough negative stimulus.

#figure(
	caption: [The black fog approaching the camera.],
	grid(
		columns: 2,
		column-gutter: 0.5em,
		image("./assets/depth-fog/1.png"),
		image("./assets/depth-fog/2.png"),
	)
) <fig:black-fog>

=== Door knobs

As mentioned in @par:experience-door-knobs, a creaking sound would be made when the player interacts with a door knob, so that they know that they've just successfully interacted with it.

= MDA Analysis

#{
	set align(center);
	set text(size: 0.8em);
	show table.cell.where(y: 0): set text(weight: "bold", size: 1.25em);
	show table.cell.where(x: 0): set par(justify: false);
	table(
		columns: (11em, 1.5fr, 1fr),
		align: left,
		stroke: none,
		table.hline(stroke: 1pt),
		table.header([Aesthetics], [Dynamics], [Mechanics]),
		table.hline(stroke: 0.5pt),

		[Being stuck in a weird time loop],
		[The never-ending repeating class dismissal, and the time-reset when failed to reach the classroom on time.],
		[The core game loop.],

		[The unpleasant, spooky liminal vibe],
		[The player just views and listens.],
		[The visual and acoustic designs.],

		[Being played or watched],
		[The scene changes in each cycle, as if something is controlling the time loop.],
		[The level progression.],

		[Exploration],
		[The player needs to explore and traverse the scene to pass the levels.],
		[The levels are passed by arriving the correct classrooms.],

		[Discovery],
		[The players could pass a level by figuring out its pattern.],
		[Each level features a special puzzle design.],

		[Surprising],
		[Noticing that something is changed in the scene.],
		[Levels could add/remove/modify scene objects.],

		[Complement and achievement],
		[The game has a clear ending to tell the player that they've completed the game.],
		[The ending animation and UI.],

		table.hline(stroke: 1pt),
	);
}

= Appendix

== Level Design
<sec:level-design>

#[

#show heading.where(level: 3): set heading(outlined: true)

=== Tutorial \& Advanced tutorial

The starting levels have an extremely important task: introduce the basic mechanism of the game to the player.
We hope that in the first one or two levels, the player would learn that:

+ The goal of this game is to go to classroom 404;
+ they need to actively find it before the time runs out.

Having this goal, we designed two levels, _Tutorial_ (@fig:l-tutorial) and _Advanced Tutorial_ (@fig:l-advanced-tutorial) as the starting levels of the game.

#grid(
	columns: 2,
	column-gutter: 0.5em,
	[#figure(
		image("./assets/levels/tutorial.png"),
		caption: [The designed route of _Tutorial_.],
	) <fig:l-tutorial>],
	[#figure(
		image("./assets/levels/advanced tutorial.png"),
		caption: [The designed route of _Advanced Tutorial_.],
	) <fig:l-advanced-tutorial>],
)

In _Tutorial_, the player spawns in room 402 on the east side.
Upon opening the door, the mobile notification would pop out to tell them to go to 404.
They'll see that they're inside a hallway, and the numbers on the room doors.
Naturally, they'll move along the direction that the numbers are increasing, because they just came out from 402, but after 403 it's 405.
Then after the first corner, the player should be able to directly see that 404 is just on the other side (@fig:l1-corner), which is the actual room 407 (the door number is changed by the program), so they could go straight for it.

#figure(
	image("./assets/levels/l1 corner.png", height: 16em),
	caption: [The view at the corner in _Tutorial_.],
) <fig:l1-corner>

As the player enters room 407 (appearing as 404), the class dismissal bell would ring again and the light in the room would go off.
When they turn around, the player should be able to see the actual room number hanging on the interior wall above the door (@fig:l1-ending).
This is where they might get confused and try to figure out what's wrong.
They might wander around in the room for a while to see if anything is interactable, or they might just go out directly to check the room number on the outside.
Either way, the mobile notification would pop up again in a short period (faster if they went out), urging the player to go and find the "new" room 404.

#figure(
	image("./assets/levels/l1 ending.png", height: 16em),
	caption: [The true room number the player sees after finishing _Tutorial_.],
) <fig:l1-ending>

That's where the second level, _Advanced Tutorial_ comes out.
In this level, another important element for this game is introduced: the floor map (as can be seen in the previous @fig:floor-map-clean).
It serves as a in-game guidance; the player could use it as a reference to locate room 404 faster.

So when they come out of room 407, by inertia they might continue going down the hallway in the previous direction.
The destination for this level is hidden in the corner of the map, so it's very likely they'll miss it when walking pass by the cross.
When they turned west on the southern corner, it's hard not to see the floor map because it's a very big piece hanging on the wall, while also being highlighted by a spot light.
So they'll walk up to read it, and find the destination easily.

=== Blockade series

There are two levels in this series, all featuring the appearance of objects blocking the hallway.

#grid(
	columns: 2,
	column-gutter: 0.5em,
	[#figure(
		image("./assets/levels/blockade.png"),
		caption: [The designed route of _Blockades_.],
	) <fig:l-blockade>],
	[#figure(
		image("./assets/levels/hidden passageway.png"),
		caption: [The designed route of _Hidden Passageway_.],
	) <fig:l-hidden-passageway>],
)

In _Blockades_ (@fig:l-blockade), two pieces of blockades appear in the hallway, completely blocking the player's way to the upper part of the floor.
When the player gets out of 410, they'll first see the east blockade.
Since the way to the north is blocked, they'll go read the floor map and see that room 404 is on the north-west corner.
Then they'll try to use the west path, only to find that it's blocked as well.
They might try to wander around to find some hidden tunnels, but when they eventually go back and carefully read the floor map again, they'll see that the game lab in the middle actually has 2 doors, linking the upper and lower part of the floor.

In _Hidden Passageway_ (@fig:l-hidden-passageway), the door of 404 is blocked, but the wall of an adjacent room is removed, so the player could enter 404 from that room's door.

=== Erased plate series

In this series, all the numbers on the doors are erased (@fig:erased), so the player has to either explore by brute force, or rely on the floor map.

#figure(
	image("./assets/levels/erased-doorplates.png", height: 16em),
	caption: [All the numbers on the doors are erased.],
) <fig:erased>

In _Flipped map_, the floor map is... well, flipped (@fig:flipped).
This requires the player to use their spatial imagery to pass the level.

#figure(
	image("./assets/levels/flipped-floor-map.png", height: 16em),
	caption: [The flipped floor map.],
) <fig:flipped>

In _Dislocated map_, the floor map is moved to the wall on the north-most side of the floor, breaking the player's expectation.

=== Black-out

All the lights in the teaching hall went off (@fig:black-out), except for:

- the floor lights, which remains on to show the pathways,
- and the spot light lighting the floor map.

The player also must rely on the floor map to locate classroom 404 in this level, and the darked out environment makes this more necessary, as it's very easy to lose direction.

#figure(
	image("./assets/levels/black-out.png", height: 16em),
	caption: [All lights are turned off in _Black-out_.],
) <fig:black-out>

=== Humor

The destination classroom is a stall in the restrooms.

The map is also torn to force the player to stop and think the possible locations of 404.
The only unvisited rooms at this stage should be the restrooms.
We hope that this could bring the player a strong _Eureka!_ moment and the satisfaction of their theory getting validated.

=== Final escape

The floor map is totally normal, but 404 is missing.
A door leading to 404 that doesn't previously exist appears on the south-west corner of the hallway, which used to be empty.
When opened, the player could see blue sky behind it (as can be seen in the previous @fig:escape).
The game-ending animation would be rolled after the player left from the door.

]

== Trivia

- When Bob was playing _Humor_ in the live session, it didn't take him a second to figure out that it was the restrooms.
	In fact, he shouted "I knew it!"