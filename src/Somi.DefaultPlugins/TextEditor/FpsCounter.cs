using System;
using System.Collections.Generic;
using System.Linq;
using Somi.Core;
using Somi.Core.Graphics;
using Somi.UI;

namespace Somi.DefaultPlugins
{
    public class FpsCounter : UIElement
    {
        public List<float> deltaTimes = new();
        
        public override void Draw()
        {
            deltaTimes.Add(Application.Window.DeltaTime);
            var averageDt = deltaTimes.Average();

            while (averageDt > 0 && deltaTimes.Count > (int)(1f / averageDt))
                deltaTimes.RemoveAt(0);
            
            RenderQueue.DrawText("FPS = " + MathF.Round((1f / averageDt),1).ToString(), Position, Size.Y, Color.White);
            base.Draw();
        }
    }
}