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
As of right now, there is just 1 option available, and that is to set if the engine should log its own things or not, turning it off will not prevent any logging calls you make from going trough.

To disable the Engine default logging you simply add this to the Program.cs:
```cs
EngineOptions.EnableLogger = false;
```
The logger is on by default.

However the Engine logging wont show unless you follow the steps to setup a logger