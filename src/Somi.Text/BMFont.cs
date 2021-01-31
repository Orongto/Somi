using System.Collections.Generic;
using System.IO;
using Somi.Core.Graphics;

namespace Nurose.Text
{
    public class BMFont
    {
        public Texture Texture { get; private set; }
        internal FontFile FontFile;
        private Dictionary<char, FontChar> fontchars = new();

        internal FontChar FindChar(char c)
        {
            fontchars.TryGetValue(c, out FontChar val);
            return val;
        }

        public BMFont(string path)
        {
            FontFile = FontLoader.Load(path);
            if (FontFile is null)
            {
                return;
            }

            Texture = Texture.LoadFromFile(Path.GetDirectoryName(path) + "\\" + FontFile.Pages[0].File);
           // Texture.MinFilter = TextureFilter.Linear;            
            //Texture.MagFilter = TextureFilter.Linear;            
            foreach (var c in FontFile.Chars)
            {
                fontchars.Add((char)c.ID, c);
            }

        }
    }
}