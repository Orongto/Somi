using Somi.Core;

namespace Somi.UI
{
    public class ConstraintParameters
    {
        public ConstraintParameters(UIElement element, Vector2I parentPosition, Vector2I parentSize)
        {
            Element = element;
            ParentSize = parentSize;
            ParentPosition = parentPosition;
        }

        public float CurrentValue { get; set; }
        public float Scale { get; private set; } = 1;
        public UIElement Element { get; private set; }
        public Vector2I ParentSize { get; private set; }
        public Vector2I ParentPosition { get; private set; }
    }
}