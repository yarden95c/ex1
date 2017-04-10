using System;
using System.Net.Sockets;

namespace ConsoleApp2
{
    internal class JoinMazeCommand : ICommand
    {
        private IModel model;

        public JoinMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            throw new NotImplementedException();
        }
    }
}