using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class StartMazeCommand : ICommand
    {
        private IModel model;

        public StartMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client , string closeConnection, string keepOpen)
        {
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            MazeLib.Maze maze = this.model.GetMaze(name, rows, cols);
            Game game = new Game(client, maze);
            bool exist = this.model.AddStartGame(game, name);
            if (!exist)
            {
                new Controller.NestedError("This game is already exist", client);
                Thread.Sleep(200);
                return keepOpen;
            }
            Thread.Sleep(200);
            return keepOpen;
        }
        public bool IsValid(string[] args)
        {
            return (args.Length >= 3);
        }
    }
}