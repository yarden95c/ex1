using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Server
{
    /// <summary>
    /// solution class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T>
    {
        private List<State<T>> solutionList;
        private int numberOfStepst;
        private int numberOfStepsCalculate;
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        public Solution()
        {
            this.solutionList = null;
            this.numberOfStepst = 0;
            this.numberOfStepsCalculate = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        /// <param name="listSolution">The list solution.</param>
        /// <param name="number">The number.</param>
        /// <param name="name">The name.</param>
        public Solution(List<State<T>> listSolution, int number, string name)
        {
            this.solutionList = listSolution;
            this.numberOfStepst = this.solutionList.Count();
            this.numberOfStepsCalculate = number;
            this.name = name;
        }
        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <returns></returns>
        public List<State<T>> GetSolution()
        {
            return solutionList;
        }

        /// <summary>
        /// Sets the solution.
        /// </summary>
        /// <param name="newSolution">The new solution.</param>
        public void SetSolution(List<State<T>> newSolution)
        {
            this.solutionList = newSolution;
        }

        /// <summary>
        /// Equalses the specified solution.
        /// </summary>
        /// <param name="solution">The solution.</param>
        /// <returns></returns>
        public bool Equals(Solution<T> solution) // we overload Object's Equals method
        {
            return solutionList.Equals(solution.GetSolution());
        }

        /// <summary>
        /// Prints the solution.
        /// </summary>
        public void PrintSolution()
        {
            Console.WriteLine(this.numberOfStepst);
            Console.WriteLine(this.name);
            Console.WriteLine(this.numberOfStepsCalculate);
            //   foreach (State<T> i in this.solutionList)
            //    {
            //      Console.WriteLine(i.ToString());
            //  }
        }
        /* public string ToJSON()
         {
             return "TOJSON SOLUTION";
         }*/
        public int GetNumberEvaluated()
        {
            return this.numberOfStepst;
        }
    }
}
