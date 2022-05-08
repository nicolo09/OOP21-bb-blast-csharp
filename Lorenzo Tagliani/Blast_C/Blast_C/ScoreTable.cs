﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blast_C
{
    /// <summary>
    /// Class that create a table of scores.
    /// </summary>
    public class ScoreTable
    {
        private readonly IList<Score> _l;
        /// <summary>
        /// Create a list of scores.
        /// </summary>
        public ScoreTable()
        {
            _l = new List<Score>();
        }
        /// <summary>
        /// Add a score to the list.
        /// </summary>
        /// <param name="s">The score to add.</param>
        public void AddScore(Score s)
        {
            _l.Add(s);
        }
        /// <summary>
        /// Return the list of scores.
        /// </summary>
        public IList<Score> GetList()
        {
            return new List<Score>(_l);
        }
    }
}