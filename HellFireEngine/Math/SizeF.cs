using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using HellFireEngine.Interfaces;
using Microsoft.Xna.Framework;

namespace HellFireEngine
{
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct SizeF : IEquatable<SizeF>, IEquatableByRef<SizeF>
    {
        public static readonly SizeF Empty = new();

        public float Width;
        public float Height;

        public bool IsEmpty => (Width == 0 && Height == 0);

        public SizeF(float width, float height)
        {
            Width = width; 
            Height = height;
        }

        public bool Equals(SizeF other)
        {
            return Equals(ref other);
        }

        public bool Equals(ref SizeF other)
        {
            return (Width == other.Width && Height == other.Height);
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return obj is SizeF a && Equals(a);
        }

        public static bool operator !=(SizeF first, SizeF second)
        {
            return !(first == second);
        }

        public static bool operator ==(SizeF first, SizeF second)
        {
            return first.Equals(second);
        }

        public static SizeF operator +(SizeF first, SizeF second)
        {
            return Add(first, second);
        }

        public static SizeF Add(SizeF first, SizeF second)
        {
            SizeF size;
            size.Width = first.Width + second.Width;
            size.Height = first.Height + second.Height;
            return size;
        }

        public static SizeF operator -(SizeF first, SizeF second)
        {
            return Subtract(first, second);
        }

        public static SizeF Subtract(SizeF first, SizeF second)
        {
            SizeF size;
            size.Width = first.Width - second.Width;
            size.Height = first.Height - second.Height;
            return size;
        }

        public static SizeF operator /(SizeF size, float value)
        {
            return new SizeF(size.Width / value, size.Height / value);
        }

        public static SizeF operator *(SizeF size, float value)
        {
            return new SizeF(size.Width * value, size.Height * value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Width.GetHashCode() * 397) ^ Height.GetHashCode();
            }
        }

        public static implicit operator SizeF(Point point)
        {
            return new SizeF(point.X, point.Y);
        }

        public static implicit operator PointF(SizeF size)
        {
            return new PointF(size.Width, size.Height);
        }

        public static implicit operator Vector2(SizeF size)
        {
            return new Vector2(size.Width, size.Height);
        }

        public static implicit operator SizeF(Vector2 vector)
        {
            return new SizeF(vector.X, vector.Y);
        }

        public static explicit operator Point(SizeF size)
        {
            return new Point((int)size.Width, (int)size.Height);
        }

        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }

        internal string DebugDisplayString => ToString();
    }
}
