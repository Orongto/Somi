using Somi.Core.Graphics;
using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Somi.Core
{
    public static class Application
    {
        public static IWindow Window { get; private set; }
        public static Input Input => Window.Input;
        public static IGraphics Graphics { get; private set; }
        public static Context Context { get; private set; } = new();
        public static RenderQueue RenderQueue = new();

        public static event Action OnUpdate;

        public static void Start(IWindow window, IGraphics graphics)
        {
            Window = window;
            Graphics = graphics;

            ProgramLoop();
        }

        private static void ProgramLoop()
        {
            var stopwatch = Stopwatch.StartNew();
            while (Window.IsOpen)
            {
                Window.ProcessEvents();
                Window.IsVisible = true;
                if (Window.IsFocused)
                {
                    OnUpdate?.Invoke();
                    Window.Render();
                }

                var elapsed = stopwatch.Elapsed.TotalSeconds;
                Window.DeltaTime = (float) elapsed;
                stopwatch.Restart();
            }

            Window.Dispose();
        }
    }
}