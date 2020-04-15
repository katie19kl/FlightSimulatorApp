using System;
using System.Configuration;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /* Constructor. */
        public MainWindow()
        {
            // Creating all the VMs.
            VM_Sensor vmSensor = new VM_Sensor(App.myFlightSimulatorModel);
            VM_Map vmMap = new VM_Map(App.myFlightSimulatorModel);
            VM_Navigator_Controller vmController = new VM_Navigator_Controller(App.myFlightSimulatorModel);
            VM_Warnings vmWarnings = new VM_Warnings(App.myFlightSimulatorModel);

            // User enters IP and Port.
            UserInput popUpInput = new UserInput();
            popUpInput.ShowDialog();

            string port = ConfigurationManager.AppSettings.Get("Port");
            string ip = ConfigurationManager.AppSettings.Get("IP");

            InitializeComponent();
            Joystick_Var.SetVM(vmController);
            Sensors_Var.SetVM(vmSensor);
            MyWarningsIndicator.SetVM(vmWarnings);

            DataContext = new
            {
                vmMap,
                vmSensor,
                vmController,
                vmWarnings
            };

            if (!App.myFlightSimulatorModel.Connect(ip, port)) // Attempt of connection failed.
            {
                App.myFlightSimulatorModel.ShowIndicationOnScreen("Cannot Connect to server!");
            }

            else
            {
                App.myFlightSimulatorModel.Start(); // Execute the program. 
            }
        }

        /* Empty. */
        private void Joystick_Loaded(object sender, RoutedEventArgs e) { }

        /* For sake of joystick(return knob to the center). */
        private void ButtonMouse_Up(object sender, MouseButtonEventArgs e)
        {
            this.Joystick_Var.SetPiptickToCenter_NO_UPDATE();
            Joystick_Var.mouseIsPressed = false;
        }
    }
}