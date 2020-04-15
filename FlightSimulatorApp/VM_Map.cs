using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;


namespace FlightSimulatorApp
{
    public class VM_Map : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        IFlightSimulatorModel model;
        private Location locationPushPin;
        private double latitude;
        private double longitude;

        /* Constructor receives a model. */
        public VM_Map(IFlightSimulatorModel model_l)
        {
            model = model_l;
            locationPushPin = new Location();
            this.VM_LocationPushPin = new Location(32.0055, 34.8854); // Initialized to Ben Gurion.

            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        /* Property of vm_locationPushPin variable. */
        public Location VM_LocationPushPin
        {
            get
            {
                //returns new location to XAML with updated parameters (here we don't know what was changed).
                return new Location(model.Latitude, model.Longitude);

            }
            set
            {
                this.locationPushPin = value;
            }

        }

        /*
         * When one of parameters was updated 
         * in set in THAT parameter we are
         * calling notify property of Location
         * to update it on the Map.
         */
        public double VM_Latitude
        {
            get
            {
                VM_Latitude = model.Latitude;
                return model.Latitude;
            }
            set
            {
                latitude = value;
                NotifyPropertyChanged("VM_LocationPushPin");
            }
        }

        /* Property of vm_longitude variable. */
        public double VM_Longitude
        {
            get
            {
                VM_Longitude = model.Longitude;
                return model.Longitude;
            }
            set
            {
                longitude = value;
                NotifyPropertyChanged("VM_LocationPushPin");
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