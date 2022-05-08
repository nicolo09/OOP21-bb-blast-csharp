using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// The interface which models a Level.
    /// </summary>
    public interface ILevel
    {
        /// <summary>
        /// The BubblesGrid of this Level.
        /// </summary>
        IBubblesGrid GameGrid { get;  }
        /// <summary>
        /// The GridInfo of this Level
        /// </summary>
        IGridInfo GameGridInfo { get; }

        /// <summary>
        /// The Cannon used in this Level.
        /// </summary>
        ICannon GameCannon { get; }
        /// <summary>
        /// The current score of this Level. 
        /// </summary>
        int GameScore { get; }

        /// <summary>
        /// Fills the specified number of rows of the BubblesGrid.
        /// </summary>
        /// <param name="rows"> the rows of Bubbles to add</param>
        void FillGameBubblesGrid(int rows);
    }
}
