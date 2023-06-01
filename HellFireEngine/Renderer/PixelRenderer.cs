using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HellFireEngine.Renderer
{
    /// <summary>
    /// While this is an option, its highly unoptimized for actual games to use, as it will have to render every pixel every frame
    /// </summary>
    public class PixelRenderer : MonoBehaviour
    {
        private readonly Dictionary<Point, Color> Pixels = new();
        private readonly Texture2D PixelTexture;

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            PixelTexture?.Dispose();
            GC.ReRegisterForFinalize(this);
        }

        public Color GetPixel(Point point)
        {
            if (Pixels.TryGetValue(point, out var color)) 
                return color;
            return Color.Transparent;
        }

        public void SetPixel(Point point, Color color)
        {
            if (Pixels.ContainsKey(point)) 
                Pixels[point] = color;
            else 
                Pixels.Add(point, color);
        }

        public PixelRenderer()
        {
            PixelTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
            PixelTexture.SetData(new[] { Color.White });
        }

        public PixelRenderer(Dictionary<Point, Color> pixels) : this()
        {
            Pixels = pixels;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var pair in Pixels)
            {
                Vector2 point = new Vector2(pair.Key.X, pair.Key.Y) + Transform.Position;
                SpriteBatch.Draw(PixelTexture, point, pair.Value);
            }
        }
    }
}
