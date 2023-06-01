using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HellFireEngine.Renderer
{
    public class TextRenderer : MonoBehaviour
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Color Color { get; set; } = Color.White;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public SpriteEffects Effect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0f;

        public override void Draw(GameTime gameTime)
        {
            if (Font is null || string.IsNullOrEmpty(Text)) return;

            SpriteBatch.DrawString(Font,
                Text,
                Transform.Position,
                Color,
                Transform.Rotation,
                Origin,
                Transform.Scale,
                Effect,
                LayerDepth);
        }
    }
}
