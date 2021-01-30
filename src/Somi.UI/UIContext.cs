namespace Somi.UI
{
    public static class UIContext
    {
        public static UIElement Root = new EmptyUIElement()
        {
            Anchor = new Anchor()
            {
                X = new ConstraintProperty(new PixelConstraint(0)),
                Y = new ConstraintProperty(new PixelConstraint(0)),
                Width = new ConstraintProperty(new WidthRatioConstraint(1)),
                Height = new ConstraintProperty(new HeightRatioConstraint(1))
            }
        };
    }
}