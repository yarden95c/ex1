using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    public abstract class StackSearcher<T> : ISearcher<T>
    {
        private Stack<State<T>> openList;
        private int evaluatedNodes;
        public StackSearcher()
        {
            openList = new Stack<State<T>>();
            evaluatedNodes = 0;
        }
        public void addToOpenList(State<T> state)
        {
            openList.Push(state);
        }
        public bool isEmpty()
        {
            if (this.openList.Count > 0)
            {
                return false;
            }
            return true;
        }
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Pop();
        }
        public virtual int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        public abstract Solution<T> search(ISearchable<T> searchable);
    }
}
