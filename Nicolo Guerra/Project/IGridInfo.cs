namespace Project
{
    /// <summary>
    /// Gives informations about a bubble grid.
    /// </summary>
    public interface IGridInfo
    {
        /// <summary>
        /// The grid's width in bubbles.
        /// </summary>
        public int BubbleWidth { get; }
        /// <summary>
        /// The grid's height in bubbles.
        /// </summary>
        public int BubbleHeight { get; }
        /// <summary>
        /// The grid's width in points.
        /// </summary>
        public double PointsWidth { get; }
        /// <summary>
        /// The grid's height in points.
        /// </summary>
        public double PointsHeight { get; }
        /// <summary>
        /// The radius of a bubble in this grid
        /// </summary>
        public double BubbleRadius { get; }

    }
}
