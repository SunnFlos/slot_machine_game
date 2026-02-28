&nbsp;Unity Slot Machine Game (WebGL)



Game Concept

This is a 2D Slot Machine game developed in Unity as part of a technical assignment.  

The game simulates a classic slot machine experience where players place bets, pull a handle, and spin reels to win based on matching symbols.



The project focuses on:

* Clean architecture
* object-Oriented Programming
* Smooth animations
* Fair random outcomes (RNG)



---



&nbsp;Gameplay Features

* 3-Reel Slot Machine
* Winning Logic, Player wins when all reels stop on the same symbol
* Randomized Outcomes using Unity's RNG
* Handle Interaction to start spinning
* Bet System with adjustable bet values
* WebGL Build playable in browser



---



Technical Details

* Engine Unity (2D)
* Language: C#
* Build Target: WebGL
* Architecture: Object-Oriented Programming (OOP)
* UI System: Unity Canvas



---

Project Structure

Assets/

&nbsp;Scripts/

* SlotManager.cs
* Reel.cs
* HandleController.cs
* AudioManager.cs

Assets/Prefab/ 

* 7 Symbol.prefab
* Bar Symbol.prefab
* Bell Symbol.prefab
* BetTable.prefab
* Cherry Symbol.prefab
* Handle.prefab

Assets/Audio/

* ButtonSound
* WinSound
* LossSound

Assets/Sprites/

* Machine.png
* Handle.png



BuildVersion/

&nbsp;WebGL/ 

WebGL builds must be run using a local server



