# How to use the Sprite Renderer:
The Sprite Renderer lets you draw a simple texture to the screen, and have it respect position, rotation and scale, along with some effect and depth.

Here is how you will add it to a object in the Scenes LoadContent function:
```cs
var spriteRendererObject = new GameObject(new Vector2(50f, 50f), 0f, new Vector2(1f, 1f), SceneManager);
var spriteRenderer = new SpriteRenderer
{
    Sprite = Content.Load<Texture2D>("My-Sprite")
};

spriteRendererObject.AddComponent<SpriteRenderer>(spriteRenderer);

Add(spriteRendererObject);
```
This will now draw a centered sprite at the position 50, 50 at normal scale.

# Some issues you may run into:
 - "The content is missing" - You need to add the sprite to the games content folder and build the content file. To do so look at [this page](https://docs.monogame.net/articles/getting_started/4_adding_content.html).