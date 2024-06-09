using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        


        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            {GridValue.Empty, Images.Background},
            {GridValue.Brick, Images.Brick},
            {GridValue.FlowerStem, Images.FlowerStem},
            {GridValue.FlowerTopOld, Images.FlowerTopOld},
            {GridValue.FlowerTopYoung, Images.FlowerTopYoung},
            {GridValue.Ground, Images.Ground},
            {GridValue.Character, Images.Character},
            {GridValue.LeafLeft, Images.LeafLeft},
            {GridValue.LeafRight, Images.LeafRight},
            {GridValue.Projectile, Images.Projectile},
            {GridValue.Worm, Images.Worm},
            {GridValue.Worm2, Images.Worm2}
        };
        
        private readonly int rows = 21;
        private readonly int cols = 33;
        private readonly Image[,] gridImages;
        private GameState gameState;

        private readonly DispatcherTimer projectileTimer;
        private readonly DispatcherTimer wormTimer;
        private int score = 0;
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            gameState = new GameState(rows, cols);
            gridImages = SetUpGrid();
            gameState.SpawnCharacter();

            projectileTimer = new DispatcherTimer();
            projectileTimer.Interval = TimeSpan.FromSeconds(0.05); // 2 grid squares per second
            projectileTimer.Tick += ProjectileTimer_Tick;
            projectileTimer.Start();
            wormTimer = new DispatcherTimer();
            wormTimer.Interval = TimeSpan.FromSeconds(2); // Spawn a worm every 2 seconds
            wormTimer.Tick += WormTimer_Tick;
            wormTimer.Start();
            Instance = this;

        }
        public void IncrementScore()
        {
            score++;
            ScoreText.Text = $"SCORE {score}";
        }
        public void GrowFlowerStem()
        {
            gameState.GrowFlowerStem();
        }


        public void FlowerStem_HPChanged(object sender, EventArgs e)
        {
            FlowerHPText.Text = $"FLOWER HP: {gameState.GetFlowerStemHP()}";
            if (gameState.GetFlowerStemHP() <= 0)
            {
                // Game over
                GameOver();
            }
        }
        private void GameOver()
        {
            // Show a game over message or restart the game
            MessageBox.Show("Game Over!");
            //...
        }

        private void WormTimer_Tick(object sender, EventArgs e)
        {
            gameState.SpawnWorms();
        }
        private void ProjectileTimer_Tick(object sender, EventArgs e)
        {
            gameState.UpdateProjectiles();
            gameState.GrowFlowerStem(); // Call the GrowFlowerStem method to update the flower stem
            gameState.UpdateWorms(); // Update the worms
            DrawGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    gameState.MoveCharacter(Direction.Up);
                    break;
                case Key.Down:
                    gameState.MoveCharacter(Direction.Down);
                    break;
                case Key.Left:
                    gameState.MoveCharacter(Direction.Left);
                    break;
                case Key.Right:
                    gameState.MoveCharacter(Direction.Right);
                    break;
                case Key.Space:
                    gameState.ShootProjectile();
                    break;
            }

            DrawGrid();
        }


        private Image[,] SetUpGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image();
                    GridValue gridValue;

                    if (c == 0 && r != rows - 1 || c == cols - 1 && r != rows - 1)
                    {
                        image.Source = Images.Brick;
                        gridValue = GridValue.Brick;
                    }
                    else if (r == rows - 1)
                    {
                        image.Source = Images.Ground;
                        gridValue = GridValue.Ground;
                    }
                    else if (r == 0 && c > 0 && c < 3 || r == 0 && c > 29 && c < 32 || r == 4 && c > 0 && c < 3 || r == 4 && c > 29 && c < 32 || r == 8 && c > 0 && c < 3 || r == 8 && c > 29 && c < 32 || r == 12 && c > 0 && c < 3 || r == 12 && c > 29 && c < 32 || r == 16 && c > 0 && c < 3 || r == 16 && c > 29 && c < 32)
                    {
                        image.Source = Images.Brick;
                        gridValue = GridValue.Brick;
                    }
                    else if (r == 19 && c == 16)
                    {
                        image.Source = Images.FlowerStem;
                        gridValue = GridValue.FlowerStem;
                    }
                    else
                    {
                        image.Source = Images.Background;
                        gridValue = GridValue.Empty;
                    }
                    gameState.Grid[r, c] = gridValue;



                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        private void Draw()
        {
            DrawGrid();
        }

        private void DrawGrid()
        {
            for(int r =0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridVal = gameState.Grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridVal];


                }
            }
        }  

        
    }
}
