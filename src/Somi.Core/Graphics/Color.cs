using System;

namespace Somi.Core.Graphics
{
    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color(float r, float g, float b, float a = 1)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            R = r / 255f;
            G = g / 255f;
            B = b / 255f;
            A = a / 255f;
        }

        public Color WithAlpha(float alpha) => new Color(R, G, B, alpha);

        public override string ToString()
        {
            return $"({R},{G},{B},{A})";
        }

        public override bool Equals(object obj)
        {
            return obj is Color color &&
                   R == color.R &&
                   G == color.G &&
                   B == color.B &&
                   A == color.A;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public static readonly Color Transparent = new Color(0, 0, 0, 0);
        public static readonly Color White = new Color(1f, 1, 1, 1);
        public static readonly Color Grey = new Color(0.5f, 0.5f, 0.5f, 1);
        public static readonly Color Black = new Color(0f, 0, 0, 1);
        public static readonly Color Red = new Color(1f, 0, 0);
        public static readonly Color Blue = new Color(0, 0, 1f);
        public static readonly Color Green = new Color(0, 1f, 0);
        public static readonly Color Magenta = new Color(1f, 0, 1f);

        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        public static Color operator *(Color left, Color right)
        {
            return new Color(
                left.R * right.R,
                left.G * right.G,
                left.B * right.B,
                left.A * right.A
                );
        }

        public static Color operator *(float v, Color c)
        {
            return new Color(
                c.R * v,
                c.G * v,
                c.B * v,
                c.A * v
                );
        }

        public static Color operator *(Color c, float v)
        {
            return new Color(
                c.R * v,
                c.G * v,
                c.B * v,
                c.A * v
                );
        }

        public static bool operator >(Color left, Color right)
        {
            return
                left.R > right.R &&
                left.G > right.G &&
                left.B > right.B &&
                left.A > right.A;
        }

        public static bool operator <(Color left, Color right)
        {
            return
                left.R < right.R &&
                left.G < right.G &&
                left.B < right.B &&
                left.A < right.A;
        }
        public static bool operator >=(Color left, Color right)
        {
            return
                left.R >= right.R &&
                left.G >= right.G &&
                left.B >= right.B &&
                left.A >= right.A;
        }

        public static bool operator <=(Color left, Color right)
        {
            return
                left.R <= right.R &&
                left.G <= right.G &&
                left.B <= right.B &&
                left.A <= right.A;
        }
    }
}
