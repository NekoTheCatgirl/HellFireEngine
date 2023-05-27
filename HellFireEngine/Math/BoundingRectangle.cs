using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HellFireEngine.Interfaces;
using Microsoft.Xna.Framework;

namespace HellFireEngine
{

    public struct BoundingRectangle : IEquatable<BoundingRectangle>, IEquatableByRef<BoundingRectangle>
    {
        public static readonly BoundingRectangle Empty = new();

        public PointF Center;
        public Vector2 HalfExtents;

        public BoundingRectangle(PointF center, SizeF halfExtents)
        {
            Center = center;
            HalfExtents = halfExtents;
        }

        public static void CreateFrom(PointF minimum, PointF maximum, out BoundingRectangle result)
        {
            result.Center = new PointF((maximum.X + minimum.X) * 0.5f, (maximum.Y + minimum.Y) * 0.5f);
            result.HalfExtents = new Vector2((maximum.X - minimum.X) * 0.5f, (maximum.Y - minimum.Y) * 0.5f);
        }

        public static BoundingRectangle CreateFrom(PointF minimum, PointF maximum)
        {
            CreateFrom(minimum, maximum, out BoundingRectangle result);
            return result;
        }

        public static void CreateFrom(IReadOnlyList<PointF> points, out BoundingRectangle result)
        {
            PrimitivesHelper.CreateRectangleFromPoints(points, out PointF minimum, out PointF maximum);
            CreateFrom(minimum, maximum, out result);
        }

        public static BoundingRectangle CreateFrom(IReadOnlyList<PointF> points)
        {
            CreateFrom(points, out BoundingRectangle result);
            return result;
        }

        public static void Transform(ref BoundingRectangle boundingRectangle,
            ref Matrix transformMatrix, out BoundingRectangle result)
        {
            PrimitivesHelper.TransformRectangle(ref boundingRectangle.Center, ref boundingRectangle.HalfExtents, ref transformMatrix);
            result.Center = boundingRectangle.Center;
            result.HalfExtents = boundingRectangle.HalfExtents;
        }

        public static BoundingRectangle Transform(BoundingRectangle boundingRectangle,
            ref Matrix transformMatrix)
        {
            Transform(ref boundingRectangle, ref transformMatrix, out BoundingRectangle result);
            return result;
        }

        public static void Union(ref BoundingRectangle first, ref BoundingRectangle second, out BoundingRectangle result)
        {
            var firstMinimum = first.Center - first.HalfExtents;
            var firstMaximum = first.Center + first.HalfExtents;
            var secondMinimum = second.Center - second.HalfExtents;
            var secondMaximum = second.Center + second.HalfExtents;

            var minimum = PointF.Minimum(firstMinimum, secondMinimum);
            var maximum = PointF.Maximum(firstMaximum, secondMaximum);

            result = CreateFrom(minimum, maximum);
        }

        public static BoundingRectangle Union(BoundingRectangle first, BoundingRectangle second)
        {
            Union(ref first, ref second, out BoundingRectangle result);
            return result;
        }

        public BoundingRectangle Union(BoundingRectangle boundingRectangle)
        {
            return Union(this, boundingRectangle);
        }

        public static void Intersection(ref BoundingRectangle first,
            ref BoundingRectangle second, out BoundingRectangle result)
        {
            var firstMinimum = first.Center - first.HalfExtents;
            var firstMaximum = first.Center + first.HalfExtents;
            var secondMinimum = second.Center - second.HalfExtents;
            var secondMaximum = second.Center + second.HalfExtents;

            var minimum = PointF.Maximum(firstMinimum, secondMinimum);
            var maximum = PointF.Minimum(firstMaximum, secondMaximum);

            if ((maximum.X < minimum.X) || (maximum.Y < minimum.Y))
                result = new BoundingRectangle();
            else
                result = CreateFrom(minimum, maximum);
        }

        public static BoundingRectangle Intersection(BoundingRectangle first,
            BoundingRectangle second)
        {
            Intersection(ref first, ref second, out BoundingRectangle result);
            return result;
        }

        public static bool Intersects(ref BoundingRectangle first, ref BoundingRectangle second)
        {
            var distance = first.Center - second.Center;
            var radii = first.HalfExtents + second.HalfExtents;
            return Math.Abs(distance.X) <= radii.X && Math.Abs(distance.Y) <= radii.Y;
        }

        public static bool Intersects(BoundingRectangle first, BoundingRectangle second)
        {
            return Intersects(ref first, ref second);
        }

        public bool Intersects(ref BoundingRectangle boundingRectangle)
        {
            return Intersects(ref this, ref boundingRectangle);
        }

        public bool Intersects(BoundingRectangle boundingRectangle)
        {
            return Intersects(ref this, ref boundingRectangle);
        }

        public void UpdateFromPoints(IReadOnlyList<PointF> points)
        {
            var boundingRectangle = CreateFrom(points);
            Center = boundingRectangle.Center;
            HalfExtents = boundingRectangle.HalfExtents;
        }

        public static bool Contains(ref BoundingRectangle boundingRectangle, ref PointF point)
        {
            var distance = boundingRectangle.Center - point;
            var radii = boundingRectangle.HalfExtents;

            return (Math.Abs(distance.X) <= radii.X) && (Math.Abs(distance.Y) <= radii.Y);
        }

        public static bool Contains(BoundingRectangle boundingRectangle, PointF point)
        {
            return Contains(ref boundingRectangle, ref point);
        }

        public bool Contains(PointF point)
        {
            return Contains(this, point);
        }

        public float SquaredDistanceTo(PointF point)
        {
            return PrimitivesHelper.SquaredDistanceToPointFromRectangle(Center - HalfExtents, Center + HalfExtents, point);
        }

        public PointF ClosestPointTo(PointF point)
        {
            PrimitivesHelper.ClosestPointToPointFromRectangle(Center - HalfExtents, Center + HalfExtents, point, out PointF result);
            return result;
        }

        public static bool operator ==(BoundingRectangle first, BoundingRectangle second)
        {
            return first.Equals(ref second);
        }

        public static bool operator !=(BoundingRectangle first, BoundingRectangle second)
        {
            return !(first == second);
        }

        public bool Equals(BoundingRectangle boundingRectangle)
        {
            return Equals(ref boundingRectangle);
        }

        public bool Equals(ref BoundingRectangle boundingRectangle)
        {
            return (boundingRectangle.Center == Center) && (boundingRectangle.HalfExtents == HalfExtents);
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return obj is BoundingRectangle a && Equals(a);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Center.GetHashCode() * 397) ^ HalfExtents.GetHashCode();
            }
        }

        public static implicit operator BoundingRectangle(Rectangle rectangle)
        {
            var radii = new SizeF(rectangle.Width * 0.5f, rectangle.Height * 0.5f);
            var centre = new PointF(rectangle.X + radii.Width, rectangle.Y + radii.Height);
            return new BoundingRectangle(centre, radii);
        }

        public static explicit operator Rectangle(BoundingRectangle boundingRectangle)
        {
            var minimum = boundingRectangle.Center - boundingRectangle.HalfExtents;
            return new Rectangle((int)minimum.X, (int)minimum.Y, (int)boundingRectangle.HalfExtents.X * 2,
                (int)boundingRectangle.HalfExtents.Y * 2);
        }

        public static implicit operator BoundingRectangle(RectangleF rectangle)
        {
            var radii = new SizeF(rectangle.Width * 0.5f, rectangle.Height * 0.5f);
            var centre = new PointF(rectangle.X + radii.Width, rectangle.Y + radii.Height);
            return new BoundingRectangle(centre, radii);
        }

        public static implicit operator RectangleF(BoundingRectangle boundingRectangle)
        {
            var minimum = boundingRectangle.Center - boundingRectangle.HalfExtents;
            return new RectangleF(minimum.X, minimum.Y, boundingRectangle.HalfExtents.X * 2,
                boundingRectangle.HalfExtents.Y * 2);
        }

        public override string ToString()
        {
            return $"Centre: {Center}, Radii: {HalfExtents}";
        }

        internal string DebugDisplayString => ToString();
    }
}
