using System.Linq;

namespace Somi.Core.Graphics
{
    public abstract class Input
    {
        public abstract InputState State { get; set; }

        public Vector2I WindowSize => State.WindowSize;
        public Vector2I MousePosition => State.MousePosition;
        public Vector2I PreviousMousePosition => State.PreviousMousePosition;
        public int MouseWheelDelta => State.MouseWheelDelta;
        public string CharsInputtedLastFrame => State.CharInputs;

        public virtual void RefreshInput()
        {
            State.KeysPressed.Clear();
            State.KeysReleased.Clear();
            State.ButtonsReleased.Clear();
            State.ButtonsPressed.Clear();
            State.MouseWheelDelta = 0;
            State.PreviousMousePosition = MousePosition;
            State.CharInputs = "";
            State.MousePosition = new Vector2I(-1, -1);
        }

        public bool IsKeyHeld(Key key) => State.KeysHeld.Contains(key);
        public bool IsKeyPressed(Key key) => State.KeysPressed.Contains(key);
        public bool IsButtonReleased(MouseButton button) => State.ButtonsReleased.Contains(button);
        public bool IsButtonHeld(MouseButton button) => State.ButtonsHeld.Contains(button);
        public bool IsButtonPressed(MouseButton button) => State.ButtonsPressed.Contains(button);
        public bool IsKeyReleased(Key key) => State.KeysReleased.Contains(key);

        public bool AnyKeyHeld => State.KeysHeld.Any();
        public bool AnyKeyPressed => State.KeysPressed.Any();
        public bool AnyKeyReleased => State.KeysReleased.Any();

        public Key? PressedKey => AnyKeyPressed ? new Key?(State.KeysPressed.First()) : null;
        public Key? HeldKey => AnyKeyHeld ? new Key?(State.KeysHeld.First()) : null;
        public Key? ReleasedKey => AnyKeyReleased ? new Key?(State.KeysReleased.First()) : null;
        
        public bool AnyButtonHeld => State.ButtonsHeld.Any();
        public bool AnyButtonPressed => State.ButtonsPressed.Any();
        public bool AnyButtonReleased => State.ButtonsReleased.Any();

        public MouseButton? PressedButton => AnyButtonPressed ? new MouseButton?(State.ButtonsPressed.First()) : null;
        public MouseButton? HeldButton => AnyButtonHeld ? new MouseButton?(State.ButtonsHeld.First()) : null;
        public MouseButton? ReleasedButton => AnyButtonReleased ? new MouseButton?(State.ButtonsReleased.First()) : null;
    }
}