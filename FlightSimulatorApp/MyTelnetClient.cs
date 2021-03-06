﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace FlightSimulatorApp
{

    public class MyTelnetClient : ITelnetClient
    {
        private NetworkStream networkStream;
        private bool isConnected;
        private TcpClient tcpClient;

        /* Connect to the server given IP and Port. */
        public bool Connect(string ip, int port)
        {
            try
            {
                isConnected = true;
                this.tcpClient = new TcpClient();
                tcpClient.Connect(ip, port);

                this.networkStream = tcpClient.GetStream();
                this.networkStream.ReadTimeout = 5000; // 5 seconds
                return isConnected;
            }
            catch (SocketException e)
            {
                isConnected = false;
                Console.WriteLine(e.Source);
                return isConnected;

            }
        }

        /* Disconnect from the server. */
        public void Disconnect()
        {
            if (this.networkStream != null)
            {
                this.networkStream.Close();

            }
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();

            }
        }

        /* Flushes the buffer of the tcp client. */
        public void MakeFlush()
        {
            networkStream.Flush();
        }

        /* Reads the data from the buffer,
         * after it was sent by the server.
         */
        public string Read()
        {
            try
            {
                byte[] response = new byte[256];
                int k = networkStream.Read(response, 0, 256);
                string result = System.Text.Encoding.UTF8.GetString(response);

                networkStream.Flush();

                return result;
            }
            catch (IOException)
            {
                if (this.tcpClient.Connected) // Timeout
                {
                    return "time out\n"; // The server didn't crash, there was a timeout
                }
                else // Server crashed!
                {
                    throw new IOException("didn't succeed to Read");
                }
            }
            catch (ObjectDisposedException)
            {
                throw new ObjectDisposedException("Cannot Read data, as you are not connected to a server!");
            }
        }

        /* Writes commands to the server,
         * given a string of the command.
         */
        public void Write(string command)
        {
            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
                networkStream.Write(data, 0, data.Length);

            }
            catch (IOException) // In case of a timeout
            {
                throw new IOException("didn't succeed to Write");
            }
            catch (ObjectDisposedException)
            {
                throw new ObjectDisposedException("Cannot Write data, as you are not connected to a server!");
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Cannot Write since not connected to a server!");
            }
        }
    }
}