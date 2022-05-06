using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class Position : IPosition
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Position(double x, double y) => SetCoords(x, y);

        public IPosition GetCopy() => new Position(this.X, this.Y);

        public void Translate(double dx, double dy) => SetCoords(this.X + dx, this.Y + dy);
        
        public void SetCoords(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

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

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return "Position: X=" + this.X + " Y=" + this.Y;
        }

    }
}
