using System;
using System.Collections.Generic;
using System.Text;
using Academy.Collections.Generic;

namespace SearchAlgorithmsLib
{
    public abstract class PrioritySearcher<T> : ISearcher<T>
    {
        private PriorityQueue<State<T>> openList;
        private int evaluatedNodes;

        public PrioritySearcher(IComparer<State<T>> comparer)
        {
            openList = new PriorityQueue<State<T>>(new List<State<T>>(), comparer);
            evaluatedNodes = 0;
        }
        public void addToOpenList(State<T> state)
        {
            openList.Enqueue(state);
        }
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }
        public bool openContains(State<T> state)
        {
            foreach (State<T> s in this.openList)
            {
                if (state.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }
        // a property of openList
        public int OpenListSize
        { // it is a read-only property :)
            get { return openList.Count; }
        }

        // ISearcher’s methods:

        public virtual int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        /*public void updateItem(State<T> state, float coast)
        {
            this.openList.UpdatePriority(state, coast);
        }*/
        public void updateItem(State<T> newState)
        {
            List<State<T>> poppedStates = new List<State<T>>();
            State<T> poppedState = openList.Dequeue();

            // Pop all the states until you reach the state we wish to update.
            while (OpenListSize != 0 && openList.Peek() != newState)
            {
                poppedStates.Add(openList.Dequeue());
            }

            // If the new state's cost is actually lower than the old one's.
            if (openList.Peek().Cost > newState.Cost)
            {
                // Dequeue the state with the old cost 
                // and enqueue the state with the new cost.
                openList.Dequeue();
                openList.Enqueue(newState);
            }

            // Re-enqueue the popped states
            foreach (State<T> s in poppedStates)
            {
                openList.Enqueue(s);
            }
        }
        public abstract Solution<T> search(ISearchable<T> searchable);

    }
}
