using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    class MyTelnetClient : ITelnetClient
    {

        private NetworkStream networkStream;
        private bool isConnected;
        private TcpClient tcpClient;

        public bool connect(string ip, int port)
        {
            //string message_SET_THROTTLE = "set /controls/engines/current-engine/throttle 10\n";


            //tring message_GET_THROTTLE = "get /controls/engines/current-engine/throttle\n";

            try
            {
                isConnected = true;
                this.tcpClient = new TcpClient();
                tcpClient.Connect(ip, port);

                this.networkStream = tcpClient.GetStream();
                return isConnected;

            } catch (SocketException e)
            {
                isConnected = false;
                Console.WriteLine(e.Source);
                return isConnected;

            }
           

            /*Byte[] data_SET = System.Text.Encoding.ASCII.GetBytes(message_SET_THROTTLE);
            networkStream.Write(data_SET, 0, data_SET.Length);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message_GET_THROTTLE);
            networkStream.Write(data, 0, data.Length);
            

            byte[] responce = new byte[256];
            int k = networkStream.Read(responce, 0, 100);


            string result = System.Text.Encoding.UTF8.GetString(responce);

            */
        }

        public void disconnect()
        {
            if(this.networkStream != null)
            {
                this.networkStream.Close();

            }
            
            if(this.tcpClient != null)
            {
                this.tcpClient.Close();

            }
        }

        public string read()
        {
            throw new NotImplementedException();
        }

        public void write(string command)
        {
            throw new NotImplementedException();
        }
    }
}
