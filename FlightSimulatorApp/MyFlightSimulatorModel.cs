using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;
using System.IO;
using System.Windows;

namespace FlightSimulatorApp
{
    public class MyFlightSimulatorModel : IFlightSimulatorModel
    {

        private ITelnetClient telnetClient;
        private static Mutex mut1 = new Mutex();
        volatile bool stop;
        private string warningString;
        private DispatcherTimer timer;
        private string timeoutMsg = "time out";
        private bool isNumber;
        private double dummy;
        private double rudder;
        private double elevator;
        private double aileron;
        private double throttle;
        private double latitude;
        private double longitude;
        private double airSpeed; // 8 values from here!
        private double altitude;
        private double roll;
        private double pitch;
        private double altimeter;
        private double heading;
        private double groundSpeed;
        private double verticalSpeed;

        public event PropertyChangedEventHandler PropertyChanged;

        /* Private constructor. */
        private MyFlightSimulatorModel(ITelnetClient MyTelnetClient)
        {
            this.telnetClient = MyTelnetClient;
            stop = false;
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromSeconds(3); // Showing msg on screen for 3 seconds
            this.timer.Tick += delegate { this.WarningString = String.Empty; }; // Removing msg
        }

        // Creating Singelton model.
        private static readonly IFlightSimulatorModel Instance = new MyFlightSimulatorModel(new MyTelnetClient());

        /* Getter for the model instance. */
        public static IFlightSimulatorModel model
        {
            get
            {
                return Instance;
            }
        }

        /* Property of warningString variable. */
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

        /* Property of rudder variable. */
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

        /* Property of elevator variable. */
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

        /* Property of airSpeed variable. */
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

        /* Property of altitude variable. */
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

        /* Property of roll variable. */
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

        /* Property of pitch variable. */
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

        /* Property of altimeter variable. */
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

        /* Property of heading variable. */
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

        /* Property of groundSpeed variable. */
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

        /* Property of verticalSpeed variable. */
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

        /* Property of latitude variable. */
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

        /* Property of longitude variable. */
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

        /* Property of aileron variable. */
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

        /* Property of throttle variable. */
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

        /* Notifies the observers of a change that occures. */
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /* In charge of connecting to a server given:
         *
         * string ip
         * string port
         * 
         * returns bool for succeeding or failing to Connect. 
         */
        public bool Connect(string ip, string port)
        {
            int actualPort = Int32.Parse(port);
            return this.telnetClient.Connect(ip, actualPort);
        }

        /* When called - disconnects from the server. */
        public void Disconnect()
        {
            stop = true;
            this.telnetClient.Disconnect();
        }

        /* In charge of executing the model's algorithm. */
        public void Start()
        {
            new Thread(delegate ()
            {
                Random rnd = new Random();
                Thread th = Thread.CurrentThread;

                int i = th.ManagedThreadId;
                while (!stop)
                {
                    try
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

                        this.telnetClient.Write(airSpeed1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.AirSpeed = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");

                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving AirSpeed value");
                            }
                        }

                        this.telnetClient.Write(altitude1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.Altitude = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving Altitude value");
                            }
                        }

                        this.telnetClient.Write(roll1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.Roll = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving Roll value");
                            }
                        }

                        this.telnetClient.Write(pitch1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.Pitch = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving Pitch value");
                            }
                        }

                        this.telnetClient.Write(altimeter1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.Altimeter = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving Altimeter value");
                            }
                        }

                        this.telnetClient.Write(heading1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.Heading = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving Heading value");
                            }
                        }

                        this.telnetClient.Write(groundSpeed1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.GroundSpeed = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving GroundSpeed value");
                            }
                        }

                        this.telnetClient.Write(verticalSpeed1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            this.VerticalSpeed = Double.Parse(answer);
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving VerticalSpeed value");
                            }
                        }

                        this.telnetClient.Write(latitude1);
                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            double lat = Double.Parse(answer);
                            if ((-90 < lat) && (lat < 90))
                            {
                                this.Latitude = Double.Parse(answer);
                            }
                            else
                            {
                                ShowIndicationOnScreen("Invalid Latitude value on map(cannot get out of Earth)");
                            }
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving Latitude value");
                            }
                        }

                        this.telnetClient.Write(longitude1);

                        answer = this.telnetClient.Read().Split('\n')[0];
                        isNumber = double.TryParse(answer, out dummy);

                        if (answer != "ERR" && isNumber)
                        {
                            double longi = Double.Parse(answer);
                            if ((-180 < longi) && (longi < 180))
                            {
                                this.Longitude = Double.Parse(answer);
                            }
                            else
                            {
                                ShowIndicationOnScreen("Invalid Longitude value on map(cannot get out of Earth)");
                            }
                        }
                        else
                        {
                            if (answer == timeoutMsg)
                            {
                                this.timer.Interval = TimeSpan.FromSeconds(4);
                                ShowIndicationOnScreen("Server is under load of requests");
                            }
                            else // Got an ERR as an answer
                            {
                                ShowIndicationOnScreen("Error when receiving Longitude value");
                            }
                        }

                        mut1.ReleaseMutex();

                        Thread.Sleep(125);


                    }
                    catch (IOException e)
                    {
                        this.timer.Interval = TimeSpan.FromSeconds(10);
                        ShowIndicationOnScreen(e.Message);
                        mut1.ReleaseMutex();

                        Disconnect();

                    }
                    catch (ObjectDisposedException e)
                    {
                        this.timer.Interval = TimeSpan.FromSeconds(1);
                        ShowIndicationOnScreen(e.Message);
                        mut1.ReleaseMutex();
                    }
                    catch (NullReferenceException e)
                    {
                        ShowIndicationOnScreen(e.Message);
                    }
                }

            }).Start();

        }

        /* Responsible for showing indication on the UI,
         * of an error that occures.
         */
        public void ShowIndicationOnScreen(string warningMsg)
        {
            this.WarningString = warningMsg; // Updates the warning string.
            timer.Start();
        }

        /* Given a string of a set request and the variable name, 
         * sends the request to the server.
         */
        public void SendSetRequest(string setRequest, string varName)
        {
            try
            {
                if (mut1.WaitOne(2)) // When the server is not under load of requests that slow it down.
                {
                    this.telnetClient.Write(setRequest);
                    string answer = this.telnetClient.Read();
                    mut1.ReleaseMutex();
                    int index = answer.IndexOf("\n");
                    string subStr = answer.Substring(0, index);

                    if (subStr == "ERR")
                    {
                        ShowIndicationOnScreen("Error when setting " + varName + " value");
                    }
                }
            }
            catch (IOException e) // Server crashed!
            {
                this.timer.Interval = TimeSpan.FromSeconds(10);
                ShowIndicationOnScreen(e.Message);
                Disconnect();
                mut1.ReleaseMutex();
            }
            catch (ObjectDisposedException e)
            {
                this.timer.Interval = TimeSpan.FromSeconds(1);
                ShowIndicationOnScreen(e.Message);
                mut1.ReleaseMutex();

            }
            catch (NullReferenceException e) // When trying to write to a server, while there's no connection. 
            {
                ShowIndicationOnScreen(e.Message);
            }
        }
    } // End of class.
}