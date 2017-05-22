using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    /// <summary>
    /// start command class
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    internal class StartMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartMazeCommand(IModel model)
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
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            MazeLib.Maze maze = this.model.GetMaze(name, rows, cols);
            Game game = new Game(client, maze);
            model.AddStartGame(game, name);
            //Thread.Sleep(200);
            model.StartAndPlayingMutexRealese();
            return keepOpen;
        }
        /// <summary>
        /// Returns true if the inputs is valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public string IsValid(string[] args)
        {
            string name = args[0];
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

            model.StartAndPlayingMutexWaitOn();
            if (model.IsGameAlreadyExist(name))
            {
                model.StartAndPlayingMutexRealese();
                return "This game is already exist";
            }
            
            return null;
        }
    }
}