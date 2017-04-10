using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class SolveMazeCommand : ICommand
    {
        private IModel model;
        public SolveMazeCommand(IModel model)
        {
            this.model = model;

        }
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[1];
            int algorithm = int.Parse(args[2]);
            if(algorithm == 0)
            {
                return model.getBFSSolution(name).ToJSon();
            }
            return model.getDFSSolution(name).ToJSon();
        }
    }
}
