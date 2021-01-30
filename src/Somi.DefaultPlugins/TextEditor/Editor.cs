﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Nurose.Text;
using Somi.Core;
using Somi.Core.Graphics;
using Somi.UI;
using Color = Somi.Core.Graphics.Color;

namespace Somi.DefaultPlugins
{
    public class Editor : UIElement
    {
        private string[] lines;
        private List<Vector2I> Cursors;
        private string currentSelectedFilePath;

        private TextDrawableGenerator generator;

        public Editor()
        {
            generator = new TextDrawableGenerator(new BMFont("Resources/Fonts/cascadia.fnt"));
            generator.Text = "Somi Editor. Waarom is de tekst zo krom? maybe door de font png.";
            generator.Color = Color.White;
            generator.ScaleStyle = ScaleStyle.ForceLineHeight;
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
                @"
using System;

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
            
            
           // if (IsHovering)
           //     RenderQueue.DrawRect(Position, Size, new Color(36, 24, 29));
            
            if (IsClicked)
                RenderQueue.DrawRect(Position, Size, new Color(36, 34, 39));
            
            var drawable = generator.GetDrawable(new TransformData(Position, Vector2.One * 32).CalcModelMatrix());
            RenderQueue.Add(drawable);

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