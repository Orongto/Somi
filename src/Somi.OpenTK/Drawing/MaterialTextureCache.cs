using OpenTK.Graphics.OpenGL;
using Somi.Core.Graphics;
using System;
using System.Collections.Immutable;

namespace Somi.OpenTK.Drawing
{
    internal class MaterialTextureCache : Cache<Texture, Material>
    {
        protected override Material CreateNew(Texture raw)
        {
            var shader = Shader.Default;
            var programIndex = GL.CreateProgram();
            var shaderHandles = Storage.Shaders.Load(shader);
            var loadedTexture = Storage.Textures.Load(raw);

            AttachShaders(programIndex, shaderHandles);
            GL.ValidateProgram(programIndex);
            ReleaseShaders(programIndex, shaderHandles);

            var material = new Material(programIndex, shader);

            material.SetUniform(ShaderConstants.TintUniformName, Color.White);
            material.SetUniform(ShaderConstants.MainTexUniformName, raw);

            return material;
        }

        private static void AttachShaders(int programIndex, ShaderHandles shaderHandles)
        {
            GL.AttachShader(programIndex, shaderHandles.VertexShaderIndex);
            GL.AttachShader(programIndex, shaderHandles.FragmentShaderIndex);

            GL.LinkProgram(programIndex);
            GL.GetProgram(programIndex, GetProgramParameterName.LinkStatus, out int linkStatus);

            bool linkingFailed = linkStatus == (int)All.False;

            if (linkingFailed)
            {
                GL.DeleteProgram(programIndex);
                GL.DeleteShader(shaderHandles.VertexShaderIndex);
                GL.DeleteShader(shaderHandles.FragmentShaderIndex);
                throw new Exception("Shader program failed to link");
            }
        }

        private static void ReleaseShaders(int programIndex, ShaderHandles shaderHandles)
        {
            GL.DetachShader(programIndex, shaderHandles.VertexShaderIndex);
            GL.DetachShader(programIndex, shaderHandles.FragmentShaderIndex);
        }

        protected override void DisposeOf(Material loaded)
        {
            GL.DeleteProgram(loaded.ProgramIndex);
        }
    }
}
