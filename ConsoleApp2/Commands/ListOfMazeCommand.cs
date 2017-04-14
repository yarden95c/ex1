using Newtonsoft.Json;
using System;
using System.Net.Sockets;


namespace Server
{
    internal class ListOfMazeCommand : ICommand
    {
        private IModel model;

        public ListOfMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            return JsonConvert.SerializeObject(model.GetList());
                
        }
    }
}