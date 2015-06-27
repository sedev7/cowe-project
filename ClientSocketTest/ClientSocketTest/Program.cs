using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Co-Residency Project - Client Socket Test\n\n");
            Console.WriteLine("Attempting to connect to host...\n");

            byte[] remoteHostIpAddrByteArr = { 10,10,10,208 };
            int RemoteHostPortNumber = 8080;
            string RemoteHostName = "ubuntu";
            string RemoteHostIpAddress = "10.10.10.208";

            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            IPAddress ipAddress = new IPAddress(remoteHostIpAddrByteArr);

            // Create a TCP/IP  socket.
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to a remote device.
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, RemoteHostPortNumber);

                s.Connect(remoteEP);

                Console.WriteLine("Connected to remote host: {0} {1}", ipAddress.ToString(), RemoteHostPortNumber);

                string strSocketMessage = "";

                strSocketMessage = "Socket connected to GTC:" + s.RemoteEndPoint.ToString() + "\r\n";

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes("start");

                // Send the data through the socket.
                int bytesSent = s.Send(msg);

                // Receive the response from the remote device.
                int bytesRec = s.Receive(bytes);

                Console.WriteLine("Received message: {0}", bytesRec);

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine ("ArgumentNullException for " + ipAddress.ToString() + ": " + ane.Message);
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException for " + ipAddress.ToString() + ": " + se.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception for " + ipAddress.ToString() + ": " + ex.Message);
            }

            finally
            {
                // Release the socket.
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }

            Console.ReadLine();
        }
    }
}
