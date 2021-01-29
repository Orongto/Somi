using OpenTK.Graphics.OpenGL;
using Somi.Core.Graphics;
using System.Collections.Generic;
using System.Numerics;

namespace Somi.OpenTK.Drawing
{
    internal class Material
    {
        public readonly int ProgramIndex;

        public readonly Shader Shader;
        private readonly Dictionary<string, int> uniformLocations = new();
        private static readonly float[] matrixBuffer = new float[16];

        public Material(int programIndex, Shader shader)
        {
            ProgramIndex = programIndex;
            Shader = shader;
        }

        public int GetUniformLocation(string name)
        {
            if (uniformLocations.TryGetValue(name, out int loc))
                return loc;

            loc = GL.GetUniformLocation(ProgramIndex, name);
            uniformLocations.Add(name, loc);
            return loc;
        }

        //TODO alles heeft voor nu maar 1 texture nodig dus we hebben geen systeem om de gebruikte textureunits bij te houden.
        public void SetUniform(string name, Texture texture)
        {
            var loc = GetUniformLocation(name);
            var t = Storage.Textures.Load(texture);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, t.Index);
            GL.ProgramUniform1(ProgramIndex, loc, 0);
        }

        public void SetUniform(string name, Color color)
        {
            var loc = GetUniformLocation(name);
            GL.ProgramUniform4(ProgramIndex, loc, color.R, color.G, color.B, color.A);
        }

        public void SetUniform(string name, Matrix4x4 matrix)
        {
            var loc = GetUniformLocation(name);
            SetMatrixBuffer(matrix);
            GL.ProgramUniformMatrix4(ProgramIndex, loc, 1, false, matrixBuffer);
        }

        private void SetMatrixBuffer(Matrix4x4 v)
        {
            matrixBuffer[0] = v.M11;
            matrixBuffer[1] = v.M12;
            matrixBuffer[2] = v.M13;
            matrixBuffer[3] = v.M14;

            matrixBuffer[4] = v.M21;
            matrixBuffer[5] = v.M22;
            matrixBuffer[6] = v.M23;
            matrixBuffer[7] = v.M24;

            matrixBuffer[8] = v.M31;
            matrixBuffer[9] = v.M32;
            matrixBuffer[10] = v.M33;
            matrixBuffer[11] = v.M34;

            matrixBuffer[12] = v.M41;
            matrixBuffer[13] = v.M42;
            matrixBuffer[14] = v.M43;
            matrixBuffer[15] = v.M44;
        }
    }
}
