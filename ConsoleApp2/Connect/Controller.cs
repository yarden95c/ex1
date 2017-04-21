using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server;
using Newtonsoft.Json;
using System.IO;

namespace Server
{
    /// <summary>
    /// controller
    /// </summary>
    /// <seealso cref="Server.IController" />
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            model = new Model();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand(model));
            commands.Add("start", new StartMazeCommand(model));
            commands.Add("list", new ListOfMazeCommand(model));
            commands.Add("join", new JoinMazeCommand(model));
            commands.Add("play", new PlayCommand(model));
            commands.Add("close", new CloseCommand(model));
            // more commands...
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <param name="closeConnection">The close connection.</param>
        /// <param name="keepOpen">The keep open.</param>
        /// <returns></returns>
        public string ExecuteCommand(string commandLine, TcpClient client, string closeConnection, string keepOpen)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
            {
                new NestedError("Command not found", client);
                return closeConnection;
             }

            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            string error = command.IsValid(args);
            if (error!= null)  
             {
                new NestedError(error, client);
                return closeConnection;

             }
            return command.Execute(args, client, closeConnection, keepOpen);
        }
        /// <summary>
        /// nested error - for sending messeges in Json
        /// </summary>
        public class NestedError
        {
            public string Error;
            /// <summary>
            /// Initializes a new instance of the <see cref="NestedError"/> class.
            /// </summary>
            /// <param name="error">The error.</param>
            /// <param name="client">The client.</param>
            public NestedError(string error, TcpClient client)
            {
                this.Error = error;
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(JsonConvert.SerializeObject(this));
                writer.Flush();   
            }
        }
    }
}
