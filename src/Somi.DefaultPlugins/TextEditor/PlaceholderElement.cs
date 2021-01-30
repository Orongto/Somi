using Somi.Core.Graphics;
using Somi.UI;

namespace Somi.DefaultPlugins
{
    public class PlaceholderElement : UIElement
    {
        public string Text;

        public override void Draw()
        {
            RenderQueue.DrawRect(Position, Size, new Color(38,35,42));
            base.Draw();
        }
    }
}