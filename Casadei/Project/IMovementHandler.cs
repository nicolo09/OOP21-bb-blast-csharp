using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// The interface which models a MovementHandler.
    /// </summary>
    public interface IMovementHandler
    {
        /// <summary>
        /// The set status of the shot handled by the MovementHandler.
        /// </summary>
        bool IsShotSet { get; }
        /// <summary>
        /// The shot handled by the MovementHandler, it can be null.
        /// </summary>
        IMovingBubble? Shot { get; set; }
        /// <summary>
        /// Handles the movement of the currently set MovingBubble.
        /// </summary>
        /// <returns> true if the MovingBubble could move, false otherwise</returns>
        bool Handle();
    }
}
