using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    /// <summary>
    /// search by bfs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Ass1.PrioritySearcher{T}" />
    public class BFS<T> : PrioritySearcher<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BFS{T}"/> class.
        /// </summary>
        public BFS() : base(State<T>.GetDefaultCostComparer()) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="BFS{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public BFS(IComparer<State<T>> comparer) : base(comparer) { }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            AddToOpenList(searchable.GetInitialState()); // inherited from Searcher
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList(); // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.GetGoalState()))
                    return BackTrace(n, this.GetNumberOfNodesEvaluated(), searchable.GetName()); // private method, back traces through the parents

                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s))
                    {
                        // If the current child state isn't in the open list.
                        if (!this.OpenContains(s))
                        {
                            searchable.UpdateCameFrom(s, n);
                            searchable.UpdateCost(s, n);
                            this.AddToOpenList(s);
                        }
                        // If the current child state exists in the open list.
                        else
                        {
                            bool checkBetterDirection = searchable.BetterDiraction(s, n);
                            if (checkBetterDirection)
                            {
                                searchable.UpdateCameFrom(s, n);
                                searchable.UpdateCost(s, n);
                                UpdateItem(s);
                            }
                        }
                    }
                }
            }
            return null;
        }
    }

}
