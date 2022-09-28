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
using System.Threading;
// Low, Kok Wei (M214391)

namespace AstronomicalProcessing
{
    public partial class AstronomicalProcessing : Form
    {
        //Globals
        private IAstroContract pipeProxy;
        public AstronomicalProcessing()
        {
            InitializeComponent();
            CreateNamedPipeChannel();            
        }

        private void CreateNamedPipeChannel()
        {
            ChannelFactory<IAstroContract> pipeFactory =
                new ChannelFactory<IAstroContract>(
                    new NetNamedPipeBinding(),
                    new EndpointAddress("net.pipe://localhost/PipeAstro"));

            pipeProxy = pipeFactory.CreateChannel();
        }

        private void btStarVelocity_Click(object sender, EventArgs e)
        {
            double d1 = Convert.ToDouble(tbStarVelocityObservedWaveIn.Text);
            double d2 = Convert.ToDouble(tbStarVelocityRestWaveIn.Text);                              
            ShowListViewAstroProcessing(0, pipeProxy.MeasureStarVelocity(d1, d2).ToString());
        }        

        private void btStarDistance_Click(object sender, EventArgs e)
        {
            double d1 = Convert.ToDouble(tbStarDistanceIn.Text);
            ShowListViewAstroProcessing(1, pipeProxy.MeasureStarDistance(d1).ToString());
        }

        private void btTemperature_Click(object sender, EventArgs e)
        {   
            double d1 = Convert.ToDouble(tbTemperatureIn.Text);
            ShowListViewAstroProcessing(2, pipeProxy.MeasureTemperature(d1).ToString());
        }

        private void btEventHorizon_Click(object sender, EventArgs e)
        {
            double d1 = Convert.ToDouble(tbEventHorizonBlackholeIn.Text);
            int i1 = Convert.ToInt32(tbEventHorizonPowerIn.Text);            
            ShowListViewAstroProcessing(3, pipeProxy.MeasureEventHorizon(d1, i1).ToString());
        }

        private void ShowListViewAstroProcessing(int index, string output)
        {
            if (index == 0)
            {
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { output, null, null, null }));
            }
            else if (index == 1)
            {
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { null, output, null, null }));
            }
            else if (index == 2)
            {
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { null, null, output, null }));
            }
            else
            {
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { null, null, null, output }));
            }
            listViewAstroProcessing.Items[listViewAstroProcessing.Items.Count-1].EnsureVisible();
        }
        private void btEnglish_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
        }
        private void btFrench_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
        }

        private void btGerman_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
        }
        private void ChangeLanguage(string language)
        {
            switch (language)
            {
                case "English":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    //this.BackgroundImage = Properties.Resources.Flag_of_US;
                    break;
                case "French":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");
                    //this.BackgroundImage = Properties.Resources.Flag_of_France;
                    break;
                case "German":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
                    //this.BackgroundImage = Properties.Resources.Flag_of_Spain;
                    break;
            }
            Controls.Clear();
            InitializeComponent();
        }

        
    }
}
