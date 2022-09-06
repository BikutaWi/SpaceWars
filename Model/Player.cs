using System;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceWars.Model
{
    class Player : RectangleItem
    {
        public Player(double x, double y)
        {
            X = x;
            Y = y;
            Height = 64;
            Width = 76;
            Opacity = 1.0;

            ImageBrush playerXwing = new ImageBrush();
            playerXwing.ImageSource = new BitmapImage(new Uri(@"assets/images/XWing.png", UriKind.Relative));
            Fill = playerXwing;
        }

        /// <summary>
        /// Create new Laser
        /// </summary>
        /// <returns>Laser</returns>
        public Laser Shoot()
        {
            double x = this.X + (this.Width / 2);
            double y = this.Y - (this.Height / 2);

            // play laser sound
            SoundPlayer player = new SoundPlayer(@"assets\sounds\xwing-laser.wav");
            player.Load();
            player.Play();

            return new Laser(x, y, true);
        }
    }
}
