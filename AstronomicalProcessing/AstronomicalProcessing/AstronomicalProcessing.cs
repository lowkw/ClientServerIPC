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
        #region globals
        //Globals
        private IAstroContract pipeProxy;
        bool dayModeOn = false;
        bool nightModeOn = false;
        bool colourDialogOn = false;        
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
        #endregion globals
        #region textboxes
        /*
         * 7.2 (a)
         * Series of textboxes for large numeric data
         */
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
        #endregion textboxes
        #region buttons
        /*
         * 7.2 (c)
         * Button(s) to initiate an event and send/receive data
         */
        private void btStarVelocity_Click(object sender, EventArgs e)
        {
            if (ValidateDoubleInput("Observed Wavelength",tbStarVelocityObservedWaveIn))
            {
                double d1 = Convert.ToDouble(tbStarVelocityObservedWaveIn.Text);
                if (ValidateDoubleInput("Rest Wavelength",tbStarVelocityRestWaveIn))
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
            if (ValidateDoubleInput("Parallax Angle", tbStarDistanceIn))
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
            if (ValidateDoubleInput("Celsius", tbTemperatureIn))
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
            if (ValidateDoubleInput("Blackhole Mass", tbEventHorizonBlackholeIn))
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
        private bool ValidateDoubleInput(string textBoxName, TextBox inputTextBox)
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
        #endregion buttons
        #region listview
        /*
         * 7.2 (b)	
         * A listview/datagrid for display of processed information from the server
         */
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
        #endregion listview
        #region form's style
        /*
         * 7.4	
         * Menu option to change the form’s style (colours and visual appearance).
         */
        private void dayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDayMode();
            dayModeOn = true;
            nightModeOn = false;
            colourDialogOn = false;
        }
        private void SetDayMode()
        {
            BackgroundImage = null;
            BackColor = Color.YellowGreen;
            ForeColor = Color.Black;
            foreach (var label in Controls.OfType<Label>())
            {
                label.ForeColor = Color.DarkGreen;
            }
            foreach (var button in Controls.OfType<Button>())
            {
                button.ForeColor = Color.DarkRed;
                button.BackColor = Color.FromArgb(255,3,218,197);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.OrangeRed;
            }            
            foreach (var textBox in Controls.OfType<TextBox>())
            {
                textBox.BackColor = Color.FromArgb(255, 255, 235, 238);
                textBox.ForeColor = Color.DarkOrchid;
            }
            foreach (var listView in Controls.OfType<ListView>())
            {
                listView.BackColor = Color.FromArgb(255,255,235,238);
                listView.ForeColor = Color.Black;
            }
        }

        private void nightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetNightMode();
            nightModeOn = true;
            dayModeOn = false;
            colourDialogOn = false;
        }
        private void SetNightMode()
        {
            BackgroundImage = null;
            BackColor = Color.FromArgb(255,18,18,18);
            ForeColor = Color.White;
            foreach (var label in Controls.OfType<Label>())
            {
                label.ForeColor = Color.White;
            }
            foreach (var button in Controls.OfType<Button>())
            {
                button.ForeColor = Color.FromArgb(255,200,75,49);
                button.BackColor = Color.FromArgb(255,48,63,159);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.SlateBlue;

            }            
            foreach (var textBox in Controls.OfType<TextBox>())
            {
                textBox.BackColor = Color.Gray;
                textBox.ForeColor = Color.Black;
            }
            foreach (var listView in Controls.OfType<ListView>())
            {
                listView.BackColor = Color.Gray;
                listView.ForeColor = Color.Black;
            }
        }
        #endregion form's style
        #region Color Dialogbox
        /*
         * 7.5	
         * Menu/Button option to select a custom colour from a colour palette (Color Dialogbox)
         */
        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colourDialogOn = false;
            SetColourDialog();
            colourDialogOn = true;
            dayModeOn = false;
            nightModeOn = false;
        }
        private void SetColourDialog()
        {
            if (!colourDialogOn)
            {
                ColorDialog colorDlg = new ColorDialog();
                if (colorDlg.ShowDialog() == DialogResult.OK)
                {
                    BackgroundImage = null;
                    BackColor = colorDlg.Color;
                    byte r = (byte)(255 - BackColor.R);
                    byte g = (byte)(255 - BackColor.G);
                    byte b = (byte)(255 - BackColor.B);
                    ForeColor = Color.FromArgb(255, r, g, b);                    
                    SetControlsColour();
                }
            } else
            {
                SetControlsColour();

            }
        }

        private void SetControlsColour()
        {
            byte[] rgbColour = AdjustColour(BackColor.R, BackColor.G, BackColor.B, 190);

            foreach (var label in Controls.OfType<Label>())
            {
                label.ForeColor = Color.FromArgb(255, rgbColour[0], rgbColour[1], rgbColour[2]);
            }
            foreach (var button in Controls.OfType<Button>())
            {
                button.ForeColor = Color.FromArgb(255, rgbColour[2], rgbColour[0], rgbColour[1]);
                button.BackColor = Color.FromArgb(255, rgbColour[1], rgbColour[2], rgbColour[0]);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.FromArgb(255, rgbColour[2], rgbColour[0], rgbColour[1]);

            }
            foreach (var textBox in Controls.OfType<TextBox>())
            {
                textBox.BackColor = Color.FromArgb(255, rgbColour[2], rgbColour[1], rgbColour[0]);
                textBox.ForeColor = Color.FromArgb(255, rgbColour[0], rgbColour[2], rgbColour[1]);
            }
            foreach (var listView in Controls.OfType<ListView>())
            {
                listView.BackColor = Color.FromArgb(255, rgbColour[2], rgbColour[1], rgbColour[0]);
                listView.ForeColor = Color.FromArgb(255, rgbColour[0], rgbColour[2], rgbColour[1]);
            }
        }
        
        private byte[] AdjustColour(byte r, byte g, byte b, byte increment)
        {
            byte []colour = new byte[3];
            int newColour;                

            for (int i = 0; i < colour.Length; i++)
            {
                if (i == 0) 
                    newColour = r + increment; 
                else if (i == 1)  
                    newColour = g + increment; 
                else 
                    newColour = b + increment; 
                
                if (newColour > 255)
                {
                    colour[i] = (byte)(newColour - 255);
                }
                else colour[i] = (byte)newColour;
            }
            return colour;
        }
        #endregion Color Dialogbox
        #region language
        /*
         * 7.3	
         * Menu/Button option(s) to change the language and layout for the three different countries.
         */
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
            if (dayModeOn)
                SetDayMode();            
            if (nightModeOn)
                SetNightMode();
            if (colourDialogOn)
                SetColourDialog();
        }
        #endregion language
    }
}
