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
        public string Execute(string[] args, TcpClient client, string closeConnection, string keepOpen)
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
                getNumberEvaluated = model.GetBFSSolution(name).GetNumberEvaluated();
            }
            else
            {
                solution = MazeAdapter.PrintSolution(model.GetDFSSolution(name));
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

        public string IsValid(string[] args)
        {
            if (args.Length < 2)
            {
                return "Missing argument";
            }
            
            try
            {
                int algorithm = int.Parse(args[1]);

                if (algorithm != 0 && algorithm != 1)
                {
                    return "Wrong number of algorithm, you can choose only 0 or 1";
                }
            }
            catch (System.Exception)
            {
                return "invalid input";

            }
            string name = args[0];
            if (!model.IsContainMazeForSolution(name))
            {
                return "The maze does not exists";
            }

            return null;
        }
    }


}