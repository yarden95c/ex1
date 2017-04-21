using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// client handler class
    /// </summary>
    /// <seealso cref="Server.IClientHandler" />
    class ClientHandler : IClientHandler
    {
        private IController controller;
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        public ClientHandler()
        {
            controller = new Controller();
        }
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="closeConnection">The close connection.</param>
        /// <param name="keepOpen">The keep open.</param>
        public void HandleClient(TcpClient client, string closeConnection, string keepOpen)
        {
            new Task(() =>
            {

                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (true)
                    {
                      //  Console.WriteLine("waiting for message...");
                        string commandLine = reader.ReadLine();

                        if (commandLine != null)
                        {
                            Console.WriteLine("Got command: {0}", commandLine);
                            string result = controller.ExecuteCommand(commandLine, client, closeConnection, keepOpen);
                            Thread.Sleep(300);
                           
                            if (result == keepOpen)
                            {
                                writer.Write(result);
                                writer.Flush();
                                continue;
                            }
                            else
                            {
                                if (!client.Connected)
                                {
                                    writer.Write(result);
                                    writer.Flush();
                                    break;
                                }
                            }
                        }
                    }
                    client.Close();
                }

            }).Start();
        }
    }
}
