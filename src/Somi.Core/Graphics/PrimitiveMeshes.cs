using System.Numerics;

namespace Somi.Core.Graphics
{
    public struct PrimitiveMeshes
    {
        public static readonly Mesh CenteredQuad = new Mesh(
                new Vertex[]
                {
                    new Vertex(-0.5f, -0.5f) { TexCoords = new Vector2(0,0) } ,
                    new Vertex(0.5f, -0.5f)  { TexCoords = new Vector2(1,0) },
                    new Vertex(0.5f, 0.5f)   { TexCoords = new Vector2(1,1) },
                    new Vertex(-0.5f, 0.5f)  { TexCoords = new Vector2(0,1) },
                },
                new uint[]
                {
                    0,1,2,
                    0,3,2
                }
            );

        public static readonly Mesh Quad = new Mesh(
                new Vertex[]
                {
                    new Vertex(0, 0) { TexCoords = new Vector2(0,0) } ,
                    new Vertex(1, 0) { TexCoords = new Vector2(1,0) },
                    new Vertex(1, 1) { TexCoords = new Vector2(1,1) },
                    new Vertex(0, 1) { TexCoords = new Vector2(0,1) },
                },
                new uint[]
                {
                    0,1,2,
                    0,3,2
                }
            );
    }
}
