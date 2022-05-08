using System;
using System.Collections.Generic;
using System.Text;
using Blast_C;

namespace Project
{
    /// <summary>
    /// Implementation of Cannon.
    /// </summary>
    public class Cannon : ICannon
    {
        /// <inheritdoc/>
        public int Angle { get; private set; }

        private readonly Tuple<double, double> _pos;

        private readonly int _fps;

        private readonly int _speedModule;

        private IMovingBubble _loadedBubble;

        private readonly IBubbleGenerator _generator;

        private static readonly int StartAngle = 90;
        private static readonly int MaxAngle = 175;
        private static readonly int MinAngle = 5;
        /// <summary>
        /// Returns a new Cannon
        /// </summary>
        /// <param name="pos"> the position of the center of the Cannon</param>
        /// <param name="fps"> the frame per second of this game</param>
        /// <param name="speedModule"> the speed at which the MovingBubble will be shot</param>
        /// <param name="generator"> the generator of new shots</param>
        public Cannon(Tuple<double, double> pos, int fps, int speedModule, IBubbleGenerator generator)
        {
            this.Angle = StartAngle;
            this._pos = new Tuple<double, double>(pos.Item1, pos.Item2);
            this._fps = fps;
            this._speedModule = speedModule;
            this._generator = generator;
            this._loadedBubble = new MovingBubble(generator.Generate(pos));
        }
        /// <inheritdoc/>
        public IBubble GetCurrentlyLoadedBubble() => _loadedBubble.GetStationaryCopy();

        /// <inheritdoc/>
        public void Move(int angle)
        {
            if (angle >= MinAngle && angle <= MaxAngle)
            {
                Angle = angle;
            }
        }
        /// <inheritdoc/>
        public IMovingBubble Shoot()
        {
            this._loadedBubble.SetSpeed(CalculateSpeed(Angle));
            var shooting = _loadedBubble;
            this._loadedBubble = new MovingBubble(this._generator.Generate(_pos));
            return shooting;
        }

        private Tuple<double, double> CalculateSpeed(int angle)
        {
            var speedX = _speedModule * Math.Cos(angle * Math.PI / 180) / _fps;
            var speedY = -_speedModule * Math.Sin(angle * Math.PI / 180) / _fps;

            return new Tuple<double, double>(speedX, speedY);
        }
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Cannon cannon &&
                   Angle == cannon.Angle &&
                   EqualityComparer<Tuple<double, double>>.Default.Equals(_pos, cannon._pos) &&
                   EqualityComparer<IMovingBubble>.Default.Equals(_loadedBubble, cannon._loadedBubble);
        }
        /// <inheritdoc/>
        public override string? ToString()
        {
            return "Cannon [angle=" + Angle + ", loadedBubble=" + _loadedBubble.ToString() + "]";
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Angle, _pos, _loadedBubble);
        }
    }
}
