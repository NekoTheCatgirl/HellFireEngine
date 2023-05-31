using HellFireEngine;
using Serilog;
using TestApplication;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

EngineOptions.EnableLogger = false;

var scene = new TestScene();
using var sceneManager = new SceneManager(scene);
sceneManager.Run();
