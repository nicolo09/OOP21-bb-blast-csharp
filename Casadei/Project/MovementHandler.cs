using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class MovementHandler : IMovementHandler
    {
        public bool IsShotSet { get; private set; }

        public IMovingBubble? Shot
        {
            get { return Shot; }
            set
            {
                Shot = value;
                IsShotSet = true;
            }
        }

        private readonly IGridInfo _infos;

        private readonly IBubblesGrid _grid;

        private readonly Action<IBubble> _action;

        public MovementHandler(IGridInfo infos, IBubblesGrid grid, Action<IBubble> action)
        {
            _infos = infos;
            _grid = grid;
            _action = action;

            IsShotSet = false;
            Shot = null;
        }
        public bool Handle()
        {
            if (!IsShotSet)
            {
                return false;
            }
            var staticCopy = Shot.GetStationaryCopy();
            if (_grid.IsBubbleAttachable(staticCopy))
            {
                _grid.AddBubble(staticCopy);
                _action.Invoke(staticCopy);
                this.Shot = null;
                this.IsShotSet = false;
                return false;
            }
            if (this.Shot.GetSpeedY() > 0)
            {
                return false;
            }

            bool fixedX = false;
            bool fixedY = false;
            var nextPos = GetNextPos(Shot);

            if (nextPos.Item1 < _infos.BubbleRadius || nextPos.Item1 > _infos.PointsWidth - _infos.BubbleRadius)
            {
                FixMovement(Shot, nextPos);
                fixedX = true;
            }

            if (nextPos.Item2 < _infos.BubbleRadius) 
            {
                Shot.MoveBy(new Tuple<double, double>(0, -Shot.Position.Item2 + _infos.BubbleRadius));
                fixedY = true;
            }

            if (fixedX && !fixedY)
            {
                Shot.MoveBy(new Tuple<double, double>(0, Shot.GetSpeedY()));
            }
            else if (!fixedX && fixedY)
            {
                Shot.MoveBy(new Tuple<double, double>(Shot.GetSpeedX(), 0));
            }
            else if (!fixedX && !fixedY)
            {
                Shot.Move();
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
