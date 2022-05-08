using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// Implementation of a MovingBubble
    /// </summary>
    public class MovingBubble : Bubble, IMovingBubble
    {

        private Tuple<double, double> _speed;
        /// <summary>
        /// Returns a new MovingBubble
        /// </summary>
        /// <param name="pos"> the initial position</param>
        /// <param name="c"> the color of this MovingBubble</param>
        public MovingBubble(Tuple<double, double> pos, COLOR c) : base(pos, c)
        {
            _speed = new Tuple<double, double>(0, 0);
        }
        /// <summary>
        /// Returns a new MovingBubble with the same characteristics as the given Bubble
        /// </summary>
        /// <param name="b"> the bubble</param>
        public MovingBubble(IBubble b) : this(b.Position, b.Color)
        {
        }
        /// <inheritdoc/>
        public double GetSpeedX() => _speed.Item1;
        /// <inheritdoc/>
        public double GetSpeedY() => _speed.Item2;
        /// <inheritdoc/>
        public IBubble GetStationaryCopy() => new Bubble(this);
        /// <inheritdoc/>
        public void Move() => this.MoveBy(_speed);
        /// <inheritdoc/>
        public void SetSpeed(Tuple<double, double> speed) => _speed = new Tuple<double, double>(speed.Item1, speed.Item2);
        /// <inheritdoc/>
        public void SwapSpeedX() => _speed = new Tuple<double, double>(-_speed.Item1, _speed.Item2);
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is MovingBubble bubble &&
                   Position.Item1.Equals(bubble.Position.Item1) && Position.Item2.Equals(bubble.Position.Item2) &&
                   Color == bubble.Color;
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            return "Moving" + base.ToString() + ", " + this._speed.ToString();
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), _speed);
        }
    }
}
