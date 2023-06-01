using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace HellFireEngine
{
    public class SceneManager : Game
    {
        public delegate void SceneChangedDelegate(Scene previousScene, Scene newScene);

        private static SceneManager Instance { get; set; }

        public SceneManager(Scene startScene)
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            if (EngineOptions.ForceFullscreen)
            {
                GraphicsDeviceManager.IsFullScreen = true;

                GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
                GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            }
            else
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = EngineOptions.ScreenWidth;
                GraphicsDeviceManager.PreferredBackBufferHeight = EngineOptions.ScreenHeight;
            }
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            CurrentScene = startScene;
            CurrentScene.SetSceneManager(this);
            Instance = this;
        }

        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        public SpriteBatch SpriteBatch = null;

        public Scene CurrentScene;

        private Input _input;

        public static void LoadScene(Scene scene)
        {
            Instance.Local_LoadScene(scene);
        }

        public static event SceneChangedDelegate OnSceneChanged;

        internal void Local_LoadScene(Scene scene)
        {
            if (EngineOptions.EnableLogger) 
                Log.Information("Unloading content for old scene");
            UnloadContent();
            var oldScene = CurrentScene;
            scene.SetSceneManager(this);
            CurrentScene = scene;
            OnSceneChanged?.Invoke(oldScene, CurrentScene);
            if (EngineOptions.EnableLogger) 
                Log.Information("Loading content for new scene");
            LoadContent();
        }

        public static T FindObjectOfType<T>() where T : MonoBehaviour
        {
            foreach (var x in Instance.CurrentScene.GameObjects)
            {
                if (x.GetComponent<T>() != null)
                {
                    return x.GetComponent<T>();
                }
            }
            return null;
        }

        public static List<T> FindObjectsOfType<T>() where T : MonoBehaviour
        {
            var objects = new List<T>();
            Instance.CurrentScene.GameObjects.ForEach(x =>
            {
                if (x.GetComponent<T>() != null)
                {
                    objects.Add(x.GetComponent<T>());
                }
            });
            return objects;
        }

        public static void Stop()
        {
            Instance.Exit();
        }

        #region Boilerplate code...
        protected override void Initialize()
        {
            _input = new Input();
            if (EngineOptions.EnableLogger)
                Log.Information("HellFireEngine Initialized, Engine version {Version}", Version.EngineVersion);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch ??= new SpriteBatch(GraphicsDevice);
            CurrentScene?.LoadContent();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            CurrentScene?.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update(gameTime);
            CurrentScene?.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            CurrentScene?.Draw(gameTime);
            base.Draw(gameTime);
        }
        #endregion
    }
}
