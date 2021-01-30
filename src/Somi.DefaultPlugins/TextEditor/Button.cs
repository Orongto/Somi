using Somi.Core.Graphics;
using Somi.UI;

namespace Somi.DefaultPlugins
{
    public class Button : UIElement
    {
        public string Text;

        public override void Draw()
        {
            RenderQueue.DrawRect(Position, Size, new Color(26, 24, 29));

            if (IsHovering)
                RenderQueue.DrawRectOutline(Position, Size, Color.Greyscale(.8f), 1);
            base.Draw();
        }
    }
}