using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    /// <summary>
    /// Implementation of MovementHandler.
    /// </summary>
    public class MovementHandler : IMovementHandler
    {
        /// <inheritdoc/>
        public bool IsShotSet { get; private set; }
        /// <inheritdoc/>
        public IMovingBubble? Shot
        {
            get { return _shot; }
            set
            {
                _shot = value;
                if(value != null) 
                { 
                    IsShotSet = true;
                }
            }
        }
        private IMovingBubble? _shot;

        private readonly IGridInfo _infos;

        private readonly IBubblesGrid _grid;

        private readonly Action<IBubble> _action;
        /// <summary>
        /// Returns a new MovementHandler.
        /// </summary>
        /// <param name="infos"> the informations about the grid</param>
        /// <param name="grid"> the BubblesGrid where the MovementBubble will be attached to</param>
        /// <param name="action"> the action to perform when a MovingBubble is attached to the BubblesGrid</param>
        public MovementHandler(IGridInfo infos, IBubblesGrid grid, Action<IBubble> action)
        {
            _infos = infos;
            _grid = grid;
            _action = action;

            IsShotSet = false;
            _shot = null;
        }
        /// <inheritdoc/>
        public bool Handle()
        {
            if (!IsShotSet)
            {
                return false;
            }
            var staticCopy = _shot.GetStationaryCopy();
            if (_grid.IsBubbleAttachable(staticCopy))
            {
                _grid.AddBubble(staticCopy);
                _action.Invoke(staticCopy);
                this._shot = null;
                this.IsShotSet = false;
                return false;
            }
            if (this._shot.GetSpeedY() > 0)
            {
                return false;
            }

            bool fixedX = false;
            bool fixedY = false;
            var nextPos = GetNextPos(_shot);

            if (nextPos.Item1 < _infos.BubbleRadius || nextPos.Item1 > _infos.PointsWidth - _infos.BubbleRadius)
            {
                FixMovement(_shot, nextPos);
                fixedX = true;
            }

            if (nextPos.Item2 < _infos.BubbleRadius) 
            {
                _shot.MoveBy(new Tuple<double, double>(0, -_shot.Position.Item2 + _infos.BubbleRadius));
                fixedY = true;
            }

            if (fixedX && !fixedY)
            {
                _shot.MoveBy(new Tuple<double, double>(0, _shot.GetSpeedY()));
            }
            else if (!fixedX && fixedY)
            {
                _shot.MoveBy(new Tuple<double, double>(_shot.GetSpeedX(), 0));
            }
            else if (!fixedX && !fixedY)
            {
                _shot.Move();
            }


            return true;
        }

        private Tuple<double, double> GetNextPos(IMovingBubble shot) => new Tuple<double, double>(shot.Position.Item1 + shot.GetSpeedX(), shot.Position.Item2 + shot.GetSpeedY());

        private void FixMovement(IMovingBubble shot, Tuple<double, double> nextPos) 
        {
            var limitX = shot.GetSpeedX() > 0
                ? _infos.PointsWidth - _infos.BubbleRadius - shot.Position.Item1
                : -shot.Position.Item1 + _infos.BubbleRadius;
            var correctX = shot.GetSpeedX() > 0
                ? _infos.PointsWidth - _infos.BubbleRadius - nextPos.Item1
                : -nextPos.Item1 + _infos.BubbleRadius;

            shot.MoveBy(new Tuple<double, double>(limitX + correctX, 0));
            shot.SwapSpeedX();
        }

    }
}
