namespace Somi.UI
{
    public class PixelConstraint : IConstraint
    {
        readonly int Offset;

        public PixelConstraint(int offset)
        {
            Offset = offset;
        }

        public void Calculate(ConstraintParameters parameters)
        {
            parameters.CurrentValue += Offset * parameters.Scale;
        }
    }
}