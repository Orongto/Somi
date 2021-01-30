using System.Numerics;

namespace Somi.Core.Graphics
{
    public struct Drawable
    {
        public Mesh Mesh;
        public Texture Texture;
        public Color Tint;
        public Matrix4x4 Transformation;

        public Drawable(Mesh mesh)
        {
            Mesh = mesh;
            Texture = Texture.White;
            Transformation = Matrix4x4.Identity;
            Tint = Color.White;
        }

        public Drawable(Mesh mesh, Texture texture)
        {
            Mesh = mesh;
            Texture = texture;
            Transformation = Matrix4x4.Identity;
            Tint = Color.White;
        }

        public Drawable(Mesh mesh, Texture texture, Matrix4x4 transformation)
        {
            Mesh = mesh;
            Texture = texture;
            Transformation = transformation;
            Tint = Color.White;
        }
        
        public Drawable(Mesh mesh, Texture texture, Matrix4x4 transformation, Color tint)
        {
            Mesh = mesh;
            Texture = texture;
            Transformation = transformation;
            Tint = tint;
        }
    }
}
