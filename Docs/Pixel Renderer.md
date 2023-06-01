# Some warnings about the Pixel Renderer:
The Pixel Renderer is a rather expensive renderer to use, and may cause heavy performance drops when reaching higher than a few thousand pixels.

Also, the Pixel Renderer can only be moved using the Transform.Position value, and does not respect Rotation and Scale.

Another thing to keep in mind, after you set a pixel, you cant unset it.

That in mind, here is how to use the Pixel Renderer

# How to use the Pixel Renderer:
The Pixel Renderer is a way to draw single pixels to the screen.

Here is how you will add it to a object in the Scenes `LoadContent` function:
```cs
var pixelRendererObject = new GameObject(new Vector2(50f, 50f), 0f, new Vector2(1f, 1f), SceneManager);
var pixelRenderer = new PixelRenderer()

for (int x = 0; x < 128; x++)
{
    for (int y = 0; y < 128; y++)
    {
        pixelRenderer.SetPixel(new Point(x, y), Color.Yellow);
    }
}

pixelRendererObject.AddComponent<PixelRenderer>(pixelRenderer);

Add(pixelRendererObject);
```
This will create a pixel texture that is all yellow, and with a size of 128x128

