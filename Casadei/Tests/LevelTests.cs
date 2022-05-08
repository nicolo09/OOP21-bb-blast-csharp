using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Text;
using Blast_C;

namespace Project.Tests
{
    [TestClass]
    public class LevelTests
    {
        private static readonly int Fps = 60;

        [TestMethod]
        public void LevelCreationTest()
        {
            IGridInfo infos = new RegularHexGridInfo(10, 10, 1);
            IBubbleGenerator generator = new BubbleGenerator(new List<COLOR>((COLOR[])Enum.GetValues(typeof(COLOR))));
            ILevel lvl = new Level(infos, generator, Fps);

            Assert.IsNotNull(lvl.GameGrid);
            Assert.IsNotNull(lvl.GameCannon);
        }

        [TestMethod]
        public void FillGameBubblesGridTest()
        {
            IGridInfo infos = new RegularHexGridInfo(10, 10, 1);
            IBubbleGenerator generator = new BubbleGenerator(new List<COLOR>((COLOR[])Enum.GetValues(typeof(COLOR))));
            ILevel lvl = new Level(infos, generator, Fps);

            lvl.FillGameBubblesGrid(1);
            Assert.AreEqual(infos.BubbleWidth, lvl.GameGrid.GetBubbles().Count);
            lvl.FillGameBubblesGrid(2);
            Assert.AreEqual(3 * infos.BubbleWidth, lvl.GameGrid.GetBubbles().Count);
        }
    }
}