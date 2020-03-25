﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    public class MyTelnetClient : ITelnetClient
    {

        private NetworkStream networkStream;
        private bool isConnected;
        private TcpClient tcpClient;

        public bool connect(string ip, int port)
        {
            //string message_SET_THROTTLE = "set /controls/engines/current-engine/throttle 10\n";

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
            byte[] response = new byte[512];
            int k = networkStream.Read(response, 0, 100);
            string result = System.Text.Encoding.UTF8.GetString(response);

            return result;

        }

        public void write(string command)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            networkStream.Write(data, 0, data.Length);
        }
    }
}