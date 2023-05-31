using HellFireEngine.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace HellFireEngine
{
    public abstract class MonoBehaviour : IUpdateable, IDrawable, IDisposable
    {
        public string Name => GameObject.Name;
        protected SpriteBatch SpriteBatch => _gameObject.SpriteBatch;
        
        internal GameObject _gameObject;
        public GameObject GameObject { get { return _gameObject; } }
        public ITransform Transform => _gameObject;

        public virtual void Dispose()
        {

        }

        public static T FindObjectOfType<T>() where T : MonoBehaviour => SceneManager.FindObjectOfType<T>();

        public static List<T> FindObjectsOfType<T>() where T : MonoBehaviour => SceneManager.FindObjectsOfType<T>();
        public T AddComponent<T>() where T : MonoBehaviour, new() => _gameObject.AddComponent<T>();
        public void AddComponent<T>(T component) where T : MonoBehaviour => _gameObject.AddComponent<T>(component);
        public T GetComponent<T>() where T : MonoBehaviour => _gameObject.GetComponent<T>();
        public void RemoveComponent<T>() where T : MonoBehaviour => _gameObject.RemoveComponent<T>();

        private readonly bool _enabled = true;
        public bool Enabled => _enabled;

        private readonly int _updateOrder = 0;
        public int UpdateOrder => _updateOrder;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        private int _drawOrder = 0;
        public int DrawOrder => _drawOrder;

        public void SetDrawOrder(int drawOrder)
        {
            _drawOrder = drawOrder;
            DrawOrderChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool _visible = true;
        public bool Visible => _visible;

        public void SetVisibility(bool vis)
        {
            _visible = vis;
            VisibleChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

    }
}
