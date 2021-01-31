using OpenTK.Graphics.OpenGL;
using Somi.Core.Graphics;

namespace Somi.OpenTK
{
    internal struct TypeMapper
    {
        public static PrimitiveType Convert(Primitive primitive) => primitive switch
        {
            Primitive.Points => PrimitiveType.Points,
            Primitive.Lines => PrimitiveType.Lines,
            Primitive.LineLoop => PrimitiveType.LineLoop,
            Primitive.LineStrip => PrimitiveType.LineStrip,
            Primitive.Triangles => PrimitiveType.Triangles,
            Primitive.TriangleStrip => PrimitiveType.TriangleStrip,
            Primitive.TriangleFan => PrimitiveType.TriangleFan,
            _ => PrimitiveType.Triangles,
        };

        public static TextureMagFilter Convert(TextureFilter filter) => filter switch
        {
            TextureFilter.Nearest => TextureMagFilter.Nearest,
            TextureFilter.Linear => TextureMagFilter.Linear,
        };
    }
}
