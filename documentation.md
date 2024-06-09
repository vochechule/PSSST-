# Project Documentation

## Overview
This project is a simple 2D game implemented in C# using the WPF (Windows Presentation Foundation) framework. The player controls a character that can move around the grid, shoot projectiles, and grow a flower stem. The objective is to protect the flower stem from worms that appear on the grid.

## Main Classes and Methods

### MainWindow Class
This is the main class that initializes the game window and handles user interactions, rendering, and game state updates.

#### Fields
- `gridValToImage`: A dictionary mapping `GridValue` enums to corresponding image sources.
- `rows` and `cols`: The number of rows and columns in the grid.
- `gridImages`: A 2D array of `Image` objects representing the game grid.
- `gameState`: An instance of the `GameState` class managing the game's state.
- `projectileTimer` and `wormTimer`: Timers for updating projectiles and spawning worms.
- `score`: The player's score.
- `Instance`: A static reference to the main window instance.

#### Methods
- `MainWindow()`: Initializes the main window, sets up the grid, spawns the character, and starts the timers.
- `IncrementScore()`: Increments the score and updates the score text.
- `GrowFlowerStem()`: Calls the `GrowFlowerStem` method in `GameState`.
- `FlowerStem_HPChanged()`: Updates the flower's HP text and checks for game over condition.
- `GameOver()`: Displays a game over message.
- `WormTimer_Tick()`: Spawns worms at each timer tick.
- `ProjectileTimer_Tick()`: Updates projectiles and worms, and redraws the grid.
- `Window_Loaded()`: Draws the initial grid when the window is loaded.
- `Window_KeyDown()`: Handles character movement and shooting based on user input.
- `SetUpGrid()`: Sets up the initial game grid with images.
- `Draw()`: Calls `DrawGrid` to render the grid.
- `DrawGrid()`: Updates the grid images based on the current game state.

### GameState Class
Manages the game state, including the positions of the character, projectiles, and worms, as well as the flower stem's growth and health.

#### Fields
- `Rows` and `Cols`: The number of rows and columns in the grid.
- `Grid`: A 2D array representing the grid values.
- `Score`: The player's score.
- `GameOver`: A flag indicating if the game is over.
- `CharacterRow`, `CharacterCol`, and `CharacterDirection`: The character's position and direction.
- `wormTimerCount`: A counter for the worm timer.
- `flowerStem`: An instance of the `FlowerStem` class.
- `growTimer`: A timer for growing the flower stem.
- `worms`: A list of `Worm` objects.
- `lastHorizontalDirection`: The last horizontal direction the character faced.
- `projectiles`: A list of `Projectile` objects.
- `rnd`: A random number generator.

#### Methods
- `GameState(int rows, int cols)`: Initializes the game state with the specified grid size.
- `SpawnCharacter()`: Spawns the character at the initial position.
- `MoveCharacter(Direction direction)`: Moves the character in the specified direction.
- `ShootProjectile()`: Shoots a projectile in the last horizontal direction.
- `UpdateProjectiles()`: Updates the positions of all projectiles.
- `GrowFlowerStem()`: Grows the flower stem over time and checks for winning condition.
- `GetFlowerStemHP()`: Returns the current HP of the flower stem.
- `DecreaseFlowerStemHP()`: Decreases the flower stem's HP.
- `Shoot()`: Shoots a projectile based on the character's last direction.
- `SpawnWorms()`: Spawns worms at random positions.
- `UpdateWorms()`: Updates the positions of all worms and handles interactions with projectiles and the flower stem.

### FlowerStem Class
Represents the flower stem in the game.

#### Fields
- `Row` and `Col`: The position of the flower stem.
- `Height`: The current height of the flower stem.
- `HP`: The health points of the flower stem.

#### Methods
- `FlowerStem(int row, int col, int height, int hp)`: Initializes the flower stem with the specified position, height, and HP.
- `Damage()`: Decreases the flower stem's HP.

### Worm Class
Represents a worm in the game.

#### Fields
- `Row` and `Col`: The position of the worm.
- `XVelocity` and `YVelocity`: The velocity of the worm.

#### Methods
- `Worm(int row, int col, int xVelocity, int yVelocity)`: Initializes the worm with the specified position and velocity.
- `Move()`: Moves the worm based on its velocity.

### Projectile Class
Represents a projectile in the game.

#### Fields
- `Row` and `Col`: The position of the projectile.
- `XVelocity` and `YVelocity`: The velocity of the projectile.

#### Methods
- `Projectile(int row, int col, int xVelocity, int yVelocity)`: Initializes the projectile with the specified position and velocity.
- `Move()`: Moves the projectile based on its velocity.

### Images Class
Loads and provides image sources for the game.

#### Fields
- Static `ImageSource` fields for various game elements like `Background`, `Brick`, `FlowerStem`, etc.

#### Methods
- `LoadImage(string filename)`: Loads an image from the specified filename.

### Enums
- `GridValue`: Represents various values a grid cell can have (e.g., `Empty`, `Brick`, `Ground`, `FlowerStem`, etc.).
- `ProjectileDirection`: Represents the direction of a projectile (e.g., `Left`, `Right`).
- `Direction`: Represents the direction of the character (e.g., `Up`, `Down`, `Left`, `Right`).

## Summary
The project is a simple game where the player controls a character to protect a growing flower stem from attacking worms. The player can move the character and shoot projectiles to destroy the worms. The game ends if the flower stem's health reaches zero. The game is implemented using WPF and consists of several classes managing different aspects of the game, including the game state, character movements, projectiles, worms, and rendering the grid.