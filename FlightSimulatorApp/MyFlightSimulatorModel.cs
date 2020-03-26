using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulatorApp
{
    public class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        private ITelnetClient telnetClient;
        volatile bool stop;

        private double rudder;
        private double elevator;
        private double airSpeed; //8 values from here!
        private double altitude;
        private double roll;
        private double pitch;
        private double altimeter;
        private double heading;
        private double groundSpeed;
        private double verticalSpeed;

        public MyFlightSimulatorModel(ITelnetClient MyTelnetClient)
        {
            this.telnetClient = MyTelnetClient;
            stop = false;
        }

        public double Rudder
        { 
            get
            {
                return this.rudder;
            }

            set
            {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public double Elevator
        { 
            get
            {
                return this.elevator;
            }

            set
            {
                this.elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        public double AirSpeed
        {
            get
            {
                return this.airSpeed;
            }

            set
            {
                this.airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }

        public double Altitude 
        {
            get
            {
                return this.altitude;
            }

            set
            {
                this.altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }

        public double Roll {
            get
            {
                return this.roll;
            }

            set
            {
                this.roll = value;
                NotifyPropertyChanged("Roll");
            }
        }

        public double Pitch
        {
            get
            {
                return this.pitch;
            }

            set
            {
                this.pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }

        public double Altimeter
        {
            get
            {
                return this.altimeter;
            }

            set
            {
                this.altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }

        public double Heading {
            get
            {
                return this.heading;
            }

            set
            {
                this.heading = value;
                NotifyPropertyChanged("Heading");
            }
        }

        public double GroundSpeed {
            get
            {
                return this.groundSpeed;
            }

            set
            {
                this.groundSpeed = value;
                NotifyPropertyChanged("GroundSpeed");
            }
        }

        public double VerticalSpeed {
            get
            {
                return this.verticalSpeed;
            }

            set
            {
                this.verticalSpeed = value;
                NotifyPropertyChanged("VerticalSpeed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public bool connect(string ip, string port)
        {
            int actualPort = Int32.Parse(port);
            return this.telnetClient.connect(ip, actualPort);
        }

        public void disconnect()
        {
            stop = true;
            this.telnetClient.disconnect();
        }

        public void start()
        {
            new Thread(delegate () {
                while (!stop)
                {
                    /*string airSpeed = "get /instrumentation/altimeter/indicated-altitude-ft\n";
                    string altitude = "get /instrumentation/gps/indicated-altitude-ft\n";
                    string roll = "get /instrumentation/attitude-indicator/internal-roll-deg\n";
                    string pitch = "get /instrumentation/attitude-indicator/internal-pitch-deg\n";
                    string altimeter = "get /instrumentation/altimeter/indicated-altitude-ft\n"; //same as first!!!!
                    string heading = "get /instrumentation/heading-indicator/indicated-heading-deg\n";
                    string groundSpeed = "get /instrumentation/gps/indicated-ground-speed-kt\n";
                    string verticalSpeed = "get /instrumentation/gps/indicated-vertical-speed\n";*/

                    string msg = "get /instrumentation/altimeter/indicated-altitude-ft\n"
                    + "get /instrumentation/gps/indicated-altitude-ft\n" 
                    + "get /instrumentation/attitude-indicator/internal-roll-deg\n"
                    + "get /instrumentation/attitude-indicator/internal-pitch-deg\n" 
                    + "get /instrumentation/altimeter/indicated-altitude-ft\n"
                    + "get /instrumentation/heading-indicator/indicated-heading-deg\n"
                    + "get /instrumentation/gps/indicated-ground-speed-kt\n"
                    + "get /instrumentation/gps/indicated-vertical-speed\n";


                    /*------------------------------------- TEST----------------------------------------------*/
                    string set1 = "set /instrumentation/altimeter/indicated-altitude-ft 1\n";
                    this.telnetClient.write(set1);
                    string set2 = "set /instrumentation/gps/indicated-altitude-ft 2\n";
                    this.telnetClient.write(set2);
                    string set3 = "set /instrumentation/attitude-indicator/internal-roll-deg 3\n";
                    this.telnetClient.write(set3);
                    string set4 = "set /instrumentation/attitude-indicator/internal-pitch-deg 4\n";
                    this.telnetClient.write(set4);
                    string set5 = "set /instrumentation/altimeter/indicated-altitude-ft 5\n";
                    this.telnetClient.write(set5);
                    string set6 = "set /instrumentation/heading-indicator/indicated-heading-deg 6\n";
                    this.telnetClient.write(set6);
                    string set7 = "set /instrumentation/gps/indicated-ground-speed-kt 7\n";
                    this.telnetClient.write(set7);
                    string set8 = "set /instrumentation/gps/indicated-vertical-speed7\n";
                    this.telnetClient.write(set8);

                    string answer1 = telnetClient.read();
                    string[] values1 = parseAnswer(answer1);
                    setValues(values1);

                    /*------------------------------------- TEST----------------------------------------------*/


                    this.telnetClient.write(msg);

                    string answer = this.telnetClient.read();
                    string[] values = parseAnswer(answer);
                    setValues(values);
                    Thread.Sleep(125);// read the data in 4Hz
                }
            }).Start();

        }

        /*lexer- divides into tokens->each token is a string representing the value of a member */
        private string[] parseAnswer(string answer)
        {
            int index = answer.IndexOf("\0");
            string subStr = answer.Substring(0, index);
            string[] values = subStr.Split('\n');
            
            if(values[values.Length - 1] == "")
            {
                Array.Resize(ref values, values.Length - 1);
            }

            return values;
        }

        /* Parser- converts to Double and sets the values.
         * In case of a value=ERR, do nothing-> stays with previous value.
         */
        private void setValues(string[] values)
        {
            if(values.Length == 8)
            {
                if (values[0] != "ERR") //airSpeed
                {
                    this.AirSpeed = Double.Parse(values[0]);
                }

                if (values[1] != "ERR") //altitude
                {
                    this.Altitude = Double.Parse(values[1]);
                }

                if (values[2] != "ERR") //roll
                {
                    this.Roll = Double.Parse(values[2]);
                }

                if (values[3] != "ERR") //pitch
                {
                    this.Pitch = Double.Parse(values[3]);
                }

                if (values[4] != "ERR") //altimeter
                {
                    this.Altimeter = Double.Parse(values[4]);
                }

                if (values[5] != "ERR") //heading
                {
                    this.Heading = Double.Parse(values[5]);
                }

                if (values[6] != "ERR") //groundSpeed
                {
                    this.GroundSpeed = Double.Parse(values[6]);
                }

                if (values[7] != "ERR") //verticalSpeed
                {
                    this.VerticalSpeed = Double.Parse(values[7]);
                }
            }
        }
    }
}