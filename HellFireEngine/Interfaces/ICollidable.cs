using Microsoft.Xna.Framework;

namespace HellFireEngine.Interfaces
{
    public interface ICollidable
    {
        public Rectangle Bounds { get; }
        public PointF[] Points { get; }
    }
}
