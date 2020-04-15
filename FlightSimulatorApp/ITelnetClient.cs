using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{

    public interface ITelnetClient
    {

        /* Connect to the server given IP and Port. */
        bool Connect(string ip, int port);

        /* Writes commands to the server,
         * given a string of the command.
         */
        void Write(string command);

        /* Reads the data from the buffer,
         * after it was sent by the server.
         */
        string Read();

        /* Disconnect from the server. */
        void Disconnect();

        /* Flushes the buffer of the tcp client. */
        void MakeFlush();
    }
}