using Newtonsoft.Json;
using System.IO;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
    internal class PlayCommand : ICommand
    {
        private IModel model;
        private List<String> directions; 

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
            if (!directions.Contains(direction))
            {
                new Controller.NestedError("Not a direction", client);
                return keepOpen;
            }
            Game game = this.model.FindGameByClient(client);
            if (game != null)
            {
                NestedPlay play = new NestedPlay(game.GetMaze().Name, direction);
                TcpClient clientOpponent = game.GetOpponent(client);
                NetworkStream stream = clientOpponent.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(JsonConvert.SerializeObject(play));
                writer.Flush();
                Thread.Sleep(200);
            }
            else
            {
                new Controller.NestedError("you need to start a game first", client);
            }
            return keepOpen;
        }

        public bool IsValid(string[] args)
        {
            return (args.Length == 1);
        }

        public class NestedPlay
        {
            public string Name;
            public string Direction;
            public NestedPlay(string name, string direction)
            {
                this.Name = name;
                this.Direction = direction;
            }
        }
    }
}