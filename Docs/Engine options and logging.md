# How to setup logging:
The engine uses [Serilog](https://serilog.net/) to handle its logging, and therefore you will need to initialize the logger, and install and activate the sink you want to use.

If you are using the console sink, you will also have to change the projects output type to **Console Application**.

To setup the logger you will need to add this to the Program.cs
```cs
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
```
This will create a logger that writes to the console.

# Engine options:
The engine options container lets you modify some values before starting the scene manager, and will change the look and behavior of the engine.

## Logger:
To disable the Engine default logging you simply add this to the Program.cs:
```cs
EngineOptions.EnableLogger = false;
```
The logger is on by default.

However the Engine logging wont show unless you follow the steps to setup a logger

## Fullscreen mode:
To force a fullscreen mode you will simply add this:
```cs
EngineOptions.ForceFullscreen = true;
```
The fullscreen is off by default.

## Screen size:
To change the screen size, to a custom value, you can do this:
```cs
EngineOptions.ScreenWidth = 1000;
EngineOptions.ScreenHeight = 500;
```
The default screen size is 800x480