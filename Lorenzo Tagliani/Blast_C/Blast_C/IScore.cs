using System;
using System.Collections.Generic;
using System.Text;

namespace Blast_C
{
    /// <summary>
    /// Interface that represent the Score type.
    /// </summary>
    public interface IScore
    {
        /// <summary>
        /// The name of the actual player.
        /// </summary>
        String Name { get; }
        /// <summary>
        /// The score value of the actual score.
        /// </summary>
        int ScoreValue { get; }
        /// <summary>
        /// The date of the actual score.
        /// </summary>
        DateTime Date { get; }
    }
}
