using System;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HellFireEngine
{
    /// <summary>
    /// Do not create a instance of this class, it is already implemented by default in the scene manager.
    /// </summary>
    public class Input : IUpdateable
    {
        #region Static fields and methods:
        private static bool IsInitialized = false;
        private static InputCollection Collection;

        /// <summary>
        /// Use this method to detect if a key is being held.
        /// </summary>
        public static bool GetKey(Keys key)
        {
            if (IsInitialized)
            {
                var state = Collection.KeyboardState[key];
                if (state == KeyState.Down) return true;
            }
            return false;
        }

        /// <summary>
        /// Use this method to detect if a key was pressed this frame
        /// </summary>
        public static bool GetKeyDown(Keys key)
        {
            if (IsInitialized)
            {
                var state = Collection.KeyboardState[key];
                var previousState = Collection.PreviousKeyboardState[key];
                if (state == KeyState.Down && previousState == KeyState.Up) return true;
            }
            return false;
        }

        /// <summary>
        /// Use this method to detect if a key was released this frame
        /// </summary>
        public static bool GetKeyUp(Keys key)
        {
            if (IsInitialized)
            {
                var state = Collection.KeyboardState[key];
                var previousState = Collection.PreviousKeyboardState[key];
                if (state == KeyState.Up && previousState == KeyState.Down) return true;
            }
            return false;
        }

        /// <summary>
        /// Use this to get the current mouse position
        /// </summary>
        public static Point MousePosition()
        {
            if (IsInitialized)
            {
                return Collection.MouseState.Position;
            }
            return Point.Zero;
        }

        private static ButtonState GetMouseButtonState(MouseButton button)
        {
            return button switch
            {
                MouseButton.LeftMouseButton => Collection.MouseState.LeftButton,
                MouseButton.RightMouseButton => Collection.MouseState.RightButton,
                MouseButton.MiddleMouseButton => Collection.MouseState.MiddleButton,
                _ => ButtonState.Released,
            };
        }

        private static ButtonState GetPreviousMouseButtonState(MouseButton button)
        {
            return button switch
            {
                MouseButton.LeftMouseButton => Collection.PreviousMouseState.LeftButton,
                MouseButton.RightMouseButton => Collection.PreviousMouseState.RightButton,
                MouseButton.MiddleMouseButton => Collection.PreviousMouseState.MiddleButton,
                _ => ButtonState.Released,
            };
        }

        /// <summary>
        /// Use this method to detect if a mouse button is being held
        /// </summary>
        public static bool GetMouseButton(MouseButton button)
        {
            if (IsInitialized)
            {
                return GetMouseButtonState(button) is ButtonState.Pressed;
            }
            return false;
        }

        /// <summary>
        /// Use this method to detect if a mouse button was pressed this frame
        /// </summary>
        public static bool GetMouseDown(MouseButton button)
        {
            if (IsInitialized)
            {
                var state = GetMouseButtonState(button);
                var prevState = GetPreviousMouseButtonState(button);
                if (state == ButtonState.Pressed && prevState == ButtonState.Released) return true;
            }
            return false;
        }

        /// <summary>
        /// Use this method to detect if a mouse button was released this frame
        /// </summary>
        public static bool GetMouseUp(MouseButton button)
        {
            if (IsInitialized)
            {
                var state = GetMouseButtonState(button);
                var prevState = GetPreviousMouseButtonState(button);
                if (state == ButtonState.Released && prevState == ButtonState.Pressed) return true;
            }
            return false;
        }
        #endregion

        public Input()
        {
            IsInitialized = true;
            var initKeyboardState = Keyboard.GetState();
            var initMouseState = Mouse.GetState();
            Collection = new InputCollection(initKeyboardState, initKeyboardState, initMouseState, initMouseState);
        }

        private readonly bool _enabled = true;
        public bool Enabled => _enabled;

        private readonly int _updateOrder = 0;
        public int UpdateOrder => _updateOrder;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Update(GameTime gameTime)
        {
            Collection = new InputCollection(Keyboard.GetState(), Collection.KeyboardState, Mouse.GetState(), Collection.MouseState);
        }
    }

    public enum MouseButton
    {
        LeftMouseButton,
        MiddleMouseButton,
        RightMouseButton
    }
}
