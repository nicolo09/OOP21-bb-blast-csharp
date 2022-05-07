using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public interface IMovingBubble : IBubble
    {

        void SetSpeed(Tuple<double, double> speed);

        double GetSpeedX();

        double GetSpeedY();

        void Move();

        void SwapSpeedX();

        IBubble GetStationaryCopy();
    }
}
