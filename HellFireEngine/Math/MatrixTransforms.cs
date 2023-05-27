using Microsoft.Xna.Framework;

namespace HellFireEngine
{
    public static class MatrixTransforms
    {
        public static Vector2 Transform(this Matrix m, Vector2 vector)
        {
            Transform(m, vector, out Vector2 result);
            return result;
        }

        public static void Transform(this Matrix m, Vector2 vector, out Vector2 result)
        {
            result.X = vector.X * m.M11 + vector.Y * m.M21 + m.M31;
            result.Y = vector.X * m.M12 + vector.Y * m.M22 + m.M32;
        }
    }
}
