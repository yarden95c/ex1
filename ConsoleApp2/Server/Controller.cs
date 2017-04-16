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
    class Controller : IController
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
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
        public string ExecuteCommand(string commandLine, TcpClient client, string closeConnection, string keepOpen)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
            {
                new NestedError("Command not found", client);
                return "close connection";
             }

            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            if (!command.IsValid(args))  
                {
                new NestedError("Missing argument", client);
                return "close connection";

             }
            return command.Execute(args, client, closeConnection, keepOpen);
        }
        public class NestedError
        {
            public string Error;
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
