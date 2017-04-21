using System.IO;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    /// <summary>
    /// close command class
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    internal class CloseCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseCommand(IModel model)
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

        /// <summary>
        /// Returns true if the inputs is valid.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
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