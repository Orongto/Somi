using Somi.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Somi.Core
{
    public static class Application
    {
        public static IWindow Window { get; private set; }
        public static IGraphics Graphics { get; private set; }

        public static event Action OnUpdate;

        public static void Start(IWindow window, IGraphics graphics)
        {
            Window = window;
            Graphics = graphics;

            ProgramLoop();
        }

        private static void ProgramLoop()
        {
            while (Window.IsOpen)
            {
                Window.ProcessEvents();
                Window.Render();
                OnUpdate?.Invoke();
            }

            Window.Dispose();
        }
    }
}
