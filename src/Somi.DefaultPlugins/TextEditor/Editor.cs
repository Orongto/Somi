using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.CodeAnalysis;
using Nurose.Text;
using Somi.Core;
using Somi.Core.Graphics;
using Somi.UI;
using Color = Somi.Core.Graphics.Color;

namespace Somi.DefaultPlugins
{
    public class CachedWordMesh
    {
        public Mesh Mesh;
        public int Width;
    }

    public class CachedLineRenderer
    {
        public Dictionary<string, CachedWordMesh> cachedMeshes = new();
        private TextDrawableGenerator generator;
        private int lineHeight = 21;
        private int lineMargin = 0;
        private string[] keywords = new[] {"public", "private", "namespace", "using", "static", "void", "class", "string", "new"};
        private string[] keywords2 = new[] {"(",")","[","]",";", "\"", ".", ","};
        public CachedLineRenderer()
        {
            generator = new TextDrawableGenerator(new BMFont("Resources/Fonts/cascadia.fnt"));
            generator.Text = "public static void Somi()";
            generator.Color = Color.White;
            generator.ScaleStyle = ScaleStyle.ForceLineHeight;
        }

        public void Render(string[] lines, Vector2I Offset)
        {
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                var words = line.InclusiveSplit("\\s|\\[|\\]|\\(|\\)|\\.|\\,").ToArray();

                int xPos = 0;
                foreach (var word in words)
                {
                    if (!cachedMeshes.ContainsKey(word))
                    {
                        generator.Text = word;
                        var newmesh = new Mesh();
                        generator.UpdateBuffer(newmesh);
                        cachedMeshes.Add(word, new CachedWordMesh {Mesh = newmesh, Width = generator.CalcTextWidth(lineHeight, word)});
                    }

                    var cachedWordMesh = cachedMeshes[word];

                    var color = Color.Greyscale(.8f);
                    
                    if (keywords.Contains(word.Trim()))
                        color = Color.Magenta;
                    
                    if (keywords2.Contains(word.Trim()))
                        color = Color.Green;
                    
                    var position = new Vector2(Offset.X + xPos, Offset.Y + i * (lineHeight + lineMargin));
                    Application.RenderQueue.Add(new Drawable(cachedWordMesh.Mesh, generator.BMFont.Texture,
                        new TransformData(position, Vector2.One * lineHeight).CalcModelMatrix(), color));
                    
                    xPos += cachedWordMesh.Width;
                }
            }
        }
    }


    public class Editor : UIElement
    {
        private string[] lines;
        private List<Vector2I> Cursors;
        private string currentSelectedFilePath;
        private CachedLineRenderer lineRenderer;

        public Editor()
        {
            lineRenderer = new CachedLineRenderer();
        }

        public void Start()
        {
            EditorWideEvents.OnSelectedFileChanged += EditorWideEventsOnOnSelectedFileChanged;
        }

        private void EditorWideEventsOnOnSelectedFileChanged(object? sender, PathEventArgs e)
        {
            currentSelectedFilePath = e.Path;
        }


        public override void Draw()
        {
            lines =
                @"using System;

namespace Somi.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            new Editor().Draw();
            Console.WriteLine(""Hello World!"");
        }
    }
}
".Split('\n');
/*
            var tree = CSharpSyntaxTree.ParseText(String.Join('\n', lines));
            var root = tree.GetCompilationUnitRoot();
          //  new Walker().Visit(root);
            foreach (var child in root.ChildNodes())
            {
                Render(child);
            }
*/


            var drawable1 = new Drawable(
                PrimitiveMeshes.Quad,
                Texture.LoadFromFile("testImage.png"),
                new TransformData(new Vector2(10, 10), new Vector2(100, 100)).CalcModelMatrix());


            RenderQueue.DrawRect(Position, Size, new Color(26, 24, 29));


            //if (IsClicked)
         //       RenderQueue.DrawRect(Position, Size, Color.Green);


            lineRenderer.Render(lines, Position + new Vector2I(1, 1));

            //  var drawable = generator.GetDrawable(new TransformData(Position, Vector2.One * 21).CalcModelMatrix());
            // RenderQueue.Add(drawable);

            //RenderQueue.Add(drawable1);
        }

        private void Render(SyntaxNode node)
        {
            if (node.ChildNodes().Any())
            {
                foreach (var c in node.ChildNodes())
                {
                    Render(c);
                }
            }
            else
            {
                var tokens = node.GetAnnotatedNodesAndTokens().Select(s => s.Span);
                var text = node.GetText();
                int c = 5;
            }
        }
    }
}