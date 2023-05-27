using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HellFireEngine.Renderer
{
    public class SpriteRenderer : MonoBehaviour
    {
        public Texture2D Sprite { get; set; }
        public SpriteEffects Effect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0f;

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Sprite,
                Transform.Position,
                Sprite.Bounds,
                Color.White,
                Transform.Rotation, 
                Sprite.Bounds.Center.ToVector2(),
                Transform.Scale, 
                Effect, 
                LayerDepth);
        }
    }
}
