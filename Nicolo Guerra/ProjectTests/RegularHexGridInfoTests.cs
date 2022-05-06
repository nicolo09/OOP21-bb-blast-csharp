using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Project.Tests
{
    [TestClass()]
    public class RegularHexGridInfoTests
    {
        [TestMethod()]
        public void RegularHexGridInfoTest()
        {
            int _bubbleWidth = 10;
            int _bubbleHeight = 20;
            int _ratio = 50;
            IGridInfo grid = new RegularHexGridInfo(_bubbleWidth, _bubbleHeight, _ratio);
            Assert.AreEqual(grid.PointsWidth, _bubbleWidth * _ratio + _ratio / 2, "Wrong points width");
            Assert.AreEqual(grid.PointsHeight, (double)3 / 4 * (2 * ((_ratio * Math.Sqrt(3)) / 3) * (_bubbleHeight - 1)) + 2 * ((_ratio * Math.Sqrt(3)) / 3));
        }
    }
}