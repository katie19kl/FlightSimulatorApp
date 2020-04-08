﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace FlightSimulatorApp
{
    public class MyTelnetClient : ITelnetClient
    {

        private NetworkStream networkStream;
        private bool isConnected;
        private TcpClient tcpClient;
        private string timeoutMsg = "time out\n";

        public bool connect(string ip, int port)
        {
            try
            {
                isConnected = true;
                this.tcpClient = new TcpClient();
                tcpClient.Connect(ip, port);

                this.networkStream = tcpClient.GetStream();
                this.networkStream.ReadTimeout = 5000; // 5 seconds
                return isConnected;

            } catch (SocketException)
            {
                isConnected = false;
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
            try
            {
                byte[] response = new byte[256];
                int k = networkStream.Read(response, 0, 256);
                string result = System.Text.Encoding.UTF8.GetString(response);

                networkStream.Flush();

                return result;

            } catch (IOException)
            {
                if(this.tcpClient.Connected) // Timeout
                {
                    return timeoutMsg; // "time out\n"

                }
                else // Server crashed!
                {
                    throw new IOException("Cannot read data!");
                }

            } catch (ObjectDisposedException)
            {
                throw new ObjectDisposedException("Cannot read data!");
            }
        }

        public void write(string command)
        {
            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                networkStream.Write(data, 0, data.Length);

            } catch (IOException) // Server crashed
            {
                throw new IOException("Cannot write data!");
            } catch (ObjectDisposedException)
            {
                throw new ObjectDisposedException("Cannot write data!");
            }                   
        }
    }
}
