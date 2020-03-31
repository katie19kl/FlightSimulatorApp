using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for UserInput.xaml
    /// </summary>
    public partial class UserInput : Window
    {

        private string ip;
        private string port;
        private bool ip_valid = false;
        private bool port_valid = false;


        public string IP
        {
            get
            {
                return this.ip;
            }

            set
            {
                this.ip = value;
            }
        }

        public string Port
        {
            get
            {
                return this.port;
            }

            set
            {
                this.port = value;

            }
        }

        public UserInput()
        {
            InitializeComponent();
        }

        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            if (ip_valid && port_valid)
            {
                ConfigurationManager.AppSettings.Set("IP", this.ip);
                ConfigurationManager.AppSettings.Set("Port", this.port);


                Close();
            }
        }

        private void Default_buttonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IP_txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.IP = this.IP_txtBox.Text;
            bool isNumeric = false;
            int numericIP = 0;
        
            //check if valid
            string[] nums = this.IP.Split('.');

            if(nums.Length != 4) //invalid
            {
                ip_valid = false;
                if((string)this.errorLabelIP.Content == "")
                {
                    this.errorLabelIP.Content = "IP is invalid";
                }
            } else
            {
                foreach(string str in nums)
                {
                    isNumeric = int.TryParse(str, out numericIP);
                    if(!isNumeric)
                    {
                        ip_valid = false;
                        if ((string)this.errorLabelIP.Content == "")
                        {
                            this.errorLabelIP.Content = "IP is invalid";
                        }
                        break;
                    }
                }
                
                if(isNumeric) //ip is valid
                {
                    this.ip_valid = true;
                    this.errorLabelIP.Content = "";
                }
            }
        }

        private void Port_txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numericPort = 0;
            bool isNumeric = int.TryParse(this.Port_txtBox.Text, out numericPort);

            if (!isNumeric)
            {
                this.port_valid = false;
                this.errorLabelPort.Content = "Port is invalid";
            }

            else
            {
                if((string)this.errorLabelPort.Content != "")
                {
                    this.errorLabelPort.Content = "";
                }
                this.Port = Port_txtBox.Text;
                port_valid = true;
            }
        }
    }
}
