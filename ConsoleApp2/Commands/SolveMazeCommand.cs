
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Server
{
    class SolveMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;

        }
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client = null)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            
            if (algorithm == 0)
            {
                string solution = MazeAdapter.PrintSolution(model.GetBFSSolution(name));
                writer.WriteLine(model.GetBFSSolution(name).ToJSON(solution));
            }
            else
            {
                string solution = MazeAdapter.PrintSolution(model.GetDFSSolution(name));
                writer.WriteLine(model.GetDFSSolution(name).ToJSON(solution));
               
            
            }

            writer.Flush();
            return "close connection";
        }
    }
    
    
}