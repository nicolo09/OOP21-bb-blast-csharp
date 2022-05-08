using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Project.Tests
{
    [TestClass]
    public class MovementHandlerTests
    {

        [TestMethod]
        public void BasicMovementHandlerTest()
        {
            int counter = 0;
            IGridInfo infos = new RegularHexGridInfo(10, 10, 1);
            Action<IBubble> action = new Action<IBubble>(b => counter++);
            IBubble bubbleBase = new Bubble(new Tuple<double, double>(infos.PointsWidth / 2 + 0.5, infos.PointsHeight), COLOR.PURPLE);
            
            IBubblesGrid grid = new BubblesGrid(infos);
            IMovementHandler handler = new MovementHandler(infos, grid, action);
            IMovingBubble mb1 = new MovingBubble(bubbleBase);
            IMovingBubble mb2 = new MovingBubble(bubbleBase);
            mb2.SetSpeed(new Tuple<double, double>(0, 1));

            Assert.IsFalse(handler.Handle(), "The shot isn't set");

            handler.Shot = mb1;

            Assert.IsTrue(handler.IsShotSet);
            Assert.IsTrue(handler.Handle(), "The shot is set and can move, but right now it doesn't have any speed");

            handler.Shot = mb2;

            Assert.IsFalse(handler.Handle(), "The shot is set but has positive Y speed, which can't be handled");

        }

        [TestMethod]
        public void CollisionTest() 
        {
            int counter = 0;
            IGridInfo infos = new RegularHexGridInfo(10, 10, 1);
            Action<IBubble> action = new Action<IBubble>(b => counter++);
            IBubble bubbleBase = new Bubble(new Tuple<double, double>(infos.PointsWidth / 2, infos.PointsHeight), COLOR.PURPLE);
            IBubblesGrid grid = new BubblesGrid(infos);

            IMovementHandler handler = new MovementHandler(infos, grid, action);
            IMovingBubble mb1 = new MovingBubble(bubbleBase);
            IMovingBubble mb2 = new MovingBubble(bubbleBase);
            IMovingBubble mb3 = new MovingBubble(bubbleBase);
            mb1.SetSpeed(new Tuple<double, double>(1, 0));
            handler.Shot = mb1;

            for (int i = 0; i < infos.BubbleWidth / 2 - 1; i++) 
            {
                handler.Handle();
            }

            Assert.AreEqual(new Tuple<double, double>(9.25, infos.PointsHeight), handler.Shot.Position, "The shot has moved of 4 units");
            handler.Handle();
            Assert.AreEqual(new Tuple<double, double>(9.75, infos.PointsHeight), handler.Shot.Position, "The shot could move 0.75 units forward, bounced on the right wall, then moved 0.25 back");
            for (int i = 0; i < infos.BubbleWidth ; i++) {
                handler.Handle();
            }
            Assert.AreEqual(new Tuple<double, double>(1.25, infos.PointsHeight), handler.Shot.Position, "The shot bounced on the left wall");

            mb2.SetSpeed(new Tuple<double, double>(0, -1));
            handler.Shot = mb2;
            while (handler.Handle()) { }
            Assert.AreEqual(1, grid.GetBubbles().Count, "The grid should contain the bubble attached to the top");
            Assert.IsFalse(handler.IsShotSet);

            mb3.SetSpeed(new Tuple<double, double>(1, -0.5));
            handler.Shot = mb3;
            for (int i = 0; i < infos.BubbleWidth / 2; i++)
            {
                handler.Handle();
            }
            Assert.AreEqual(new Tuple<double,double>(9.75, infos.PointsHeight - Math.Abs(mb3.GetSpeedY() * 5)), handler.Shot.Position,"The shot bounced while going up");

        }

        [TestMethod]
        public void AttachmentOnGenericGridTest()
        {
            int counter = 0;
            IGridInfo infos = new RegularHexGridInfo(30, 40, 10);
            Action<IBubble> action = new Action<IBubble>(b => counter++);
            IBubble bubbleBase = new Bubble(new Tuple<double, double>(infos.PointsWidth / 2, infos.PointsHeight), COLOR.PURPLE);

            IBubble b1 = new Bubble(new Tuple<double, double>(infos.BubbleRadius, infos.BubbleRadius), COLOR.RED);
            IBubble b2 = new Bubble(new Tuple<double, double>(2 * infos.BubbleRadius + infos.BubbleRadius, infos.BubbleRadius), COLOR.BLUE);
            IBubble b3 = new Bubble(new Tuple<double, double>(2 * 2 * infos.BubbleRadius + infos.BubbleRadius, infos.BubbleRadius), COLOR.GREEN);
            IBubble b4 = new Bubble(new Tuple<double, double>(2 * 3 * infos.BubbleRadius + infos.BubbleRadius, infos.BubbleRadius), COLOR.YELLOW);
            Collection<IBubble> bcoll = new Collection<IBubble> { b1, b2, b3, b4 };

            IBubblesGrid grid = new BubblesGrid(bcoll, infos);

            IMovementHandler handler = new MovementHandler(infos, grid, action);
            IMovingBubble mb1 = new MovingBubble(bubbleBase);
            mb1.SetSpeed(new Tuple<double, double>(5, -10));
            handler.Shot = mb1;
            int previousDim = grid.GetBubbles().Count;
            while (handler.Handle()) { }
            Assert.AreEqual(previousDim + 1, grid.GetBubbles().Count);
        }

    }
}