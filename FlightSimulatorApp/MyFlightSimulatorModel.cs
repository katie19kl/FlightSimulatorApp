using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        private double rudder;
        private double elevator;
        private ITelnetClient telnetClient;
        volatile bool stop;

        public MyFlightSimulatorModel(ITelnetClient MyTelnetClient)
        {
            this.telnetClient = MyTelnetClient;
            stop = false;
        }

        public double Rudder 
        { 
            get
            {
                return this.rudder;
            }

            set
            {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public double Elevator
        { 
            get
            {
                return this.elevator;
            }

            set
            {
                this.elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public bool connect(string ip, int port)
        {
            return this.telnetClient.connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            this.telnetClient.disconnect();
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}
