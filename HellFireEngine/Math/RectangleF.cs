using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using HellFireEngine.Interfaces;
using Microsoft.Xna.Framework;

namespace HellFireEngine
{
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct RectangleF : IEquatable<RectangleF>, IEquatableByRef<RectangleF>, IShapeF
    {
        public static readonly RectangleF Empty = new();

        [DataMember] public float X;
        [DataMember] public float Y;
        [DataMember] public float Width;
        [DataMember] public float Height;

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public bool IsEmpty => Width.Equals(0) && Height.Equals(0) && X.Equals(0) && Y.Equals(0);

        public PointF Position
        {
            get => new(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        
        public SizeF Size
        {
            get => new(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public PointF Center => new PointF(X + Width / 2, Y + Height / 2);

        public PointF TopLeft => new(X, Y);
        public PointF TopRight => new(X + Width, Y);
        public PointF BottomLeft => new(X, Y + Height);
        public PointF BottomRight => new(X + Width, Y + Height);

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(PointF position, SizeF size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public static void CreateFrom(PointF minimum, PointF maximum, out RectangleF result)
        {
            result.X = minimum.X;
            result.Y = minimum.Y;
            result.Width = maximum.X - minimum.X;
            result.Height = maximum.Y - minimum.Y;
        }

        public static RectangleF CreateFrom(PointF minimum, PointF maximum)
        {
            CreateFrom(minimum, maximum, out RectangleF result);
            return result;
        }

        public static void CreateFrom(IReadOnlyList<PointF> points, out RectangleF result)
        {
            PrimitivesHelper.CreateRectangleFromPoints(points, out PointF minimum, out PointF maximum);
            CreateFrom(minimum, maximum, out result);
        }

        public static RectangleF CreateFrom(IReadOnlyList<PointF> points)
        {
            CreateFrom(points, out RectangleF result);
            return result;
        }

        public static void Transform(ref RectangleF rect, ref Matrix transformMatrix, out RectangleF result)
        {
            var center = rect.Center;
            var halfExtents = (Vector2)rect.Size * 0.5f;

            PrimitivesHelper.TransformRectangle(ref center, ref halfExtents, ref transformMatrix);

            result.X = center.X - halfExtents.X;
            result.Y = center.Y - halfExtents.Y;
            result.Width = halfExtents.X * 2;
            result.Height = halfExtents.Y * 2;
        }

        public static RectangleF Transform(RectangleF rect, ref Matrix transformMatrix)
        {
            Transform(ref rect, ref transformMatrix, out RectangleF result);
            return result;
        }

        public static void Union(ref RectangleF rect1, ref RectangleF rect2, out RectangleF result)
        {
            result.X = Math.Min(rect1.X, rect2.X);
            result.Y = Math.Min(rect1.Y, rect2.Y);
            result.Width = Math.Max(rect1.Right, rect2.Right) - result.X;
            result.Height = Math.Max(rect1.Bottom, rect2.Bottom) - result.Y;
        }

        public static RectangleF Union(RectangleF rect1, RectangleF rect2)
        {
            Union(ref rect1, ref rect2, out RectangleF result);
            return result;
        }

        public static void Intersection(ref RectangleF rect1, ref RectangleF rect2, out RectangleF result)
        {
            var firstMinimum = rect1.TopLeft;
            var firstMaximum = rect1.BottomRight;
            var secondMinimum = rect2.TopLeft;
            var secondMaximum = rect2.BottomRight;

            var minimum = PointF.Maximum(firstMinimum, secondMinimum);
            var maximum = PointF.Minimum(firstMaximum, secondMaximum);

            if ((maximum.X < minimum.X) || (maximum.Y < minimum.Y))
                result = Empty;
            else
                result = CreateFrom(minimum, maximum);
        }

        public static RectangleF Intersection(RectangleF rect1, RectangleF rect2)
        {
            Intersection(ref rect1, ref rect2, out RectangleF result);
            return result;
        }

        public RectangleF Intersection(RectangleF rect)
        {
            Intersection(ref this, ref rect, out RectangleF result);
            return result;
        }

        public static bool Intersects(ref RectangleF rect1, ref RectangleF rect2)
        {
            return rect1.X < rect2.X + rect2.Width && rect1.X + rect1.Width > rect2.X &&
                rect1.Y < rect2.Y + rect2.Height && rect1.Y + rect1.Height > rect2.Y;
        }

        public static bool Intersects(RectangleF rect1, RectangleF rect2)
        {
            return Intersects(ref rect1, ref rect2);
        }

        public bool Intersects(RectangleF rect)
        {
            return Intersects(ref this, ref rect);
        }

        public static bool Contains(ref RectangleF rect, ref PointF point)
        {
            return rect.X <= point.X && point.X < rect.X + rect.Width & rect.Y <= point.Y && point.Y < rect.Y + rect.Height;
        }

        public static bool Contains(RectangleF rect, PointF point)
        {
            return Contains(ref rect, ref point);
        }

        public bool Contains(PointF point)
        {
            return Contains(ref this, ref point);
        }

        public void UpdateFromPoints(IReadOnlyList<PointF> points)
        {
            var rectangle = CreateFrom(points);
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

        public float SquaredDistanceTo(PointF point)
        {
            return PrimitivesHelper.SquaredDistanceToPointFromRectangle(TopLeft, BottomRight, point);
        }

        public float DistanceTo(PointF point)
        {
            return (float)Math.Sqrt(SquaredDistanceTo(point));
        }

        public PointF ClosestPointTo(PointF point)
        {
            PrimitivesHelper.ClosestPointToPointFromRectangle(TopLeft, BottomRight, point, out PointF result);
            return result;
        }

        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2;
            Height += verticalAmount * 2;
        }

        public void Offset(float offsetX, float offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        public void Offset(Vector2 amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        public static bool operator ==(RectangleF first, RectangleF second)
        {
            return first.Equals(ref second);
        }

        public static bool operator !=(RectangleF first, RectangleF second)
        {
            return !(first == second);
        }

        public bool Equals(RectangleF rectangle)
        {
            return Equals(ref rectangle);
        }

        public bool Equals(ref RectangleF rectangle)
        {
            return X == rectangle.X && Y == rectangle.Y && Width == rectangle.Width && Height == rectangle.Height;
        }
        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return obj is RectangleF a && Equals(a);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }

        public static implicit operator RectangleF(Rectangle rectangle)
        {
            return new RectangleF
            {
                X = rectangle.X,
                Y = rectangle.Y,
                Width = rectangle.Width,
                Height = rectangle.Height
            };
        }

        public static explicit operator Rectangle(RectangleF rectangle)
        {
            return new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Width: {Width}, Height: {Height}";
        }

        internal string DebugDisplayString => string.Concat(X, "  ", Y, "  ", Width, "  ", Height);
    }
}
