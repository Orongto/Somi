using OpenTK.Graphics.OpenGL;
using Somi.Core.Graphics;
using System.Numerics;

namespace Somi.OpenTK.Drawing
{
    public struct Graphics : IGraphics
    {
        private Material currentMaterial;

        public Matrix4x4 Projection { get; set; }

        public void Draw(Drawable drawable)
        {
            var mesh = drawable.Mesh;
            var texture = drawable.Texture;

            var handles = Storage.Meshes.Load(mesh);
            
            if (mesh.NeedsUpdate)
                MeshCache.UploadData(mesh, handles);

            GL.BindVertexArray(handles.VAO);

            SetMaterialFromTexture(texture);

            currentMaterial.SetUniform(ShaderConstants.ProjectionUniformName, Projection);
            currentMaterial.SetUniform(ShaderConstants.ModelUniformName, drawable.Transformation);
            currentMaterial.SetUniform(ShaderConstants.TintUniformName, drawable.Tint);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Storage.Textures.Load(drawable.Texture).Index);

            GL.DrawElements(
                TypeMapper.Convert(mesh.PrimitiveType),
                mesh.IndexCount,
                DrawElementsType.UnsignedInt, 0);
        }

        internal void SetMaterialFromTexture(Texture texture)
        {
            var m  = Storage.TextureMaterials.Load(texture);
            if (currentMaterial == m)
                return;

            GL.UseProgram(m.ProgramIndex);
            currentMaterial = m;
        }
    }
}
