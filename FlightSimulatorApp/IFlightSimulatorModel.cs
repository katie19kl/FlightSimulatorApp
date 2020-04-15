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

        /* Property of Rudder variable */
        double Rudder
        {
            get; set;
        }

        /* Property of Elevator variable */
        double Elevator
        {
            get; set;
        }

        /* Property of AirSpeed variable */
        double AirSpeed
        {
            get; set;
        }

        /* Property of Altitude variable */
        double Altitude
        {
            get; set;
        }

        /* Property of Roll variable */
        double Roll
        {
            get; set;
        }

        /* Property of Pitch variable */
        double Pitch
        {
            get; set;
        }

        /* Property of Altimeter variable. */
        double Altimeter
        {
            get; set;
        }

        /* Property of Heading variable. */
        double Heading
        {
            get; set;
        }

        /* Property of GroundSpeed variable. */
        double GroundSpeed
        {
            get; set;
        }

        /* Property of VerticalSpeed variable. */
        double VerticalSpeed
        {
            get; set;
        }

        /* Property of Latitude variable. */
        double Latitude
        {
            get; set;
        }

        /* Property of Longitude variable. */
        double Longitude
        {
            get; set;
        }

        /* Property of Aileron variable. */
        double Aileron
        {
            get; set;
        }

        /* Property of Throttle variable. */
        double Throttle
        {
            get; set;
        }

        /* Property of WarningString variable. */
        string WarningString
        {
            get; set;
        }

        /* In charge of connecting to a server given:
         *
         * string ip
         * string port
         * 
         * returns bool for succeeding or failing to Connect. 
         */
        bool Connect(string ip, string port);

        /* When called - disconnects from the server. */
        void Disconnect();

        /* In charge of executing the model's algorithm. */
        void Start();

        /* Given a string of a set request and the variable name, 
         * sends the request to the server.
         */
        void SendSetRequest(string sendRequest, string varName);

        /* Responsible for showing indication on the UI,
         * of an error that occures.
         */
        void ShowIndicationOnScreen(string warningMsg);
    }
}
