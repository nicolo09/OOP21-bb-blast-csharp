using System;

namespace Project
{
    public class TripletIntegerUtility
    {
        /// <summary>
        /// This method sums the components of two 3 parameted tuples
        /// </summary>
        /// <param name="a">the first tuple</param>
        /// <param name="b">the second tuple</param>
        /// <returns>a new tuple with as components the sum of a and b</returns>
        public static Tuple<int, int, int> add(Tuple<int, int, int> a, Tuple<int, int, int> b)
        {
            return new Tuple<int, int, int>(a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3);

        }
    }
}
