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
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            CurrentScene = startScene;
            CurrentScene.SetSceneManager(this);
            Instance = this;
        }

        public SceneManager(Scene startScene, EngineOptions engineOptions) : this(startScene)
        {
            EngineOptions = engineOptions;
        }

        public EngineOptions EngineOptions { get; } = new();

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

        public static void Stop()
        {
            Instance.Exit();
        }

        protected override void Initialize()
        {
            _input = new Input();
            if (EngineOptions.EnableLogger)
                Log.Information("HellFireEngine Initialized, Engine version {Version}", Version.EngineVersion);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (SpriteBatch == null)
                SpriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentScene?.LoadContent();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            CurrentScene?.UnloadContent();
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
    }
}
