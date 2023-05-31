using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HellFireEngine
{
    public abstract class Scene : IDisposable
    {
        protected SceneManager SceneManager;
        protected SpriteBatch SpriteBatch => SceneManager.SpriteBatch;
        protected ContentManager Content => SceneManager.Content;

        public List<GameObject> GameObjects = new();

        /// <summary>
        /// This add method only adds in a unsafe manor if used outside of the <see cref="Scene.LoadContent"/> method
        /// </summary>
        /// <param name="gameObject">The gameobject to add to the scene</param>
        protected void Add(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }

        internal void SetSceneManager(SceneManager manager) => SceneManager = manager;

        public abstract void LoadContent();

        public virtual void UnloadContent()
        {
            GameObjects.ForEach(x => x.Dispose());
        }

        public virtual void Update(GameTime gameTime)
        {
            GameObjects.ForEach(x => x.Update(gameTime));
        }

        public virtual void Draw(GameTime gameTime) 
        {
            if (OrthographicCamera.MainCamera is not null)
            {
                var transformMatrix = OrthographicCamera.MainCamera.GetViewMatrix();
                SpriteBatch.Begin(transformMatrix: transformMatrix);
            }
            else
            {
                SpriteBatch.Begin();
            }
            GameObjects.ForEach(x => x.Draw(gameTime));
            SpriteBatch.End();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            UnloadContent();
            GC.ReRegisterForFinalize(this);
        }
    }
}
