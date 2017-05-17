using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// search by dfs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Ass1.StackSearcher{T}" />
    public class DFS<T> : StackSearcher<T>
    {
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T> state = null;
            HashSet<State<T>> hashSet = new HashSet<State<T>>();
            AddToOpenList(searchable.GetInitialState());
            while (!IsEmpty())
            {
                state = PopOpenList();
                if (state.Equals(searchable.GetGoalState()))
                {
                    return BackTrace(state, this.GetNumberOfNodesEvaluated(), searchable.GetName());
                }
                hashSet.Add(state);
                List<State<T>> succerssors = searchable.GetAllPossibleStates(state);
                foreach (State<T> i in succerssors)
                {
                    if (!hashSet.Contains(i))
                    {
                        searchable.UpdateCameFrom(i, state);
                        AddToOpenList(i);
                    }
                }
            }
            return null;
        }
    }
}
