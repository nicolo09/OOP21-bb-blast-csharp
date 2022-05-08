using System;
using System.Collections.Generic;
using System.Text;

namespace Blast_C
{
    /// <summary>
    /// Interface that represent the score manager.
    /// </summary>
    public interface IScoreManager
    {
        /// <summary>
        /// Save a score
        /// </summary>
        /// <param name="s">The input score</param>
        void Save(IScore s);
        /// <summary>
        /// Load a score
        /// </summary>
        /// <returns>List of loaded scores</returns>
        IList<IScore> Load();
        /// <summary>
        /// Reset all the scores
        /// </summary>
        void ResetScore();
    }
}
