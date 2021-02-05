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
        private string[] lines = new[]
            {"Geen file path gekozen. Zet je program argument naar een file path.", "Later maken we wel een mooie home screen.", "", "[S O M I] !(>=) 6 / 2"};

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
            Cursors = new List<Vector2I>() {new Vector2I(0, 24 - 1)};
        }

        public override void Draw()
        {
            LoadLinesIfNeeded();

            DrawBackground();

            if (lines != null)
            {
                UpdateScrollOffset();
                DrawLines();

                UpdateCursors();
                DrawCursors();
            }
        }

        private void DrawCursors()
        {
            foreach (var loc in Cursors)
            {
                var pos = lineRenderer.GetRenderPositionOfCoordinates(lines, GetValidCursorPos(loc)) + GetRoundedOffset() + Position;
                RenderQueue.DrawRect(pos, new Vector2I(2, lineHeight), Color.White);
            }
        }

        private void UpdateCursors()
        {
            for (var index = 0; index < Cursors.Count; index++)
            {
                var cursorLoc = Cursors[index];

                var newLoc = new Vector2I();
                
                if (Window.Input.IsKeyPressed(Key.Left))
                    newLoc += new Vector2I(-1, 0);
                if (Window.Input.IsKeyPressed(Key.Right))
                    newLoc += new Vector2I(1, 0);
                if (Window.Input.IsKeyPressed(Key.Up))
                    newLoc += new Vector2I(0, -1);
                if (Window.Input.IsKeyPressed(Key.Down))
                    newLoc += new Vector2I(0, 1);

                newLoc += cursorLoc;

                var newX = newLoc.X;
                if ((newLoc - cursorLoc).X != default)
                {
                    cursorLoc = GetValidCursorPos(cursorLoc);
                    newX = Utils.MinMax(0, lines[cursorLoc.Y].Length, newLoc.X);
                }

                  var newY = Utils.MinMax(0, lines.Length, newLoc.Y);
                  cursorLoc = new Vector2I(newX, newY);

                  Cursors[index] = cursorLoc;
            }
        }

        private Vector2I GetValidCursorPos(Vector2I cursorLoc)
        {
            var newX = Utils.MinMax(0, lines[cursorLoc.Y].Length, cursorLoc.X);
            var newY = Utils.MinMax(0, lines.Length, cursorLoc.Y);
            return new Vector2I(newX, newY);
        }

        private void DrawBackground()
        {
            RenderQueue.DrawRect(Position, Size, new Color(26, 24, 29));
        }

        private void LoadLinesIfNeeded()
        {
            if (currentSelectedFilePath != Application.Context.SelectedFilePath)
            {
                currentSelectedFilePath = Application.Context.SelectedFilePath;
                lines = File.ReadAllLines(currentSelectedFilePath);
            }
        }

        private void DrawLines()
        {
            var roundedDown = GetRoundedOffset();
            lineRenderer.Render(lines, Position + new Vector2I(1, 1) + roundedDown);
        }

        private Vector2I GetRoundedOffset()
        {
            return new Vector2I((int) MathF.Round(offset.X), (int) MathF.Round(offset.Y));
        }

        private void UpdateScrollOffset()
        {
            scrollTarget += new Vector2(0, Application.Input.MouseWheelDelta * scrollAmount);

            var clamped = new Vector2(0, Utils.MinMax(-lines.Length * lineHeight + Window.Size.Y - Position.Y, 0, scrollTarget.Y));
            scrollTarget = Utils.Lerp(scrollTarget, clamped, Window.DeltaTime * scrollLerpSpeed);

            offset = Utils.Lerp(offset, scrollTarget, 20.0f * Window.DeltaTime);

            if (Utils.Distance(offset, scrollTarget) < 1f)
            {
                offset = scrollTarget;
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