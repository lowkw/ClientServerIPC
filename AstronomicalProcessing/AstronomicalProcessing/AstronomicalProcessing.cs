using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace AstronomicalProcessing
{
    public partial class AstronomicalProcessing : Form
    {
        public AstronomicalProcessing()
        {
            InitializeComponent();
            ChannelFactory<IAstroContract> pipeFactory =
                new ChannelFactory<IAstroContract>(
                    new NetNamedPipeBinding(),
                    new EndpointAddress("net.pipe://localhost/PipeAstro"));

            IAstroContract pipeProxy = pipeFactory.CreateChannel();
        }

        private void btStarVelocity_Click(object sender, EventArgs e)
        {
            pipeProxy.
        }
    }
}
