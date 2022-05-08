using System;
using System.Collections.Generic;
using System.Text;
using Blast_C;

namespace Project
{
    /// <summary>
    /// Implementation of a Level.
    /// </summary>
    public class Level : ILevel
    {
        /// <inheritdoc/>
        public IBubblesGrid GameGrid { get; }
        /// <inheritdoc/>
        public IGridInfo GameGridInfo { get; }
        /// <inheritdoc/>
        public ICannon GameCannon { get; }
        /// <inheritdoc/>
        public int GameScore { get; private set; }

        private static readonly int InitScore = 0;
        private static readonly double CannonVerticalOffsetPercent = 0.97;
        private static readonly double SpeedMultiplier = 16;

        private readonly IBubbleGenerator _generator;

        /// <summary>
        /// Returns a new Level.
        /// </summary>
        /// <param name="infos"> the informations about the grid</param>
        /// <param name="generator"> the random bubble generator</param>
        /// <param name="fps"> the frame per second of this Level</param>
        public Level(IGridInfo infos, IBubbleGenerator generator, int fps)
        {
            GameScore = InitScore;
            GameGridInfo = infos;
            GameGrid = new BubblesGrid(GameGridInfo);
            _generator = generator;
            var cannonPos = new Tuple<double, double>(GameGridInfo.PointsWidth / 2, GameGridInfo.PointsHeight * CannonVerticalOffsetPercent);
            int speed = Convert.ToInt32(Math.Round(SpeedMultiplier * GameGridInfo.BubbleRadius));
            GameCannon = new Cannon(cannonPos, fps, speed, generator);

        }
        /// <inheritdoc/>
        public void FillGameBubblesGrid(int rows)
        {
            GameGrid.MoveBubblesDown(rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < GameGridInfo.BubbleWidth; j++)
                {
                    var centerX = i % 2 == 0
                        ? j * 2 * GameGridInfo.BubbleRadius + GameGridInfo.BubbleRadius
                        : j * 2 * GameGridInfo.BubbleRadius + 2 * GameGridInfo.BubbleRadius;
                    var centerY = i * 2 * GameGridInfo.BubbleRadius + GameGridInfo.BubbleRadius;
                    GameGrid.AddBubble(_generator.Generate(new Tuple<double, double>(centerX, centerY)));
                }
            }
        }
        /// <inheritdoc/>
        public override string? ToString()
        {
            return "Level [score=" + GameScore + ", infos=" + GameGridInfo.ToString() + ", gameGrid=" + GameGrid.ToString() + ", gameCannon="
                + GameCannon.ToString() + ", generator=" + _generator.ToString() + "]";
        }
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Level level &&
                   EqualityComparer<IBubblesGrid>.Default.Equals(GameGrid, level.GameGrid) &&
                   EqualityComparer<IGridInfo>.Default.Equals(GameGridInfo, level.GameGridInfo) &&
                   EqualityComparer<ICannon>.Default.Equals(GameCannon, level.GameCannon) &&
                   GameScore == level.GameScore &&
                   EqualityComparer<IBubbleGenerator>.Default.Equals(_generator, level._generator);
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(GameGrid, GameGridInfo, GameCannon, GameScore, _generator);
        }
    }

}
