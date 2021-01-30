using Somi.Core.Graphics;
using Somi.UI;

namespace Somi.DefaultPlugins
{
    public class ColoredUIElement : UIElement
    {
        public Color Color;
        
        public override void Draw()
        {
            RenderQueue.DrawRect(Position, Size, Color);
            base.Draw();
        }
    }
}