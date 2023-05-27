using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace HellFireEngine
{
    [DataContract]
    [DebuggerDisplay("{ToString(),nq}")]
    public struct Angle : IComparable<Angle>, IEquatable<Angle>
    {
        private const float _tau = (float)(Math.PI * 2.0);
        private const float _tauInv = (float)(0.5 / Math.PI);
        private const float _degreeRadian = (float)(Math.PI / 180.0);
        private const float _radianDegree = (float)(180.0 / Math.PI);
        private const float _gradianRadian = (float)(Math.PI / 200.0);
        private const float _radianGradian = (float)(200.0 / Math.PI);

        [DataMember]
        public float Radians { get; set; }

        public float Degrees
        {
            get => Radians * _radianDegree;
            set => Radians = value * _degreeRadian;
        }

        public float Gradians
        {
            get => Radians * _radianGradian;
            set => Radians = value * _gradianRadian;
        }

        public float Revolutions
        {
            get => Radians * _tauInv;
            set => Radians = value * _tau;
        }

        public Angle(float value, AngleType angleType = AngleType.Radian)
        {
            Radians = angleType switch
            {
                AngleType.Radian => value,
                AngleType.Degree => value * _degreeRadian,
                AngleType.Revolution => value * _tau,
                AngleType.Gradian => value * _gradianRadian,
                _ => 0f,
            };
        }

        public float GetValue(AngleType angleType)
        {
            return angleType switch
            {
                AngleType.Radian => Radians,
                AngleType.Degree => Degrees,
                AngleType.Revolution => Revolutions,
                AngleType.Gradian => Gradians,
                _ => 0f,
            };
        }

        public void Wrap()
        {
            var angle = Radians % _tau;
            if (angle <= Math.PI) angle += _tau;
            if (angle > Math.PI) angle -= _tau;
            Radians = angle;
        }

        public void WrapPosetive()
        {
            Radians %= _tau;
            if (Radians < 0d) Radians += _tau;
        }

        public static Angle FromVector(Vector2 vector)
        {
            return new Angle((float)Math.Atan2(-vector.Y, vector.X));
        }

        public Vector2 ToUnitVector() => ToVector(1);

        public Vector2 ToVector(float length)
        {
            return new Vector2(length * (float)Math.Cos(Radians), -length * (float)Math.Sin(Radians));
        }

        public static bool IsBetween(Angle value, Angle min, Angle end)
        {
            return end < min
                ? (value >= min) || (value <= end)
                : (value >= min) && (value <= end);
        }

        public int CompareTo(Angle other)
        {
            WrapPosetive();
            other.WrapPosetive();
            return Radians.CompareTo(other.Radians);
        }

        public bool Equals(Angle other)
        {
            WrapPosetive();
            other.WrapPosetive();
            return Radians.Equals(other.Radians);
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return obj is Angle a && Equals(a);
        }

        public override int GetHashCode()
        {
            return Radians.GetHashCode();
        }

        public static implicit operator float(Angle angle)
        {
            return angle.Radians;
        }

        public static explicit operator Angle(float angle)
        {
            return new Angle(angle);
        }

        public static Angle operator - (Angle angle)
        {
            return new Angle(-angle.Radians);
        }

        public static bool operator ==(Angle a, Angle b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Angle a, Angle b)
        {
            return !a.Equals(b);
        }

        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left.Radians - right.Radians);
        }

        public static Angle operator *(Angle left, float right)
        {
            return new Angle(left.Radians * right);
        }

        public static Angle operator *(float left, Angle right)
        {
            return new Angle(left * right.Radians);
        }

        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.Radians + right.Radians);
        }

        public override string ToString()
        {
            return $"{Radians} Radians";
        }
    }

    public enum AngleType : byte
    {
        Radian = 0,
        Degree,
        Revolution,
        Gradian
    }
}
