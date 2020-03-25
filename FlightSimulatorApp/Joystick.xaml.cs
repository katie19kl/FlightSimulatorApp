using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace FlightSimulatorApp
{
    public partial class Joystick
    {
        private const double lookBetter = 50;

        public double x1 { set; get; }
        public double y1 { set; get; }

        private double initX;
        private double initY;


        public bool mouseIsPressed = false;

        private double dummyDoubleWidth;

        private double radius_Small;
        private double radius_Outside;
        private double calculatedElevator;
        private double calculatedRudder;
        private Point position_TO_MOVE;
        private Point position;


        private bool checkBoard;

        VM_Navigator_Controller vm;

        public Joystick()
        {
            InitializeComponent();
            initX = this.knobPosition.X;
            initY = this.knobPosition.Y;

            radius_Small = KnobBase.Height / 2;
            radius_Outside = OuttestEllipse.Width / 2;

            dummyDoubleWidth = 2 * dummy_centre.Width + 1;
            dummy_centre.Fill = new SolidColorBrush(Colors.Red);

        }
        public void SetVM(VM_Navigator_Controller vm_1)
        {
            vm = vm_1;
        }

        private void MouseLeftDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mouseIsPressed = true;
        }

        void centerKnob_Completed(object sender, EventArgs e)
        {

        }

        private void MouseMove_J(object sender, System.Windows.Input.MouseEventArgs e)
        {
            position = e.GetPosition(dummy_centre);

            x1 = position.X;
            y1 = position.Y;

            if (mouseIsPressed)
            {

                checkBoard = (Math.Pow(x1 + knobPosition.X - initX, 2) +
                    Math.Pow(y1 + knobPosition.Y - initY, 2)) 
                    <= Math.Pow(radius_Outside - radius_Small + lookBetter, 2);

                if (checkBoard)
                {
                    knobPosition.X += x1;
                    knobPosition.Y += y1;

                    position_TO_MOVE = getTruePoint(e.GetPosition(OuttestEllipse));
                    // may be radius be added


                    calculatedRudder = (position_TO_MOVE.X) / (radius_Outside + lookBetter - radius_Small - dummyDoubleWidth);
                    calculatedRudder = Math.Round(calculatedRudder, 3);
                    this.vm.vm_Rudder = calculatedRudder;


                    calculatedElevator = (position_TO_MOVE.Y) / (radius_Outside + lookBetter - radius_Small - dummyDoubleWidth);
                    calculatedElevator = Math.Round(calculatedElevator, 3);
                    this.vm.vm_Elevator = calculatedElevator;
                }
            }
        }
        public void SetPiptickToCenter()
        {
            knobPosition.X = initX;
            this.vm.vm_Rudder = initX;
            knobPosition.Y = initY;
            this.vm.vm_Elevator = initY;
        }

        private void MouseReturnToJOY(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(dummy_centre);


            if (mouseIsPressed)
            {
                knobPosition.X -= -position.X;
                knobPosition.Y -= -position.Y;
            }
        }

        private Point getTruePoint(Point pointToCorrect)
        {
            double xToSet = 0, yToSet = 0;
            double xPoint = pointToCorrect.X;
            double yPoint = pointToCorrect.Y;

            if (yPoint <= 0)
            {
                yPoint = 0;
            }
            if (yPoint >= 340)
            {
                yPoint = 340;
            }


            if (xPoint <= 0)
            {
                xPoint = 0;
            }
            if (xPoint >= 340)
            {
                xPoint = 340;
            }

            if (xPoint < radius_Outside && yPoint < radius_Outside) // 2 
            {
                xToSet = -(radius_Outside - xPoint);
                yToSet = radius_Outside - yPoint;
            }
            else if (xPoint >= radius_Outside && yPoint <= radius_Outside) // 1
            {
                xToSet = xPoint - radius_Outside;
                yToSet = radius_Outside - yPoint;
            }
            else if (xPoint > radius_Outside && yPoint > radius_Outside) // 4
            {
                xToSet = xPoint - radius_Outside;
                yToSet = -(yPoint - radius_Outside);
            }
            else if (xPoint < radius_Outside && yPoint > radius_Outside) //3
            {

                xToSet = -(radius_Outside - xPoint);
                yToSet = -(yPoint - radius_Outside);
            }

            return new Point(xToSet, yToSet);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }





}
