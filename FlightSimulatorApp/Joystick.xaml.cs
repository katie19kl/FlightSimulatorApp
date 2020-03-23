
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
        public bool mouseReturn = false;

        private double radiusKnobBase;

        private double radius_Small;
        private double radius_Outside;
        double calculatedElevator;
        double calculatedRudder;
        private Point position_TO_MOVE;
        private bool checkBoard;


        public Joystick()
        {
            InitializeComponent();
            initX = this.knobPosition.X;
            initY = this.knobPosition.Y;

            radiusKnobBase = KnobBase.Height / 2;

            radius_Small = KnobBase.Height / 2;
            radius_Outside = OuttestEllipse.Width / 2;

            dummy_centre.Fill = new SolidColorBrush(Colors.Red);
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
            Point position = e.GetPosition(dummy_centre);

            x1 = position.X;
            y1 = position.Y;

            if (mouseIsPressed)
            {

                checkBoard = (Math.Pow(x1 + knobPosition.X - initX, 2) +
                    Math.Pow(y1 + knobPosition.Y - initY, 2)) <= Math.Pow(radius_Outside - radius_Small + lookBetter, 2);
                if (checkBoard)
                {
                    knobPosition.X += x1;
                    knobPosition.Y += y1;

                    position_TO_MOVE = getTruePoint(e.GetPosition(OuttestEllipse));

                    calculatedRudder = (position_TO_MOVE.X) / (radius_Outside + lookBetter - radius_Small);
                    calculatedElevator = (position_TO_MOVE.Y) / (radius_Outside + lookBetter - radius_Small);
                }
                else
                {
                    mouseReturn = true;
                }
            }
        }
        public void SetPiptickToCenter()
        {
            knobPosition.X = initX;
            knobPosition.Y = initY;
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

            if (xPoint <= radius_Outside && yPoint <= radius_Outside) // second 
            {
                xToSet = -(radius_Outside - xPoint);
                yToSet = radius_Outside - yPoint;
            }
            else if (xPoint > radius_Outside && yPoint < radius_Outside) // first
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
    }
}

