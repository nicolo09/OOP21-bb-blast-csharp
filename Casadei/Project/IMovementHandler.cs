using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public interface IMovementHandler
    {
        bool IsShotSet { get; }

        IMovingBubble? Shot { get; set; }

        bool Handle();
    }
}
