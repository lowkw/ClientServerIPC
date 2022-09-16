using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
// Low, Kok Wei (M214391)

namespace AstronomicalProcessing
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AstronomicalProcessing());

            ChannelFactory<IAstroContract> pipeFactory =
                new ChannelFactory<IAstroContract>(
                    new NetNamedPipeBinding(),
                    new EndpointAddress("net.pipe://localhost/PipeAstro"));

            IAstroContract pipeProxy = pipeFactory.CreateChannel();
        }
    }
}
