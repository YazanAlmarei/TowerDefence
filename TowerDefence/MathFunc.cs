using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class MathFunc
    {
        // this is a helper method copied from my MathFunc class
        //static public so it can be used anywhere
        //both of these vector2s represent locations
        //it gets the angle of somthing at PointFrom pointing towards PointTo
        public static float VectorPointAngle(Vector2 PointFrom, Vector2 PointTo)
        {
            return MathF.Atan2(PointTo.Y - PointFrom.Y, PointTo.X - PointFrom.X);
        }

        //another function from my MathFunc class
        //Rectangles in monogame has the location represent top left
        //this just makes a rectangle from the center
        public static Rectangle RectangleFromCenterSize(Point center, Point size)
        {
            return new Rectangle(center - new Point(size.X / 2, size.Y / 2), size);
        }
    }
}
