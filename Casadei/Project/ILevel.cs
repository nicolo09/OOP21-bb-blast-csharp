using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public interface ILevel
    {
        IBubblesGrid GameGrid { get;  }

        IGridInfo GameGridInfo { get; }

        ICannon GameCannon { get; }

        int GameScore { get; }

        void FillGameBubblesGrid(int rows);
    }
}
