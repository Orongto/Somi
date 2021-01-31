using System.Collections.Generic;
using Somi.Core.Graphics;

namespace Somi.Core
{
    public class RenderQueue
    {
        public readonly Queue<Drawable> Tasks = new();

        public void Add(Drawable drawable)
        {
            Tasks.Enqueue(drawable);
        }
    }
}