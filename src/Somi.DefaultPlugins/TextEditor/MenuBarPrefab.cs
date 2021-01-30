using Somi.Core.Graphics;
using Somi.UI;

namespace Somi.DefaultPlugins
{
    public static class MenuBarPrefab
    {
        public static void Instantiate(UIElement root, int height)
        {
            var back = new ColoredUIElement()
            {
                Color = new Color(33,30,37),
                Anchor = new Anchor()
                {
                    X = new ConstraintProperty(new PixelConstraint(0)),
                    Y = new ConstraintProperty(new PixelConstraint(0)),
                    Width = new ConstraintProperty(new WidthRatioConstraint(1)),
                    Height = new ConstraintProperty(new PixelConstraint(height))
                }            
            };
            root.AddChild(back);

            int buttonwidth = 100;
            int buttonMargin = 10;
            int Ymargin = 5;
            for (int i = 0; i < 4; i++)
            {
                back.AddChild(new Button
                {
                    Anchor = new Anchor()
                    {
                        X = new ConstraintProperty(new PixelConstraint((buttonwidth+buttonMargin)*i)),
                        Y = new ConstraintProperty(new PixelConstraint(Ymargin)),
                        Width = new ConstraintProperty(new PixelConstraint(buttonwidth)),
                        Height = new ConstraintProperty(new HeightRatioConstraint(1),  new PixelConstraint(-Ymargin*2))
                    }
                });
            }
        }
    }
}