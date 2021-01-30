namespace Somi.UI
{
    public class HeightRatioConstraint : IConstraint
    {
        public float Ratio;

        public HeightRatioConstraint(float ratio)
        {
            Ratio = ratio;
        }

        public void Calculate(ConstraintParameters parameters)
        {
            parameters.CurrentValue += (((float)parameters.ParentSize.Y * Ratio) );
        }
    }
}