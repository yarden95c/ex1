using System;
using System.Collections.Generic;
using System.Text;
using MazeLib;

namespace SearchAlgorithmsLib
{
    /*
     * the class represent us the object we work with.
     * */
    public class State<T>
    {
        private T state;  // the state represented by a T
        private Position initialPos;

        private State(T state) // CTOR
        {
            this.state = state;
        }

        public State(Position initialPos)
        {
            this.initialPos = initialPos;
        }

        public override int GetHashCode()
        {
            return String.Intern(state.ToString()).GetHashCode();
        }

        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as State<T>);
        }
        public float Cost
        {
            get;
            set;
        }
        public State<T> Parent
        {
            get;
            set;
        }
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(State<T> left, State<T> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(State<T> left, State<T> right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
        /// </returns>
        /// <exception cref="System.ArgumentException"></exception>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            State<T> other = obj as State<T>;
            if (other != null)
                return this.Cost.CompareTo(other.Cost);
            else
                throw new ArgumentException();
        }
        //TODO change this position

        public T getPosition()
        {
            return this.state;
        }
        public override string ToString()
        {
            return state.ToString();
        }
        public static class StatePool
        {
            private static Dictionary<T, State<T>> pool = new Dictionary<T, State<T>>();
            public static State<T> GetState(T item)
            {
                if (!pool.ContainsKey(item))
                {
                    pool.Add(item, new State<T>(item));
                }
                return pool[item];
            }
        }
        public static IComparer<State<T>> GetDefaultCostComparer()
        {
            return new DefaultCostComparer();
        }
        private class DefaultCostComparer : IComparer<State<T>>
        {
            public int Compare(State<T> x, State<T> y)
            {
                if (x.Cost > y.Cost)
                {
                    return 1;
                }
                if (x.Cost < y.Cost)
                {
                    return -1;
                }
                return 0;
            }
        }

    }
}
