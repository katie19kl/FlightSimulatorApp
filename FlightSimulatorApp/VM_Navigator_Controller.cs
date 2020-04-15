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
        public event PropertyChangedEventHandler PropertyChanged;
        private double vm_rudder = 0;
        private double vm_elevator = 0;
        private double vm_aileron;
        private double vm_throttle;


        /* Constructor receives a model. */
        public VM_Navigator_Controller(IFlightSimulatorModel simulatorModel)
        {
            this.model = simulatorModel;
        }

        /* Property of vm_rudder variable. */
        public double VM_Rudder
        {
            get { return vm_rudder; }
            set
            {
                vm_rudder = value;
                this.model.Rudder = value;
                string msg = "set /controls/flight/rudder " + value.ToString() + "\n";
                model.SendSetRequest(msg, "Rudder");
                this.NotifyPropertyChanged("VM_Rudder");
            }
        }

        /* Property of vm_elevator variable. */
        public double VM_Elevator
        {
            get { return vm_elevator; }
            set
            {
                vm_elevator = value;
                this.model.Elevator = value;
                string msg = "set /controls/flight/elevator " + value.ToString() + "\n";
                model.SendSetRequest(msg, "Elevator");
                this.NotifyPropertyChanged("VM_Elevator");
            }
        }

        /* Property of vm_aileron variable. */
        public double VM_Airelon
        {
            get { return vm_aileron; }
            set
            {
                vm_aileron = value;
                this.model.Aileron = value;
                string msg = "set /controls/flight/aileron " + value.ToString() + "\n";
                model.SendSetRequest(msg, "Aileron");
                this.NotifyPropertyChanged("VM_Airelon");
            }
        }

        /* Property of vm_throttle variable. */
        public double VM_Throttle
        {
            get { return vm_throttle; }
            set
            {
                vm_throttle = value;
                this.model.Throttle = value;
                string msg = "set /controls/engines/current-engine/throttle " + value.ToString() + "\n";
                model.SendSetRequest(msg, "Throttle");
                this.NotifyPropertyChanged("VM_Throttle");
            }
        }

        /* Notifies the observers of a change that occures. */
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}