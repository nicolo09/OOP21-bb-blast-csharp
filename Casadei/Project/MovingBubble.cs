using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class MovingBubble : Bubble, IMovingBubble
    {

        private Tuple<double, double> _speed;

        public MovingBubble(Tuple<double, double> pos, COLOR c) : base(pos, c)
        {
            _speed = new Tuple<double, double>(0, 0);
        }
        public MovingBubble(IBubble b) : this(b.Position, b.Color) 
        {
        }
        public double GetSpeedX() => _speed.Item1;

        public double GetSpeedY() => _speed.Item2;

        public IBubble GetStationaryCopy() => new Bubble(this);

        public void Move() => this.MoveBy(_speed);

        public void SetSpeed(Tuple<double, double> speed) => _speed = speed;

        public void SwapSpeedX() => _speed = new Tuple<double, double>(-_speed.Item1, _speed.Item2);

        public override bool Equals(object obj)
        {
            return obj is MovingBubble bubble &&
                   Position.Item1.Equals(bubble.Position.Item1) && Position.Item2.Equals(bubble.Position.Item2) &&
                   Color == bubble.Color;
        }

        public override string ToString()
        {
            return "Moving" + base.ToString() + ", " + this._speed.ToString();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), _speed);
        }
    }
}
