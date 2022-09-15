using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AstroMath;
// Low, Kok Wei (M214391)
namespace ConsoleAppServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class AstroServer : IAstroContract
    {
        AstroFormula formulaObj = new AstroFormula();
        const int c = 299792458;
        public double MeasureStarVelocity(double observedWaveLength, double restWaveLength)
        {
            return formulaObj.MeasureStarVelocity(observedWaveLength, restWaveLength);            
        }

        public double MeasureStarDistance(double parallaxAngle)
        {
            return formulaObj.MeasureStarDistance(parallaxAngle);
        }

        public double MeasureTemperature(double celsius)
        {
            return formulaObj.MeasureTemperature(celsius);
        }
        public double MeasureEventHorizon(double blackholeMass, int kgPower)
        {
            return formulaObj.MeasureEventHorizon(blackholeMass, kgPower);
        } 
    }
}
