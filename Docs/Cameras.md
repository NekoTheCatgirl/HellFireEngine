# Initializing the camera:
Here you will learn how to use the camera system.

Fair warning, there is only 1 camera currently supported, and adding support for other cameras is not a priority.

The current camera type supported is the Orthographic Camera.

To use the camera, you would just need to create a camera, it will then be globally instantiated.

Like this:
```cs
var camera = new OrthographicCamera();
```
You can also specify the Viewport Adapter type to use like this:
```cs
var adapter = new ScalingViewportAdapter(GraphicsDevice, 400, 800);
var camera = new OrthographicCamera(adapter);
```
Keep in mind, that for a viewport adapter, you will need the Graphics device to be initialized, so it can only be initialized after the Scene Manager is instantiated, and running.

# Using the camera:
Now, you can access the camera from anywhere in your code like this:
```cs
OrthographicCamera.MainCamera
```

On the camera you will find functions that handle screen to world and world to screen math, along with the position field, rotation field and zoom field, that you can modify like this:
```cs
OrthographicCamera.MainCamera.Position = new Vector2(50f, 50f);
```
This would move the camera to the position 50, 50.

Alternatively you can edit the position like this
```cs
OrthographicCamera.MainCamera.Move(new Vector2(15f, 15f));
```
This would move the camera by 15x and y, adding it to the position.

And rotation can be handled by calling the Rotate function, like this
```cs
OrthographicCamera.MainCamera.Rotate(15f);
```
This would rotate the camera by 15 radians

To manage the zoom you can just set the zoom variable, or alternatively you can use the Zoom function.

Here is using the Zoom variable directly:
```cs
OrthographicCamera.MainCamera.Zoom = 1.6f;
```
And here is using the ZoomIn method:
```cs
OrthographicCamera.MainCamera.ZoomIn(1.4f);
```
And here is using the ZoomOut method:
```cs
OrthographicCamera.MainCamera.ZoomOut(1.4f);
```

If you need to be able to say, click a sprite on the screen, you will have to convert it to the world position, to do this, you can use the ScreenToWorld function, like this:
```cs
var position = OrthographicCamera.MainCamera.ScreenToWorld(50f, 50f);
```
Alternatively it can take a Vector2 as input aswell, like this:
```cs
var position = OrthographicCamera.MainCamera.ScreenToWorld(new Vector2(50f, 50f));
```

# Adding custom cameras:
This topic requries a lot of work, and you will need to override the scenes draw call with the code that handles starting the SpriteBatch with the right matrix.

Firsly, you will need to inherit from the Camera class, and specify the positional type, typically Vector2.

Now the draw call will have to be overriden like this:
```cs
public override void Draw(GameTime gameTime) 
{
    if (OrthographicCamera.MainCamera is not null)
    {
        var transformMatrix = OrthographicCamera.MainCamera.GetViewMatrix();
        SpriteBatch.Begin(transformMatrix: transformMatrix);
    }
    else if (MyCamera.MainCamera is not null)
    {
        var transformMatrix = MyCamera.MainCamera.GetViewMatrix();
        SpriteBatch.Begin(transformMatrix: transformMatrix);
    }
    else
    {
        SpriteBatch.Begin();
    }
    GameObjects.ForEach(x => x.Draw(gameTime));
    SpriteBatch.End();
}
```

I however cant give any more detail on how to setup a camera as that topic is a bit too advanced to get into more details about, for normal games, the builtin Orthographic camera should do just fine.