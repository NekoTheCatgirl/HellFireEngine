using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HellFireEngine.ViewportAdapters
{
    public class DefaultViewportAdapter : ViewportAdapter
    {
        public DefaultViewportAdapter(GraphicsDevice graphicsDevice) : base(graphicsDevice) { }

        public override int VirtualWidth => GraphicsDevice.Viewport.Width;

        public override int VirtualHeight => GraphicsDevice.Viewport.Height;

        public override int ViewportWidth => GraphicsDevice.Viewport.Width;

        public override int ViewportHeight => GraphicsDevice.Viewport.Height;

        public override Matrix GetScaleMatrix()
        {
            return Matrix.Identity;
        }
    }
}
