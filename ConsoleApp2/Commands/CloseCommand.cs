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
            Game game = this.model.GetGame(name);
            if (game == null)
            {
                writer.WriteLine("The Game Is Not Exist!");
                writer.Flush();
                Thread.Sleep(200);
                return keepOpen;
            }
            else
            {
                model.DeleteGameFromPlayingGames(name);
                writer.WriteLine("exit");
                writer.Flush();
                NetworkStream stream2 = game.GetOpponent(client).GetStream();
                StreamReader reader2 = new StreamReader(stream2);
                StreamWriter writer2 = new StreamWriter(stream2);
                writer2.WriteLine("exit");
                writer2.Flush();
                Thread.Sleep(200);
                return closeConnection;
            }

        }

        public bool IsValid(string[] args)
        {
            return (args.Length >= 1);
        }
    }
}