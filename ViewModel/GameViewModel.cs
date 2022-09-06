using SpaceWars.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SpaceWars.ViewModel
{
    class GameViewModel : INotifyPropertyChanged
    {
        // GAME CONST
        const int TIMER = 15;
        const int SCREEN_LIMIT = 80;

        // PLAYER CONST
        const int PLAYER_SPEED = 4;
        const int PLAYER_SHOOT_COOLDOWN = 10;
        const int PLAYER_LASER_SPEED = 15;
        const int PLAYER_MAX_LIFE = 3;

        // ENEMY CONST
        const int NB_ENEMY = 20;
        const int ENEMY_WAIT_BEFORE_SPAWN = 75;
        const int ENEMY_SHOOT_COOLDOWN = 75;
        const int ENEMY_LASER_SPEED = 3;
        const double ENEMY_SPEED = 1.5;

        // ASTEROID CONST
        const double ASTEROID_SPEED = 1;
        const int ASTEROID_WAIT_BEFORE_SPAWN = 300;

        private MainWindow mainWindow;

        // game status
        private string gameStatus;
        private ImageBrush gameLife;

        // player
        private Player player;
        private int playerShootCounter = 0;
        private int playerLife = PLAYER_MAX_LIFE;
        private bool isPlayerInvisible = false;

        // player movement
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool isMovingUp = false;
        private bool isMovingDown = false;

        // enemy
        private List<Enemy> enemyList = new List<Enemy>();
        private int nbEnemies = NB_ENEMY;
        private int enemyCounter = ENEMY_WAIT_BEFORE_SPAWN;
        private int enemyShootCounter = ENEMY_WAIT_BEFORE_SPAWN;

        //asteroid
        private List<Asteroid> asteroidList = new List<Asteroid>();
        private int asteroidCounter = ASTEROID_WAIT_BEFORE_SPAWN;

        // tools
        private List<RectangleItem> garbageList = new List<RectangleItem>();
        public ObservableCollection<RectangleItem> ItemList { get; set; }
        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Render);

        public GameViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            // init game status (life and nb enemies)
            GameStatus = "Empire ship's left : " + NB_ENEMY;

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(@"assets/images/3heart.png", UriKind.Relative));
            GameLife = brush;

            // create item list
            ItemList = new ObservableCollection<RectangleItem>();

            // create player
            this.player = new Player(this.mainWindow.Width/2, this.mainWindow.Height - SCREEN_LIMIT * 1.5);
            ItemList.Add(this.player);

            // create game timer and call method to update view
            gameTimer.Interval = TimeSpan.FromMilliseconds(TIMER);
            gameTimer.Tick += Game;
            gameTimer.Start();
        }

        public string GameStatus
        {
            get => gameStatus;
            set
            {
                gameStatus = value;
                OnPropertyRaised("GameStatus");
            }
        }

        public ImageBrush GameLife
        {
            get => gameLife;
            set
            {
                gameLife = value;
                OnPropertyRaised("GameLife");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call event when property change
        /// </summary>
        /// <param name="propertyname"></param>
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        /// <summary>
        /// When user press a KEY
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PressKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                this.isMovingLeft = true;
            if (e.Key == Key.Right)
                this.isMovingRight = true;
            if (e.Key == Key.Up)
                this.isMovingUp = true;
            if (e.Key == Key.Down)
                this.isMovingDown = true;

            // if user press space key and player can shoot
            if (e.Key == Key.Space && playerShootCounter < 0)
            {
                // create player laser
                ItemList.Add(this.player.Shoot());

                // init counter again
                this.playerShootCounter = PLAYER_SHOOT_COOLDOWN;
            }
        }

        /// <summary>
        /// When user release a key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReleaseKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                this.isMovingLeft = false;
            if (e.Key == Key.Right)
                this.isMovingRight = false;
            if (e.Key == Key.Up)
                this.isMovingUp = false;
            if (e.Key == Key.Down)
                this.isMovingDown = false;
        }



        /// <summary>
        /// Game LOOP (Execute alaways during the game)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game(object sender, EventArgs e)
        {
            // Move player
            if (this.isMovingLeft && this.player.X > 0)
                this.player.Move(-PLAYER_SPEED, 0);

            if (this.isMovingRight && this.player.X + SCREEN_LIMIT < this.mainWindow.Width)
                this.player.Move(PLAYER_SPEED, 0);

            if (this.isMovingUp && this.player.Y > 0)
                this.player.Move(0, -PLAYER_SPEED);

            if (this.isMovingDown && this.player.Y + SCREEN_LIMIT < this.mainWindow.Height)
                this.player.Move(0, PLAYER_SPEED);

            // decrease counters
            this.playerShootCounter--;
            this.enemyCounter--;
            this.enemyShootCounter--;
            this.asteroidCounter--;

            // if counter is null and nb enemies to destroy is greather than zero
            if (this.enemyCounter < 0 && this.nbEnemies > 0)
            {
                // create new Enemy
                EnemySpawner();

                // inti counter again
                this.enemyCounter = ENEMY_WAIT_BEFORE_SPAWN;
            }

            // if counter is null and nb enemies to destroy is greather than zero
            if (this.enemyShootCounter < 0 && this.nbEnemies > 0)
            {
                // create new enemy Laser
                CreateEnemyLaser();

                // init counter again
                this.enemyShootCounter = ENEMY_SHOOT_COOLDOWN;
            }

            // if counter is null
            if(this.asteroidCounter < 0)
            {
                // create new Asteroid
                AsteroidSpawner();

                // init counter again
                this.asteroidCounter = ASTEROID_WAIT_BEFORE_SPAWN;
            }

            UpdateView();
        }

        /// <summary>
        /// Update view:
        ///     - Move every RectangleItem in view
        ///     - Check position of every Item
        ///     - Destroy some of them if necessary
        /// </summary>
        private void UpdateView()
        {
            // foreach item in view
            foreach (RectangleItem item in ItemList)
            {
                if (item is Laser)
                {
                    Laser laser = item as Laser;

                    // if it's a player's laser
                    if(laser.IsPlayerLaser)
                    {
                        laser.Move(0, -PLAYER_LASER_SPEED);

                        // if laser is out of screen
                        if(laser.Y < 0)
                        {
                            garbageList.Add(laser);
                        }
                        else
                        {
                            Rect laserHitBox = laser.GetHitBox();

                            // foreach enemy in view
                            foreach (Enemy enemy in enemyList)
                            {
                                // if laser intersects with enemy
                                if (laserHitBox.IntersectsWith(enemy.GetHitBox()))
                                {
                                    nbEnemies--;
                                    GameStatus = "Empire ship's left : " + nbEnemies;

                                    //destroy laser and enemy
                                    garbageList.Add(enemy);
                                    garbageList.Add(laser);
                                }
                            }

                            // foreach asteroid in view
                            foreach (Asteroid asteroid in asteroidList)
                            {
                                // if laser intersects with asteroid
                                if (laserHitBox.IntersectsWith(asteroid.GetHitBox()))
                                {
                                    //destroy laser
                                    garbageList.Add(laser);
                                }
                            }
                        }
                    }
                    // else it's an enemy's laser
                    else
                    {
                        laser.Move(0, ENEMY_LASER_SPEED);

                        CheckPosition(laser);
                    }
                }

                if(item is Enemy)
                {
                    Enemy enemy = item as Enemy;

                    enemy.Move(0, ENEMY_SPEED);

                    CheckPosition(enemy);
                }

                if(item is Asteroid)
                {
                    Asteroid asteroid = item as Asteroid;

                    asteroid.Move(0, ASTEROID_SPEED);

                    CheckPosition(asteroid);
                }
            }

            // if enemy counter is null
            if(nbEnemies == 0)
            {
                // player win
                EndGame(true);
            }

            // remove all garbage element from view
            foreach(RectangleItem item in garbageList)
            {
                if(item is Enemy)
                {
                    Enemy enemy = item as Enemy;
                    enemyList.Remove(enemy);
                }

                if(item is Asteroid)
                {
                    Asteroid asteroid = item as Asteroid;
                    asteroidList.Remove(asteroid);
                }

                ItemList.Remove(item);
            }

            // clear garbage list
            garbageList.Clear();

            // refresh items positions in view
            CollectionViewSource.GetDefaultView(ItemList).Refresh();

            // if timer stoped (end of the game)
            if(!this.gameTimer.IsEnabled)
            {
                //clear all elements
                ItemList.Clear();
                enemyList.Clear();
                asteroidList.Clear();
            }
        }

        private void CheckPosition(RectangleItem item)
        {
            // if item is out of screen
            if (item.Y > this.mainWindow.Height)
            {
                garbageList.Add(item);
                return;
            }

            Rect hitBox = item.GetHitBox();

            // item intersects with player and player is visible
            if (hitBox.IntersectsWith(this.player.GetHitBox()) && !this.isPlayerInvisible)
            {
                // touch player
                DecreaseLife();
                PlayerInvisible();

                // add item in garbage to detroy at the end of the loop
                garbageList.Add(item);
            }
        }

        /// <summary>
        /// Create enemy and add it to game
        /// </summary>
        private void EnemySpawner()
        {
            Random rand = new Random();

            double y = -100;

            // get random value within the window width range
            double x = rand.Next(SCREEN_LIMIT, Convert.ToInt32(this.mainWindow.Width) - SCREEN_LIMIT);

            Enemy enemy = new Enemy(x, y);
            enemyList.Add(enemy);
            ItemList.Add(enemy);
        }

        /// <summary>
        /// Create Laser of enemies
        /// </summary>
        private void CreateEnemyLaser()
        {
            Random rand = new Random();

            // get random enemy in enemyList and call shoot method
            int i = rand.Next(enemyList.Count());

            Laser laser = enemyList[i].Shoot();
            ItemList.Add(laser);
        }

        /// <summary>
        /// Create asteroid and add it to game
        /// </summary>
        private void AsteroidSpawner()
        {
            Random rand = new Random();

            double y = -100;

            // get random value within the window width range
            double x = rand.Next(SCREEN_LIMIT, Convert.ToInt32(this.mainWindow.Width) - SCREEN_LIMIT);

            Asteroid asteroid = new Asteroid(x, y);
            asteroidList.Add(asteroid);
            ItemList.Add(asteroid);
        }


        /// <summary>
        /// Deacrease player life and update view
        /// </summary>
        private void DecreaseLife()
        {
            this.playerLife--;
            ImageBrush brush = new ImageBrush();

            // change heart brush
            switch (this.playerLife)
            {
                case 0:
                    EndGame(false);
                    break;
                case 1: 
                    brush.ImageSource = new BitmapImage(new Uri(@"assets/images/1heart.png", UriKind.Relative));
                    break;
                case 2:
                    brush.ImageSource = new BitmapImage(new Uri(@"assets/images/2heart.png", UriKind.Relative));    
                    break;
            }

            GameLife = brush;
        }

        /// <summary>
        /// Change Opacitiy and set player invisible for a short time
        /// </summary>
        private async void PlayerInvisible()
        {
            // Set player invisible
            this.isPlayerInvisible = true;
            this.player.Opacity = 0.5;

            await Task.Delay(1500);

            // Set player visible
            this.isPlayerInvisible = false;
            this.player.Opacity = 1.0;
        }

        /// <summary>
        /// When the game is over, both case
        /// </summary>
        /// <param name="playerWon">player has won ? </param>
        private void EndGame(bool playerWon)
        {
            // Stop timer
            this.gameTimer.Stop();

            //change view
            this.mainWindow.DataContext = new EndMenuViewModel(this.mainWindow, playerWon);
        }

    }
}
