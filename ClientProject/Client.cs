using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Client
    {
        private int port;
        public Client(int port)
        {
            this.port = port;
        }

        public void Connect()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), this.port);
                TcpClient client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                using (StreamWriter buffer = new StreamWriter(stream))
                {
                    Task t = new Task(() =>
                    {
                        Console.Write("Please enter a command: \n");

                        while (true)
                        {

                            string input = reader.ReadLine();
                            if (input != null)
                            {
                                Console.WriteLine(input);
                            }
                        }

                    });
                    t.Start();
                    // Send data to server

                    while (true)
                    {
                        buffer.Flush();
                        buffer.Write(Console.ReadLine() + "\n");
                    }
                    client.Close();

                }
            }
            catch (SocketException)
            {
                Console.WriteLine("EXCEPTION!");
            }

        }
        static void Main(string[] args)
        {
            Client c = new Client(8000);
            c.Connect();
        }

    }
}
