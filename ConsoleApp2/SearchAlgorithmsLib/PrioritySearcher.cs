using System;
using System.Collections.Generic;
using System.Text;
using Academy.Collections.Generic;

namespace Server
{
    /// <summary>
    /// abstract class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.ISearcher{T}" />
    public abstract class PrioritySearcher<T> : AbstractSearchers<T>
    {
        private PriorityQueue<State<T>> openList;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrioritySearcher{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public PrioritySearcher(IComparer<State<T>> comparer)
        {
            openList = new PriorityQueue<State<T>>(new List<State<T>>(), comparer);
            evaluatedNodes = 0;
        }

        /// <summary>
        /// Adds to open list.
        /// </summary>
        /// <param name="state">The state.</param>
        public void AddToOpenList(State<T> state)
        {
            openList.Enqueue(state);
        }

        /// <summary>
        /// Pops the open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> PopOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }

        /// <summary>
        /// Opens the contains.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public bool OpenContains(State<T> state)
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

        /// <summary>
        /// Gets the size of the open list.
        /// </summary>
        /// <value>
        /// The size of the open list.
        /// </value>
        public int OpenListSize
        { // it is a read-only property :)
            get { return openList.Count; }
        }

        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="newState">The new state.</param>
        public void UpdateItem(State<T> newState)
        {
            List<State<T>> poppedStates = new List<State<T>>();

            // Pop all the states until you reach the state we wish to update.
            while (OpenListSize != 0 && openList.Peek() != newState)
            {
                poppedStates.Add(openList.Dequeue());
            }
            openList.Dequeue();
            openList.Enqueue(newState);
            foreach (State<T> s in poppedStates)
            {
                openList.Enqueue(s);
            }
        }

    }
}
