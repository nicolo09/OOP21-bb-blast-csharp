using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public interface IPosition
    {

        double X { get; }
        double Y { get; }

        void Translate(double dx, double dy);

        void SetCoords(double x, double y);

        IPosition GetCopy();
    }
}
