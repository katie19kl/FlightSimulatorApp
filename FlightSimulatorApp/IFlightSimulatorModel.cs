using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    interface IFlightSimulatorModel: INotifyPropertyChanged
    {

        double Rudder
        {
            get;
            set;
        }

        double Elevator
        {
            get;
            set;
        }

        bool connect(string ip, int port);

        void disconnect();

        void start();
        
    }
}
