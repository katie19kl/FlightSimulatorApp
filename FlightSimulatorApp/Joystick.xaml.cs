using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace FlightSimulatorApp
{
    public partial class Joystick: UserControl
    {

        public bool isPressed = false;
        private Point point;

        public Joystick()
        {
            InitializeComponent();
        }

        void centerKnob_Completed(object sender, EventArgs e) {}

        private void RealKnob_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isPressed = true;
            point = e.GetPosition(this);
            
           
        }

        private void RealKnob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isPressed)
            {
                Point position = e.GetPosition(this);

                double xVal = position.X - point.X;
                double yVal = position.Y - point.Y;

                if(Math.Sqrt(xVal*xVal + yVal*yVal) < (Base.Width / 2) - 43)
                {
                    knobPosition.X = xVal;
                    knobPosition.Y = yVal;
                }
            }
        }
    }
}