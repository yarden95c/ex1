using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public abstract class AbstractSearchers<T> : ISearcher<T>
    {
        protected int evaluatedNodes;
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        public virtual int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
        /// <summary>
        /// Backs the trace.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="numberOfNodesEvaluated">The number of nodes evaluated.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected Solution<T> BackTrace(State<T> state, int numberOfNodesEvaluated, string name)
        {
            Stack<State<T>> openStack = new Stack<State<T>>();
            List<State<T>> openList = new List<State<T>>();
            openStack.Push(state);
            while ((state = state.Parent) != null)
            {
                openStack.Push(state);
            }
            while (openStack.Count > 0)
            {
                openList.Add(openStack.Pop());
            }
            return new Solution<T>(openList, numberOfNodesEvaluated, name);
        }
    }

}
