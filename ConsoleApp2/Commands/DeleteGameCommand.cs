using System;
using System.IO;
using System.Net.Sockets;

namespace Server
{
    internal class DeleteGameCommand : ICommand
    {
        private IModel model;

        public DeleteGameCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client, string closeConnection, string keepOpen)
        {
            string name = args[0];

            model.DeleteSingleGame(name);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("delete: " + name);
            writer.Flush();
            model.MazesMutexRealese();
            return closeConnection;

        }

        public string IsValid(string[] args)
        {

            if (args.Length < 1)
            {
                return "Missing argument";
            }
            model.MazesMutexWaitOn();
            if (!model.IsContainMazeForSolution(args[0]))
            {
                model.MazesMutexRealese();
                return "This name of maze ia already exist";
            }

            return null;
        }
    }
}