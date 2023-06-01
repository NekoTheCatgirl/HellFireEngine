# How to use the Text Renderer:
The text renderer will let you render some text to the screen, using any font you desire.

Here is how you will add it to a object in the Scenes `LoadContent` function:
```cs
var textRendererObject = new GameObject(new Vector2(50f, 50f), 0f, new Vector2(1f, 1f), SceneManager);
var textRenderer = new TextRenderer
{
    Text = "My amazing text",
    Font = Content.Load<SpriteFont>("My-Font")
};

textRendererObject.AddComponent<TextRenderer>(textRenderer);

Add(textRendererObject);
```
This example should render a bit of text saying "My amazing text" at position 50,50 using the font "My-Font"

# Some issues you may run into:
 - "The content is missing" - You need to add the sprite font to the games content folder and build the content file. To do so look at [this page](https://docs.monogame.net/articles/getting_started/4_adding_content.html).