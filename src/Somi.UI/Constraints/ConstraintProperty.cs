using System.Collections.Generic;
using Somi.Core;

namespace Somi.UI
{
    public class ConstraintProperty
    {
        public IConstraint[] Constraints;

        
        public int Calculate(UIElement element, UIElement parent, float scale)
        {
            if (Constraints == default)
                return 0;

            Vector2I pos = default;
            var size = Application.Window.Size;

            if (parent != null)
            {
                pos = parent.Position;
                size = parent.Size;
            }

            var parameters = new ConstraintParameters(element, pos, size);

            foreach (var constrain in Constraints)
            {
                constrain.Calculate(parameters);
            }

            return (int) parameters.CurrentValue;
        }
        
        public ConstraintProperty(params IConstraint[] constraints)
        {
            Constraints = constraints;
        }
    }
}