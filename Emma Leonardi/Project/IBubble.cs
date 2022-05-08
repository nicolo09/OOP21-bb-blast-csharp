using System;
namespace Project
{
    /// <summary>
    /// Models a Bubble
    /// </summary>
    public interface IBubble
    {
        Tuple<double, double> Position { get; }
        COLOR Color { get; }

        /// <summary>
        /// The position to add so that the Bubble is in position (x + px, y +py).
        /// </summary>
        /// <param name="p">the position to add </param>
        void MoveBy(Tuple<double,double> p);


    }

}

