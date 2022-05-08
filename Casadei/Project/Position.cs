using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// Implementation of a bidimensional Position.
    /// </summary>
    public class Position : IPosition
    {
        /// <inheritdoc/>
        public double X { get; private set; }
        /// <inheritdoc/>
        public double Y { get; private set; }
        /// <summary>
        /// Returns a Position with the specified coordinates
        /// </summary>
        /// <param name="x"> the x coordinate</param>
        /// <param name="y"> the y coordinate</param>
        public Position(double x, double y) => SetCoords(x, y);
        /// <inheritdoc/>
        public IPosition GetCopy() => new Position(this.X, this.Y);
        /// <inheritdoc/>
        public void Translate(double dx, double dy) => SetCoords(this.X + dx, this.Y + dy);
        /// <inheritdoc/>
        public void SetCoords(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Position))
            {
                return false;
            }
            var pos = (Position)obj;
            return (this.X == pos.X) && (this.Y == pos.Y);
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            return "Position: X=" + this.X + " Y=" + this.Y;
        }

    }
}
