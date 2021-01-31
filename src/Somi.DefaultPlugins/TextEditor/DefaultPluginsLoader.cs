using System.Collections.Generic;
using Somi.UI;

namespace Somi.DefaultPlugins
{
    public static class DefaultPluginsLoader
    {
        public static void Load(UIElement root)
        {
            int leftPaneWidth = 300;
            int navHeight = 30;
            
            root.AddChild(new PlaceholderElement()
            {
                Text = "Solution Explorer",
                Anchor = new Anchor()
                {
                    X = new ConstraintProperty(new PixelConstraint(0)),
                    Y = new ConstraintProperty(new PixelConstraint(navHeight)),
                    Width = new ConstraintProperty(new PixelConstraint(leftPaneWidth)),
                    Height = new ConstraintProperty(new HeightRatioConstraint(1), new PixelConstraint(-navHeight))
                }
            });
            
            root.AddChild(new Editor()
            {
                Anchor = new Anchor()
                {
                    X = new ConstraintProperty(new PixelConstraint(leftPaneWidth)),
                    Y = new ConstraintProperty(new PixelConstraint(navHeight)),
                    Width = new ConstraintProperty(new WidthRatioConstraint(1), new PixelConstraint(-leftPaneWidth)),
                    Height = new ConstraintProperty(new HeightRatioConstraint(1), new PixelConstraint(-navHeight))
                }
            });
            
            MenuBarPrefab.Instantiate(root, navHeight);
            //UIMaker.AddChild(root, new Editor());
        }
    }
}