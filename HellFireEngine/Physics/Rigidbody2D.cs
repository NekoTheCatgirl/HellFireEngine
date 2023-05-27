using HellFireEngine.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace HellFireEngine.Physics
{
    public class Rigidbody2D
    {
        private Vector2 _velocity;
        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        private readonly ITransform _transform;
        public ITransform Transform => _transform;

        private readonly ICollidable _collider;
        public ICollidable Collider => _collider;

        private bool _enabled = true;
        public bool Enabled => _enabled;

        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;

        private int _updateOrder = 0;
        public int UpdateOrder => _updateOrder;

        public void SetUpdateOrder(int order) => _updateOrder = order;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        private void HandleVelocityChange()
        {

        }

        private void HandleCollisionStep()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (_enabled)
            {

            }
        }
    }
}
