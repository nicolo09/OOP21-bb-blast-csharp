using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public interface ICannon
    {

        int Angle { get; }
        void Move(int angle);

        IMovingBubble Shoot();

        IBubble GetCurrentlyLoadedBubble();
    }
}
