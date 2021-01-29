using System.Numerics;

namespace Somi.Core.Graphics
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector2 TexCoords;
        public Color Color;

        public Vertex(Vector3 position, Vector2 texCoords, Color color)
        {
            Position = position;
            TexCoords = texCoords;
            Color = color;
        }        
        
        public Vertex(Vector3 position)
        {
            Position = position;
            TexCoords = Vector2.Zero;
            Color = Color.White;
        }

        public Vertex(float x, float y, float z = 0)
        {
            Position = new Vector3(x,y,z);
            TexCoords = Vector2.Zero;
            Color = Color.White;
        }
    }
}
