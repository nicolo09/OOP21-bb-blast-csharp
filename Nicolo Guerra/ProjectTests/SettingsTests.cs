using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Tests
{
    [TestClass()]
    public class SettingsTests
    {
        [TestMethod()]
        public void SettingsTest()
        {
            int master = 82;
            int music = 10;
            int effects = 68;
            ISettings settings = new Settings(master,music,effects);
            Assert.AreEqual(settings.MasterVolume, master);
            Assert.AreEqual(settings.MusicVolume, music);
            Assert.AreEqual(settings.EffectsVolume, effects);
        }
    }
}