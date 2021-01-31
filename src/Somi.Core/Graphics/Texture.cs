using System;
using System.Collections.Immutable;

namespace Somi.Core.Graphics
{
    public class Texture
    {
        public int Width { get; }
        public int Height { get; }

        public TextureFilter MinFilter = TextureFilter.Nearest;
        public TextureFilter MagFilter = TextureFilter.Nearest;
        
        private readonly Color[] pixels;

        public Texture(int width, int height, Color[] pixels)
        {
            Width = width;
            Height = height;
            this.pixels = pixels;
        }

        public ImmutableArray<Color> GetAllPixels() => pixels.ToImmutableArray();

        public static readonly Texture White = new Texture(1, 1, new Color[] { Color.White });

        //TODO dit is tijdelijk neem ik aan 2
        public static Texture LoadFromFile(string path, bool flipY = true)
        {
            var image = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(path, out _);

            Color[] pixels = new Color[image.Height * image.Width];
            int i = 0;
            for (int yy = 0; yy < image.Height; yy++)
            {
                int y = flipY ? (image.Height - 1 - yy) : yy;
                Span<SixLabors.ImageSharp.PixelFormats.Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);
                for (int x = 0; x < image.Width; x++)
                {
                    var c = pixelRowSpan[x];
                    pixels[i] = new Color(c.R, c.G, c.B, c.A);
                    i++;
                }
            }

            Texture tex = new Texture(image.Width, image.Height, pixels);
            return tex;
        }
    }
}
