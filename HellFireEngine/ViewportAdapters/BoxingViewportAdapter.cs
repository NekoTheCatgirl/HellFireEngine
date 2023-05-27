using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HellFireEngine.ViewportAdapters
{
    public class BoxingViewportAdapter : ScalingViewportAdapter
    {
        private readonly GameWindow Window;
        public BoxingViewportAdapter(GameWindow window, GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight, int horizontalBleed = 0, int verticalBleed = 0) : base(graphicsDevice, virtualWidth, virtualHeight)
        {
            Window = window;
            Window.ClientSizeChanged += OnClientSizeChanged;
            HorizontalBleed = horizontalBleed;
            VerticalBleed = verticalBleed;
        }

        public override void Dispose()
        {
            Window.ClientSizeChanged -= OnClientSizeChanged;
            base.Dispose();
        }

        public int HorizontalBleed { get; }
        public int VerticalBleed { get; }

        public BoxingMode BoxingMode { get; private set; }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            var clientBounds = Window.ClientBounds;

            var worldScaleX = (float)clientBounds.Width / VirtualWidth;
            var worldScaleY = (float)clientBounds.Height / VirtualHeight;

            var safeScaleX = (float)clientBounds.Width / (VirtualWidth - HorizontalBleed);
            var safeScaleY = (float)clientBounds.Height / (VirtualHeight - VerticalBleed);

            var worldScale = MathHelper.Max(worldScaleX, worldScaleY);
            var safeScale = MathHelper.Min(safeScaleX, safeScaleY);
            var scale = MathHelper.Min(worldScale, safeScale);

            var width = (int)(scale * VirtualWidth + 0.5f);
            var height = (int)(scale * VirtualHeight + 0.5f);

            if (height >= clientBounds.Height && width < clientBounds.Width)
            {
                BoxingMode = BoxingMode.Pillarbox;
            }
            else
            {
                if (width >= clientBounds.Height && height <= clientBounds.Height)
                {
                    BoxingMode = BoxingMode.Letterbox;
                }
                else
                {
                    BoxingMode = BoxingMode.None;
                }
            }

            var x = clientBounds.Width / 2 - width / 2;
            var y = clientBounds.Height / 2 - height / 2;
            GraphicsDevice.Viewport = new Viewport(x, y, width, height);
        }

        public override void Reset()
        {
            base.Reset();
            OnClientSizeChanged(this, EventArgs.Empty);
        }

        public override Point PointToScreen(int x, int y)
        {
            var viewport = GraphicsDevice.Viewport;
            return base.PointToScreen(x - viewport.X, y - viewport.Y);
        }
    }

    public enum BoxingMode
    {
        None,
        Letterbox,
        Pillarbox
    }
}
