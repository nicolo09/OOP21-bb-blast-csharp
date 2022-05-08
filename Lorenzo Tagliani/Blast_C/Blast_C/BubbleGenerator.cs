using System;
using System.Collections.Generic;
using System.Text;
using Project;

namespace Blast_C
{
    /// <summary>
    /// Class for the generation of a random bubble.
    /// </summary>
    public class BubbleGenerator : IBubbleGenerator
    {
        private readonly List<COLOR> _col;
        private readonly Random _rnd;
        /// <summary>
        /// Constructor to generate list of color and random.
        /// </summary>
        /// <param name="col">List of colors.</param>
        public BubbleGenerator(List<COLOR> col)
        {
            _col = col;
            _rnd = new Random();
        }
        /// <summary>
        /// Generate a bubble.
        /// </summary>
        /// <param name="p">Position of the bubble to generate</param>
        /// <returns>Random color generated bubble.</returns>
        public Bubble Generate(Tuple<double, double> p)
        {
            Array v = Enum.GetValues(typeof(COLOR));
            if (_col.Count == 0)
            {
                return new Bubble(p, COLOR.PURPLE);
            }
            COLOR color = (COLOR)v.GetValue(_rnd.Next(v.Length-1));
            return new IBubble(p, color);
        }

        public override bool Equals(object obj)
        {
            return obj is BubbleGenerator generator &&
                   EqualityComparer<List<COLOR>>.Default.Equals(_col, generator._col) &&
                   EqualityComparer<Random>.Default.Equals(_rnd, generator._rnd);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_col, _rnd);
        }
    }
}
