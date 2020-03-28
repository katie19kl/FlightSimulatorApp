using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FlightSimulatorApp
{
    class VM_Sensor : INotifyPropertyChanged
    {
        private double vm_airSpeed; //8 values from here!
        private double vm_altitude;
        private double vm_roll;
        private double vm_pitch;
        private double vm_altimeter;
        private double vm_heading;
        private double vm_groundSpeed;
        private double vm_verticalSpeed;

        public double vm_AirSpeed
        {
            get
            {
                return this.model.AirSpeed;
            }
        }

        public double vm_Altitude
        {
            get
            {
                return this.model.Altitude;
            }
        }

        public double vm_Roll
        {
            get
            {
                return this.model.Roll;
            }
        }

        public double vm_Pitch
        {
            get
            {
                return this.model.Pitch;
            }
        }


        public double vm_Altimeter
        {
            get
            {
                return this.model.Altimeter;
            }
        }

        public double vm_Heading
        {
            get
            {
                return this.model.Heading;
            }
        }

        public double vm_GroundSpeed
        {
            get
            {
                return this.model.GroundSpeed;
            }
        }

        public double vm_VerticalSpeed
        {
            get
            {
                return this.model.VerticalSpeed;
            }

        }
        IFlightSimulatorModel model;
        public VM_Sensor(IFlightSimulatorModel model_l)
        {
            model = model_l;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
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