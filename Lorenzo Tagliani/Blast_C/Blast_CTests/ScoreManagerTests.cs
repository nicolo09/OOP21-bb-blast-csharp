using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blast_C;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Project;

namespace Blast_C.Tests
{
    [TestClass()]
    public class ScoreManagerTests
    {
        private readonly static String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".bbblast-test-csharp", "testfile");
        private readonly IScoreManager x = new ScoreManager(new FilePersister<ScoreTable>(filePath));

        [TestMethod()]
        public void ScoreManagerTest()
        {
            Assert.IsNotNull(x, "It was not created");
            x.ResetScore();
        }

        [TestMethod()]
        public void ScoreUpgradeTest()
        {
            IScore s1 = new Score("nicolo", 50);
            IScore s2 = new Score("casa", 1000);
            IScore s3 = new Score("emma", 5000);
            IList<IScore> y = new List<IScore>();
            y.Add(s1);
            y.Add(s2);
            y.Add(s3);
            x.Save(s1);
            x.Save(s2);
            x.Save(s3);
            Assert.AreEqual(new List<IScore>(x.Load()), y, "They are not the same");
        }

        [TestMethod()]
        public void ScoreResetTest()
        {
            x.ResetScore();
            Assert.AreEqual(x.Load().Count, 0, "It hasn't been resetted");
        }
    }
}