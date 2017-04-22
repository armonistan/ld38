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
  * OPT - hold to 'charge' wall bounce power
* E
  * pause the game
* OPT - mouse & left click
  * move cursor within play space
  * left click creates a temporary object that ball can bounce off of
  * cooldown after creating temporary object
* DEV - Q
  * engage debug mode

## Variants on Core Gameplay Loop

* Game objects appear within the play space
  * ball can collide with them, no timed input necessary for successful collision
  * OPT - durability of objects so that they eventually break
  * OPT - object causes powerup
  * OPT - objects alleviate timed failure condition
  * OPT - points awarded on bounce

* walls undergo change over time
  * OPT - constantly closing in towards ball
    * can be pushed back outward by accomplishing some sort of task
  * OPT - walls rotate
  * OPT - walls swap or get remapped to keys

* mouse pointer can create a 'ripple'
  * ball bounces off ripple
  * OPT - recharge timer
  * OPT - wall bounces recharge ripple power

* powerups can be acquired to adjust game mechanics
  * speed up
  * slow down
  * 'extra life' - shield of some kind that prevents death when you hit a wall, single use
  * multiball - as long as you have one ball around, you don't lose
  * point multiplier
    * OPT - persistent
    * OPT - fading over time, collecting another refreshes meter

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