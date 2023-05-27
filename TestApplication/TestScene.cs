using HellFireEngine;
using HellFireEngine.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestApplication
{
    public class TestScene : Scene
    {        
        public override void LoadContent()
        {
            var gameobjectTest = new GameObject(new Vector2(50, 50), 0f, new Vector2(0.4f, 0.4f), SceneManager);
            var spriteRenderer = new SpriteRenderer
            {
                Sprite = Content.Load<Texture2D>("Button-BlueDeep-02")
            };
            gameobjectTest.AddComponent(spriteRenderer);

            var testObject = new TestBehaviour()
            {
                FunnyText = "Test text"
            };
            gameobjectTest.AddComponent(testObject);

            GameObjects.Add(gameobjectTest);
        }
    }
}
