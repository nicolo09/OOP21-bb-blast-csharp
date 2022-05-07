using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Project
{
    public class BubblesGrid : IBubblesGrid
    {
        private IDictionary<Tuple<int, int, int>, IBubble> _grid;
        private IList<Tuple<int, int, int>> _directions = new List<Tuple<int, int, int>>()
        {
            new Tuple<int, int, int>(1, 0, -1),new Tuple<int, int, int>(1, -1, 0),
            new Tuple<int, int, int>(0, -1, 1),new Tuple<int, int, int>(-1, 0, 1),
            new Tuple<int, int, int>(-1, 1, 0),new Tuple<int, int, int>(0, 1, -1)
        };
        private readonly IList<IBubble> _neighborsList = new List<IBubble>();
        private readonly IGridInfo _info;
        private readonly double _size;
        private readonly double _xshift = 1.0 / 6.0;
        private readonly double _yshift = -1.0 / 3.0;
        private readonly int _maxBubbles;
        private static readonly double RATIOBUBBLES = 3.0 / 5.0;

        /// <summary>
        /// This constructor creates an empty BubblesGrid
        /// </summary>
        /// <param name="info">The GridInfo that defines the dimentions of the grid</param>
        public BubblesGrid(IGridInfo info)
        {
            _info = info;
            _grid = new Dictionary<Tuple<int, int, int>, IBubble>();
            _size = _info.PointsHeight / (2.0 * (((3.0 / 4.0) * (_info.BubbleHeight - 1)) + 1));
            _maxBubbles = Convert.ToInt32(RATIOBUBBLES * _info.BubbleHeight);
        }

        /// <summary>
        /// This constructor creates a BubbleGrid with every bubble contained in the
        /// collection. Could return a grid with unconnected bubbles, if the collection
        /// contains them.
        /// </summary>
        /// <param name="collection"> The collection from which to read the bubbles to load</param>
        /// <param name="info"> the GridInfo that defines the dimentions of the grid</param>
        public BubblesGrid(Collection<IBubble> collection, IGridInfo info) : this(info)
        {
            foreach (var elem in collection)
            {
                Tuple<int, int, int> triplet = ConvertCoords(elem.Position);
                Tuple<double, double> position = RoundCoords(triplet);
                _grid.Add(triplet, new Bubble(position, elem.Color));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Collection<IBubble> GetBubbles()
        {
            var app = _grid.Values.GetEnumerator();
            var list = new List<IBubble>();
            while (app.MoveNext())
            {
                list.Add(app.Current);
            }
            return new Collection<IBubble>(list);
        }
        /// <summary>
        /// <inheritdoc/>
        /// Returns 0 with an empty BubblesGrid
        /// </summary>
        public double GetLastRowY()
        {
            if (_grid.Count > 0)
            {
                Tuple<int,int,int> lowestBubbleTripl = null;
                foreach(var entry in _grid)
                {
                    if (lowestBubbleTripl==null)
                    {
                        lowestBubbleTripl = entry.Key;
                    }
                    else
                    {
                        if (lowestBubbleTripl.Item2 < entry.Key.Item2)
                        {
                            lowestBubbleTripl = entry.Key;
                        }
                    }

                }
                return _grid[lowestBubbleTripl].Position.Item2;
            }
            return 0;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool EndReached()
        {
            if (_grid.Count > 0)
            {
                Tuple<int, int, int> lowestBubbleTripl = null;
                    foreach (var entry in _grid)
                    {
                        if (lowestBubbleTripl == null)
                        {
                            lowestBubbleTripl = entry.Key;
                        }
                        else
                        {
                            if (lowestBubbleTripl.Item2 < entry.Key.Item2)
                            {
                                lowestBubbleTripl = entry.Key;
                            }
                        }

                    }
                    return lowestBubbleTripl.Item2>=_maxBubbles;
                }
            return false;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void AddBubble(IBubble b)
        {
            if (!_grid.ContainsKey(ConvertCoords(b.Position)) && IsBubbleAttachable(b))
            {
                var triplet = ConvertCoords(b.Position);
                var position = RoundCoords(triplet);
                _grid.Add(ConvertCoords(position), new Bubble(position, b.Color));
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void RemoveBubble(Tuple<double, double> p)
        {
            _grid.Remove(ConvertCoords(p));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsBubbleAttachable(IBubble b)
        {
            if (!_grid.ContainsKey(ConvertCoords(b.Position)))
            {
                var triplet = ConvertCoords(b.Position);
                var position = RoundCoords(triplet);
                if (triplet.Item1 < _info.BubbleWidth && triplet.Item2 < _info.BubbleHeight && position.Item1 > 0 && position.Item2 > 0 && triplet.Item2 >= 1)
                {
                    if (triplet.Item2 == 1)
                    {
                        return true;
                    }
                    if (_grid.Count > 0)
                    {
                        foreach (var dir in _directions)
                        {
                            var tripletNeighbor = TripletIntegerUtility.add(triplet, dir);
                            if (_grid.ContainsKey(tripletNeighbor))
                            {
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void MoveBubblesDown(int rows)
        {
            if (rows > 0)
            {
                IDictionary<Tuple<int, int, int>, IBubble> tmp = new Dictionary<Tuple<int, int, int>, IBubble>();
                foreach (var entry in _grid)
                {
                    int r = entry.Key.Item2 + rows;
                    int q = rows % 2 == 1 ? entry.Key.Item1 - rows / 2 - r % 2 : entry.Key.Item1 - rows / 2;
                    var triplet = new Tuple<int, int, int>(q, r, -q - r);
                    tmp.Add(triplet, new Bubble(RoundCoords(triplet), entry.Value.Color));
                }
                _grid.Clear();
                foreach (var entry in tmp)
                {
                    _grid.Add(entry);
                }

            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Collection<IBubble> CheckForUnconnectedBubbles()
        {
            // The neighborsList hosts all the bubbles that are reachable by a bubble
            // connected to the top
            _neighborsList.Clear();
            if (_grid.Count > 0)
            {
                foreach (var elem in _grid)
                {
                    if (elem.Key.Item2 == 1)
                    {
                        CheckForUnconnectedBubblesRecursive(elem.Key);
                    }
                }
            }
            // neighborsList contains now all the connected bubbles.
            IDictionary<Tuple<int, int, int>, IBubble> difference = new Dictionary<Tuple<int, int, int>, IBubble>(_grid);
            foreach (var elem in _neighborsList)
            {
                difference.Remove(ConvertCoords(elem.Position));
            }
            var values = new IBubble[difference.Values.Count];
            difference.Values.CopyTo(values, 0);
            return new Collection<IBubble>(values);
        }

        private void CheckForUnconnectedBubblesRecursive(Tuple<int, int, int> key)
        {
            _neighborsList.Add(_grid[key]);
            foreach (var dir in _directions)
            {
                var bVisiting = TripletIntegerUtility.add(dir, key);
                if (_grid.ContainsKey(bVisiting) && !_neighborsList.Contains(_grid[bVisiting]))
                {
                    // If the bubble is present in the grid but isn't already visited
                    CheckForUnconnectedBubblesRecursive(bVisiting);
                }

            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Collection<IBubble> GetSameColorNeighbors(IBubble b)
        {
            _neighborsList.Clear();
            var t = ConvertCoords(b.Position);
            if (_grid.Count > 0 && _grid.ContainsKey(t))
            {
                GetSameColorNeighborsRecursive(t);
            }
            var values = new IBubble[_neighborsList.Count];
            _neighborsList.CopyTo(values, 0);
            return new Collection<IBubble>(values);
        }

        private void GetSameColorNeighborsRecursive(Tuple<int, int, int> t)
        {
            if (_grid.ContainsKey(t) && !_neighborsList.Contains(_grid[t]))
            {
                // We have visited the bubble
                _neighborsList.Add(new Bubble(_grid[t]));

                foreach (var dir in _directions)
                {
                    var tripletNeighbor = TripletIntegerUtility.add(t, dir);
                    if (_grid.ContainsKey(tripletNeighbor)
                            && _grid[tripletNeighbor].Color.Equals(_grid[t].Color))
                    {

                        // We visit the neighbor bubbles recursively
                        GetSameColorNeighborsRecursive(tripletNeighbor);
                    }
                }
            }
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void RemoveBubblesCascading(Tuple<double, double> p)
        {
            RemoveBubble(p);
            RemoveUnconnectedBubbles();
        }

        public void RemoveUnconnectedBubbles()
        {
            var coll = CheckForUnconnectedBubbles();
            foreach(var elem in coll)
            {
                _grid.Remove(ConvertCoords(elem.Position));
            }
        }

        /// <summary>
        /// Converts a 2D tuple into a 3D one
        /// </summary>
        /// <param name="position">The position to convert</param>
        /// <returns>The converted tuple</returns>
        private Tuple<int, int, int> ConvertCoords(Tuple<double, double> position)
        {
            var q = Math.Round((Math.Sqrt(3.0) / 3.0 * position.Item1 - 1.0 / 3.0 * position.Item2) / _size);
            var r = Math.Round((2.0 / 3.0 * position.Item2) / _size);
            return new Tuple<int, int, int>(Convert.ToInt32(q), Convert.ToInt32(r), Convert.ToInt32(-q - r));
        }

        private Tuple<double, double> RoundCoords(Tuple<int, int, int> triplet)
        {
            double x = ((triplet.Item1 * 1.0 + _xshift) * Math.Sqrt(3.0))
                + ((triplet.Item2 * 1.0 + _yshift) * (Math.Sqrt(3.0) / 2.0));
            double y = ((triplet.Item2 * 1.0 + _yshift) * (3.0 / 2.0));

            return new Tuple<double, double>(x * _size, y * _size);
        }

        public override bool Equals(object obj)
        {
            return obj is BubblesGrid grid &&
                   EqualityComparer<IDictionary<Tuple<int, int, int>, IBubble>>.Default.Equals(_grid, grid._grid) &&
                   EqualityComparer<IGridInfo>.Default.Equals(_info, grid._info);
        }

        public override int GetHashCode()
        {
            int hashCode = -595143820;
            hashCode = hashCode * -1521134295 + EqualityComparer<IDictionary<Tuple<int, int, int>, IBubble>>.Default.GetHashCode(_grid);
            hashCode = hashCode * -1521134295 + EqualityComparer<IGridInfo>.Default.GetHashCode(_info);
            return hashCode;
        }


        public override string ToString()
        {
            String s = "BubblesGrid [grid=[";
            foreach (var entry in _grid)
            {
                s = s + entry.ToString();
            }
            s = s + "]]";
            return s;
        }
    }
}
