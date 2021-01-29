using Somi.Core;
using Somi.Core.Graphics;
using System;
using System.Numerics;

namespace Somi.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new OpenTK.Windowing.Window("Somi is een tijdelijke naam", new Vector2I(1280, 720))
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

            //dit event gedoe is tijdelijk hoop ik
            Application.OnUpdate += () =>
            {
                Application.Graphics.Projection = window.CalculateProjection();
                Application.Graphics.Draw(drawable1);
                Application.Graphics.Draw(drawable2);
            };

            Application.Start(
                window,
                new OpenTK.Drawing.Graphics()
                );
        }
    }
}