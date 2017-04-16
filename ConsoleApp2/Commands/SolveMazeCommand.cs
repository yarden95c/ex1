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
        public string Execute(string[] args, TcpClient client , string closeConnection, string keepOpen)
        {
            string name = args[0];
            int algorithm = int.Parse(args[1]);
            int getNumberEvaluated = 0;
            string solution = null;
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            if (algorithm == 0)
            {
                solution = MazeAdapter.PrintSolution(model.GetBFSSolution(name));
                //writer.WriteLine(solution);
                getNumberEvaluated = model.GetBFSSolution(name).GetNumberEvaluated();
            }
            else
            {
                solution = MazeAdapter.PrintSolution(model.GetDFSSolution(name));
                //writer.WriteLine(solution);
                getNumberEvaluated = model.GetDFSSolution(name).GetNumberEvaluated();
            }
            NestedSolve solve = new NestedSolve(name, solution, getNumberEvaluated);
            writer.WriteLine(JsonConvert.SerializeObject(solve));
            writer.Flush();
            return closeConnection;
        }
        public class NestedSolve
        {
            public string NameOfMaze;
            public string Solution;
            public int GetNumberEvaluets;
            public NestedSolve(string nameOfMaze, string solution, int getNumberEvaluets)
            {
                this.GetNumberEvaluets = getNumberEvaluets;
                this.Solution = solution;
                this.NameOfMaze = nameOfMaze;
            }
        }
        public bool IsValid(string[] args)
        {
            return (args.Length == 2);
        }
    }


}