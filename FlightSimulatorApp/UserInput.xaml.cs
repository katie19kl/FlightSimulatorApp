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
        private const double minPort = 0; // Defines the range.
        private const double maxPort = 65535; // Defines the range.

        /* Propery of the ip variable. */
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

        /* Property of the port variable. */
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

        /* Constructor. */
        public UserInput()
        {
            InitializeComponent();
        }

        /* When the OK Button was pressed, checks if the ip and port are valid.
         * If so, updates in the App.Config the key-value for the IP and Port.
         */
        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            if (ip_valid && port_valid)
            {
                ConfigurationManager.AppSettings.Set("IP", this.ip);
                ConfigurationManager.AppSettings.Set("Port", this.port);

                Close();
            }
            else
            {
                if (!ip_valid)
                {
                    this.errorLabelIP.Content = "IP is invalid"; // Show indication on the UI.
                }
            }
        }

        /* When was pressed, the program will use the IP and Port that were defined
         * as default.
         */
        private void Default_buttonClick(object sender, RoutedEventArgs e)
        {
            Close(); // Not using input given in this window.
        }

        /* Shows indication of an error in the format of the IP inserted. */
        private void IP_txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.IP = this.IP_txtBox.Text;
            bool isNumeric = false;
            int numericIP = 0;
        
            // Check if valid.
            string[] nums = this.IP.Split('.');

            if(nums.Length != 4) // Invalid.
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
                
                if(isNumeric) // Ip is valid.
                {
                    this.ip_valid = true;
                    this.errorLabelIP.Content = "";
                }
            }
        }

        /* Shows indication of an error in the inserted Port. */
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
                if ((minPort <= numericPort) && (numericPort <= maxPort)) // Check if Port is in range.
                {
                    if ((string)this.errorLabelPort.Content != "")
                    {
                        this.errorLabelPort.Content = "";
                    }
                    this.Port = Port_txtBox.Text;
                    this.port_valid = true;
                }
                else
                {
                    this.port_valid = false;
                    this.errorLabelPort.Content = "Port not in range!";
                }
                
            }
        }
    }
}
