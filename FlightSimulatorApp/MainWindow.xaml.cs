using System;
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
using System.Configuration;
using System.Windows.Threading;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            IFlightSimulatorModel myFlightSimulatorModel = new MyFlightSimulatorModel(new MyTelnetClient());


            VM_Sensor vmSensor = new VM_Sensor(myFlightSimulatorModel);

            VM_Map vmMap = new VM_Map(myFlightSimulatorModel);            

            VM_Navigator_Controller vmController = new VM_Navigator_Controller(myFlightSimulatorModel);

            VM_Warnings vmWarnings = new VM_Warnings(myFlightSimulatorModel);

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

            UserInput popUpInput = new UserInput();
            popUpInput.ShowDialog();

            string port = ConfigurationManager.AppSettings.Get("Port");
            string ip = ConfigurationManager.AppSettings.Get("IP");


            if(!myFlightSimulatorModel.connect(ip, port)) {
                myFlightSimulatorModel.showIndicationOnScreen("Cannot connect to server");
            } else
            {
                myFlightSimulatorModel.start();
            }

        }

        private void Joystick_Loaded(object sender, RoutedEventArgs e) { }

        //For sake of joystick
        private void ButtonMouse_Up(object sender, MouseButtonEventArgs e)
        {
            this.Joystick_Var.SetPiptickToCenter();
            Joystick_Var.mouseIsPressed = false;
        }
    }
}