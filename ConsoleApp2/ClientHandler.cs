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
    class ClientHandler : IClientHandler
    {
        private IController controller;
        public ClientHandler()
        {
            controller = new Controller();
        }
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
                        Console.WriteLine("waiting for message...");
                        string commandLine = reader.ReadLine();

                        if (commandLine != null)
                        {
                            Console.WriteLine("Got command: {0}", commandLine);
                            string result = controller.ExecuteCommand(commandLine, client, closeConnection, keepOpen);
                            Thread.Sleep(300);
                            //if (result == "close connection")
                            //{
                            //    writer.Write(result);
                            //    writer.Flush();
                            //    break;
                            //}
                            if (result == keepOpen)
                            {
                                writer.Write(result);
                                writer.Flush();
                                continue;
                            }
                            else
                            {
                                writer.Write(result);
                                    writer.Flush();
                                    break;
                            }
                        }
                    }
                }
                client.Close();
            }).Start();
        }
    }
}
