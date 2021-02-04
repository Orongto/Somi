using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Microsoft.CodeAnalysis;
using Somi.Core;
using Somi.Core.Graphics;
using Somi.UI;
using Color = Somi.Core.Graphics.Color;

namespace Somi.DefaultPlugins
{
    public class Editor : UIElement
    {
        private string[] lines = new []{"Geen file path gekozen. Zet je program argument naar een file path.", "Later maken we wel een mooie home screen.", "", "[S O M I] !(>=) 6 / 2"};
        private List<Vector2I> Cursors;
        private string currentSelectedFilePath;
        private CachedLineRenderer lineRenderer;
        private Vector2 offset;
        private Vector2 scrollTarget;


        private float scrollLerpSpeed = 40;
        private int lineHeight = 21;
        private float scrollAmount = 21 * 2;

        public Editor()
        {
            lineRenderer = new CachedLineRenderer();
        }

        public override void Draw()
        {
            if (currentSelectedFilePath != Application.Context.SelectedFilePath)
            {
                currentSelectedFilePath = Application.Context.SelectedFilePath;
                lines = File.ReadAllLines(currentSelectedFilePath);
            }


            scrollTarget += new Vector2(0, Application.Input.MouseWheelDelta * scrollAmount);
            
            RenderQueue.DrawRect(Position, Size, new Color(26, 24, 29));

            if (lines != null)
            {
                var clamped = new Vector2(0, Utils.MinMax(-lines.Length * lineHeight + Window.Size.Y - Position.Y, 0, scrollTarget.Y));
                scrollTarget = Utils.Lerp(scrollTarget, clamped, Window.DeltaTime * scrollLerpSpeed);

                offset = Utils.Lerp(offset, scrollTarget, 15.3f * Window.DeltaTime);


                var roundedDown = new Vector2I((int) MathF.Floor(offset.X), (int) MathF.Floor(offset.Y));
                lineRenderer.Render(lines, Position + new Vector2I(1, 1) + roundedDown);
            }
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