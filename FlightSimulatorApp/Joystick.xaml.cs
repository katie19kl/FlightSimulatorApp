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

        private bool isPressed = false;

        public Joystick()
        {
            InitializeComponent();
        }

        void centerKnob_Completed(object sender, EventArgs e)
        {
            //empty
        }

        private void RealKnob_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isPressed = true;
           
        }

        private void RealKnob_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isPressed = false;
           

             

            /*Canvas.SetLeft(this.RealKnob, 0);
            Canvas.SetTop(this.RealKnob, 0);

            Canvas.SetLeft(this.KnobBase, 0);
            Canvas.SetTop(this.KnobBase, 0);

            Canvas.SetLeft(this.LeftEllipse, 0);
            Canvas.SetTop(this.LeftEllipse, 0);

            Canvas.SetLeft(this.MiddleRightEllipse, 0);
            Canvas.SetTop(this.MiddleRightEllipse, 0);

            Canvas.SetLeft(this.LeftEllipse2, 10);
            Canvas.SetTop(this.LeftEllipse2, 10);

            Canvas.SetLeft(this.SmallLeftEllipse, 10);
            Canvas.SetTop(this.SmallLeftEllipse, 10);
            
            Canvas.SetLeft(this.whiteThing, 16);
            Canvas.SetTop(this.whiteThing, 16);
            */
        }

        private void RealKnob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isPressed)
            {
                Point position = e.GetPosition(this.centerEllipse);

                knobPosition.X = position.X;
                knobPosition.Y = position.Y;


                /*Canvas.SetLeft(this.RealKnob, position.X);
                Canvas.SetTop(this.RealKnob, position.Y);


                Canvas.SetLeft(this.KnobBase, position.X);
                Canvas.SetTop(this.KnobBase, position.Y);

                Canvas.SetLeft(this.LeftEllipse, position.X);
                Canvas.SetTop(this.LeftEllipse, position.Y);

                Canvas.SetLeft(this.MiddleRightEllipse, position.X);
                Canvas.SetTop(this.MiddleRightEllipse, position.Y);

                Canvas.SetLeft(this.LeftEllipse2, position.X + 10);
                Canvas.SetTop(this.LeftEllipse2, position.Y + 10);

                Canvas.SetLeft(this.SmallLeftEllipse, position.X + 10);
                Canvas.SetTop(this.SmallLeftEllipse, position.Y + 10);

                Canvas.SetLeft(this.whiteThing, position.X + 16);
                Canvas.SetTop(this.whiteThing, position.Y + 16);*/


                
            }
        }
    }
}