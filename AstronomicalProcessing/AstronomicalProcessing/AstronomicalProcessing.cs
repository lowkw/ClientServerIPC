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
            try
            {
                ChannelFactory<IAstroContract> pipeFactory =
                    new ChannelFactory<IAstroContract>(
                        new NetNamedPipeBinding(),
                        new EndpointAddress("net.pipe://localhost/PipeAstro"));

                pipeProxy = pipeFactory.CreateChannel();
                if (pipeProxy != null)
                {
                    toolStripStatusLabel1.Text = "";
                }
            }
            catch (Exception ex)
            {                
                toolStripStatusLabel1.Text ="Server not connected";
            }
        }

        private void tbStarVelocityObservedWaveIn_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbStarVelocityObservedWaveIn.Text, out double result))
            {
                MessageBox.Show("Observed Wavelength must be a floating-point number only.");
            }
        }

        private void tbStarVelocityRestWaveIn_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbStarVelocityRestWaveIn.Text, out double result))
            {
                MessageBox.Show("Rest Wavelength must be a floating-point number only.");
            }
        }

        private void tbStarDistanceIn_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbStarDistanceIn.Text, out double result))
            {
                MessageBox.Show("Parallax Angle must be a floating-point number only.");
            }
        }

        private void tbTemperatureIn_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbTemperatureIn.Text, out double result))
            {
                MessageBox.Show("Celsius must be a floating-point number only.");
            }
            else if (double.Parse(tbTemperatureIn.Text) <= -273)
            {
                MessageBox.Show("Celsius must be greater than -273.");
            }
        }

        private void tbEventHorizonBlackholeIn_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(tbEventHorizonBlackholeIn.Text, out double result))
            {
                MessageBox.Show("Blackhole Mass must be a floating-point number only.");
            }
        }

        private void tbEventHorizonPowerIn_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tbEventHorizonPowerIn.Text, out int result))
            {
                MessageBox.Show("Powers of 10 must be an integer number only.");
            }
        }

        
        private void btStarVelocity_Click(object sender, EventArgs e)
        {
            if (validateDoubleInput("Observed Wavelength",tbStarVelocityObservedWaveIn))
            {
                double d1 = Convert.ToDouble(tbStarVelocityObservedWaveIn.Text);
                if (validateDoubleInput("Rest Wavelength",tbStarVelocityRestWaveIn))
                {
                    double d2 = Convert.ToDouble(tbStarVelocityRestWaveIn.Text);
                    if (d2 == 0)
                    {
                        MessageBox.Show("Rest Wavelength must not be 0.");
                    }
                    else
                    {
                        try
                        {
                            toolStripStatusLabel1.Text = "Server connected";
                            ShowListViewAstroProcessing(0, pipeProxy.MeasureStarVelocity(d1, d2));
                        }
                        catch (Exception ex)
                        {
                            CreateNamedPipeChannel();
                            toolStripStatusLabel1.Text = "Server not connected !";
                        }
                    }
                }
            }
        }        

        private void btStarDistance_Click(object sender, EventArgs e)
        {
            if (validateDoubleInput("Parallax Angle", tbStarDistanceIn))
            {
                double d1 = Convert.ToDouble(tbStarDistanceIn.Text);
                if (d1 == 0)
                {
                    MessageBox.Show("Parallax Angle must not be 0.");
                }
                else
                {
                    try
                    {
                        toolStripStatusLabel1.Text = "Server connected";
                        ShowListViewAstroProcessing(1, pipeProxy.MeasureStarDistance(d1));
                    }
                    catch (Exception ex)
                    {
                        CreateNamedPipeChannel();
                        toolStripStatusLabel1.Text = "Server not connected !";
                    }
                }
            }
        }

        private void btTemperature_Click(object sender, EventArgs e)
        {
            if (validateDoubleInput("Celsius", tbTemperatureIn))
            {
                double d1 = Convert.ToDouble(tbTemperatureIn.Text);
                if (double.Parse(tbTemperatureIn.Text) <= -273)
                {
                    MessageBox.Show("Celsius must be greater than -273.");
                }
                else
                {
                    try
                    {
                        toolStripStatusLabel1.Text = "Server connected";
                        ShowListViewAstroProcessing(2, pipeProxy.MeasureTemperature(d1));
                    }
                    catch (Exception ex)
                    {
                        CreateNamedPipeChannel();
                        toolStripStatusLabel1.Text = "Server not connected !";
                    }
                }
            }
        }

        private void btEventHorizon_Click(object sender, EventArgs e)
        {
            if (validateDoubleInput("Blackhole Mass", tbEventHorizonBlackholeIn))
            {
                double d1 = Convert.ToDouble(tbEventHorizonBlackholeIn.Text);
                if (!int.TryParse(tbEventHorizonPowerIn.Text, out int result))
                {
                    MessageBox.Show("Powers of 10 must be an integer number only.");
                }
                else
                {
                    int i1 = Convert.ToInt32(tbEventHorizonPowerIn.Text);
                    try
                    {
                        toolStripStatusLabel1.Text = "Server connected";
                        ShowListViewAstroProcessing(3, pipeProxy.MeasureEventHorizon(d1, i1));
                    }
                    catch (Exception ex)
                    {
                        CreateNamedPipeChannel();
                        toolStripStatusLabel1.Text = "Server not connected !";
                    }
                }
            }
        }

        private bool validateDoubleInput(string textBoxName, TextBox inputTextBox)
        {
            if (!double.TryParse(inputTextBox.Text, out double result))
            {
                MessageBox.Show(textBoxName + " must be a floating-point number only.");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ShowListViewAstroProcessing(int index, double output)
        {
            string result;
            if (index == 0)
            {
                result = String.Format("{0:#}m/s", output);
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { result, null, null, null }));
            }
            else if (index == 1)
            {
                result = String.Format("{0:#.00}parsecs", output);
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { null, result, null, null }));
            }
            else if (index == 2)
            {
                result = String.Format("{0}K", output);
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { null, null, result, null }));
            }
            else
            {
                result = String.Format("{0:#.0E+0}m", output);
                listViewAstroProcessing.Items.Add(new ListViewItem(new[] { null, null, null, result}));
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
                    break;
                case "French":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");                    
                    break;
                case "German":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
                    break;
            }
            Controls.Clear();
            InitializeComponent();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
        }

        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
        }
    }
}
