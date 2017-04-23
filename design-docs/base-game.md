# LD 38 Game Design Doc

## Components of the game space

* Program window (or fullscreen)
* 4 walls inset from the window
* free space between walls and window boundaries
* ball
* user interface
    * hint text to describe which wall maps to a key

### Component dimensions

* game window - 1280x720
* play space - 512x512
* ball size - 16x16
* score counter - 32pt Monaco Regular, offset (24, 16), left aligned
    * [Monaco on Github](https://github.com/todylu/monaco.ttf)

## Base Game Rules

### Core Gameplay Loop

* Ball starts moving towards wall
* When ball is about to touch wall, player hits corresponding wall button
* ball bounces off wall on successful button press
  * successful button press is hitting button within timing window
  * points for successful bounce are awarded
  * OPT - bonus points for frame window within successful bounce
* repeat until unsuccessfully timed button press, ball collides with wall and game over

### Loss Condition
* ball collides with wall
* OPT - ball stops

### Victory Condition
* high score

### Paused State
* dimmed screen
* large text indicating the game is paused and how to unpause
* OPT - Completely block game space from player view, have a 'frozen' game state for a few seconds after unpause that is visible

### Controls
* W,A,S,D
  * each are mapped to a wall
  * hold a button to select a wall
  * one wall selectable at a time
    * OPT - last button pressed wins
    * OPT - held button pressed wins
* space bar
  * press to engage selected wall
  * engaged walls have slight cooldown after firing
  * hold to 'charge' wall bounce power
* left arrow, right arrow
  * add directional influence to the ball so it will try to turn as it flies
* E
  * pause the game
* OPT - mouse & left click
  * move cursor within play space
  * left click creates a temporary object that ball can bounce off of
  * cooldown after creating temporary object
* DEV - Q
  * engage debug mode

### Obstacles
* below the play space, a counter is displayed to the player that shows the number of bounces they have before a new obstacle spawns
  * obstacles do not spawn when the player has an active powerup
* obstacles are spawned in a 384x384 bounded region centered within the play space
  * the obstacle should try to spawn near the trajectory of the ball (not necessarily directly in front of it but also not somewhere completely irrelevant)
* the ball can collide with obstacles, it doesn't require any input to bounce
* after a collision, the obstacle's durability is reduced by 1 and its sprite changes
  * three concentric circles represent the durability
  * each hit breaks off the outermost circle
  * first two circles are normal reflects
  * third circle is a strong reflect
* if the ball collides with an obstacle, the player earns points based on how degraded the obstacle is
  * more points the more degraded
* if the ball breaks an obstacle, it still bounces
  * if the obstacle has a powerup, the player gains the powerup after breaking it
  * earning a powerup changes the obstacle counter to a powerup countdown
  * powerups remain active for a set number of bounces, after which the powerup countdown reverts back to an obstacle counter
* the obstacle counter decreases based on point thresholds that the player crosses

### Points
* the player earns points in the following ways
  * bouncing off walls successfully
  * sweet spot bouncing
  * bouncing off of obstacles
  * destroying obstacles
  * acquiring a powerup

* the player earns a point multiplier in the following ways
  * performing a strong reflect
  * performing a strong sweet spot reflect

* the player loses their point multiplier by doing a regular strength reflect, regardless of if it hits the sweet spot or not

### Powerups
* a percent chance determines if a powerup spawns instead of an obstacle
* each powerup has a certain number of bounces it stays active for. they are not all necessarily the same
* the following powerups are spawnable during the game
  * speed up
    * the ball speeds up to strong reflect speed and stays that way regardless of the kind of reflect the player times
  * slow down
    * the ball slows down below the base speed. strong reflects speed it up to normal speed.
  * 'extra life'
    * the ball gains a shield that prevents death when you hit a wall
    * the shield evaporates after it prevents death once
  * multiball
    * spawns another ball within the restricted play area that has a random direction
    * as long as you have one ball around, you don't lose
    * BS applies to all balls
  * pointmania
    * the restricted play area is littered with small gems that exist for the duration of the powerup
    * colliding with a gem doesn't cause a bounce, but gives the player some points
    * at the end of the powerup duration, all gems disappear

## Variants on Core Gameplay Loop

(canonical, refer to dedicated sections)
* Game objects appear within the play space
  * ball can collide with them, no timed input necessary for successful collision
  * durability of objects so that they eventually break
    * three concentric circles
    * each hit breaks off the outermost circle
    * first two circles are normal reflects
    * third circle is a strong reflect
  * object causes powerup
    * powerup objects have points in the outer two circles
    * innermost circle is a powerup
  * OPT - objects alleviate timed failure condition

(non-canon)
* walls undergo change over time
  * OPT - constantly closing in towards ball
    * can be pushed back outward by accomplishing some sort of task
  * OPT - walls rotate
  * OPT - walls swap or get remapped to keys

(non-canon)
* mouse pointer can create a 'ripple'
  * ball bounces off ripple
  * OPT - recharge timer
  * OPT - wall bounces recharge ripple power

(non-canon)
* arrow keys control another object that the ball always bounces off of

(canonical, refer to dedicated section)
* powerups can be acquired to adjust game mechanics
  * speed up
  * slow down
  * 'extra life' - shield of some kind that prevents death when you hit a wall, single use
  * multiball - as long as you have one ball around, you don't lose
  * point multiplier
    * OPT - persistent
    * OPT - fading over time, collecting another refreshes meter
    * OPT - breaking one turns another into a star powerup for a time, getting that
  * stronk man - every object breaks in one hit
  * change size of playable area
  * bigger or smaller ball
  * pointmania
  * bouncy house
    * walls always in reflect state

(non-canon)
* ball speeds up with each successful bounce
* ball slows down over time
  * prevent this by charging shots with the wall

## Debug Mode
* allows gameplay variants to be turned on/off
* gameplay variants that are on have exposed, adjustable parameters


## Game Aesthetics

### Visual Options
* black and white
  * Walls emit a force that pushes the ball
  * selected wall pulses a transparent version of that force
  * animation timing prevents another pulse from being created out of the same wall
  * ball leaves a light contrail as it moves
  * score counter somewhere
    * OPT - multiplier displayed
  * on a good bounce, particle effect to show you did it
    * OPT - particles rush to ball afterword
  * OPT - hit pause on successful actions

* colorized

### Musical Options
* silent
* sound for collisions
* special sounds for powerups
* sound for sweet spot effect
* sound for points
* game over sound
* backing track
  * simple
  * ambient
  * rhythmic, but not upbeat

### Narrative Options
* text on screen telling a story influenced by the theme of A Small World
* story of a person who uses an escape from the real world to stay focused and have purpose in life
  * work as an escape
  * games as an escape
  * outlines various life events that align with powerups or score milestones
  * story repeats itself after you go through far enough