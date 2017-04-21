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
    /// <summary>
    /// generate command class
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class GenerateMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(IModel model)
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
            model.MazesMutexRealese();
            return closeConnection;
        }

        /// <summary>
        /// Returns true if the inputs is valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
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
            model.MazesMutexWaitOn();
            if (model.IsContainMazeForSolution(args[0]))
            {
                model.MazesMutexRealese();
                return "This name of maze ia already exist";
            }

            return null;
        }
    }
}

