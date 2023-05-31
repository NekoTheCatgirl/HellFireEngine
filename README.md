# HellFireEngine
A custom game engine built using [MonoGame.Extended](https://github.com/craftworkgames/MonoGame.Extended) as a big reference, and [Microsoft.Xna.Framework via MonoGame](https://www.monogame.net/) as the building blocks

Keep in mind this game engine is heavily work in progress, and features may come and go, so expect to have to change often as i update it.

This game engine is intended for more experienced developers as its a rather low level game engine, and not very beginner friendly.

Keep scrolling for a bit of a intro to how to use this engine.

# To do:
  - [ ] Setup the physics system
  - [ ] Handle all collisions
  - [ ] Allow for child objects on the game object system
  - [ ] Error testing, lots and lots of error testing
  - [ ] Create some documentation.

# Quick setup:
To setup this game engine you will first have to download and install the [.Net 6.0 development SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

Then using the Visual Studio extension manager, look for the [MonoGame Framework C# project templates](https://marketplace.visualstudio.com/items?itemName=MonoGame.MonoGame-Templates-VSExtension) and install it.
For more info, follow [this setup guide](https://docs.monogame.net/articles/getting_started/0_getting_started.html) to learn more.

If you have done all these steps, you should now be able to in visual studio, go to **Create a new project > MonoGame Cross Platform Desktop Application**

Once that is done, delete the generated game code, as you will be using this engine instead.

In the generated Content folder you will find a Content.mgcb file, open it with the MonoGame content builder, and add a image with the name "My-cool-sprite" and press build.

Create a new file, name it whatever you want, this will be the scene that you will have your game in. The basic setup should look like this:
```cs
using HellFireEngine;
using Microsoft.Xna.Framework;

namespace MyGame
{
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
}
```

Now add this game engine to the projects package references, and setup the Program.cs file like this:
```cs
using HellFireEngine;
using MyGame;

var scene = new MyScene();
using var sceneManager = new SceneManager(scene);
sceneManager.Run();
```

After this, you should see the texture you added to the content rendered at position 50, 50 at normal scale.

To get more in depth on things like custom scripting and such, you will have to turn to the docs for that.

# Common issues:
  - "HellFireEngine namespace does not exist!" - This is because you forgot to add the HellFireEngine.dll file as a reference to your project, add it and you should see that error go away.
  - "It says that my sprite does not exist" - Either you did not spell the name right when you ran load, or you may have forgotten to press build.
  - "Microsoft.Xna.Framework namespace does not exist!" - This could be a result of forgetting to install the MonoGame extension, or that you forgot to select the MonoGame Cross Platform environment.
