using System;
using System.Collections.Generic;
using System.Text;

namespace Blast_C
{
    /// <summary>
    /// Class that represent a score.
    /// </summary>
    public class Score : IScore
    {
        public string Name { get; }
        public int ScoreValue { get; }
        public DateTime Date { get; }
        /// <summary>
        /// Create a score type.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="scoreValue">The value of the score.</param>
        public Score(string name, int scoreValue)
        {
            Name = name;
            ScoreValue = scoreValue;
            Date = DateTime.Now;
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            return "Score [name=" + Name + ", score=" + ScoreValue + ", date=" + Date + "]";
        }
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Score score &&
                   Name == score.Name &&
                   ScoreValue == score.ScoreValue &&
                   Date == score.Date;
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ScoreValue, Date);
        }
    }

}
