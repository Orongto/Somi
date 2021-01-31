using System.Collections.Generic;
using Somi.Core;
using Somi.Core.Graphics;
using Somi.DefaultPlugins;
using Somi.UI;

namespace Somi.Desktop
{
    public static class UIStateRefresher
    {
        private const int draggingOffset = 20;

        public static void RefreshState(UIData data, List<UIElement> ordered)
        {
            foreach (var element in ordered)
            {
                element.IsHovering = false;
                element.IsPressed = false;
                element.IsClicked = false;
            }

            var somethingHovered = false;
            var newSelectedElement = false;
            var draggingLetGo = false;
            var heldLetGo = false;

            var Input = Application.Window.Input;

            foreach (var element in ordered)
            {
                if (!element.IsEnabled || !AllParentEnabled(element))
                    continue;


                if (!element.IsInteractable)
                    continue;


                if (!somethingHovered && Utils.IsPointInsideBounds(Input.MousePosition, element.Position, element.Position + element.Size))
                {
                    element.IsHovering = true;
                    somethingHovered = true;
                }


                var released = Input.IsButtonReleased(MouseButton.Left);
                element.IsReleased = (element.IsDragging || element.IsHeld) && released;
                element.IsPressed = element.IsHovering && Application.Window.Input.IsButtonPressed(MouseButton.Left);


                if (element.IsPressed)
                    element.LastPressedPosition = Input.MousePosition;

                var distanceLargerThenDraggingOffset = Utils.Distance(element.LastPressedPosition, Input.MousePosition) > draggingOffset;
                if (Input.IsButtonHeld(MouseButton.Left) && element.LastPressedPosition != default)
                {
                    if (!element.IsHeld)
                    {
                        element.IsHeld = true;
                    }

                    if (!element.IsDragging && distanceLargerThenDraggingOffset)
                    {
                        element.IsDragging = true;
                        data.Dragging = element;
                        element.StartDragging();
                    }
                }

                if (element.IsDragging && released)
                    draggingLetGo = true;

                if (element.IsHeld && released)
                    heldLetGo = true;

                if (element.IsPressed && !newSelectedElement)
                {
                    DeselectElements(ordered);
                    Select(element);
                    newSelectedElement = true;
                    element.LastPressedPosition = Input.MousePosition;
                }

                if (element.IsReleased)
                    element.LastPressedPosition = default;

                if (element.IsPressed && element.IsActive)
                    element.IsClicked = true;


                if (element.IsHovering && released && data.DragPayload != null)
                {
                    element.OnDropped();
                    data.Dragging = null;
                    data.DragPayload = null;
                }

                PropageteToNonInteractableChildren(element);
            }

            if (Input.IsButtonPressed(MouseButton.Left) && !newSelectedElement)
            {
                DeselectElements(ordered);
            }

            if (draggingLetGo)
            {
                foreach (var e in ordered)
                {
                    if (e.IsDragging)
                    {
                        e.IsDragging = false;
                        e.StopDragging();
                    }
                }

                if (data.Dragging != null)
                {
                    data.Dragging.DroppedInVoid();
                    data.Dragging = null;
                    data.DragPayload = null;
                }
            }

            if (heldLetGo)
            {
                foreach (var e in ordered)
                {
                    e.IsHeld = false;
                }
            }
        }

        private static void Select(UIElement element)
        {
            element.IsSelected = true;
        }


        private static bool AllParentEnabled(UIElement element)
        {
            if (element.Parent == null)
                return true;

            var parent = element.Parent;


            return parent.IsEnabled && AllParentEnabled(parent);
        }

        private static void DeselectElements(List<UIElement> elements)
        {
            foreach (var e in elements)
            {
                if (e.IsSelected)
                    e.IsSelected = false;
            }
        }

        private static void PropageteToNonInteractableChildren(UIElement element)
        {
            if (element.Children == null)
                return;

            foreach (var child in element.Children)
            {
                if (!child.IsInteractable)
                {
                    child.IsHovering = element.IsHovering;
                    child.IsPressed = element.IsPressed;
                    child.IsActive = element.IsActive;
                    child.IsSelected = element.IsSelected;
                }

                PropageteToNonInteractableChildren(child);
            }
        }
    }
}