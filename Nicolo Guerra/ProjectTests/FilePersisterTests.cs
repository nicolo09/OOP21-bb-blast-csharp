using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Project.Tests
{
    [TestClass()]
    public class FilePersisterTests
    {
        String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".bbblast-test-csharp", "testfile");
        [TestMethod()]
        public void FilePersisterTest()
        {
            IPersister<PersonForTest> persister = new FilePersister<PersonForTest>(filePath);
            PersonForTest mario = new PersonForTest("Mario", "Rossi", 45);
            try { 
                persister.Load();
                Assert.Fail("Should throw an exception");
            }
            catch { }
            persister.Save(mario);
            Assert.AreEqual(persister.Load(), mario);
            persister.Reset();
        }
    }
}