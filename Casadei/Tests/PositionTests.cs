using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void StartingPositionTest()
        {
            IPosition pos = new Position(0, 0);
            pos.Translate(1, 1);
            Assert.AreEqual(1, pos.X);
            IPosition copy = pos.GetCopy();
            Assert.AreEqual(pos, copy);
            pos.Translate(1, 1);
            Assert.AreEqual(1, copy.X);
        }

        [TestMethod]
        public void SetCoordsTest()
        {
            IPosition start = new Position(0, 0);
            IPosition stop = new Position(2, 7);
            start.SetCoords(stop.X, stop.Y);

            Assert.AreEqual(start, stop);
        }
    }
}