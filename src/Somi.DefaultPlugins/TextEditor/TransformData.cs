using System;
using System.Numerics;

namespace Somi.DefaultPlugins
{
    [Serializable]
    public struct TransformData
    {
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public Vector2 RotationPivot { get; private set; }
        public Vector2 Size { get; private set; }

        public static TransformData Zero => new TransformData() { Position = Vector2.Zero, Size = Vector2.One };

        public TransformData WithPosition(Vector2 pos)
        {
            Position = pos;
            return this;
        }

        public TransformData OffsetPosition(Vector2 offset)
        {
            Position += offset;
            return this;
        }


        public TransformData(Vector2 position, Vector2 size, float rotation = 0, Vector2 rotationPivot = default)
        {
            Position = position;
            Size = size;
            Rotation = rotation;
            RotationPivot = rotationPivot;
        }

        public Matrix4x4 CalcModelMatrix()
        {
            Matrix4x4 rotation = Matrix4x4.CreateRotationX(-(Rotation * Utils.DegreeToRadian));

            Matrix4x4 translation = Matrix4x4.CreateTranslation(Position.X, Position.Y,0);

            Matrix4x4 scale = Matrix4x4.CreateScale(Size.X, Size.Y,0);

            if (Rotation == 0)
            {
                return scale * translation;
            }

            Matrix4x4 rotationPivot = Matrix4x4.CreateTranslation(RotationPivot.X, RotationPivot.Y,0);

            Matrix4x4 rotationPivotInvert = Matrix4x4.CreateTranslation(-RotationPivot.X, -RotationPivot.Y,0);

            return scale * rotationPivotInvert * rotation * rotationPivot * translation;
        }

        public Matrix4x4 CalcViewMatrix()
        {
            Matrix4x4 translation = Matrix4x4.CreateTranslation(-Position.X, -Position.Y, 0);
            Matrix4x4 scale = Matrix4x4.CreateScale(Size.X);
            Matrix4x4 rotation = Matrix4x4.CreateRotationZ(Rotation * Utils.DegreeToRadian);
            return translation * rotation * scale;
        }

        public TransformData WithRotation(float value)
        {
            Rotation = value;
            return this;
        }

        public TransformData WithSize(Vector2 value)
        {
            Size = value;
            return this;
        }

        public TransformData WithRotationPivot(Vector2 value)
        {
            RotationPivot = value;
            return this;
        }
    }
}