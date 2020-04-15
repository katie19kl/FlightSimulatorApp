using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FlightSimulatorApp
{
    public class VM_Sensor : INotifyPropertyChanged
    {
        IFlightSimulatorModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        /* Getter of airSpeed value. */
        public double VM_AirSpeed
        {
            get
            {
                return this.model.AirSpeed;
            }
        }

        /* Getter of altitude value. */
        public double VM_Altitude
        {
            get
            {
                return this.model.Altitude;
            }
        }

        /* Getter of roll value. */
        public double VM_Roll
        {
            get
            {
                return this.model.Roll;
            }
        }

        /* Getter of pitch value. */
        public double VM_Pitch
        {
            get
            {
                return this.model.Pitch;
            }
        }

        /* Getter of altimeter value. */
        public double VM_Altimeter
        {
            get
            {
                return this.model.Altimeter;
            }
        }

        /* Getter of heading value. */
        public double VM_Heading
        {
            get
            {
                return this.model.Heading;
            }
        }

        /* Getter of groundSpeed value. */
        public double VM_GroundSpeed
        {
            get
            {
                return this.model.GroundSpeed;
            }
        }

        /* Getter of verticalSpeed value. */
        public double VM_VerticalSpeed
        {
            get
            {
                return this.model.VerticalSpeed;
            }

        }

        /* Constructor receives a model. */
        public VM_Sensor(IFlightSimulatorModel model_l)
        {
            model = model_l;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
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