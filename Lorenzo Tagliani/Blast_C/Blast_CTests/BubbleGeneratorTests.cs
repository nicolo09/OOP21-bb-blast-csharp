using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blast_C;
using System;
using System.Collections.Generic;
using System.Text;
using Project;

namespace Blast_C.Tests
{
    [TestClass()]
    public class BubbleGeneratorTests
    {
        private readonly IBubbleGenerator b1 = new BubbleGenerator(new List<COLOR>((COLOR[])Enum.GetValues(typeof(COLOR))));
        [TestMethod()]
        public void BubbleGeneratorTest()
        {
            Assert.AreNotEqual(b1.Generate(new Tuple<double, double>(0, 0)), null, "The bubble hasn't been generated");
        }
        [TestMethod()]
        public void BubblePositionTest()
        {
            Assert.AreEqual(b1.Generate(new Tuple<double, double>(1, 2)).Position, new Tuple<double, double>(1, 2), "They are in diffrent positions");
        }
    }
}