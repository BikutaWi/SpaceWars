using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceWars.Model
{
    class Laser : RectangleItem
    {
        private bool isPlayerLaser;

        public bool IsPlayerLaser
        {
            get { return isPlayerLaser; }
            set { isPlayerLaser = value; }
        }

        public Laser(double x, double y, bool isPlayerLaser)
        {
            X = x;
            Y = y;
            Height = 20;
            Width = 5;
            Opacity = 1.0;

            this.isPlayerLaser = isPlayerLaser;

            ImageBrush laserBrush = new ImageBrush();
            
            // change Laser color
            if (this.isPlayerLaser)
                laserBrush.ImageSource = new BitmapImage(new Uri(@"assets/images/PurpleLaser.png", UriKind.Relative));
            else
                laserBrush.ImageSource = new BitmapImage(new Uri(@"assets/images/RedLaser.png", UriKind.Relative));

            Fill = laserBrush;
        }
    }
}
