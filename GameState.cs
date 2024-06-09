using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project
{
    public enum ProjectileDirection { Left, Right }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class GameState
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }
        public int CharacterRow { get; private set; }
        public int CharacterCol { get; private set; }
        public Direction CharacterDirection { get; private set; }
        private int wormTimerCount;
        private FlowerStem flowerStem;

        private int growTimer = 0;
        private List<Worm> worms = new List<Worm>();

        private ProjectileDirection lastHorizontalDirection;


        private readonly Random rnd = new Random();

        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];

            SpawnCharacter();

            flowerStem = new FlowerStem(19, 16, 1, 5); // Initialize the flower stem at row 19, column 16, with a height of 1 and 5 HP
        }

        public void SpawnCharacter()
        {
            CharacterRow = 5;
            CharacterCol = 5;
            CharacterDirection = Direction.Right;
            Grid[CharacterRow, CharacterCol] = GridValue.Character;
        }

        public void MoveCharacter(Direction direction)
        {
            int newRow = CharacterRow;
            int newCol = CharacterCol;

            switch (direction)
            {
                case Direction.Up:
                    newRow--;
                    break;
                case Direction.Down:
                    newRow++;
                    break;
                case Direction.Left:
                    newCol--;
                    lastHorizontalDirection = ProjectileDirection.Left; // Update lastHorizontalDirection
                    break;
                case Direction.Right:
                    newCol++;
                    lastHorizontalDirection = ProjectileDirection.Right; // Update lastHorizontalDirection
                    break;
            }

            if (newRow >= 0 && newRow < Rows && newCol >= 0 && newCol < Cols)
            {
                if (Grid[newRow, newCol] != GridValue.Brick && Grid[newRow, newCol] != GridValue.Ground && Grid[newRow, newCol] != GridValue.FlowerStem && Grid[newRow, newCol] != GridValue.LeafLeft && Grid[newRow, newCol] != GridValue.LeafRight && Grid[newRow, newCol] != GridValue.FlowerTopYoung && Grid[newRow, newCol] != GridValue.FlowerTopOld)
                {
                    Grid[CharacterRow, CharacterCol] = GridValue.Empty;
                    Grid[newRow, newCol] = GridValue.Character;
                    CharacterRow = newRow;
                    CharacterCol = newCol;
                }
            }
        }
        private List<Projectile> projectiles = new List<Projectile>();

        public void ShootProjectile()
        {
            int newRow = CharacterRow;
            int newCol = CharacterCol;

            int xVelocity = 0;
            int yVelocity = 0;

            if (lastHorizontalDirection == ProjectileDirection.Left)
            {
                xVelocity = -1; // Shoot to the left
                newCol -= 1; // Spawn projectile one tile to the left
            }
            else if (lastHorizontalDirection == ProjectileDirection.Right)
            {
                xVelocity = 1; // Shoot to the right
                newCol += 1; // Spawn projectile one tile to the right
            }

            Projectile projectile = new Projectile(newRow, newCol, xVelocity, yVelocity);
            projectiles.Add(projectile);
            Grid[newRow, newCol] = GridValue.Projectile;
        } 


        public void UpdateProjectiles()
        {
            foreach (Projectile projectile in projectiles.ToList())
            {
                Grid[projectile.Row, projectile.Col] = GridValue.Empty; // Clear the previous position

                projectile.Move();

                if (projectile.Row < 0 || projectile.Row >= Rows || projectile.Col < 0 || projectile.Col >= Cols)
                {
                    // Remove the projectile when it goes out of bounds
                    projectiles.Remove(projectile);
                }
                else if (Grid[projectile.Row, projectile.Col] == GridValue.Brick)
                {
                    // Remove the projectile when it hits a brick
                    projectiles.Remove(projectile);
                    Grid[projectile.Row, projectile.Col] = GridValue.Brick;
                }
                else if (Grid[projectile.Row, projectile.Col] == GridValue.FlowerStem)
                {
                    projectiles.Remove(projectile);
                    Grid[projectile.Row, projectile.Col] = GridValue.FlowerStem;
                }
                else if (Grid[projectile.Row, projectile.Col] == GridValue.FlowerTopYoung)
                {
                    projectiles.Remove(projectile);
                    Grid[projectile.Row, projectile.Col] = GridValue.FlowerTopYoung;
                }
                else
                {
                    Grid[projectile.Row, projectile.Col] = GridValue.Projectile;
                }
            }
        }

        public void GrowFlowerStem()
        {
            growTimer++;
            if (growTimer >= 60) // 100 ticks = 5 seconds (assuming 20 ticks per second)
            {
                growTimer = 0;
                if (flowerStem.Height < 12) // Limit the stem height to 12
                {
                    flowerStem.Height++;
                    Grid[flowerStem.Row, flowerStem.Col] = GridValue.FlowerStem;
                    for (int i = 1; i < flowerStem.Height; i++)
                    {
                        Grid[flowerStem.Row - i, flowerStem.Col] = GridValue.FlowerStem;
                    }
                    // Set the top tile to FlowerTopYoung image
                    Grid[flowerStem.Row - flowerStem.Height, flowerStem.Col] = GridValue.FlowerTopYoung;
                }
                else if (flowerStem.Height == 12)
                {
                    // Stem has grown fully, display "You win" message
                    MessageBox.Show("You win!");

                    // Set the top tile to FlowerTopOld image
                    Grid[flowerStem.Row - flowerStem.Height, flowerStem.Col] = GridValue.FlowerTopOld;
                }
            }
        }

        public int GetFlowerStemHP()
        {
            return flowerStem.HP;
        }

        public void DecreaseFlowerStemHP()
        {
            flowerStem.HP--;
        }

        public void Shoot()
        {
            Projectile projectile;
            if (lastHorizontalDirection == ProjectileDirection.Left)
            {
                projectile = new Projectile(CharacterRow, CharacterCol, -1, 0); // Shoot left
            }
            else
            {
                projectile = new Projectile(CharacterRow, CharacterCol, 1, 0); // Shoot right
            }
            projectiles.Add(projectile);
        }

        public void SpawnWorms()
        {
            if (worms.Count < 6) // Only spawn a new worm if there are less than 6 worms
            {
                int col = rnd.Next(0, 2) == 0 ? rnd.Next(2, 4) : rnd.Next(30, 32); // Random column between 2, 3, 30, 31
                int row;
                do
                {
                    row = rnd.Next(1, Rows - 1); // Random row between 1 and Rows - 1
                } while (row % 4 == 0); // Ensure row is not 0, 4, 8, 12, 16
                bool goesRight = col < Cols / 2; // Worms on the left side go right, worms on the right side go left
                Worm worm = new Worm(row, col, goesRight ? 1 : -1, 0);
                worms.Add(worm);
                Grid[row, col] = GridValue.Worm;
            }
        }

        public void UpdateWorms()
        {
            foreach (Worm worm in worms.ToList())
            {
                Grid[worm.Row, worm.Col] = GridValue.Empty; // Clear the previous position

                if (wormTimerCount % 10 == 0) // Move the worm every 20 ticks (1 second)
                {
                    worm.Move();
                }
                foreach (Projectile projectile in projectiles)
                {
                    if (worm.Row == projectile.Row && worm.Col == projectile.Col)
                    {
                        // Remove the worm and the projectile
                        worms.Remove(worm);
                        MainWindow.Instance.IncrementScore();
                        projectiles.Remove(projectile);
                        Grid[worm.Row, worm.Col] = GridValue.Empty;
                        break;
                    }
                }

                if (worm.Col < 0 || worm.Col >= Cols || worm.Row < 0 || worm.Row >= Rows)
                {
                    // Remove the worm when it goes out of bounds
                    worms.Remove(worm);
                }
                else if (Grid[worm.Row, worm.Col] == GridValue.Projectile)
                {
                    // Remove the worm when it hits a projectile
                    worms.Remove(worm);
                    Grid[worm.Row, worm.Col] = GridValue.Empty;
                }
                else if (Grid[worm.Row, worm.Col] == GridValue.FlowerStem)
                {
                    // Damage the flower stem when a worm hits it
                    flowerStem.HP--;
                    worms.Remove(worm);
                    // Call the FlowerStem_HPChanged method to update the UI
                    MainWindow.Instance.FlowerStem_HPChanged(null, null);
                }
                else
                {
                    Grid[worm.Row, worm.Col] = GridValue.Worm;
                }
            }
            wormTimerCount++;
        }

    }
}
