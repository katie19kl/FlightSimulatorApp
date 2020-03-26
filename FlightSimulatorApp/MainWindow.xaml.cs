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
            ITelnetClient telnetClient = new MyTelnetClient();
            IFlightSimulatorModel simulatorModel = new MyFlightSimulatorModel(telnetClient);
            simulatorModel.connect("127.0.0.1", "7777");
            VM_Navigator_Controller viewModelController = new VM_Navigator_Controller(simulatorModel);
            MapViewModel mapViewModel = new MapViewModel(simulatorModel);
            SensorsViewModel sensorsViewModel = new SensorsViewModel(simulatorModel);

            this.DataContext = sensorsViewModel;
            this.Joystick_Var.SetVM(viewModelController);

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
