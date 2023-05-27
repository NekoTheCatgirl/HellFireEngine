using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HellFireEngine.ViewportAdapters;
using Serilog;

namespace HellFireEngine
{
    public class OrthographicCamera : Camera<Vector2>
    {
        public static OrthographicCamera MainCamera { get; private set; }

        private readonly ViewportAdapter Adapter;
        private float _maximumZoom = float.MaxValue;
        private float _minimumZoom;
        private float _zoom;

        public OrthographicCamera(GraphicsDevice graphicsDevice) : this(new DefaultViewportAdapter(graphicsDevice)) { }

        public OrthographicCamera(ViewportAdapter adapter)
        {
            Adapter = adapter;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(Adapter.VirtualWidth / 2f, Adapter.VirtualHeight / 2f);
            Position = Vector2.Zero;

            MainCamera = this;
        }

        public override Vector2 Position { get; set; }
        public override float Rotation { get; set; }
        public override Vector2 Origin { get; set; }
        public override Vector2 Center => Position + Origin;
        
        public override float Zoom
        {
            get => _zoom;
            set
            {
                if (value < MinimumZoom || value > MaximumZoom)
                {
                    Log.Error("Zoom must be between MinimumZoom and MaximumZoom");
                    return;
                }

                _zoom = value;
            }
        }

        public override float MinimumZoom
        {
            get => _minimumZoom;
            set
            {
                if (value < 0)
                {
                    Log.Error("MinimumZoom must be greater than zero");
                    return;
                }

                _minimumZoom = value;

                if (Zoom < value)
                {
                    Zoom = value;
                }
            }
        }

        public override float MaximumZoom
        {
            get => _maximumZoom;
            set
            {
                if (value < 0)
                {
                    Log.Error("MaximumZoom must be greater than zero");
                    return;
                }

                if (Zoom > value)
                {
                    Zoom = value;
                }

                _maximumZoom = value;
            }
        }

        public override RectangleF BoundingRectangle
        {
            get
            {
                var frustum = GetBoundingFrustum();
                var corners = frustum.GetCorners();
                var topLeft = corners[0];
                var bottomRight = corners[1];
                var width = bottomRight.X - topLeft.X;
                var height = bottomRight.Y - topLeft.Y;
                return new RectangleF(topLeft.X, topLeft.Y, width, height);
            }
        }

        public override ContainmentType Contains(Vector2 vector)
        {
            return GetBoundingFrustum().Contains(new Vector3(vector.X, vector.Y, 0));
        }

        public override ContainmentType Contains(Point point)
        {
            return Contains(point.ToVector2());
        }

        public override ContainmentType Contains(Rectangle rectangle)
        {
            var max = new Vector3(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height, 0.5f);
            var min = new Vector3(rectangle.X, rectangle.Y, 0.5f);
            var boundingBox = new BoundingBox(min, max);
            return GetBoundingFrustum().Contains(boundingBox);
        }

        public override BoundingFrustum GetBoundingFrustum()
        {
            var viewMatrix = GetVirtualViewMatrix();
            var projectionMatrix = GetProjectionMatrix(viewMatrix);
            return new BoundingFrustum(projectionMatrix);
        }

        private Matrix GetProjectionMatrix(Matrix viewMatrix)
        {
            var projection = Matrix.CreateOrthographicOffCenter(0, Adapter.VirtualWidth, Adapter.VirtualHeight, 0, -1, 0);
            Matrix.Multiply(ref viewMatrix, ref projection, out projection);
            return projection;
        }

        public override Matrix GetInverseViewMatrix()
        {
            return Matrix.Invert(GetViewMatrix());
        }

        private Matrix GetVirtualViewMatrix(Vector2 parallaxFactor)
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position * parallaxFactor, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        private Matrix GetVirtualViewMatrix()
        {
            return GetVirtualViewMatrix(Vector2.One);
        }

        public Matrix GetViewMatrix(Vector2 parallaxFactor)
        {
            return GetVirtualViewMatrix(parallaxFactor) * Adapter.GetScaleMatrix();
        }

        public override Matrix GetViewMatrix()
        {
            return GetViewMatrix(Vector2.One);
        }

        public override void LookAt(Vector2 position)
        {
            Position = position - new Vector2(Adapter.VirtualWidth / 2, Adapter.VirtualHeight / 2);
        }

        public override void Move(Vector2 direction)
        {
            Position += Vector2.Transform(direction, Matrix.CreateRotationZ(-Rotation));
        }

        public override void Rotate(float deltaRadians)
        {
            Rotation += deltaRadians;
        }

        public Vector2 ScreenToWorld(float x, float y)
        {
            return ScreenToWorld(new Vector2(x, y));
        }

        public override Vector2 ScreenToWorld(Vector2 position)
        {
            var viewport = Adapter.Viewport;
            return Vector2.Transform(position - new Vector2(viewport.X, viewport.Y), GetInverseViewMatrix());
        }

        public Vector2 WorldToScreen(float x, float y)
        {
            return WorldToScreen(new Vector2(x, y));
        }

        public override Vector2 WorldToScreen(Vector2 position)
        {
            var viewport = Adapter.Viewport;
            return Vector2.Transform(position + new Vector2(viewport.X, viewport.Y), GetViewMatrix());
        }

        private void ClampZoom(float value)
        {
            if (value < MinimumZoom)
                Zoom = MinimumZoom;
            else
                Zoom = value > MaximumZoom ? MaximumZoom : value;
        }

        public override void ZoomIn(float deltaZoom)
        {
            ClampZoom(Zoom + deltaZoom);
        }

        public override void ZoomOut(float deltaZoom)
        {
            ClampZoom(Zoom - deltaZoom);
        }
    }
}
