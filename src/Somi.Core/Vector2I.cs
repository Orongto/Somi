using System.Numerics;

namespace Somi.Core
{
    public struct Vector2I
    {
        public int X;
        public int Y;

        public Vector2I(int x, int y) : this()
        {
            X = x;
            Y = y;
        }
        
        public override bool Equals(object obj)
        {
            return obj is Vector2I && Equals((Vector2I)obj);
        }


        public bool Equals(Vector2I other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        
        
        public static bool operator ==(Vector2I left, Vector2I right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2I left, Vector2I right)
        {
            return !(left == right);
        }

        public static Vector2I operator +(Vector2I v1, Vector2I v2) => new Vector2I(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2I operator -(Vector2I v1, Vector2I v2) => new Vector2I(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2I operator *(Vector2I v1, Vector2I v2) => new Vector2I(v1.X * v2.X, v1.Y * v2.Y);
        public static Vector2I operator /(Vector2I v1, Vector2I v2) => new Vector2I(v1.X / v2.X, v1.Y / v2.Y);

        public static Vector2I operator /(Vector2I v1, int i) => new Vector2I(v1.X / i, v1.Y / i);
        public static Vector2I operator *(Vector2I v1, int i) => new Vector2I(v1.X * i, v1.Y * i);
        public static Vector2I operator -(Vector2I v1) => new Vector2I(-v1.X, -v1.Y);
        
        
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static implicit operator Vector2(Vector2I v)
        {
            return new Vector2(v.X, v.Y);
        }
        
        public static explicit operator Vector2I(Vector2 v)
        {
            return new Vector2I((int)v.X, (int)v.Y);
        }
    }
}
