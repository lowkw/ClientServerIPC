using System;
// Low, Kok Wei (M214391)
namespace AstroMath
{
    public class AstroFormula
    {
        const int c = 299792458;
        public double MeasureStarVelocity(double observedWaveLength, double restWaveLength)
        {
            double V;
            double changedWaveLength = observedWaveLength - restWaveLength;
            return V = c * (changedWaveLength / restWaveLength);
        }

        public double MeasureStarDistance(double parallaxAngle)
        {
            double D;
            return D = 1 / parallaxAngle;
        }

        public double MeasureTemperature(double celsius)
        {
            double K;
            return K = celsius + 273;
        }
        public double MeasureEventHorizon(double blackholeMass, int kgPower)
        {
            double gravityConstant = 6.674 * Math.Pow(10, -11);
            double R;
            return R = (2 * gravityConstant * (blackholeMass * Math.Pow(10, kgPower))) / Math.Pow(c, 2);
        }
    }
}