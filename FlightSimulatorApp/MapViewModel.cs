using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    public class MapViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;
        private double vm_latitude;
        private double vm_longitude;

        private const double latitudeStart = -90;
        private const double latitudeEnd = 90;
        private const double longitudeStart = -180;
        private const double longitudeEnd = 180;

        public double vm_Latitude
        {
            get
            {
                return this.vm_latitude;
            }

            set
            {
                this.vm_latitude = value;
                NotifyPropertyChanged("VM_Latitude");
            }
        }

        public double vm_Longitude
        {
            get
            {
                return this.vm_longitude;
            }

            set
            {
                this.vm_longitude = value;
                NotifyPropertyChanged("VM_Longitude");
            }
        }

        public MapViewModel(IFlightSimulatorModel simulatorModel)
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
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
