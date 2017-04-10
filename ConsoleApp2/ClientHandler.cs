using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class ClientHandler : IClientHandler
    {
        private IController controller;
        public ClientHandler()
        {
            controller = new Controller();
        }
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {

                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                        string commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        string result = controller.ExecuteCommand(commandLine, client);
                        writer.Flush();
                        writer.Write(result);
                    
                }
//                client.Close();
            }).Start();
        }
    }
}
