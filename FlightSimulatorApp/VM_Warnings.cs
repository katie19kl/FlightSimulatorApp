using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    public class VM_Warnings : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;

        public string VM_WarningString
        {
            get
            {
                return this.model.WarningString;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public VM_Warnings(IFlightSimulatorModel simulatorModel)
        {
            this.model = simulatorModel;

            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
