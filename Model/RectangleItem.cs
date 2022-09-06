using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SpaceWars.Model
{
    class RectangleItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public ImageBrush Fill { get; set; }
        public double Opacity { get; set; }

        /// <summary>
        /// Move item in (x, y) direction
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public void Move(double x, double y)
        {
            X += x;
            Y += y;
        }

        /// <summary>
        /// Get shape hit box
        /// </summary>
        /// <returns>Rect hitBox</returns>
        public Rect GetHitBox()
        {
            return new Rect(X, Y, Width, Height);
        }
    }
}
