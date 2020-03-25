using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FlightSimulatorApp
{
    
    public class VM_Navigator_Controller: INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;
        private const double aileronStart = 0;
        private const double aileronEnd = 1;
        private const double throttleStart = -1;
        private const double throttleEnd = 1;
        private const double rudderStart = -1;
        private const double rudderEnd = 1;
        private const double elevatorStart = -1;
        private const double elevatorEnd = 1;

        private double vm_throttle;

        public double vm_Throttle
        {
            get
            {
                return this.vm_throttle;
            }

            set
            {
                this.vm_throttle = value;
                this.NotifyPropertyChanged("VM_Throttle");
            }
        }

        private double vm_aileron;

        public double vm_Aileron
        {
            get
            {
                return this.vm_aileron;
            }

            set
            {
                this.vm_aileron = value;
                this.NotifyPropertyChanged("VM_Aileron");
            }
        }

        private double vm_rudder;

        public double vm_Rudder
        {
            get
            {
                return this.vm_rudder;
            }

            set
            {
                this.vm_rudder = value;
                this.NotifyPropertyChanged("VM_Rudder");
            }
        }

        private double vm_elevator;

        public double vm_Elevator
        {
            get
            {
                return this.vm_elevator;
            }

            set
            {
                this.vm_elevator = value;
                this.NotifyPropertyChanged("VM_Elevator");
            }
        }

        public VM_Navigator_Controller(IFlightSimulatorModel simulatorModel)
        {
            this.model = simulatorModel;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
