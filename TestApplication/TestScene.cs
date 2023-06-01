using HellFireEngine;
using HellFireEngine.Audio;
using HellFireEngine.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

            Add(gameobjectTest);

            var audioListenerObject = new GameObject(new Vector2(60, 80), 0f, new Vector2(1, 1), SceneManager);
            audioListenerObject.AddComponent<HellFireEngine.Audio.AudioListener>();
            audioListenerObject.AddComponent<VolumeKnob>();
            Add(audioListenerObject);

            var audioSourceObject = new GameObject(new Vector2(0, 0), 0f, new Vector2(1, 1), SceneManager);
            var audioSource = new AudioSource()
            {
                AudioClip = Content.Load<SoundEffect>("Something"),
                IsLooping = true
            };
            audioSourceObject.AddComponent(audioSource);
            audioSourceObject.AddComponent<MoveObject>();
            audioSourceObject.Name = "Audio Source Test";
            Add(audioSourceObject);
        }
    }
}
