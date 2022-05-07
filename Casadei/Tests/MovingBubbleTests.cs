using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Tests
{
    [TestClass]
    public class MovingBubbleTests
    {
        [TestMethod]
        public void MovingBubbleTest()
        {
            IMovingBubble mb = new MovingBubble(new Tuple<double, double>(0,0), COLOR.RED);
            mb.SetSpeed(new Tuple<double, double>(1, 9));
            Assert.AreEqual(1, mb.GetSpeedX());
            Assert.AreEqual(9, mb.GetSpeedY());

            mb.SwapSpeedX();

            Assert.AreEqual(-1, mb.GetSpeedX());
        }

        [TestMethod]
        public void GetStationaryCopyTest()
        {
            IMovingBubble mb = new MovingBubble(new Tuple<double, double>(0, 0), COLOR.GREEN);
            mb.SetSpeed(new Tuple<double, double>(1, 1));
            IBubble copy = mb.GetStationaryCopy();

            Assert.AreNotEqual(mb, copy);
            Assert.AreEqual(mb.Position, copy.Position);
            Assert.AreEqual(mb.Color, copy.Color);
        }

        [TestMethod]
        public void MoveTest()
        {
            IMovingBubble mb = new MovingBubble(new Tuple<double, double>(0, 0), COLOR.BLUE);
            mb.SetSpeed(new Tuple<double, double>(1, 1));
            mb.Move();

            Assert.AreEqual(mb.Position, new Tuple<double, double>(1, 1));
            mb.SwapSpeedX();
            mb.Move();

            Assert.AreEqual(mb.Position, new Tuple<double, double>(0, 2));

        }

    }
}