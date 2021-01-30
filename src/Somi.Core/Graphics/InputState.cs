using System.Collections.Generic;
using System.Linq;

namespace Somi.Core.Graphics
{
    /// <summary>
    /// Represents the mouse and keyboard state of a single frame
    /// </summary>
    public class InputState
    {
        public Vector2I WindowSize;
        public Vector2I PreviousMousePosition;
        public Vector2I MousePosition;
        public int MouseWheelDelta;
        public string CharInputs = "";

        public List<MouseButton> ButtonsHeld = new List<MouseButton>();
        public List<MouseButton> ButtonsPressed = new List<MouseButton>();
        public List<MouseButton> ButtonsReleased = new List<MouseButton>();
        public List<Key> KeysPressed = new List<Key>();
        public List<Key> KeysReleased = new List<Key>();
        public List<Key> KeysHeld = new List<Key>();

        public InputState Clone()
        {
            return new InputState()
            {
                WindowSize = this.WindowSize,
                PreviousMousePosition = this.PreviousMousePosition,
                MousePosition = this.MousePosition,
                MouseWheelDelta = this.MouseWheelDelta,
                CharInputs = this.CharInputs, 
                ButtonsHeld = new List<MouseButton>(this.ButtonsHeld),
                ButtonsPressed = new List<MouseButton>(this.ButtonsPressed),
                ButtonsReleased = new List<MouseButton>(this.ButtonsReleased),
                KeysPressed = new List<Key>(this.KeysPressed),
                KeysReleased = new List<Key>(this.KeysReleased),
                KeysHeld = new List<Key>(this.KeysHeld),
            };
        }

        public override bool Equals(object obj)
        {
            return obj is InputState state &&
                   WindowSize.Equals(state.WindowSize) &&
                   PreviousMousePosition.Equals(state.PreviousMousePosition) &&
                   MousePosition.Equals(state.MousePosition) &&
                   MouseWheelDelta == state.MouseWheelDelta &&
                   CharInputs == state.CharInputs &&
                   ButtonsHeld.SequenceEqual(state.ButtonsHeld) &&
                   ButtonsPressed.SequenceEqual(state.ButtonsPressed) &&
                   ButtonsReleased.SequenceEqual(state.ButtonsReleased) &&
                   KeysPressed.SequenceEqual(state.KeysPressed) &&
                   KeysReleased.SequenceEqual(state.KeysReleased) &&
                   KeysHeld.SequenceEqual(state.KeysHeld);
        }

        public override int GetHashCode()
        {
            var hashCode = -747332127;
            hashCode = hashCode * -1521134295 + WindowSize.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2I>.Default.GetHashCode(PreviousMousePosition);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CharInputs);
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2I>.Default.GetHashCode(MousePosition);
            hashCode = hashCode * -1521134295 + MouseWheelDelta.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<MouseButton>>.Default.GetHashCode(ButtonsHeld);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<MouseButton>>.Default.GetHashCode(ButtonsPressed);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<MouseButton>>.Default.GetHashCode(ButtonsReleased);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Key>>.Default.GetHashCode(KeysPressed);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Key>>.Default.GetHashCode(KeysReleased);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Key>>.Default.GetHashCode(KeysHeld);
            return hashCode;
        }
    }
}