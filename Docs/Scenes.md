# How to make a scene:
Making a scene is really simple, you inherit from the scene class and you setup the scene in the `LoadContent` function

## Sample scene:
Here is a sample scene:
```cs
public class MyScene : Scene
{
    public override void LoadContent()
    {
        var myGameObject = new GameObject(new Vector2(50f, 50f), 0f, new Vector2(1f, 1f), SceneManager);

        var spriteRenderer = new SpriteRenderer
        {
            Sprite = Content.Load<Texture2D>("My-cool-sprite")
        };

        myGameObject.AddComponent(spriteRenderer);

        Add(myGameObject);
    }
}
```
This scene will create a simple sprite, and render it at the position 50,50

## Custom draw code:
To create some custom draw code, to say add your own effects, you simply override the `Draw` method

Here is how to do so:
```cs
public override void Draw(GameTime gameTime)
{
    SpriteBatch.Begin();
    GameObjects.ForEach(x => x.Draw(gameTime));
    SpriteBatch.End();
}
```
When overriding the `Draw` method, keep in mind that you should not call `base.Draw(gameTime)` as that could result in some unwanted behavior like drawing 2 times.