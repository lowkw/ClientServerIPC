using System;
// Low, Kok Wei (M214391)
namespace AstroMath
{
    /** 
    * <summary>A custom third-party library created with four astronomical equations </summary>
    */
    public class AstroFormula
    {
        const int c = 299792458;
        /** 
         * <summary>Star Velocity: To measure the Star velocity using the Doppler shift.</summary>
         * <param name="observedWaveLength">Observed Wavelength of type double.</param>
         * <param name="restWaveLength">Rest Wavelength of type double.</param>
         * <returns>Return a double which represents the velocity in metres per second.</returns>
         */
    public double MeasureStarVelocity(double observedWaveLength, double restWaveLength)
        {
            double V;
            double changedWaveLength = observedWaveLength - restWaveLength;
            return V = c * (changedWaveLength / restWaveLength);
        }

        /** 
         * <summary>Star Distance: To measure the star distance using the parallax angle.
         * The parallax angle is measured at two different points and works on nearby stars.
         * </summary>
         * <param name="parallaxAngle">Arcseconds angle of type double.</param>         
         * <returns>Return a double value Distance in parsecs.</returns>
         */
        public double MeasureStarDistance(double parallaxAngle)
        {
            double D;
            return D = 1 / parallaxAngle;
        }
        /**
         * <summary>
         * Temperature in Kelvin: The Kelvin temperature scale is the primary temperature used in science 
         * and is easily converted from Celsius. 
         * </summary>
         * <param name = "celsius" > Temperature in Celsius of type double.</param>         
         * <returns>Return a double which is the temperature in degrees Kelvin and must be greater than 0.</returns>          
         */
        public double MeasureTemperature(double celsius)
        {
            double K;
            return K = celsius + 273;
        }
        /**
         * <summary>
         * Event Horizon (Schwarzschild Radius): To measure the distance from the centre of a blackhole 
         * to the event horizon. 
         * </summary>
         * <param name = "blackholeMass" > Blackhole Mass of type double.</param>
         * <param name="powersOf10"> Measured in kilograms</param>
         * <returns> Return a double which is Schwarzschild radius (R) in meters.</returns>
         */
        public double MeasureEventHorizon(double blackholeMass, int powersOf10)
        {
            double gravityConstant = 6.674 * Math.Pow(10, -11);
            double R;
            return R = (2 * gravityConstant * (blackholeMass * Math.Pow(10, powersOf10))) / Math.Pow(c, 2);
        }
    }
}