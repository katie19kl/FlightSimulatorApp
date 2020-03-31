using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Controls;

namespace FlightSimulatorApp
{
    public interface IFlightSimulatorModel : INotifyPropertyChanged
    {

        double Rudder
        {
            get; set;
        }

        double Elevator
        {
            get; set;
        }

        double AirSpeed
        {
            get; set;
        }

        double Altitude
        {
            get; set;
        }

        double Roll
        {
            get; set;
        }
        
        double Pitch
        {
            get; set;
        }

        double Altimeter
        {
            get; set;
        }

        double Heading
        {
            get; set;
        }

        double GroundSpeed
        {
            get; set;
        }

        double VerticalSpeed
        {
            get; set;
        }

        double Latitude
        {
            get; set;
        }

        double Longitude
        {
            get; set;
        }

        double Aileron
        {
            get; set;
        }

        double Throttle
        {
            get; set;
        }

        string WarningString
        {
            get; set;
        }

        bool connect(string ip, string port);

        void disconnect();

        void start();

        void sendSetRequest(string sendRequest, string varName);

        void showIndicationOnScreen(string warningMsg);
    }
}
