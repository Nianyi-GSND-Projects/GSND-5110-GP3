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
#show heading.where(level: 1): set heading(numbering: (..indices) => {
	indices = indices.pos().slice(1);
	return indices.join(".")
})
#show heading.where(level: 1): it => {
	set align(center);
	set text(size: 16pt);
	v(0.5em);
	it;
	v(0.5em);
}

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

#heading(level: 1, outlined: false)[Links]

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

#outline()

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

#pagebreak()

= Overview

Reach classroom 404 before the next class starts---but every time you arrive, the room's location changes, and the clock rewinds. You're stuck in a time loop! In each loop, new obstacles and eerie surprises block your way, try to solve them with your intelligence! Can you ever reach the reality of the true classroom 404 and break the time loop?

_#(gameName)_ is a puzzle game that captures the familiar intense experience of running between classes: rushing to classes.
This relatable setting could create an emotional connection with our target audiences---students, but then the time loop mechanic offers an interesting twist on familiar themes.
Players would also constantly get stuck on expected way to the next classroom, so they need to explore unvisited areas to find their way to the destination, and discovering clues and solving puzzles is a delightful experience.
Under this setting, we could even stuff some absurdity into the puzzle designs, bringing unexpected comedic effects.

= Player Experience

#pagebreak()

= Gameplay

#figure(
	image("./assets/Classroom 404 Game Cycle.svg",
		height: 40em,
	),
	caption: [The outline flow chart of _Classroom 404_.],
)

As the class-dismissing bell rings, one class ends, and the students need to get to the next classroom in the teaching hall before the class break ends.
In this game, the player needs to do the exact same thing: Go to classroom 404 before the next class starts---except that whenever they reach to 404, the bell rings again, the time is reset, and they'll find out that the room they just arrived at is not 404.
Confused, they leave to find room 404, again, but this time they noticed that something in the hallway is changed---there are obstacles blocking their way to the classroom.
When they finally reach to the new 404, the same thing happen again.
This seems like a never-ending loop, and the player is stuck in it!

= MDA Analysis

= Playtest