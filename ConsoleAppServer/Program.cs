using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
// Low, Kok Wei (M214391)
namespace ConsoleAppServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * 6.3	
             * Create a ServiceHost and NetNamedPipeBinding in the “Program.cs” file 
             * to provide connection for the client(s). Ensure to add a Console.ReadLine 
             * to pause the program during operation.
             */
            using (ServiceHost host = new ServiceHost(typeof(AstroServer),
                    new Uri[]
                    {
                        new Uri("net.pipe://localhost")
                    }))
            {
                host.AddServiceEndpoint(typeof(IAstroContract),
                    new NetNamedPipeBinding(), "PipeAstro");
                host.Open();
                Console.WriteLine("Server service is available. " + "Press <ENTER> to exit.");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
