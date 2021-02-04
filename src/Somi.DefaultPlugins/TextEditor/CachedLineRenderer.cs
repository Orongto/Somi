using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Nurose.Text;
using Somi.Core;
using Somi.Core.Graphics;

namespace Somi.DefaultPlugins
{
    public class CachedLineRenderer
    {
        public Dictionary<string, CachedWordMesh> cachedMeshes = new();
        private TextDrawableGenerator generator;
        private int lineHeight = 21;
        private int lineMargin = 0;
        private string[] keywords = new[] 
        {
            "var", "bool", "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong", "double", "float", "decimal",
            "string", "char", "void", "object", "typeof", "sizeof", "null", "true", "false", "if", "else", "while", "for", "foreach", "do", "switch",
            "case", "default", "lock", "try", "throw", "catch", "finally", "goto", "break", "continue", "return", "public", "private", "internal",
            "protected", "static", "readonly", "sealed", "const", "fixed", "stackalloc", "volatile", "new", "override", "abstract", "virtual",
            "event", "extern", "ref", "out", "in", "is", "as", "params", "__arglist", "__makeref", "__reftype", "__refvalue", "this", "base",
            "namespace", "using", "class", "struct", "interface", "enum", "delegate", "checked", "unchecked", "unsafe", "operator", "implicit", "explicit"
        };
        private string[] keywords2 = new[] {"(", ")", "[", "]", ";", "\"", ".", ",", "=", "<", ">", "!", "*", "-", "/"};

        public CachedLineRenderer()
        {
            generator = new TextDrawableGenerator(new BMFont("Resources/Fonts/cascadia.fnt"));
            generator.Color = Color.White;
            generator.ScaleStyle = ScaleStyle.ForceLineHeight;
        }

        public void Render(string[] lines, Vector2I Offset)
        {

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var fullOffsetY = Offset.Y + i * (lineHeight + lineMargin);
                
                if(fullOffsetY > Application.Window.Size.Y  || fullOffsetY < 0)
                    continue;
                
                
                var words = line.InclusiveSplit("\\s|\\[|\\]|\\(|\\)|\\.|\\,").ToArray();

                int xPos = 35;
 
                
                foreach (var word in words)
                {
                    var cachedWordMesh = GetCachedMesh(word);

                    var color = Color.Greyscale(.8f);

                    if (keywords.Contains(word.Trim()))
                        color = new Color(.7f,.2f,.9f);

                    if (keywords2.Contains(word.Trim()))
                        color = new Color(.3f,.5f,.9f);

                    var position = new Vector2(Offset.X + xPos, fullOffsetY);
                    
                    Application.RenderQueue.Add(new Drawable(cachedWordMesh.Mesh, generator.BMFont.Texture,
                        new TransformData(position, Vector2.One * lineHeight).CalcModelMatrix(), color));

                    
                    
                    xPos += cachedWordMesh.Width;
                }
                Application.RenderQueue.Add(new Drawable(GetCachedMesh(i.ToString()).Mesh, generator.BMFont.Texture,
                    new TransformData(new Vector2(Offset.X, fullOffsetY), Vector2.One * lineHeight).CalcModelMatrix(), Color.Greyscale(.9f).WithAlpha(.2f)));
            }
        }

        private CachedWordMesh GetCachedMesh(string word)
        {
            if (!cachedMeshes.ContainsKey(word))
            {
                generator.Text = word;
                generator.Color = Color.White;
                var newmesh = new Mesh();
                generator.UpdateBuffer(newmesh);
                cachedMeshes.Add(word, new CachedWordMesh {Mesh = newmesh, Width = generator.CalcTextWidth(lineHeight, word)});
            }

            var cachedWordMesh = cachedMeshes[word];
            return cachedWordMesh;
        }
    }
}