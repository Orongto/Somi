using OpenTK.Windowing.Common;
using Somi.Core;
using Somi.Core.Graphics;

namespace Somi.OpenTK.Windowing
{
    public sealed class OpenTKInput : Input
    {
        public override InputState State { get; set; } = new InputState();
        private readonly Window windowBase;
        private int lastMouseWheelOffset = 0;
        public OpenTKInput(Window window)
        {
            windowBase = window;
            windowBase.NativeWindow.FocusedChanged += WindowBase_FocusedChanged;
            windowBase.NativeWindow.KeyDown += Keyboard_KeyDown;
            windowBase.NativeWindow.TextInput += WindowBase_TextInput;
            windowBase.NativeWindow.KeyUp += Keyboard_KeyUp;
            windowBase.NativeWindow.MouseDown += Mouse_ButtonDown;
            windowBase.NativeWindow.MouseUp += Mouse_ButtonUp;
            windowBase.NativeWindow.MouseMove += Mouse_Move;
            windowBase.NativeWindow.MouseWheel += Mouse_Wheel;
        }

        private void WindowBase_TextInput(TextInputEventArgs obj)
        {
            State.CharInputs += obj.AsString;
        }

        private void WindowBase_FocusedChanged(FocusedChangedEventArgs e)
        {
            if (!windowBase.NativeWindow.IsFocused)
            {
                State.ButtonsHeld.Clear();
                State.KeysHeld.Clear();
            }
        }

        private void Mouse_Wheel(MouseWheelEventArgs e)
        {
            State.MouseWheelDelta = (int)e.OffsetY - lastMouseWheelOffset;
            lastMouseWheelOffset = (int)e.OffsetY;
        }

        private Vector2I nextMousePosition;
        private void Mouse_Move(MouseMoveEventArgs e)
        {
            nextMousePosition = new Vector2I((int)e.X, (int)e.Y);
        }

        private void Mouse_ButtonUp(MouseButtonEventArgs e)
        {
            MouseButton button = (MouseButton)(int)e.Button;
            State.ButtonsHeld.Remove(button);
            State.ButtonsReleased.Add(button);
        }

        private void Mouse_ButtonDown(MouseButtonEventArgs e)
        {
            MouseButton button = (MouseButton)(int)e.Button;
            if (!State.ButtonsHeld.Contains(button))
            {
                State.ButtonsHeld.Add(button);
            }
            State.ButtonsPressed.Add(button);
        }

        private void Keyboard_KeyDown(KeyboardKeyEventArgs e)
        {
            Key key = (Key)(int)e.Key;
                State.KeysPressed.Add(key);
            if (!State.KeysHeld.Contains(key))
            {
                State.KeysHeld.Add(key);
            }
        }

        private void Keyboard_KeyUp(KeyboardKeyEventArgs e)
        {
            Key key = (Key)(int)e.Key;

            State.KeysHeld.Remove(key);
            State.KeysReleased.Add(key);
        }

        public override void RefreshInput()
        {
            State.WindowSize = windowBase.Size;
            State.MousePosition = nextMousePosition;
            base.RefreshInput();
        }
    }
}