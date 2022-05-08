using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// Interface which models a bidimensional Position.
    /// </summary>
    public interface IPosition
    {
        /// <summary>
        /// The x coordinate.
        /// </summary>
        double X { get; }
        /// <summary>
        /// The y coordinate.
        /// </summary>
        double Y { get; }
        /// <summary>
        /// Moves this position by the specified number.
        /// </summary>
        /// <param name="dx"> distance from the x axis</param>
        /// <param name="dy"> distance from the x axis</param>
        void Translate(double dx, double dy);

        /// <summary>
        /// Changes this Position's coordinates with new ones.
        /// </summary>
        /// <param name="x"> the new x coordinate</param>
        /// <param name="y"> the new y coordinate</param>
        void SetCoords(double x, double y);
        /// <summary>
        /// Returns a Position with the same coordinates as this one
        /// </summary>
        /// <returns> a copy of this Position</returns>
        IPosition GetCopy();
    }
}
