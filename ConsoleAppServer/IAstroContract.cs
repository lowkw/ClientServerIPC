using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
// Low, Kok Wei (M214391)
namespace ConsoleAppServer
{
    [ServiceContract]
    internal interface IAstroContract
    {
        /*
         * 6.1	
         * Create the ServiceContract file called “IAstroContract.cs” which will 
         * require an Interface that references the AstroMath.DLL and 
         * four OperationContract (one for each calculation). 
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
