using System.Collections.Generic;
using Somi.UI;

namespace Somi.DefaultPlugins
{
    
    public static class DefaultPluginsLoader
    {
        public static void Load(UIElement root)
        {
            UIContext.Root = new Editor();
            //UIMaker.AddChild(root, new Editor());
        }
    }
}