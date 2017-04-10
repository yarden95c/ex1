using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DFS<T> : StackSearcher<T>
    {
        private Solution<T> backTrace(State<T> state)
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
            return new Solution<T>(openList, this.getNumberOfNodesEvaluated());
        }
        public override Solution<T> search(ISearchable<T> searchable)
        {
            State<T> state = null;
            HashSet<State<T>> hashSet = new HashSet<State<T>>();
            addToOpenList(searchable.getInitialState());
            while (!isEmpty())
            {
                state = popOpenList();
                if (state.Equals(searchable.getGoalState()))
                {
                    return backTrace(state);
                }
                hashSet.Add(state);
                List<State<T>> succerssors = searchable.getAllPossibleStates(state);
                foreach (State<T> i in succerssors)
                {
                    if (!hashSet.Contains(i))
                    {
                        addToOpenList(i);
                        i.Parent = state;
                    }
                }
            }
            return null;
        }
    }
}
