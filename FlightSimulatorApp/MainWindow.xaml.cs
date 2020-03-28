using Microsoft.Maps.MapControl.WPF;
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
            IFlightSimulatorModel simulatorModel = new MyFlightSimulatorModel(new MyTelnetClient());

            VM_Sensor vmSensor = new VM_Sensor(simulatorModel);

            VM_Map vmMap = new VM_Map(simulatorModel);

            VM_Navigator_Controller vmController = new VM_Navigator_Controller(simulatorModel);
            Joystick_Var.SetVM(vmController);


        
            DataContext = new
            {
                vmMap,
                vmSensor,
                vmController
            };

            simulatorModel.connect("127.0.0.1", "7777");
            simulatorModel.start();


        }

        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMouse_Up(object sender, MouseButtonEventArgs e)
        {
            this.Joystick_Var.SetPiptickToCenter();
            Joystick_Var.mouseIsPressed = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
