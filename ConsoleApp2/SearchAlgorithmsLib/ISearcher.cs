using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Server
{

    /// <summary>
    /// interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns></returns>
        Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns></returns>
        int GetNumberOfNodesEvaluated();
    }
}
