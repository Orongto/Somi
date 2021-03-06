﻿using System.Numerics;
using Nurose.Text;
using Somi.Core;
using Somi.Core.Graphics;

namespace Somi.DefaultPlugins
{
    public static class RenderQueueExtentions
    {

        private static TextDrawableGenerator generator = new TextDrawableGenerator(new BMFont("Resources/Fonts/cascadia.fnt"));
        public static Mesh textMesh = new Mesh();
        
        public static void DrawRect(this RenderQueue renderQueue, Vector2I position, Vector2I size, Color color)
        {
            var matrix4X4 = Matrix4x4.CreateScale(size.X, size.Y, 0) *  Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            renderQueue.Add(new Drawable(PrimitiveMeshes.Quad, Texture.White, matrix4X4, color));
        }

        public static void DrawRectOutline(this RenderQueue renderQueue, Vector2I position, Vector2I size, Color color, int width)
        {
            renderQueue.DrawRect(position, new Vector2I(size.X, width), color);
            renderQueue.DrawRect(position + new Vector2I(size.X -width, 0), new Vector2I(width, size.Y), color);
            renderQueue.DrawRect(position + new Vector2I(0, size.Y - width), new Vector2I(size.X, width), color);
            renderQueue.DrawRect(position, new Vector2I(width, size.Y), color);
        }

        public static void DrawText(this RenderQueue renderQueue,string text,  Vector2I position, int lineheight, Color color)
        {
            generator.Text = text;
            var mesh = new Mesh();
            generator.UpdateBuffer(mesh);
            renderQueue.Add(new Drawable(mesh, generator.BMFont.Texture, new TransformData(position, new Vector2(lineheight, lineheight)).CalcModelMatrix(), color));
        }
    }
}