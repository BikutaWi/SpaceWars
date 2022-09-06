using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceWars.Model
{
    class Enemy : RectangleItem
    {
        public Enemy(double x, double y)
        {
            X = x;
            Y = y;
            Height = 60;
            Width = 50;
            Opacity = 1.0;

            Random rand = new Random();
            ImageBrush brush = new ImageBrush();

            // choose one of enemy asset randomly
            if (rand.Next(1, 3) == 1)
                brush.ImageSource = new BitmapImage(new Uri(@"assets/images/TieInterceptor.png", UriKind.Relative));  
            else
                brush.ImageSource = new BitmapImage(new Uri(@"assets/images/TieFighter.png", UriKind.Relative));

            Fill = brush;
        }

        /// <summary>
        /// Create new laser
        /// </summary>
        /// <returns>Laser</returns>
        public Laser Shoot()
        {
            double x = this.X + (this.Width / 2);
            double y = this.Y + this.Height;

            return new Laser(x, y, false);
        }
    }
}
