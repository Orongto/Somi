using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Somi.Core;
using Somi.UI;

namespace Somi.Desktop
{
    public class UIHandler
    {
        public UIData Data = new();

        public void Update()
        {
            var orderedElements = GetOrderedElements();

            CalculateConstraints(orderedElements);

            UIStateRefresher.RefreshState(Data, orderedElements);

            DrawRecursivly(UIContext.Root);
        }

        private static List<UIElement> GetOrderedElements()
        {
            var orderedElements = UIContext.Root.DecendantsIncludingSelf().ToList();
            orderedElements.Reverse();
            return orderedElements;
        }


        private static void CalculateConstraints(IEnumerable<UIElement> elements)
        {
            foreach (var element in elements)
            {
                var x = element.Anchor.X.Calculate(element, element.Parent, 1);
                var y = element.Anchor.Y.Calculate(element, element.Parent, 1);
                var w = element.Anchor.Width.Calculate(element, element.Parent, 1);
                var h = element.Anchor.Height.Calculate(element, element.Parent, 1);
                element.Position = new Vector2I(x, y);
                element.Size = new Vector2I(w, h);
            }
        }


        private static void DrawRecursivly(UIElement root)
        {
            root.Draw();

            if (root.Children != null)
                foreach (var child in root.Children)
                    DrawRecursivly(child);
            
        }
    }
}