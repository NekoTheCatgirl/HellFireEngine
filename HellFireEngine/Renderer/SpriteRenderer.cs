using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HellFireEngine.Renderer
{
    public class SpriteRenderer : MonoBehaviour
    {
        public Texture2D Sprite { get; set; }
        public Color Color { get; set; } = Color.White;
        public SpriteEffects Effect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0f;

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            Sprite?.Dispose();
            GC.ReRegisterForFinalize(this);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Sprite is null) return;

            SpriteBatch.Draw(Sprite,
                Transform.Position,
                Sprite.Bounds,
                Color,
                Transform.Rotation, 
                Sprite.Bounds.Center.ToVector2(),
                Transform.Scale, 
                Effect, 
                LayerDepth);
        }
    }
}
