# Narrative

## Game Themes

* A Small World
* retreating into a safe space
* at any point, being ripped out of that by loss
* good things happen
* bad things happen
* we each have our choices
* narrator thinks it's happening to them, but it's happening because of them
* no matter what happens to us or what we would like to happen, our sphere of influence is small. the world that we truly own is a tiny one
* we play games and things that we get sucked in to, emphasizing our role

## Story beats

* Introduction
* Death in the family
* Promotion of a friend
* Losing a girlfriend
* Getting engaged
* Having a child
* Being invited out for drinks
* Moving away to a new city

### Introduction

The narrator explains to the player how the game works. The player is the narrator's friend. They haven't known each other for too long, and this is one of the first times that they are spending together.

The narrator goes quiet for a short period of time, then starts having a kind of improptu therapy session.

---

Ok, so first you hold W, A, S, or D to select a wall. 

And then once you've selected one, you press space when the ball gets close. 

If you get it right, the ball bounces.

If you miss, the ball hits the wall and then it's game over.

It's a pretty simple game, but I've had fun with it.

It is relaxing sometimes to be able to come home and just focus on something, you know?

*narrator goes silent for a few moments*

Thanks for coming over to play. It's been a while since I've had anyone other than Jules around.

She can get pretty busy with work. 

choose between old girlfriend story/engagement story/work promotion/having a child story

I used to work like crazy myself. Back before you worked here, maybe a year or two ago.

The managers seemed to really reward people who worked a ton.

At least, they made it look like they worked a ton. 

Don't get me wrong, some of them weren't bad.

But a lot of those guys were just loudmouths. 

It bothered me, to see these people get so much credit.

I thought if you just focused on keeping your head down and getting stuff done, people would notice.

But I tried that for like five years and barely got any kind of response.

Do you feel like that at all? Like, you get a lot done, but does anyone else see that?

storyThread = [{
    "text" : "It got to me man. One weekend I stayed in the office trying to finish up for a deadline.",
    "delayMS" : 1000,
    "correspondingPowerup" : SpeedUp
}]

storyThreads[];

story = [storyThreads[0], jumble(storyThreads[1], storyThreads[storyThreads.length-2]), storyThreads[storyThreads.length-1]];

The project launched successfully, and I went home and slept.

My manager pulled the team together the next morning to congratulate us.

She thanked Tomas especially for this effort in getting the project done on time.



I didn't know how to play the game I guess.




### Powerups to integrate

* speed up
* slow down
* 'extra life' - shield of some kind that prevents death when you hit a wall, single use
* multiball - as long as you have one ball around, you don't lose
* star power
  * breaking one turns another into a star powerup for a time, getting that
* stronk man - every object breaks in one hit
* change size of playable area
* bigger or smaller ball
* pointmania