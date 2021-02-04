using System;
using System.Drawing;
using System.Numerics;
using Somi.Core;
using Color = Somi.Core.Graphics.Color;

namespace Somi.DefaultPlugins
{
    public static class Utils
    {
        public static Random RandomInstance { get; set; } = new Random();

        public const float Pi = (float) Math.PI;
        public const float Tau = 2 * Pi;
        public const float DegreeToRadian = Pi / 180f;
        public const float RadianToDegree = 180f / Pi;


        public static void SetRandomSeed(int seed)
        {
            RandomInstance = new Random(seed);
        }

        public static float Sin(float f)
        {
            return (float) Math.Sin(f);
        }

        public static float Cos(float f)
        {
            return (float) Math.Cos(f);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            var c = a - b;
            return (float) Math.Sqrt(c.X * c.X + c.Y * c.Y);
        }

        public static float Distance(Vector2I a, Vector2I b)
        {
            var c = a - b;
            return (float) Math.Sqrt((float) c.X * (float) c.X + (float) c.Y * (float) c.Y);
        }

        public static float Abs(float a)
        {
            return Math.Abs(a);
        }

        public static Vector2 RandomPointInUnitCircle()
        {
            float a = RandomFloat() * 2 * Pi;
            return RadianToVector(a) * RandomFloat();
        }

        public static float Difference(float a, float b)
        {
            return Math.Abs(a - b);
        }

        public static float Atan2(float a, float b)
        {
            return (float) Math.Atan2(a, b);
        }

        public static float Remainder(float a)
        {
            return a - Floor(a);
        }

        public static float Floor(float a)
        {
            return (float) Math.Floor(a);
        }

        public static Vector2 Floor(Vector2 v)
        {
            return new Vector2(Floor(v.X), Floor(v.Y));
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float LerpAngleDegrees(float a, float b, float c)
        {
            float result;
            float diff = b - a;
            if (diff < -180f)
            {
                b += 360f;
                result = Lerp(a, b, c);
                if (result >= 360f)
                {
                    result -= 360f;
                }
            }
            else if (diff > 180f)
            {
                b -= 360f;
                result = Lerp(a, b, c);
                if (result < 0f)
                {
                    result += 360f;
                }
            }
            else
            {
                result = Lerp(a, b, c);
            }

            return result;
        }

        public static float Sigmoid(double value)
        {
            return 1.0f / (1.0f + (float) Math.Exp(-value));
        }

        public static int MinMax(int min, int max, int value)
        {
            return Math.Min(Math.Max(min, value), max);
        }

        public static Vector2 MinMax(Vector2 min, Vector2 max, Vector2 value)
        {
            float x = Math.Min(Math.Max(min.X, value.X), max.X);
            float y = Math.Min(Math.Max(min.Y, value.Y), max.Y);
            return new Vector2(x, y);
        }

        public static float Min(float v1, float v2)
        {
            return Math.Min(v1, v2);
        }

        public static float Max(float v1, float v2)
        {
            return Math.Max(v1, v2);
        }

        public static Vector2 Multiply(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2 Divide(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        public static float MinMax(float min, float max, float value)
        {
            return Math.Min(Math.Max(min, value), max);
        }

        public static float Sqrt(float f)
        {
            return (float) Math.Sqrt(f);
        }

        public static Vector2 RadianToVector(float f)
        {
            return new Vector2(Cos(f), Sin(f));
        }

        public static float VectorToDegree(Vector2 vector)
        {
            return Atan2(vector.Y, vector.X) * RadianToDegree;
        }

        public static Vector2 DegreeToVector(float f)
        {
            float r = f * DegreeToRadian;
            return new Vector2(Cos(r), Sin(r));
        }

        public static float Lerp(float a, float b, float c)
        {
            return a * (1.0f - c) + b * c;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float c)
        {
            return a * (1.0f - c) + b * c;
        }
        
        public static Vector2I Lerp(Vector2I a, Vector2I b, float c)
        {
            return (Vector2I)Lerp((Vector2)a, (Vector2)b, c);
        }

        public static Color Lerp(Color a, Color b, float c)
        {
            var cR = Lerp(a.R, b.R, c);
            var cG = Lerp(a.G, b.G, c);
            var cB = Lerp(a.B, b.B, c);
            var cA = Lerp(a.A, b.A, c);
            return new Color(cR, cG, cB, cA);
        }

        public static float RandomFloat(float min, float max)
        {
            return Lerp(min, max, (float) RandomInstance.NextDouble());
        }

        public static float RandomFloat()
        {
            return (float) RandomInstance.NextDouble();
        }

        public static int RandomInt(int min, int max)
        {
            return RandomInstance.Next(min, max);
        }

        public static bool RandomBool()
        {
            return RandomFloat() > 0.5f;
        }

        public static bool RandomBool(float chance)
        {
            return RandomFloat() > MinMax(0, 1, 1 - chance);
        }

        public static Vector2 RandomVector2(float min, float max)
        {
            return new Vector2(RandomFloat(min, max), RandomFloat(min, max));
        }

        public static Vector2 RandomVector2(Vector2 min, Vector2 max)
        {
            return new Vector2(RandomFloat(min.X, max.X), RandomFloat(min.Y, max.Y));
        }

        public static Color RandomColor(float alpha = 1f)
        {
            return new Color(RandomFloat(), RandomFloat(), RandomFloat(), alpha);
        }

        public static Vector2 RotatePoint(Vector2 point, float degrees, Vector2 pivot = default)
        {
            float radians = degrees * DegreeToRadian;
            Vector2 offsetPoint = point - pivot;
            return pivot + new Vector2(
                Cos(radians) * offsetPoint.X - Sin(radians) * offsetPoint.Y,
                Cos(radians) * offsetPoint.Y + Sin(radians) * offsetPoint.X);
        }

        public static bool IsPointInsideBounds(Vector2I point, Vector2I min, Vector2I max)
        {
            if (point.X < min.X) return false;
            if (point.Y < min.Y) return false;

            if (point.X > max.X) return false;
            if (point.Y > max.Y) return false;

            return true;
        }

        public static bool IsPointInsideBounds(Vector2 point, Vector2 min, Vector2 max)
        {
            if (point.X < min.X) return false;
            if (point.Y < min.Y) return false;

            if (point.X > max.X) return false;
            if (point.Y > max.Y) return false;

            return true;
        }


        public static bool IsPointInsideBounds(Vector2 point, RectangleF rect)
        {
            if (point.X < rect.Left) return false;
            if (point.Y < rect.Top) return false;

            if (point.X > rect.Left + rect.Width) return false;
            if (point.Y > rect.Top + rect.Height) return false;

            return true;
        }
    }
}