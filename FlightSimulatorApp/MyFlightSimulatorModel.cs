using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FlightSimulatorApp
{
    public class MyFlightSimulatorModel : IFlightSimulatorModel
    {
        private ITelnetClient telnetClient;
        private static Mutex mut1 = new Mutex();
        volatile bool stop;
        private string warningString;
        private DispatcherTimer timer;


        private double rudder;
        private double elevator;
        private double aileron;
        private double throttle;
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
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(5); //showing msg on screen for 3 seconds
            this.timer.Tick += delegate { this.WarningString = String.Empty; }; //removing msg
        }

        public string WarningString
        {
            get
            {
                return this.warningString;
            }

            set
            {
                this.warningString = value;
                NotifyPropertyChanged("WarningString");
            }
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
                NotifyPropertyChanged("LocationPushPin");
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
                NotifyPropertyChanged("LocationPushPin");
            }
        }

        public double Aileron
        {
            get
            {
                return this.aileron;
            }

            set
            {
                this.aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }

        public double Throttle
        {
            get
            {
                return this.throttle;
            }

            set
            {
                this.throttle = value;
                NotifyPropertyChanged("Throttle");
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
                    string latitude1 = "set /position/latitude-deg" + " " + foo.ToString() + "\n";


                    foo = rnd.NextDouble() * (32 - 31) + 31;
                    string longitude1 = "set /position/longitude-deg" + " " + foo.ToString() + "\n";

                    /*string airSpeed1 = "get /instrumentation/altimeter/indicated-altitude-ft\n";
                    string altitude1 = "get /instrumentation/gps/indicated-altitude-ft\n";
                    string roll1 = "get /instrumentation/attitude-indicator/internal-roll-deg\n";
                    string pitch1 = "get /instrumentation/attitude-indicator/internal-pitch-deg\n";
                    string altimeter1 = "get /instrumentation/altimeter/indicated-altitude-ft\n";
                    string heading1 = "get /instrumentation/heading-indicator/indicated-heading-deg\n";
                    string groundSpeed1 = "get /instrumentation/gps/indicated-ground-speed-kt\n";
                    string verticalSpeed1 = "get /instrumentation/gps/indicated-vertical-speed\n";
                    string latitude1 = "get /position/latitude-deg\n";
                    string longitude1 = "get /position/longitude-deg\n";*/

                    string answer;

                    mut1.WaitOne();

                    this.telnetClient.write(airSpeed1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if(answer != "ERR")
                    {
                        this.AirSpeed = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving AirSpeed value");
                    }

                    this.telnetClient.write(altitude1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        this.Altitude = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving Altitude value");
                    }

                    this.telnetClient.write(roll1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        this.Roll = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving Roll value");
                    }

                    this.telnetClient.write(pitch1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        this.Pitch = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving Pitch value");
                    }

                    this.telnetClient.write(altimeter1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        this.Altimeter = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving Altimeter value");
                    }

                    this.telnetClient.write(heading1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        this.Heading = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving Heading value");
                    }

                    this.telnetClient.write(groundSpeed1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        this.GroundSpeed = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving GroundSpeed value");
                    }

                    this.telnetClient.write(verticalSpeed1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        this.VerticalSpeed = Double.Parse(answer);
                    } else
                    {
                        showIndicationOnScreen("Error when receiving VerticalSpeed value");
                    }

                    this.telnetClient.write(latitude1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        double lat = Double.Parse(answer);
                        if((-90 <= lat) && (lat <= 90))
                        {
                            this.Latitude = Double.Parse(answer);
                        } else
                        {
                            showIndicationOnScreen("Invalid Latitude value on map(cannot get out of Earth)");
                        }
                    } else
                    {
                        showIndicationOnScreen("Error when receiving Latitude value");
                    }

                    this.telnetClient.write(longitude1);
                    answer = this.telnetClient.read().Split('\n')[0];
                    if (answer != "ERR")
                    {
                        double longi = Double.Parse(answer);
                        if ((-180 <= longi) && (longi <= 180))
                        {
                            this.Longitude = Double.Parse(answer);
                        }
                        else
                        {
                            showIndicationOnScreen("Invalid Longitude value on map(cannot get out of Earth)");
                        }
                    } else
                    {
                        showIndicationOnScreen("Error when receiving Longitude value");
                    }

                    mut1.ReleaseMutex();


                    Thread.Sleep(125);
                }
            }).Start();

        }

        public void showIndicationOnScreen(string warningMsg)
        {
            this.WarningString = warningMsg;
            timer.Start();
        }

        public void sendSetRequest(string setRequest, string varName)
        {
            mut1.WaitOne();
            this.telnetClient.write(setRequest);
            string answer = this.telnetClient.read();

            mut1.ReleaseMutex();

            int index = answer.IndexOf("\n");
            string subStr = answer.Substring(0, index);
            if(subStr == "ERR")
            {
                showIndicationOnScreen("Error when receiving " + varName + " value");
            }
        }
    }
}