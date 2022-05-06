using System;
using System.Collections.Generic;

namespace Project
{
    interface IGameOver
    {
        /// <summary>
        /// A dictionary with the player numbers as keys and their scores as values.
        /// </summary>
        IDictionary<int, int> Scores { get; }
        /// <summary>
        /// This gameover creation timestamp.
        /// </summary>
        DateTime TimeStamp { get; }
    }
}
