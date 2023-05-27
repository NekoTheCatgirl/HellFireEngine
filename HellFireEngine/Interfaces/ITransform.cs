using Microsoft.Xna.Framework;

namespace HellFireEngine.Interfaces
{
    public interface ITransform
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
    }
}
