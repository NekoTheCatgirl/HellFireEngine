using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using HellFireEngine.Interfaces;
using HellFireEngine.Physics;
using Microsoft.Xna.Framework.Graphics;

namespace HellFireEngine
{
    public class GameObject : DrawableGameComponent, ITransform
    {
        public SpriteBatch SpriteBatch;

        public GameObject(Vector2 position, float rotation, Vector2 scale, SceneManager game) : base(game)
        {
            SpriteBatch = game.SpriteBatch;

            _position = position;
            _rotation = rotation;
            _scale = scale;
        }

        private readonly List<MonoBehaviour> _components = new();

        private int MinDrawOrder = 0;
        private int MaxDrawOrder = 0;
        private int MinUpdateOrder = 0;
        private int MaxUpdateOrder = 0;

        public T AddComponent<T>() where T : MonoBehaviour, new()
        {
            var component = new T
            {
                _gameObject = this
            };

            _components.Add(component);

            return component;
        }

        public void AddComponent<T>(T component) where T : MonoBehaviour
        {
            component._gameObject = this;

            _components.Add(component);
        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            var distinct = _components
                .Where(x => typeof(T).IsAssignableFrom(x.GetType()))
                .FirstOrDefault() as T;

            return distinct;
        }

        public void RemoveComponent<T>() where T : MonoBehaviour
        {
            var component = GetComponent<T>();
            _components.Remove(component);
        }

        public void UpdateOrders()
        {
            (MinUpdateOrder, MaxUpdateOrder) = OrderManager.GetUpdateOrder(_components);
            (MinDrawOrder, MaxDrawOrder) = OrderManager.GetDrawOrder(_components);
        }

        private Vector2 _position;
        public Vector2 Position { get => _position; set => _position = value; } 

        private float _rotation;
        public float Rotation { get => _rotation; set => _rotation = value; }

        private Vector2 _scale;
        public Vector2 Scale { get => _scale; set => _scale = value; }

        public override void Update(GameTime gameTime)
        {
            for (int i = MinUpdateOrder; i <= MaxUpdateOrder; i++)
            {
                var updatables = _components.Where(x => x.UpdateOrder == i).ToList();
                foreach (var u in updatables)
                {
                    u.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = MinDrawOrder; i <= MaxDrawOrder; i++)
            {
                var drawables = _components.Where(x => x.DrawOrder == i).ToList();
                foreach (var d in drawables)
                {
                    d.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }
    }
}
