using OpenTK.Graphics.OpenGL;
using System;

namespace Somi.OpenTK.Drawing
{
    internal class ShaderCache : Cache<Shader, ShaderHandles>
    {
        protected override ShaderHandles CreateNew(Shader raw)
        {
            var (vert, frag) = CreateShaders(raw);

            return new ShaderHandles
            {
                VertexShaderIndex = vert,
                FragmentShaderIndex = frag
            };
        }

        protected override void DisposeOf(ShaderHandles loaded)
        {
            GL.DeleteShader(loaded.VertexShaderIndex);
            GL.DeleteShader(loaded.FragmentShaderIndex);
        }

        private static (int vert, int frag) CreateShaders(Shader shader)
        {
            var vert = GL.CreateShader(ShaderType.VertexShader);
            var frag = GL.CreateShader(ShaderType.FragmentShader);

            if (!ShaderCompiler.TryCompileShader(vert, shader.Vertex))
            {
                GL.DeleteShader(vert);
                GL.DeleteShader(frag);
                throw new Exception("Vertex shader failed to compile");
            }

            if (!ShaderCompiler.TryCompileShader(frag, shader.Fragment))
            {
                GL.DeleteShader(vert);
                GL.DeleteShader(frag);
                throw new Exception("Fragment shader failed to compile");
            }

            return (vert, frag);
        }
    }
}
