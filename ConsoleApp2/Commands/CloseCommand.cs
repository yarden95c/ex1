using System.IO;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class CloseCommand : ICommand
    {
        private IModel model;

        public CloseCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client , string closeConnection, string keepOpen)
        {
            string name = args[0];
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            Game game = model.FindGameByClient(client);
            if (game == null || game.GetMaze().Name != name)
            {
                new Controller.NestedError("The game is not in the playing list", client);
                return keepOpen;
            }
            else
            {
                model.DeleteGameFromPlayingGames(name);
                NetworkStream stream2 = game.GetOpponent(client).GetStream();
                StreamReader reader2 = new StreamReader(stream2);
                StreamWriter writer2 = new StreamWriter(stream2);
                writer2.WriteLine("exit");
                writer2.Flush();
                return "exit";
            }

        }

        public string IsValid(string[] args)
        {
            if(args.Length < 1)
            {
                return "Missing argument";
            }

            return null;
        }
    }
}