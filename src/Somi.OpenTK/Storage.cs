using Somi.OpenTK.Drawing;

namespace Somi.OpenTK
{
    internal struct Storage
    {
        public static readonly MeshCache Meshes = new();
        public static readonly ShaderCache Shaders = new();
        public static readonly TextureCache Textures = new();
        public static readonly MaterialTextureCache TextureMaterials = new();
    }
}
