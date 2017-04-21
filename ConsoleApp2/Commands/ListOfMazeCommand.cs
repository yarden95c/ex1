using System;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    /// <summary>
    /// list command class
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    internal class ListOfMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListOfMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ListOfMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <param name="closeConnection">The close connection.</param>
        /// <param name="keepOpen">The keep open.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client , string closeConnection, string keepOpen)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(JsonConvert.SerializeObject(model.GetList()));
            writer.Flush();
            return closeConnection;
        }
        /// <summary>
        /// Returns true if the inputs is valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public string IsValid(string[] args)
        { 
            if(args.Length < 0)
            {
                return "Missing argument";
            }
            return null;
        }
    }
}
