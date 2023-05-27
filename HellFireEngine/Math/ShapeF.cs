namespace HellFireEngine
{
    public interface IShapeF
    {
        PointF Position { get; set; }
    }

    public static class Shape
    {
        public static bool Intersects(this IShapeF shapeA, IShapeF shapeB)
        {
            var intersects = false;

            if (shapeA is RectangleF rectangleA && shapeB is RectangleF rectangleB)
            {
                intersects = rectangleA.Intersects(rectangleB);
            }
            else if (shapeA is CircleF circleA && shapeB is CircleF circleB)
            {
                intersects = circleA.Intersects(circleB);
            }
            else if (shapeA is RectangleF rect1 && shapeB is CircleF circ1)
            {
                return Intersects(circ1, rect1);
            }
            else if (shapeA is CircleF circ2 && shapeB is RectangleF rect2)
            {
                return Intersects(circ2, rect2);
            }

            return intersects;
        }

        public static bool Intersects(CircleF circle, RectangleF rectangle)
        {
            var closestPoint = rectangle.ClosestPointTo(circle.Center);
            return circle.Contains(closestPoint);
        }
    }
}
