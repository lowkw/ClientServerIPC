using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
// Low, Kok Wei (M214391)

namespace AstronomicalProcessing
{
    [ServiceContract]
    internal interface IAstroContract
    {
        [OperationContract]
        double MeasureStarVelocity(double observedWaveLength, double restWaveLength);
        [OperationContract]
        double MeasureStarDistance(double parallaxAngle);
        [OperationContract]
        double MeasureTemperature(double celsius);
        [OperationContract]
        double MeasureEventHorizon(double blackholeMass, int kgPower);
    }
}
