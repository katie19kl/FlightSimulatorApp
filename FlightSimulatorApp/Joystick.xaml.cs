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

        /* Property of x1 value. */
        public double X1 { set; get; }

        /* Property of y1 value. */
        public double Y1 { set; get; }

        /* Constructor. */
        public Joystick()
        {
            InitializeComponent();
            initX = this.knobPosition.X;
            initY = this.knobPosition.Y;

            radius_Small = KnobBase.Height / 2;
            radius_Outside = OuttestEllipse.Width / 2;

            dummyDoubleWidth = 2 * dummy_centre.Width + 1;
            dummy_centre.Fill = new SolidColorBrush(Colors.Transparent);

        }

        /* Sets the vm of the controller member. */
        public void SetVM(VM_Navigator_Controller vm_1)
        {
            vm = vm_1;
        }

        /* Sets bool variable to true when mouse is pressed within the Joystick. */
        private void MouseLeftDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mouseIsPressed = true;
        }

        /* Empty. */
        void centerKnob_Completed(object sender, EventArgs e) { }

        /* Defines the behavior when the mouse moves within the Joystick. */
        private void MouseMove_J(object sender, System.Windows.Input.MouseEventArgs e)
        {
            position = e.GetPosition(dummy_centre);

            X1 = position.X;
            Y1 = position.Y;

            if (mouseIsPressed)
            {
                // If the point is inside circle.
                checkBoard = (Math.Pow(X1 + knobPosition.X - initX, 2) +
                    Math.Pow(Y1 + knobPosition.Y - initY, 2)) <= Math.Pow(radius_Outside - radius_Small + lookBetter, 2);

                if (checkBoard)// If it does.
                {
                    // Moving inside.
                    knobPosition.X += X1;
                    knobPosition.Y += Y1;

                    // Getting position relatively to (0,0) in center.
                    position_TO_MOVE = GetTruePoint(e.GetPosition(OuttestEllipse));


                    // Calculation to get needed range.
                    calculatedRudder = (position_TO_MOVE.X) / (radius_Outside + lookBetter - radius_Small - dummyDoubleWidth);
                    calculatedRudder = Math.Round(calculatedRudder, 3);
                    this.vm.VM_Rudder = calculatedRudder;


                    calculatedElevator = (position_TO_MOVE.Y) / (radius_Outside + lookBetter - radius_Small - dummyDoubleWidth);
                    calculatedElevator = Math.Round(calculatedElevator, 3);
                    this.vm.VM_Elevator = calculatedElevator;
                }
            }
        }

        public void SetPiptickToCenter_NO_UPDATE()
        {
            knobPosition.X = initX;
            knobPosition.Y = initY;
        }

        /* When the mouse button was released. */
        public void SetPiptickToCenter()
        {
            knobPosition.X = initX;
            this.vm.VM_Rudder = initX;
            knobPosition.Y = initY;
            this.vm.VM_Elevator = initY;
        }

        /* Mouse leave and entry with button pressed. */
        private void MouseReturnToJOY(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(dummy_centre);

            if (mouseIsPressed)
            {
                knobPosition.X -= -position.X;
                knobPosition.Y -= -position.Y;
            }
        }

        /* To get relatively to (0,0) . */
        private Point GetTruePoint(Point pointToCorrect)
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
            else if (xPoint < radius_Outside && yPoint > radius_Outside) // 3
            {

                xToSet = -(radius_Outside - xPoint);
                yToSet = -(yPoint - radius_Outside);
            }

            return new Point(xToSet, yToSet);
        }
    }
}