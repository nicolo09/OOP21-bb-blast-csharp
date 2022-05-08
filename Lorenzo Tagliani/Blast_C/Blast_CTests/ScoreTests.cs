using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blast_C;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blast_C.Tests
{
    [TestClass()]
    public class ScoreTests
    {
        private readonly IScore s1 = new Score("lorenzo", 1000);
        private readonly IScore s2 = new Score("nico", 800);
        [TestMethod()]
        public void ScoreTest()
        {
            Assert.IsTrue(s1.Name.Equals("lorenzo"), "The two names are diffrent");
            Assert.AreSame(s1.ScoreValue, (1000), "The two scores are diffrent");
            Assert.AreNotEqual(s1.Date, null, "The date exist");
            Assert.IsFalse(s2.Equals(s1), "They are the same");
        }
    }
}