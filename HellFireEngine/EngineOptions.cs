namespace HellFireEngine
{
    public static class EngineOptions
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static bool EnableLogger = true;
        public static bool ForceFullscreen = false;
        public static int ScreenWidth = 800;
        public static int ScreenHeight = 480;
#pragma warning restore CA2211 // Non-constant fields should not be visible
    }
}
