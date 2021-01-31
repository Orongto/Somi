using OpenTK.Graphics.OpenGL;
using Somi.Core.Graphics;
using System;

namespace Somi.OpenTK.Drawing
{
    internal class TextureCache : Cache<Texture, LoadedTexture>
    {
        protected override LoadedTexture CreateNew(Texture raw)
        {
            GenerateGLTexture(raw, out var textureIndex);
            WriteData(raw, 4);

            return new LoadedTexture
            {
                Index = textureIndex
            };
        }

        private static void GenerateGLTexture(Texture raw, out int textureIndex)
        {
            textureIndex = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureIndex);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)raw.MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)raw.MagFilter);

        }

        private void WriteData(Texture raw, int componentCount)
        {
            var data = new byte[raw.Width * raw.Height * componentCount];
            int i = 0;
            foreach (var pixel in raw.GetAllPixels())
            {
                data[i + 0] = floatComponentToByte(pixel.R);
                data[i + 1] = floatComponentToByte(pixel.G);
                data[i + 2] = floatComponentToByte(pixel.B);
                data[i + 3] = floatComponentToByte(pixel.A);

                i += componentCount;
            }
            SetTextureData(data, raw);

            byte floatComponentToByte(float v)
            {
                return (byte)MathF.Round(v * 255);
            }
        }

        private static void SetTextureData(byte[] data, Texture raw)
        {
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba8,
                raw.Width,
                raw.Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                data);
        }

        protected override void DisposeOf(LoadedTexture loaded)
        {

        }
    }
}
