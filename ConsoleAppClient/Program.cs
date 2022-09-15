using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
// Low, Kok Wei (M214391)
namespace ConsoleAppClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client Started");
            ChannelFactory<IAstroContract> pipeFactory =
                new ChannelFactory<IAstroContract>(
                    new NetNamedPipeBinding(),
                    new EndpointAddress("net.pipe://localhost/PipeAstro"));

            IAstroContract pipeProxy = pipeFactory.CreateChannel();      

            while (true)
            {
                Console.WriteLine("Press Y to continue or N to exit.");
                string str = Console.ReadLine();
                if (str == "N")
                {
                    break;
                }
                Console.WriteLine("pipe: " + pipeProxy.MeasureStarVelocity(500.1, 500.0));
                Console.WriteLine("pipe: " + pipeProxy.MeasureStarDistance(0.547));
                Console.WriteLine("pipe: " + pipeProxy.MeasureTemperature(27.0));
                Console.WriteLine("pipe: " + pipeProxy.MeasureEventHorizon(8.2, 36));
            }
        }
    }
}
