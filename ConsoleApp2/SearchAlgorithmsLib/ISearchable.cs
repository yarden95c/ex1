using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        State<T> GetInitialState();
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        State<T> GetGoalState();
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        List<State<T>> GetAllPossibleStates(State<T> s);
        /// <summary>
        /// Betters the diraction.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="state2">The state2.</param>
        /// <returns></returns>
        bool BetterDiraction(State<T> state, State<T> state2);
        /// <summary>
        /// Updates the cost.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="state2">The state2.</param>
        void UpdateCost(State<T> state, State<T> state2);
        /// <summary>
        /// Updates the came from.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="state2">The state2.</param>
        void UpdateCameFrom(State<T> state, State<T> state2);
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        string GetName();
        ///// <summary>
        ///// Prints the solution.
        ///// </summary>
        ///// <param name="solution">The solution.</param>
        ///// <returns></returns>
        //string PrintSolution(Solution<T> solution);
    }
}
