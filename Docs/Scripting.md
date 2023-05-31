# How to create a basic script:
This will teach you how to make a simple script that can be used on any GameObject.

Firstly, we need to make a new script file, lets name this one ExitBehaviour.cs
```cs
using HellFireEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class ExitBehaviour : MonoBehaviour
    {
        public string MyString = "Default text";

        public override void Update(GameTime gameTime)
        {
            if (Input.GetKeyDown(Keys.Escape))
            {
                SceneManager.Stop();
            }
            base.Update(gameTime);
        }
    }
}
```
This should result in a script that when added to any component, will exit the game upon the Escape key being pressed.

# How to use the custom script on a object:
Using a custom script is really easy, for scripts that does not require any extra fields being set before running, you simply can do this in your scenes LoadContent function:
```cs
var gameObject = new GameObject(new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), SceneManager);
gameObject.AddComponent<ExitBehaviour>();

Add(gameObject);
```
And for ones that require some fields being set before adding, you can do this instead:
```cs
var gameObject = new GameObject(new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), SceneManager);
var exitBehaviour = new ExitBehaviour
{
    MyString = "New text"
};
gameObject.AddComponent<ExitBehaviour>(exitBehaviour);

Add(gameObject);
```

# How to make custom renderers:
So all the scripting code essentially can be used as a renderer aswell. And is in fact how the SpriteRenderer and PixelRenderer work.

To use this feature its really simple, you need just to override the Draw call on the script. Do make sure to implement the Dispose function however to ensure that when the script is unloaded, it does not cause any memory issues.

Here is the SpriteRenderer as an example:
```cs
using HellFireEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HellFireEngine
{
    public class SpriteRenderer : MonoBehaviour
    {
        public Texture2D Sprite { get; set; }
        public SpriteEffects Effect { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0f;

        public override void Dispose()
        {
            GC.SupressFinalize(this);
            Sprite?.Dispose();
            GC.ReRegisterForFinalize(this);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Sprite,
                Transform.Position,
                Sprite.Bounds,
                Color.White,
                Transform.Rotation,
                Sprite.Bounds.Center.ToVector2(),
                Transform.Scale,
                Effect,
                LayerDepth);
        }
    }
}
```
This is how the basic SpriteRenderer is setup. And you can do pretty much anything you want to with the draw call, but keep in mind, the SpriteBatch works most effective when drawing the same texture over and over than multiple of them, so if you are drawing more than one sprite in one call, consider using a SpriteAtlas instead.