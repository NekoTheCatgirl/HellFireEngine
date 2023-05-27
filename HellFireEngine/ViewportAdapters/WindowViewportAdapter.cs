using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HellFireEngine.ViewportAdapters
{
    public class WindowViewportAdapter : ViewportAdapter
    {
        protected readonly GameWindow Window;

        public WindowViewportAdapter(GameWindow window, GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            Window = window;
            Window.ClientSizeChanged += OnClientSizeChanged;
        }

        public override void Dispose()
        {
            Window.ClientSizeChanged -= OnClientSizeChanged;
            base.Dispose();
        }
        public override int VirtualWidth => Window.ClientBounds.Width;

        public override int VirtualHeight => Window.ClientBounds.Height;

        public override int ViewportWidth => Window.ClientBounds.Width;

        public override int ViewportHeight => Window.ClientBounds.Height;

        public override Matrix GetScaleMatrix()
        {
            return Matrix.Identity;
        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            var x = Window.ClientBounds.X;
            var y = Window.ClientBounds.Y;

            GraphicsDevice.Viewport = new Viewport(0, 0, x, y);
        }
    }
}
