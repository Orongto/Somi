using System.Numerics;
using Somi.Core;
using Somi.Core.Graphics;

namespace Somi.DefaultPlugins
{
    public static class RenderQueueExtentions
    {
        public static void DrawRect(this RenderQueue renderQueue, Vector2I position, Vector2I size, Color color)
        {
            var matrix4X4 = Matrix4x4.CreateTranslation(position.X, position.Y, 0) * Matrix4x4.CreateScale(size.X, size.Y, 0);
            renderQueue.Add(new Drawable(PrimitiveMeshes.Quad, Texture.White, matrix4X4, color));
        }
    }
}