using System;
using System.Collections.Generic;
using System.Text;

namespace Blast_C
{
    /// <summary>
    /// Class that create a table of scores.
    /// </summary>
    [Serializable()]
    public class ScoreTable
    {
        private readonly IList<IScore> _l;
        /// <summary>
        /// Create a list of scores.
        /// </summary>
        public ScoreTable()
        {
            _l = new List<IScore>();
        }
        /// <summary>
        /// Add a score to the list.
        /// </summary>
        /// <param name="s">The score to add.</param>
        public void AddScore(IScore s)
        {
            _l.Add(s);
        }
        /// <summary>
        /// Return the list of scores.
        /// </summary>
        public IList<IScore> GetList()
        {
            return new List<IScore>(_l);
        }
    }
}