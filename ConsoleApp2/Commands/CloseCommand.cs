using System;
using System.Net.Sockets;

namespace ConsoleApp2
{
    internal class CloseCommand : ICommand
    {
        private IModel model;

        public CloseCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            throw new NotImplementedException();
        }
    }
}