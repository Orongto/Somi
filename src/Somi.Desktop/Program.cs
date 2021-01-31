using Somi.Core;
using Somi.Core.Graphics;
using System;
using System.Numerics;
using Somi.DefaultPlugins;
using Somi.UI;

namespace Somi.Desktop
{
    class Program
    {
        
 
        
        static void Main(string[] args)
        {
            var window = new OpenTK.Windowing.Window("Somi Editor", new Vector2I(1280, 720))
            {
                Resizable = true
            };

         

            DefaultPluginsLoader.Load(UIContext.Root);
            var uihandler = new UIHandler();
            //dit event gedoe is tijdelijk hoop ik
            void OnApplicationOnOnUpdate()
            {
                Application.Graphics.Projection = window.CalculateProjection();

                uihandler.Update();

                while (Application.RenderQueue.Tasks.Count != 0)
                {
                    var drawable = Application.RenderQueue.Tasks.Dequeue();
                    Application.Graphics.Draw(drawable);
                }
            }

            Application.OnUpdate += OnApplicationOnOnUpdate;

            Application.Start(
                window,
                new OpenTK.Drawing.Graphics()
            );
        }

       
    }
}