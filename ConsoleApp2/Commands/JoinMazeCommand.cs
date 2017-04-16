using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
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
        public string Execute(string[] args, TcpClient client , string closeConnection, string keepOpen)
        {
            string name = args[0];
            Game game = model.FindGame(name);
            if (game != null)
            {
                if(game.GetFirstClient() != client)
                {
                    game.Join(client);
                }
                else
                {
                    new Controller.NestedError("you need another client", client);
                    model.DeleteGameFromPlayingGames(name);
                    model.AddStartGame(game, name);
                }
            }
            else
            { 
                new Controller.NestedError("This game does not exist", client);
            }
            return keepOpen;
        }
        public bool IsValid(string[] args)
        {
            return (args.Length >= 1);
        }
    }
}