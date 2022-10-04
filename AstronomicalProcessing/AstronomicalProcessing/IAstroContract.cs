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
        /*
         * 7.1	
         * Create the ServiceContract called “IAstroContract.cs” which will need 
         * to be identical to the server without a reference to the AstroMath.DLL.
         */
        [OperationContract]
        double MeasureStarVelocity(double observedWaveLength, double restWaveLength);
        [OperationContract]
        double MeasureStarDistance(double parallaxAngle);
        [OperationContract]
        double MeasureTemperature(double celsius);
        [OperationContract]
        double MeasureEventHorizon(double blackholeMass, int powersOf10);
    }
}
