using System;
using System.Collections.Generic;
using System.Text;
using Project;

namespace Blast_C
{
    /// <summary>
    /// Class that implement the methods of score manager.
    /// </summary>
    public class ScoreManager : IScoreManager
    {
        private readonly IPersister<ScoreTable> _f;
        /// <summary>
        /// Create a persister by a table of scores.
        /// </summary>
        /// <param name="f">A new persister.</param>
        public ScoreManager(IPersister<ScoreTable> f)
        {
            _f = f;
        }
        /// <summary>
        /// Load the list of scores.
        /// </summary>
        /// <returns>The loaded list of scores, or a new empty list in case of error.</returns>
        public IList<Score> Load()
        {
            try
            {
                return _f.Load().GetList();
            }
            catch
            {
                return new List<Score>();
            }
        }
        /// <summary>
        /// Reset the file of scores.
        /// </summary>
        public void ResetScore() => _f.Reset();
        /// <summary>
        /// Save a score on the list of scores.
        /// </summary>
        /// <param name="s">The score to save.</param>
        public void Save(Score s)
        {
            try
            {
                var l = _f.Load();
                l.AddScore(s);
                _f.Save(l);
            }
            catch 
            {
                ScoreTable t = new ScoreTable();
                t.AddScore(s);
                _f.Save(t);
            }
        }
    }
}
