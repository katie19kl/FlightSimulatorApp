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
        }

        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Joystick_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void Joystick_Loaded_2(object sender, RoutedEventArgs e)
        {

        }

        private void Joystick_Loaded_3(object sender, RoutedEventArgs e)
        {
            
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double val = Convert.ToDouble(e.NewValue);
            string msg = String.Format("current value is: {0}", val);
            t1.Text = msg;
        }

        private void s2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double val = Convert.ToDouble(e.NewValue);
            string msg = String.Format("current value is: {0}", val);
            t1_Copy.Text = msg;
        }
    }
}
