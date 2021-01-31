using System;
using System.Collections.Generic;
using Somi.Core;

namespace Somi.UI
{
    public abstract class UIElement
    {
        public Vector2I Position;
        public Vector2I Size;

        public UIElement Parent;
        public List<UIElement> Children;

        public Anchor Anchor;

        public RenderQueue RenderQueue => Application.RenderQueue;

        public bool IsInteractable = true;
        public bool CapturesKeyboard;

        public bool IsEnabled = true;
        public bool IsActive = true;
        public bool IsSelected;
        public bool IsPressed;
        public bool IsReleased;
        public bool IsHovering;
        public bool IsDragging;
        public bool IsHeld;
        public bool IsClicked;

        public Vector2I LastPressedPosition;
        
        public IEnumerable<UIElement> DecendantsIncludingSelf()
        {
            yield return this;

            if (Children != null)
                foreach (var child in Children)
                {
                    foreach (var d in child.DecendantsIncludingSelf())
                    {
                        yield return d;
                    }
                }
        }

        public virtual void Draw()
        {
        }
        
        public virtual void StartDragging()
        {
        }
        
        public virtual void StopDragging()
        {
        }
        
        /// <summary>
        /// Element is dropped on nothing or on a non interactable element.
        /// </summary>
        public virtual void DroppedInVoid()
        {
        }
        
        public virtual void OnDropped()
        {       
        }

        public void AddChild(UIElement child)
        {
            child.Parent = this;

            if (Children == null)
                Children = new();

            Children.Add(child);
        }
    }
}