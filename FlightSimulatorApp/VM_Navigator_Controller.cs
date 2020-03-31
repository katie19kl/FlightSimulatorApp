using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    public class VM_Navigator_Controller : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;

        public VM_Navigator_Controller(IFlightSimulatorModel simulatorModel)
        {
            this.model = simulatorModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private double vm_rudder = 0;
        public double vm_Rudder
        {
            get { return vm_rudder; }
            set
            {
                vm_rudder = value;
                this.model.Rudder = value;
                string msg = "set /controls/flight/rudder " + value.ToString() + "\n";
                model.sendSetRequest(msg, "Rudder");
                this.NotifyPropertyChanged("vm_Rudder");
            }
        }

        private double vm_elevator = 0;
        public double vm_Elevator
        {
            get { return vm_elevator; }
            set
            {
                vm_elevator = value;
                this.model.Elevator = value;
                string msg = "set /controls/flight/elevator " + value.ToString() + "\n";
                model.sendSetRequest(msg, "Elevator");
                this.NotifyPropertyChanged("vm_Elevator");
            }
        }

        private double vm_aileron;
        public double vm_Airelon
        {
            get { return vm_aileron; }
            set
            {
                vm_aileron = value;
                this.model.Aileron = value;
                string msg = "set /controls/flight/aileron " + value.ToString() + "\n";
                model.sendSetRequest(msg, "Aileron");
                this.NotifyPropertyChanged("vm_Airelon");
            }
        }

        private double vm_throttle;
        public double vm_Throttle
        {
            get { return vm_throttle; }
            set
            {
                vm_throttle = value;
                this.model.Throttle = value;
                string msg = "set /controls/engines/current-engine/throttle " + value.ToString() + "\n";
                model.sendSetRequest(msg, "Throttle");
                this.NotifyPropertyChanged("vm_Throttle");
            }
        }


        public void NotifyPropertyChanged(string propName)
        {

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}