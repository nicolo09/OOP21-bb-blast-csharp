using System;
using System.Collections.Generic;

namespace Project
{
    /// <summary>
    /// Class <c>Bubble</c> implements <c>IBubble</c>
    /// </summary>
    public class Bubble : IBubble
    {

        private Tuple<double, double> _pos;
        private COLOR _color;
        /// <summary>
        /// Creates a copy of the bubble
        /// </summary>
        /// <param name="b"> the bubble to create a copy of</param>
        public Bubble(IBubble b) : this(b.Position, b.Color)
        {

        }
        /// <summary>
        /// The position of the bubble
        /// </summary>
        public Tuple<double, double> Position
        {
            get => _pos;
            private set => _pos = new Tuple<double, double>(value.Item1, value.Item2);
        }

        /// <summary>
        /// The color of the bubble
        /// </summary>
        public COLOR Color
        {
            get => _color;
            private set => _color = value;
        }

        /// <summary>
        /// Creates a bubble with the parameters given
        /// </summary>
        /// <param name="p">the position of the bubble</param>
        /// <param name="c">the color of the bubble</param>
        public Bubble(Tuple<double, double> p, COLOR c)
        {
            Position = p;
            Color = c;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void MoveBy(Tuple<double, double> p)
        {
            Position = new Tuple<double, double>(Position.Item1 + p.Item1, Position.Item2 + p.Item2);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Bubble bubble &&
                   Position.Item1.Equals(bubble.Position.Item1) && Position.Item2.Equals(bubble.Position.Item2)&&
                   Color == bubble.Color;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = -866678350;
            hashCode = hashCode * -1521134295 + EqualityComparer<Tuple<double, double>>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return "Bubble " + Color.ToString() + ", " + Position.ToString();
        }

    }

}