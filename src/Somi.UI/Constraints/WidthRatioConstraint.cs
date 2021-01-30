namespace Somi.UI
{
    public class WidthRatioConstraint : IConstraint
    {
        public float Ratio;

        public WidthRatioConstraint(float ratio)
        {
            Ratio = ratio;
        }

        public void Calculate(ConstraintParameters parameters)
        {
            parameters.CurrentValue += (((float)parameters.ParentSize.X * Ratio) );
        }
    }
}