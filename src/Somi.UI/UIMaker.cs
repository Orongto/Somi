namespace Somi.UI
{
    public static class UIMaker
    {
        public static void AddChild(UIElement parent, UIElement child)
        {
            if (parent.Children == null)
                parent.Children = new();
            
            parent.Children.Add(child);
            child.Parent = parent;
        }

        public static void AddSibling(UIElement sibling, UIElement newSibling)
        {
            sibling.Parent.Children.Add(newSibling);
        }
    }
}