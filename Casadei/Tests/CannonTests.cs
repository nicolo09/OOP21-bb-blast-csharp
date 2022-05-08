using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Text;
using Blast_C;

namespace Project.Tests
{
    [TestClass]
    public class CannonTests
    {
        [TestMethod]
        public void MoveCannonTest()
        {
            List<COLOR> colors = new List<COLOR>((COLOR[])Enum.GetValues(typeof(COLOR)));
            IBubbleGenerator generator = new BubbleGenerator(colors);
            ICannon cannon = new Cannon(new Tuple<double, double>(0, 0), 60, 10, generator);
            int angle = 145;

            cannon.Move(angle);
            Assert.AreEqual(angle, cannon.Angle);

            angle = 176;
            cannon.Move(angle);
            Assert.AreNotEqual(angle, cannon.Angle);

            angle = 4;
            cannon.Move(angle);
            Assert.AreNotEqual(angle, cannon.Angle);
        }

        [TestMethod]
        public void GetCurrentlyLoadedBubbleTest()
        {
            List<COLOR> colors = new List<COLOR>((COLOR[])Enum.GetValues(typeof(COLOR)));
            IBubbleGenerator generator = new BubbleGenerator(colors);
            ICannon cannon = new Cannon(new Tuple<double, double>(0, 0), 60, 10, generator);

            var loaded = cannon.GetCurrentlyLoadedBubble();
            var shot = cannon.Shoot();

            Assert.AreEqual(loaded, shot, "The initially loaded bubble and the shot are the same");
        }
    }
}