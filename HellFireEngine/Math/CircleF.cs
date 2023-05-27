using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using HellFireEngine.Interfaces;
using Microsoft.Xna.Framework;

namespace HellFireEngine
{
    [DataContract]
    public struct CircleF : IEquatable<CircleF>, IEquatableByRef<CircleF>, IShapeF
    {
        [DataMember] public PointF Center;
        [DataMember] public float Radius;

        public PointF Position
        {
            get => Center;
            set => Center = value;
        }

        public float Diameter => 2 * Radius;
        public float Circumference => 2 * MathHelper.Pi * Radius;

        public CircleF(PointF center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public static void CreateFrom(PointF minimum, PointF maximum, out CircleF result)
        {
            result.Center = new PointF((maximum.X + minimum.X) * 0.5f, (maximum.Y + minimum.Y) * 0.5f);
            var distanceVector = maximum - minimum;
            result.Radius = distanceVector.X > distanceVector.Y ? distanceVector.X * 0.5f : distanceVector.Y * 0.5f;
        }

        public static CircleF CreateFrom(PointF minimum, PointF maximum)
        {
            CreateFrom(minimum, maximum, out CircleF result);
            return result;
        }

        public static void CreateFrom(IReadOnlyList<PointF> points, out CircleF result)
        {
            // Real-Time Collision Detection, Christer Ericson, 2005. Chapter 4.3; Bounding Volumes - Spheres. pg 89-90

            if (points == null || points.Count == 0)
            {
                result = default(CircleF);
                return;
            }

            var minimum = new PointF(float.MaxValue, float.MaxValue);
            var maximum = new PointF(float.MinValue, float.MinValue);

            for (var index = points.Count - 1; index >= 0; --index)
            {
                var point = points[index];
                minimum = PointF.Minimum(minimum, point);
                maximum = PointF.Maximum(maximum, point);
            }

            CreateFrom(minimum, maximum, out result);
        }

        public static CircleF CreateFrom(IReadOnlyList<PointF> points)
        {
            CreateFrom(points, out CircleF result);
            return result;
        }

        public static bool Intersects(ref CircleF first, ref CircleF second)
        {
            var distanceVector = first.Center - second.Center;
            var distanceSquared = distanceVector.Dot(distanceVector);
            var radiusSum = first.Radius + second.Radius;
            return distanceSquared <= radiusSum * radiusSum;
        }

        public static bool Intersects(CircleF first, CircleF second)
        {
            return Intersects(ref first, ref second);
        }

        public bool Intersects(ref CircleF circle)
        {
            return Intersects(ref this, ref circle);
        }

        public bool Intersects(CircleF circle)
        {
            return Intersects(ref this, ref circle);
        }

        public static bool Intersects(ref CircleF circle, ref BoundingRectangle rectangle)
        {
            var distanceSquared = rectangle.SquaredDistanceTo(circle.Center);
            return distanceSquared <= circle.Radius * circle.Radius;
        }

        public static bool Intersects(CircleF circle, BoundingRectangle rectangle)
        {
            return Intersects(ref circle, ref rectangle);
        }

        public bool Intersects(ref BoundingRectangle rectangle)
        {
            return Intersects(ref this, ref rectangle);
        }

        public bool Intersects(BoundingRectangle rectangle)
        {
            return Intersects(ref this, ref rectangle);
        }

        public static bool Contains(ref CircleF circle, PointF point)
        {
            var dx = circle.Center.X - point.X;
            var dy = circle.Center.Y - point.Y;
            var d2 = dx * dx + dy * dy;
            var r2 = circle.Radius * circle.Radius;
            return d2 <= r2;
        }
        
        public static bool Contains(CircleF circle, PointF point)
        {
            return Contains(ref circle, point);
        }

        public bool Contains(PointF point)
        {
            return Contains(ref this, point);
        }

        public PointF ClosestPointTo(PointF point)
        {
            var distanceVector = point - Center;
            var lengthSquared = distanceVector.Dot(distanceVector);
            if (lengthSquared <= Radius * Radius)
                return point;
            distanceVector.Normalize();
            return Center + Radius * distanceVector;
        }

        public PointF BoundaryPointAt(float angle)
        {
            var direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            return Center + Radius * direction;
        }

        public static bool operator ==(CircleF first, CircleF second)
        {
            return first.Equals(ref second);
        }

        public static bool operator !=(CircleF first, CircleF second)
        {
            return !(first == second);
        }

        public bool Equals(CircleF circle)
        {
            return Equals(ref circle);
        }

        public bool Equals(ref CircleF circle)
        {
            return circle.Center == Center && circle.Radius == Radius;
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return obj is CircleF a && Equals(a);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Center.GetHashCode() * 397) ^ Radius.GetHashCode();
            }
        }

        public static implicit operator Rectangle(CircleF circle)
        {
            var diameter = (int)circle.Diameter;
            return new Rectangle((int)(circle.Center.X - circle.Radius), (int)(circle.Center.Y - circle.Radius),
                diameter, diameter);
        }

        public Rectangle ToRectangle()
        {
            return this;
        }

        public static implicit operator CircleF(Rectangle rectangle)
        {
            var halfWidth = rectangle.Width / 2;
            var halfHeight = rectangle.Height / 2;
            return new CircleF(new PointF(rectangle.X + halfWidth, rectangle.Y + halfHeight),
                halfWidth > halfHeight ? halfWidth : halfHeight);
        }

        public static implicit operator RectangleF(CircleF circle)
        {
            var diameter = circle.Diameter;
            return new RectangleF(circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, diameter, diameter);
        }

        public RectangleF ToRectangleF()
        {
            return this;
        }

        public static implicit operator CircleF(RectangleF rectangle)
        {
            var halfWidth = rectangle.Width * 0.5f;
            var halfHeight = rectangle.Height * 0.5f;
            return new CircleF(new PointF(rectangle.X + halfWidth, rectangle.Y + halfHeight),
                halfWidth > halfHeight ? halfWidth : halfHeight);
        }

        public override string ToString()
        {
            return $"Centre: {Center}, Radius: {Radius}";
        }

        internal string DebugDisplayString => ToString();
    }
}
