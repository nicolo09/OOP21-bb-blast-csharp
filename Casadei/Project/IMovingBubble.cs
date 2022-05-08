using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// The interface which models a MovingBubble.
    /// </summary>
    public interface IMovingBubble : IBubble
    {
        /// <summary>
        /// Sets the speed of this MovingBubble.
        /// </summary>
        /// <param name="speed"> the new speed</param>
        void SetSpeed(Tuple<double, double> speed);
        /// <summary>
        /// Gets the x component of the speed.
        /// </summary>
        /// <returns> speed on the x axis</returns>
        double GetSpeedX();
        /// <summary>
        /// Gets the y component of the speed.
        /// </summary>
        /// <returns> speed on the y axis</returns>
        double GetSpeedY();
        /// <summary>
        /// Moves this MovingBubble by its speed.
        /// </summary>
        void Move();
        /// <summary>
        /// Changes the current x speed with its opposite.
        /// </summary>
        void SwapSpeedX();
        /// <summary>
        /// Returns a IBubble copy of this MovingBubble. 
        /// </summary>
        /// <returns> the static copy of this MovingBubble</returns>
        IBubble GetStationaryCopy();
    }
}
