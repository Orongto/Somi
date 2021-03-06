﻿using Somi.Core;
using Somi.Core.Graphics;
using System;
using System.Linq;
using System.Numerics;
using Somi.DefaultPlugins;
using Somi.OpenTK.Windowing;
using Somi.UI;

namespace Somi.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Application.Context.SelectedFilePath = args[0];
            }
            else
            {
            }

            var window = new OpenTK.Windowing.Window("Somi Editor", new Vector2I(1280, 720))
            {
                Resizable = true
            };

            DefaultPluginsLoader.Load(UIContext.Root);
            var uihandler = new UIHandler();

            //dit event gedoe is tijdelijk hoop ik
            void OnApplicationUpdate()
            {
                Application.Graphics.Projection = window.CalculateProjection();


                uihandler.Update();

                if (Application.Window.IsOpen)
                    while (Application.RenderQueue.Tasks.Count != 0)
                    {
                        var drawable = Application.RenderQueue.Tasks.Dequeue();
                        Application.Graphics.Draw(drawable);
                    }
            }

            Application.OnUpdate += OnApplicationUpdate;

            Application.Start(
                window,
                new OpenTK.Drawing.Graphics()
            );
        }
    }
}