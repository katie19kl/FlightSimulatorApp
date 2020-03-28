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

        IFlightSimulatorModel model;

        public VM_Map(IFlightSimulatorModel model_l)
        {
            model = model_l;
            locationPushPin = new Location();
            this.vm_LocationPushPin = new Location(32.0055, 34.8854);//Ben Gurion


            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        private Location locationPushPin;
        public Location vm_LocationPushPin
        {
            get
            {
                //returns new location to XAML with updated paramete (here we dont know what was changed)
                return new Location(model.Latitude, model.Longitude);

            }
            set
            {
                this.locationPushPin = value;
            }

        }
        /*When one of parametres was updated 
         * in set in THAT parametre we are
         * calling notify property of Location
         * to update it on Map
         */
        private double latitude;
        public double vm_Latitude
        {
            get
            {
                vm_Latitude = model.Latitude;
                return model.Latitude;
            }
            set
            {
                latitude = value;
                NotifyPropertyChanged("vm_LocationPushPin");
            }
        }
        private double longitude;
        public double vm_Longitude
        {
            get
            {
                vm_Longitude = model.Longitude;
                return model.Longitude;
            }
            set
            {
                longitude = value;
                NotifyPropertyChanged("vm_LocationPushPin");
            }
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