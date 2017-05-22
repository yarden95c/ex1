using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    /// <summary>
    /// join command class
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    internal class JoinMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public JoinMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client, string closeConnection, string keepOpen)
        {
            string name = args[0];
            Game game = model.GetGameFromWaitingList(name);

            if (game.GetFirstClient() != client)
            {
                NetworkStream stream = game.GetFirstClient().GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("without it it doesnt work");
                writer.Flush();
                game.Join(client);
                model.JoinToGame(name);
            }
            else
            {
                new Controller.NestedError("you need another client", client);
            }

            //UNLOCK START AND PLAYING!
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
            if (args.Length < 1)
            {
                return "Missing argument";
            }
            //LOCK START AND PLAYING!
            model.StartAndPlayingMutexWaitOn();
            if (!model.IsGameInWaitingList(args[0]))
            {
                model.StartAndPlayingMutexRealese();
                return "This game is unavailable";
            }
            return null;
        }
    }
}