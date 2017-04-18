using System;
using System.Collections.Generic;
using System.Linq;
using MazeLib;
using MazeGeneratorLib;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    class GenerateMazeCommand : ICommand
    {
        private IModel model;
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client, string closeConnection, string keepOpen)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.GenerateMaze(name, rows, cols);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(maze.ToJSON());
            writer.Flush();
            return closeConnection;
        }

        public string IsValid(string[] args)
        {
            if (args.Length < 3)
            {
                return "Missing argument";
            }
            try
            {
                if (int.Parse(args[1]) <= 0 || int.Parse(args[2]) <= 0)
                {
                    return "invalid input";
                }
            }
            catch (System.Exception)
            {
                return "invalid input";

            }
            if (model.IsContainMazeForSolution(args[0]))
            {
                return "This name of maze ia already exist";
            }

            return null;
        }
    }
}

