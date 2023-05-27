using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using HellFireEngine.Interfaces;
using Microsoft.Xna.Framework;

namespace HellFireEngine
{
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct PointF : IEquatable<PointF>, IEquatableByRef<PointF>
    {
        public static readonly PointF Zero = new();
        public static readonly PointF NaN = new(float.NaN, float.NaN);

        public float X;
        public float Y;

        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(PointF first, PointF second)
        {
            return first.Equals(ref second);
        }

        public bool Equals(PointF other)
        {
            return Equals(ref other);
        }

        public bool Equals(ref PointF other)
        {
            return (other.X == X && other.Y == Y);
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return obj is PointF a && Equals(a);
        }

        public static bool operator !=(PointF first, PointF second)
        {
            return !(first == second);
        }

        public static PointF operator +(PointF point, Vector2 vector)
        {
            return Add(point, vector);
        }

        public static PointF Add(PointF point, Vector2 vector)
        {
            PointF p;
            p.X = point.X + vector.X;
            p.Y = point.Y + vector.Y;
            return p;
        }

        public static PointF operator -(PointF point, Vector2 vector)
        {
            return Subtract(point, vector);
        }

        public static PointF Subtract(PointF point, Vector2 vector)
        {
            PointF p;
            p.X = point.X - vector.X;
            p.Y = point.Y - vector.Y;
            return p;
        }

        public static Vector2 operator -(PointF point1, PointF point2)
        {
            return Displacement(point1, point2);
        }

        public static Vector2 Displacement(PointF point1, PointF point2)
        {
            Vector2 vector;
            vector.X = point2.X - point1.X;
            vector.Y = point2.Y - point1.Y;
            return vector;
        }

        public static PointF operator +(PointF point, SizeF size)
        {
            return Add(point, size);
        }

        public static PointF Add(PointF point, SizeF size)
        {
            return new PointF(point.X + size.Width, point.Y + size.Height);
        }

        public static PointF operator -(PointF point, SizeF size)
        {
            return Subtract(point, size);
        }

        public static PointF Subtract(PointF point, SizeF size)
        {
            return new PointF(point.X - size.Width, point.Y - size.Height);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^Y.GetHashCode();
            }
        }

        public static PointF Minimum(PointF first, PointF second)
        {
            return new PointF(first.X < second.X ? first.X : second.X,
                first.Y < second.Y ? first.Y : second.Y);
        }

        public static PointF Maximum(PointF first, PointF second)
        {
            return new PointF(first.X > second.X ? first.X : second.X,
                first.Y > second.Y ? first.Y : second.Y);
        }

        public static implicit operator Vector2(PointF point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static implicit operator PointF(Vector2 vector)
        {
            return new PointF(vector.X, vector.Y);
        }

        public static implicit operator PointF(Point point)
        {
            return new PointF(point.X, point.Y);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        internal string DebugDisplayString => ToString();
    }
}
