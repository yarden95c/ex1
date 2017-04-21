using Newtonsoft.Json;
using System.IO;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// play command class
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    internal class PlayCommand : ICommand
    {
        private IModel model;
        private List<String> directions;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayCommand(IModel model)
        {
            this.model = model;
            directions = new List<string>();
            directions.Add("up");
            directions.Add("left");
            directions.Add("right");
            directions.Add("down");
        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client , string closeConnection, string keepOpen)
        {
            string direction = args[0];
            Game game = model.FindGameByClient(client);
            if (game != null)
            {
                NestedPlay play = new NestedPlay(game.GetMaze().Name, direction, game.GetOpponent(client));
            }
            else
            {
                new Controller.NestedError("you need to start a game first", client);
            }
            return keepOpen;
        }
        /// <summary>
        /// Returns true if the inputs is valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public string IsValid(string[] args)
        {
            if (args.Length < 1)
            {
                return "Missing argument";
            }
            if (!directions.Contains(args[0]))
            {
                return "Not a direction";
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public class NestedPlay
        {
            public string Name;
            public string Direction;
            public NestedPlay(string name, string direction, TcpClient client)
            {
                this.Name = name;
                this.Direction = direction;
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);
                writer.WriteLine(JsonConvert.SerializeObject(this));
                writer.Flush();
                Thread.Sleep(200);

            }
        }
    }
}