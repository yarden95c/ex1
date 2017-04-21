using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// the server
    /// </summary>
    class ServerConnection
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private string closeConnection;
        private string keepOpen;
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnection"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public ServerConnection(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
            this.keepOpen = "keep open";
            this.closeConnection = "close connection";
        }
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
           
            Task task = new Task(() => {
                while (true)
                {                    
                    try
                    { 
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client, closeConnection, keepOpen);
                    }
                    catch (SocketException)
                    {
                        continue;
                    }
                }
              //  Console.WriteLine("Server stopped");
            });
            task.Start();
         }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
        
    }
}

