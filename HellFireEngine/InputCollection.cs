using Microsoft.Xna.Framework.Input;

namespace HellFireEngine
{
    internal class InputCollection
    {
        public InputCollection(KeyboardState keyboardState, KeyboardState previousKeyboardState, MouseState mouseState, MouseState previousMouseState)
        {
            KeyboardState = keyboardState;
            PreviousKeyboardState = previousKeyboardState;
            MouseState = mouseState;
            PreviousMouseState = previousMouseState;
        }
        
        public KeyboardState KeyboardState { get; }
        public KeyboardState PreviousKeyboardState { get; }
        public MouseState MouseState { get; }
        public MouseState PreviousMouseState { get; }
    }
}
