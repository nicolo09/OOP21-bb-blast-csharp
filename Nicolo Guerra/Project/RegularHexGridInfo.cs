using System;

namespace Project
{
    /// <summary>
    /// A GridInfo that models a grid of regular hexagons.
    /// </summary>
    public class RegularHexGridInfo : IGridInfo, IEquatable<RegularHexGridInfo>
    {
        /// <inheritdoc/>
        public int BubbleWidth { get; private set; }
        /// <inheritdoc/>
        public int BubbleHeight { get; private set; }
        /// <inheritdoc/>
        public double PointsWidth => this.BubbleWidth * this._ratio + (this._ratio / 2);
        /// <inheritdoc/>
        public double PointsHeight => 3.0 / 4.0 * (2 * (this._ratio / Math.Sqrt(3)) * (this.BubbleHeight - 1)) + 2 * (this._ratio / Math.Sqrt(3));
        /// <inheritdoc/>
        public double BubbleRadius => this._ratio / 2;
        private readonly double _ratio;
        public RegularHexGridInfo(int bubbleWidth, int bubbleHeight, double pointBubbleRatio)
        {
            this.BubbleWidth = bubbleWidth;
            this.BubbleHeight = bubbleHeight;
            this._ratio = pointBubbleRatio;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RegularHexGridInfo);
        }

        public bool Equals(RegularHexGridInfo other)
        {
            return other != null &&
                   BubbleWidth == other.BubbleWidth &&
                   BubbleHeight == other.BubbleHeight &&
                   PointsWidth == other.PointsWidth &&
                   PointsHeight == other.PointsHeight &&
                   BubbleRadius == other.BubbleRadius &&
                   _ratio == other._ratio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BubbleWidth, BubbleHeight, PointsWidth, PointsHeight, BubbleRadius, _ratio);
        }
    }
}
