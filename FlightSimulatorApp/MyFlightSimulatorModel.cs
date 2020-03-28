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

        public double Roll
        {
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

        public double Heading
        {
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

        public double GroundSpeed
        {
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

        public double VerticalSpeed
        {
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

        private double latitude;
        public double Latitude
        {
            get
            {
                return this.latitude;
            }

            set
            {
                this.latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }
        private double longitude;
        public double Longitude
        {
            get
            {
                return this.longitude;
            }

            set
            {
                this.longitude = value;
                NotifyPropertyChanged("Longitude");
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
                Random rnd = new Random();
                while (!stop)
                {

                    int num = rnd.Next(1000);
                    double foo = num % 10;
                    string airSpeed1 = "set /instrumentation/altimeter/indicated-altitude-ft" + " " + foo.ToString() + "\n";
                    num = rnd.Next(1000);
                    foo = num % 10;
                    string altitude1 = "set /instrumentation/gps/indicated-altitude-ft" + " " + foo.ToString() + "\n";
                    num = rnd.Next(1000);
                    foo = num % 10;
                    string roll1 = "set /instrumentation/attitude-indicator/internal-roll-deg" + " " + foo.ToString() + "\n";
                    num = rnd.Next(1000);
                    foo = num % 10;
                    string pitch1 = "set /instrumentation/attitude-indicator/internal-pitch-deg" + " " + foo.ToString() + "\n";
                    num = rnd.Next(1000);
                    foo = num % 10;
                    string altimeter1 = "set /instrumentation/altimeter/indicated-altitude-ft" + " " + foo.ToString() + "\n";
                    num = rnd.Next(1000);
                    foo = num % 10;
                    string heading1 = "set /instrumentation/heading-indicator/indicated-heading-deg" + " " + foo.ToString() + "\n";
                    num = rnd.Next(1000);
                    foo = num % 10;
                    string groundSpeed1 = "set /instrumentation/gps/indicated-ground-speed-kt" + " " + foo.ToString() + "\n";
                    num = rnd.Next(1000);
                    foo = num % 10;
                    string verticalSpeed1 = "set /instrumentation/gps/indicated-vertical-speed" + " " + foo.ToString() + "\n";



                    foo = rnd.NextDouble() * (32 - 31) + 31;
                    string latitude = "set /position/latitude-deg" + " " + foo.ToString() + "\n";


                    foo = rnd.NextDouble() * (32 - 31) + 31;
                    string longitude = "set /position/longitude-deg" + " " + foo.ToString() + "\n";


                    this.telnetClient.write(airSpeed1);
                    this.telnetClient.write(altitude1);
                    this.telnetClient.write(roll1);
                    this.telnetClient.write(pitch1);
                    this.telnetClient.write(altimeter1);
                    this.telnetClient.write(heading1);
                    this.telnetClient.write(groundSpeed1);
                    this.telnetClient.write(verticalSpeed1);

                    this.telnetClient.write(latitude);
                    this.telnetClient.write(longitude);

                    Thread.Sleep(125);


                    string answer = this.telnetClient.read();
                    string[] values = parseAnswer(answer);
                    setValues(values);

                    //Thread.Sleep(125);
                    // Thread.Sleep(1);// read the data in 4Hz
                }
            }).Start();

        }

        /*lexer- divides into tokens->each token is a string representing the value of a member */
        private string[] parseAnswer(string answer)
        {
            int index = answer.IndexOf("\0");
            string subStr = answer.Substring(0, index);
            string[] values = subStr.Split('\n');

            if (values[values.Length - 1] == "")
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
            if(values.Length == 10)
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

                if (values[8] != "ERR") //verticalSpeed
                {
                    this.Latitude = Double.Parse(values[8]);
                }
                if (values[9] != "ERR") //verticalSpeed
                {

                    this.Longitude = Double.Parse(values[9]);
                }
            }
        }
    }
}