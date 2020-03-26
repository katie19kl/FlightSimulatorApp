using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    class SensorsViewModel: INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;
        private double vm_airSpeed;
        private double vm_altitude;
        private double vm_roll;
        private double vm_pitch;
        private double vm_altimeter;
        private double vm_heading;
        private double vm_groundSpeed;
        private double vm_verticalSpeed;

        public double VM_AirSpeed
        {
            get
            {
                return this.model.AirSpeed;
            }
        }

        public double VM_Atitude
        {
            get
            {
                return this.model.Altitude;
            }
        }

        public double VM_Roll
        {
            get
            {
                return this.model.Roll;
            }
        }

        public double VM_Pitch
        {
            get
            {
                return this.model.Pitch;
            }
        }

        public double VM_Altimeter
        {
            get
            {
                return this.model.Altimeter;
            }
        }

        public double VM_Heading
        {
            get
            {
                return this.model.Heading;
            }
        }

        public double VM_GroundSpeed
        {
            get
            {
                return this.model.GroundSpeed;
            }
        }

        public double VM_VerticalSpeed
        {
            get
            {
                return this.model.VerticalSpeed;
            }
        }

        public SensorsViewModel(IFlightSimulatorModel simulatorModel)
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
