using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// The interface which models a Cannon.
    /// </summary>
    public interface ICannon
    {
        /// <summary>
        /// The current angle of this Cannon.
        /// </summary>
        int Angle { get; }
        /// <summary>
        /// Switches the Cannon's angle to the new one.
        /// </summary>
        /// <param name="angle"> the new angle</param>
        void Move(int angle);
        /// <summary>
        /// Makes the Cannon shoot the currently loaded Bubble.
        /// </summary>
        /// <returns> the MovingBubble that has been shot</returns>
        IMovingBubble Shoot();
        /// <summary>
        /// Gets the Bubble inside the Cannon.
        /// </summary>
        /// <returns> the currently loaded Bubble</returns>
        IBubble GetCurrentlyLoadedBubble();
    }
}
