using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualBasic.CompilerServices;
using Somi.Core.Graphics;

namespace Nurose.Text
{
    public enum ScaleStyle
    {
        ForceLineHeight,
        ForceSize
    }

    public class TextDrawableGenerator
    {
        public BMFont BMFont
        {
            get => bMFont;
            set
            {
                bMFont = value;
                needUpdate = true;
                UpdateFontInfo();
            }
        }

        public string Text
        {
            get => text;

            set
            {
                if (text == value)
                    return;

                text = value;
                needUpdate = true;
            }
        }
        
        public bool Centered
        {
            get => centered;
            set
            {
                centered = value;
                needUpdate = true;
            }
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                needUpdate = true;
            }
        }

        public ScaleStyle ScaleStyle
        {
            get => scaleStyle;
            set
            {
                scaleStyle = value;
                needUpdate = true;
            }
        }

        public float Depth { get; set; } = 5;


        private Color color = Color.White;
        private bool needUpdate = true;
        private bool centered = false;
        private bool invertY = true;
        private string text;

        private Vector2 imageSize;
        private Vector2 pixelSize;
        private ScaleStyle scaleStyle;
        private BMFont bMFont;
        private readonly Mesh mesh;

        public TextDrawableGenerator(BMFont fNTFont)
        {
            bMFont = fNTFont;
            UpdateFontInfo();

            mesh = new Mesh(Array.Empty<Vertex>());
        }

        public TextDrawableGenerator()
        {
            mesh = new Mesh(Array.Empty<Vertex>());
        }


        private void UpdateFontInfo()
        {
            imageSize = new Vector2(BMFont.FontFile.Common.ScaleW, BMFont.FontFile.Common.ScaleH);
            pixelSize = Vector2.One / imageSize;
        }

        public int CalcTextWidth(int lineheight, string text)
        {
            int width = 0;
            foreach (char c in text)
            {
                FontChar fontChar = BMFont.FindChar(c);
                width += fontChar.XAdvance;
            }

            return (int) (width / (float) BMFont.FontFile.Info.Size * lineheight);
        }

        private void UpdateBuffer()
        {
            Vertex[] vertices = new Vertex[Text.Length * 6];
            int currentX = 0;
            float maxX = 0;
            float maxHeight = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                char c = Text[i];
                FontChar fontChar = BMFont.FindChar(c) ?? BMFont.FindChar('?');

                if (fontChar.Height > maxHeight)
                    maxHeight = fontChar.Height;

                Vector2 startPos = new Vector2(fontChar.X, fontChar.Y);
                Vector2 size = new Vector2(fontChar.Width, fontChar.Height);
                Vector2 uvSize = pixelSize * size * 1;
                Vector2 uvStartPos = pixelSize * startPos;
                Vector2 Offset = new Vector2(currentX + fontChar.XOffset, fontChar.YOffset);

                float z = 0;

                var uvMin = pixelSize * new Vector2(fontChar.X, fontChar.Y);
                var uvMax = uvMin + new Vector2(fontChar.Width, fontChar.Height) * pixelSize;

                uvMin = new Vector2(uvMin.X, 1 - uvMin.Y);
                uvMax = new Vector2(uvMax.X, 1 - uvMax.Y);
                
                vertices[i * 6 + 0] = new Vertex(new Vector3(Offset + new Vector2(0, 0) * size, z),new Vector2(uvMin.X,uvMin.Y), Color);
                vertices[i * 6 + 1] = new Vertex(new Vector3(Offset + new Vector2(0, 1) * size, z),new Vector2(uvMin.X,uvMax.Y), Color);
                vertices[i * 6 + 2] = new Vertex(new Vector3(Offset + new Vector2(1, 1) * size, z),new Vector2(uvMax.X,uvMax.Y), Color);
                vertices[i * 6 + 3] = new Vertex(new Vector3(Offset + new Vector2(0, 0) * size, z),new Vector2(uvMin.X,uvMin.Y), Color);
                vertices[i * 6 + 4] = new Vertex(new Vector3(Offset + new Vector2(1, 0) * size, z),new Vector2(uvMax.X,uvMin.Y), Color);
                vertices[i * 6 + 5] = new Vertex(new Vector3(Offset + new Vector2(1, 1) * size, z),new Vector2(uvMax.X,uvMax.Y), Color);


    
                if (currentX + size.X > maxX)
                    maxX = currentX + size.X;

                currentX += fontChar.XAdvance;
            }

            if (centered)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i].Position -= new Vector3(maxX / 2f, 0, 0);
                }
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                switch (ScaleStyle)
                {
                    case ScaleStyle.ForceLineHeight:
                        vertices[i].Position = new Vector3(vertices[i].Position.X / BMFont.FontFile.Info.Size,
                            vertices[i].Position.Y / BMFont.FontFile.Info.Size, Depth);
                        break;
                    case ScaleStyle.ForceSize:
                        vertices[i].Position = new Vector3(vertices[i].Position.X / maxX, vertices[i].Position.Y / maxX, Depth);
                        break;
                }
            }

            mesh.Vertices = vertices;
            mesh.GenerateLinearIndices();
        }

        public Drawable GetDrawable(Matrix4x4 matrix)
        {
           if(needUpdate)
                UpdateBuffer();
            
            return new Drawable(mesh, bMFont.Texture, matrix , Color.White);
        }
    }
}