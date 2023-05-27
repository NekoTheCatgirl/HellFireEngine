using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace HellFireEngine
{
    public static class Vector2Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(this Vector2 vector1, Vector2 vector2)
        {
            return Vector2.Dot(vector1, vector2);
        }
    }
}
