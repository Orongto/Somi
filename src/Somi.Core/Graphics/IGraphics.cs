using System.Numerics;

namespace Somi.Core.Graphics
{
    public interface IGraphics
    {
        public Matrix4x4 Projection { get; set; }
        public void Draw(Drawable drawable);
    }
}
