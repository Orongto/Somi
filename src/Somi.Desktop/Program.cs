﻿using Somi.Core;
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
            Console.WriteLine("Hello World!");
            var window = new OpenTK.Windowing.Window("Somi Editor", new Vector2I(1280, 720))
            {
                Resizable = true
            };

            var drawable1 = new Drawable(
                PrimitiveMeshes.CenteredQuad,
                Texture.LoadFromFile("testImage.png"),
                Matrix4x4.CreateScale(256, 256, 1));

            var drawable2 = new Drawable(
                PrimitiveMeshes.CenteredQuad,
                Texture.LoadFromFile("testImage2.png"),
                Matrix4x4.CreateScale(443, 222, 1) * Matrix4x4.CreateTranslation(320, 50, 0));


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