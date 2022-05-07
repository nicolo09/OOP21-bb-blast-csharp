using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    [TestClass()]
    public class BubbleTests
    {
        [TestMethod()]
        public void BubbleTest()
        {
            Tuple<double, double> t = new Tuple<double, double>(3, 4);
            IBubble b = new Bubble(t, COLOR.YELLOW);
            Assert.AreEqual(b.Position, t, "The two positions are equals");
            Assert.AreEqual(b.ToString(), "Bubble " + b.Color.ToString() + ", " + b.Position.ToString(),
                    "The bubble is printed this way");
            t = new Tuple<double, double>(4, 3);
            Assert.IsFalse(b.Position.Equals(t), "Changes to the position don't impact the bubble");
            Assert.AreEqual(b.ToString(), "Bubble " + b.Color.ToString() + ", " + new Tuple<double, double>(3, 4).ToString(),
                    "The bubble retains the correct value");
        }

        [TestMethod()]
        public void EqualsTest()
        {
            IBubble b1 = new Bubble(new Tuple<double, double>(0, 0), COLOR.BLUE);
            IBubble b2 = new Bubble(new Tuple<double, double>(1, 2), COLOR.RED);
            IBubble b3 = new Bubble(new Tuple<double, double>(-1, 5), COLOR.BLUE);
            IBubble b4 = new Bubble(new Tuple<double, double>(1, 2), COLOR.GREEN);
            Assert.IsFalse(b1.Equals(b2), "Two bubbles with different colors are not equals");
            Assert.IsFalse(b1.Equals(b3), "Two bubbles with different positions are not equals");
            Assert.IsFalse(b2.Equals(b4), "Two bubbles with different positions are not equals");
            Assert.AreEqual(b1,new Bubble(b1), "Two bubbles with same positions and color are equals");
        }

        [TestMethod()]
        public void MoveBubblesByTest()
        {
            Tuple<double, double> t = new Tuple<double, double>(1, 2);
            IBubble b = new Bubble(t, COLOR.PURPLE);
            Assert.AreEqual(b.Position, t, "The two positions are equals");
            Tuple<double, double> t1 = new Tuple<double, double>(t.Item1 + 2, t.Item2 + 2);
            Assert.IsFalse(b.Position.Equals(t1), "Bubble can only be modified by Bubble methods");
            t = b.Position;
            t=new Tuple<double, double> (t.Item1 + 3, t.Item2 + 3);
            Assert.AreEqual(t, new Tuple<double, double>(1 + 3, 2 + 3), "The new position is the sum of the components");
            Assert.IsFalse(t.Equals(b.Position), "Bubble can only be modified by Bubble methods");
            b.moveBy(new Tuple<double, double>(4, 4));
            Assert.AreEqual(b.Position, new Tuple<double, double>(5, 6), "The same position");
        }

        
    }
}