using System;
using System.Collections.Generic;
using Somi.Core;

namespace Somi.UI
{
    public abstract class UIElement
    {
        public Vector2I CalculatedPosition;
        public Vector2I CalculatedSize;

        public UIElement Parent;
        public List<UIElement> Children;

        public bool IsSelected;

        public RenderQueue RenderQueue => Application.RenderQueue;
        
        public virtual void Draw()
        {
        }
    }
}