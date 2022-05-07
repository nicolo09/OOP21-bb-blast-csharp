using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    [TestClass()]
    public class BubblesGridTests
    {

        // Some test bubbles
        private IBubble b1 = new Bubble(new Tuple<double, double>(0.5, 0.577), COLOR.RED);
        private IBubble b2 = new Bubble(new Tuple<double, double>(2.5, 0.577), COLOR.ORANGE);
        private IBubble b3 = new Bubble(new Tuple<double, double>(1, 1.433), COLOR.YELLOW);
        private IBubble b4 = new Bubble(new Tuple<double, double>(2, 1.433), COLOR.GREEN);
        private IBubble b5 = new Bubble(new Tuple<double, double>(1.5, 2.309), COLOR.BLUE);
        private IBubble b6 = new Bubble(new Tuple<double, double>(2.5, 2.309), COLOR.PURPLE);
        private IGridInfo gridInfo = new RegularHexGridInfo(5, 10, 1);
        private static double DELTA = 0.0001;
        [TestMethod()]
        public void BubblesGridTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            IBubblesGrid g2 = new BubblesGrid(new Collection<IBubble>(), gridInfo);
            IBubblesGrid g3 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            Assert.AreEqual(g1, g2, "Two empty grids are equals");
            Assert.IsFalse(g1.Equals(g3), "The grids contain different bubbles");
            var coll = g3.GetBubbles();
            Assert.IsFalse(coll.Count == 0, "The returned collection has bubbles");
            Assert.AreEqual(coll.Count, 4, "The returned collection has the bubbles of the grid");
            Assert.AreEqual(coll.Count, g3.GetBubbles().Count, "The collections contain the same bubbles");
            IBubblesGrid g4 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            Assert.IsTrue(g4.GetBubbles().Intersect(g3.GetBubbles()).Count().Equals(g3.GetBubbles().Count > g4.GetBubbles().Count ? g3.GetBubbles().Count : g4.GetBubbles().Count), "Two grids with the same collection of bubbles are equals");
            coll = new Collection<IBubble>(coll);
            coll.Add(b6);
            Assert.IsFalse(coll.Equals(g3.GetBubbles()),
                    "Changes in the collection don't impact the grid, unless via grid methods");
            Assert.IsFalse(coll.Equals(g4.GetBubbles()), "Collection does't change");
            Assert.IsFalse(g1.Equals(new BubblesGrid(new RegularHexGridInfo(10, 20, 1))),
                    "Two grids with different gridInfo are not equals");
        }

        [TestMethod()]
        public void GetLastRowYTest()
        {
            IBubblesGrid g1 = new BubblesGrid(new Collection<IBubble>() { b1, b3, b5 }, gridInfo);
            var b5Lowest = g1.GetLastRowY();
            Assert.AreEqual(g1.GetLastRowY(), b5Lowest, "The lowest bubble");
            g1.RemoveBubble(b3.Position);
            Assert.AreEqual(g1.GetLastRowY(), b5Lowest, "Still the lowest bubble");
            g1.RemoveBubble(b5.Position);
            var b1Lowest = g1.GetLastRowY();
            Assert.AreEqual(g1.GetLastRowY(), b1Lowest, "New lowest bubble");
            Assert.AreNotEqual(b1Lowest, b5Lowest, DELTA, "The lowest bubble has changed");
            g1.RemoveBubble(b1.Position);
            Assert.AreEqual(g1.GetLastRowY(), 0, "No bubbles are present, so the lowest is 0");
        }

        [TestMethod()]
        public void EndReachedTest()
        {
            IBubblesGrid g1 = new BubblesGrid(new Collection<IBubble>(), gridInfo);
            Assert.IsFalse(g1.EndReached(), "The grid is empty");
            IBubblesGrid g2 = new BubblesGrid(new Collection<IBubble>() { b1 }, gridInfo);
            Assert.IsFalse(g2.EndReached(), "The grid hasn't got a bubble at the bottom");
            IBubblesGrid g3 = new BubblesGrid(new Collection<IBubble>() { new Bubble(new Tuple<double,double>(0, gridInfo.PointsHeight), COLOR.YELLOW)}, gridInfo);
            Assert.IsTrue(g3.EndReached(), "The grid has a bubble at the bottom");
            g3.RemoveUnconnectedBubbles();
            Assert.IsFalse(g3.EndReached(), "The grid hasn't got a bubble at the bottom");
        }

        [TestMethod()]
        public void AddBubbleTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            Assert.AreEqual(g1.GetBubbles().Count, 0, "An empty grid has no bubbles");
            g1.AddBubble(b1);
            Assert.AreEqual(g1.GetBubbles().Count, 1, "The grid has only a bubble");
            g1.AddBubble(b2);
            g1.AddBubble(b3);
            Assert.AreEqual(g1.GetBubbles().Count, 3, "The bubbles contained by the grid");
            g1.AddBubble(b1);
            g1.AddBubble(b4);
            Assert.AreEqual(g1.GetBubbles().Count, 4, "A bubble already inside the grid isn't added");
            var b4Clone = new Bubble(b4.Position, COLOR.PURPLE);
            g1.AddBubble(b4Clone);
            Assert.AreEqual(g1.GetBubbles().Count, 4, "A bubble already inside the grid isn't added");

            var coll = new Collection<IBubble>();
            coll.Add(b1);
            coll.Add(b2);
            IBubblesGrid g2 = new BubblesGrid(coll, gridInfo);
            Assert.AreEqual(g2.GetBubbles().Count, 2, "The original grid");
            coll.Add(b3);
            Assert.AreEqual(g2.GetBubbles().Count, 2,
                    "Changes in the collection don't impact the grid, unless via grid methods");
        }

        [TestMethod()]
        public void RemoveBubbleTest()
        {
            IBubblesGrid g1 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            Assert.AreEqual(g1.GetBubbles().Count, 4, "The original grid");
            g1.RemoveBubble(new Tuple<double, double>(4, 4));
            Assert.AreEqual(g1.GetBubbles().Count, 4, "This bubble wasn't present in the grid, so no changes have happened");
            g1.RemoveBubble(b1.Position);
            Assert.AreEqual(g1.GetBubbles().Count, 3, "Removing a bubble decreases the bubbles in the grid");
            g1.RemoveBubble(b2.Position);
            g1.RemoveBubble(b3.Position);
            g1.RemoveBubble(b4.Position);
            Assert.AreEqual(g1.GetBubbles().Count, 0, "The grid is empty");

            var coll = new Collection<IBubble>();
            coll.Add(b1);
            coll.Add(b2);
            IBubblesGrid g2 = new BubblesGrid(coll, gridInfo);
            Assert.AreEqual(g2.GetBubbles().Count, 2, "The original grid");
            coll.Remove(b3);
            Assert.AreEqual(g2.GetBubbles().Count, 2, "Collection doesn't change");
        }

        [TestMethod()]
        public void IsBubbleAttachableTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            Assert.IsTrue(g1.IsBubbleAttachable(b1), "This bubble connects to the top of the grid");
            Assert.IsFalse(g1.IsBubbleAttachable(b4), "This bubble can't be connected");
            g1.AddBubble(b1);
            Assert.IsTrue(g1.IsBubbleAttachable(b2), "This bubble connects to an existing bubble");
            Assert.IsFalse(g1.IsBubbleAttachable(b4), "This bubble still can't be connected");
            g1.AddBubble(b2);
            Assert.IsTrue(g1.IsBubbleAttachable(b4), "This bubble connects to an existing bubble");
            g1.AddBubble(b4);
            IBubble bdx = new Bubble(new Tuple<double, double>(gridInfo.PointsWidth + 0.1, 0), COLOR.GREEN);
            Assert.IsFalse(g1.IsBubbleAttachable(bdx), "This bubble is out of bounds");
            Bubble bdown = new Bubble(new Tuple<double, double>(0, gridInfo.PointsHeight + 0.1), COLOR.GREEN);
            Assert.IsFalse(g1.IsBubbleAttachable(bdown), "This bubble is out of bounds");
            Bubble bsx = new Bubble(new Tuple<double, double>(0.57, 3), COLOR.GREEN);
            Assert.IsFalse(g1.IsBubbleAttachable(bsx), "This bubble is out of bounds");
        }

        [TestMethod()]
        public void MoveBubblesDownTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            Assert.IsTrue(g1.GetBubbles().Count == 0, "An empty grid");
            g1.MoveBubblesDown(1);
            Assert.IsTrue(g1.GetBubbles().Count == 0, "No changes by moving down no bubbles");
            IBubblesGrid g2 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            g2.MoveBubblesDown(0);
            Assert.AreEqual(g2, new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo),
                    "Moving down by 0 rows doesn't change the grid");
            g2.MoveBubblesDown(-4);
            Assert.AreEqual(g2, new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo),
                    "Moving down by a negative number doesn't change the grid");
            g2.MoveBubblesDown(2);
            Assert.AreEqual(g2.GetBubbles().Count, 4, "The grid has the same number of bubbles");
        }

        [TestMethod()]
        public void CheckForUnconnectedBubblesTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            Assert.IsTrue(g1.CheckForUnconnectedBubbles().Count == 0, "An empty grid has no unconnected bubbles");
            IBubblesGrid g2 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            Assert.IsTrue(g2.CheckForUnconnectedBubbles().Count == 0, "All bubbles are connected");
            IBubblesGrid g3 = new BubblesGrid(new Collection<IBubble>() { b1, b3, b6 }, gridInfo);
            Assert.IsFalse(g3.CheckForUnconnectedBubbles().Count == 0, "Some bubbles are unconnected");
            Assert.AreEqual(g3.CheckForUnconnectedBubbles().Count, 1, "This bubble is unconnected");
            IBubblesGrid g4 = new BubblesGrid(new Collection<IBubble>() { b1, b4, b6 }, gridInfo);
            Assert.IsFalse(g4.CheckForUnconnectedBubbles().Count == 0, "Some bubbles are unconnected");
            Assert.AreEqual(g4.CheckForUnconnectedBubbles().Count, 2, "These bubbles are unconnected");
            g4.RemoveBubble(b1.Position);
            Assert.AreEqual(g4.CheckForUnconnectedBubbles().Count, 2, "These bubbles are unconnected");
            g4.RemoveBubble(b4.Position);
            g4.RemoveBubble(b6.Position);
            Assert.IsTrue(g1.CheckForUnconnectedBubbles().Count == 0, "All bubbles are unconnectes");
        }

        [TestMethod()]
        public void GetSameColorNeighborsTest()
        {
            IBubble b7 = new Bubble(new Tuple<double, double>(1, 1.433), COLOR.RED);
            IBubble b8 = new Bubble(new Tuple<double, double>(3, 1.433), COLOR.RED);
            IBubble b9 = new Bubble(new Tuple<double, double>(2, 1.433), COLOR.RED);
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            Assert.IsTrue(g1.GetSameColorNeighbors(b1).Count == 0,
                    "No neighboring bubbles are present in an empty grid");
            Assert.AreEqual(g1.GetBubbles().Count, 0, "An empty grid contains no bubbles");
            IBubblesGrid g2 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b7, b8 }, gridInfo);
            Assert.AreEqual(g2.GetSameColorNeighbors(b1).Count, 2, "The red neighbors");
            Assert.AreEqual(g2.GetSameColorNeighbors(b2).Count, 1, "The orange neighbors");
            g2.AddBubble(b9);
            Assert.AreEqual(g2.GetSameColorNeighbors(b1).Count, 4, "Adding b9 connects b8 to the neighborhood of b1");
            Assert.AreEqual(g2.GetSameColorNeighbors(b2).Count, 1, "The orange neighbors remain the same");
            g2.RemoveBubble(b7.Position);
            Assert.AreEqual(g2.GetSameColorNeighbors(b1).Count, 1, "Removing b7 divides the red bubbles");
            Assert.AreEqual(g2.GetSameColorNeighbors(b8).Count, 2, "Still neighbors after the removal of b7");

        }

        [TestMethod()]
        public void RemoveBubblesCascadingTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            var origGrid = g1.GetBubbles();
            g1.RemoveBubblesCascading(b1.Position);
            var finalGrid = g1.GetBubbles();
            Assert.IsTrue(g1.CheckForUnconnectedBubbles().Count == 0, "An empty Grid has no unconnected bubbles");
            Assert.IsTrue(finalGrid.Count == origGrid.Count, "An empty Grid has no unconnected bubbles");
            IBubblesGrid g2 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            g2.RemoveBubblesCascading(b2.Position);
            Assert.IsTrue(g2.CheckForUnconnectedBubbles().Count == 0, "All bubbles are connected");
            Assert.AreEqual(g2.GetBubbles().Count, 3, "This grid has all connected bubbles");
            Assert.IsTrue(g2.CheckForUnconnectedBubbles().Count == 0,
                    "All bubbles are connected, even after the removal of some");

            IBubblesGrid g3 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b5 }, gridInfo);
            Assert.IsFalse(g3.CheckForUnconnectedBubbles().Count == 0, "Some bubbles are unconnected");
            g3.RemoveBubblesCascading(b5.Position);
            Assert.AreEqual(g3.GetBubbles().Count, 2, "This grid has some unconnected bubbles");
            Assert.IsTrue(g3.CheckForUnconnectedBubbles().Count == 0, "All unconnected bubbles were removed");
            IBubblesGrid g4 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            Assert.IsTrue(g4.CheckForUnconnectedBubbles().Count == 0, "All bubbles are connected");
            g4.MoveBubblesDown(1);
            Assert.IsFalse(g4.CheckForUnconnectedBubbles().Count == 0,
                    "All bubbles are unconnected, after moving down the whole grid");
            g4.RemoveBubblesCascading(b1.Position);
            Assert.IsTrue(g4.GetBubbles().Count == 0, "All the bubbles were removed");
        }

        [TestMethod()]
        public void RemoveUnconnectedBubblesTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            var origGrid = g1.GetBubbles();
            g1.RemoveUnconnectedBubbles();
            var finalGrid = g1.GetBubbles();
            Assert.IsTrue(finalGrid.Count == origGrid.Count, "An empty grid has no unconnected bubbles");
            IBubblesGrid g2 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            origGrid = g2.GetBubbles();
            g2.RemoveUnconnectedBubbles();
            finalGrid = g2.GetBubbles();
            Assert.IsTrue(finalGrid.Count == origGrid.Count, "This grid has all connected bubbles");
            IBubblesGrid g3 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b5 }, gridInfo);
            Assert.AreEqual(g3.GetBubbles().Count, 3, "This grid has some unconnected bubbles");
            Assert.AreEqual(g3.CheckForUnconnectedBubbles().Count, 1, "This grid has unconnected bubbles");
            g3.RemoveUnconnectedBubbles();
            Assert.AreEqual(g3.GetBubbles().Count, 2, "This grid had some unconnected bubbles");
            Assert.IsTrue(g3.CheckForUnconnectedBubbles().Count==0, "This grid is now all connected");
            IBubblesGrid g4 = new BubblesGrid(new Collection<IBubble>() { b1, b2, b3, b4 }, gridInfo);
            Assert.AreEqual(g4.GetBubbles().Count, 4, "This grid has all connected bubbles");
            Assert.IsTrue(g4.CheckForUnconnectedBubbles().Count==0, "This grid is all connected bubbles");
            g4.MoveBubblesDown(1);
            Assert.AreEqual(g4.CheckForUnconnectedBubbles().Count, 4, "This grid is now all unconnected");
            g4.RemoveUnconnectedBubbles();
            Assert.IsTrue(g4.GetBubbles().Count==0, "This grid had some unconnected bubbles");
            Assert.IsTrue(g4.CheckForUnconnectedBubbles().Count==0, "This grid is now empty");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            IBubblesGrid g1 = new BubblesGrid(gridInfo);
            Assert.AreEqual(g1.ToString(), "BubblesGrid [grid={}]", "The representation of an empty grid");
            g1.AddBubble(b1);
            Assert.AreNotEqual(g1.ToString(), "BubblesGrid []", "A grid with a bubble is not empty");
        }
    }
}