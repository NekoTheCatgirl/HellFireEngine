using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HellFireEngine
{
    public static class PointHelper
    {
        public static Point Minimum(Point first, Point second)
        {
            return new Point(first.X < second.X ? first.X : second.X,
                first.Y < second.Y ? first.Y : second.Y);
        }

        public static Point GetMinimumInPoints(IEnumerable<Point> points)
        {
            Point min = points.ElementAt(0);

            for (int i = 1; i < points.Count(); i++)
            {
                var p = points.ElementAt(i);
                min = Minimum(min, p);
            }

            return min;
        }

        public static Point Maximum(Point first, Point second)
        {
            return new Point(first.X > second.X ? first.X : second.X,
                first.Y > second.Y ? first.Y : second.Y);
        }

        public static Point GetMaximumInPoints(IEnumerable<Point> points)
        {
            Point max = points.ElementAt(0);

            for (int i = 1; i < points.Count(); i++)
            {
                var p = points.ElementAt(i);
                max = Maximum(max, p);
            }

            return max;
        }
    }
}
