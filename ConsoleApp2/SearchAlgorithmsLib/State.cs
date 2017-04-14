using System;
using System.Collections.Generic;
using System.Text;


namespace Server
{

    /// <summary>
    /// State class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T>
    {
        private T state;  // the state represented by a T

        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        private State(T state) // CTOR
        {
            this.state = state;
            Cost = (float)System.Double.MaxValue;
            Parent = null;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return String.Intern(state.ToString()).GetHashCode();
        }


        /// <summary>
        /// Equalses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.state);
        }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as State<T>);
        }


        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public float Cost
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
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
        /// Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns></returns>
        public T GetPosition()
        {
            return this.state;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return state.ToString();
        }


        /// <summary>
        /// inner class-state pool
        /// </summary>
        public static class StatePool
        {
            private static Dictionary<T, State<T>> pool = new Dictionary<T, State<T>>();

            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <param name="item">The item.</param>
            /// <returns></returns>
            public static State<T> GetState(T item)
            {
                if (!pool.ContainsKey(item))
                {
                    pool.Add(item, new State<T>(item));
                }
                return pool[item];
            }

            /// <summary>
            /// Clears the state pool.
            /// </summary>
            public static void ClearStatePool()
            {
                pool = new Dictionary<T, State<T>>();
            }
        }

        /// <summary>
        /// Gets the default cost comparer.
        /// </summary>
        /// <returns></returns>
        public static IComparer<State<T>> GetDefaultCostComparer()
        {
            return new DefaultCostComparer();
        }


        /// <summary>
        /// default comperator.
        /// </summary>
        private class DefaultCostComparer : IComparer<State<T>>
        {

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
            /// </returns>
            public int Compare(State<T> x, State<T> y)
            {
                float hefresh = x.Cost - y.Cost;
                if (hefresh > 0)
                {
                    return 1;
                }
                if (hefresh < 0)
                {
                    return -1;
                }
                return 0;
            }
        }

    }
}
