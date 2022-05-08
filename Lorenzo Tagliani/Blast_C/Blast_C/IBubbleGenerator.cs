using System;
using System.Collections.Generic;
using System.Text;
using Project;

namespace Blast_C
{
    /// <summary>
    /// Interface for the bubble generator.
    /// </summary>
    public interface IBubbleGenerator
    {
        /// <summary>
        /// Generate a bubble.
        /// </summary>
        /// <param name="p">Position of the bubble.</param>
        /// <returns>The generated bubble.</returns>
        Bubble Generate(Tuple<double, double> p);
    }
}
