using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    public class VM_Navigator_Controller : INotifyPropertyChanged
    {

        public VM_Navigator_Controller()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private double vm_rudder = 0;
        public double vm_Rudder
        {
            get { return vm_rudder; }
            set
            {
                vm_rudder = value;
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