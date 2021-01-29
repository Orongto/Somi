using OpenTK.Graphics.OpenGL;
using System;

namespace Somi.OpenTK.Drawing
{
    internal struct ShaderCompiler
    {
        public static bool TryCompileShader(int index, string code)
        {
            CompileShader(index, code);

            GL.GetShaderInfoLog(index, out string info);
            GL.GetShader(index, ShaderParameter.CompileStatus, out int compilationResult);

            bool compilationFailed = compilationResult == (int)All.False;

            if (compilationFailed)
            {
                //TODO Logger
                Console.WriteLine($"Shader {index} error:\n{info}");
                GL.DeleteShader(index);
            }

            return !compilationFailed;
        }

        private static void CompileShader(int index, string code)
        {
            GL.ShaderSource(index, code);
            GL.CompileShader(index);
        }
    }
}
