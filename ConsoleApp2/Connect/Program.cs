using Server;
using System;
using System.Configuration;
namespace Server
{
    /// <summary>
    /// the main program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            IClientHandler ch = new ClientHandler();
            string port = ConfigurationManager.AppSettings["port"].ToString();
            ServerConnection s = new ServerConnection(int.Parse(port), ch);
            s.Start();
            Console.ReadKey();
        }
    }
}


