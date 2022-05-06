using System;
using System.Collections.Generic;

namespace Project
{
    /// <summary>
    /// Implements a GameOver with scores and a timestamp.
    /// </summary>
    class LastRowGameOver : IGameOver, IEquatable<LastRowGameOver>
    {
        /// <inheritdoc/>
        public IDictionary<int, int> Scores { get; private set; }
        /// <inheritdoc/>
        public DateTime TimeStamp { get; private set; }
        /// <summary>
        /// Creates a new GameOverImpl with now as timestamp.
        /// </summary>
        /// <param name="scores"></param>
        public LastRowGameOver(IDictionary<int, int> scores)
        {
            this.TimeStamp = DateTime.Now;
            this.Scores = new Dictionary<int, int>(scores);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LastRowGameOver);
        }

        public bool Equals(LastRowGameOver other)
        {
            return other != null &&
                   EqualityComparer<IDictionary<int, int>>.Default.Equals(Scores, other.Scores) &&
                   TimeStamp == other.TimeStamp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Scores, TimeStamp);
        }
    }
}
