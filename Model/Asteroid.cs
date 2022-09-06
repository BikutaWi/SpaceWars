using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceWars.Model
{
    class Asteroid : RectangleItem
    {
        public Asteroid(double x, double y)
        {
            X = x;
            Y = y;
            Height = 100;
            Width = 100;
            Opacity = 1.0;

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(@"assets/images/asteroid1.png", UriKind.Relative));
            Fill = brush;
        }
    }
}
