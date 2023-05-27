using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HellFireEngine
{
    public abstract class Scene
    {
        protected SceneManager SceneManager;
        protected SpriteBatch SpriteBatch => SceneManager.SpriteBatch;
        protected ContentManager Content => SceneManager.Content;

        protected List<GameObject> GameObjects = new();


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
            if (OrthographicCamera.MainCamera != null)
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
    }
}
