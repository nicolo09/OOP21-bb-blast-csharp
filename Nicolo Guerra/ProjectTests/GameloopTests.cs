using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Tests
{
    [TestClass()]
    public class GameloopTests
    {

        private class UpdatableForTest : IUpdatable
        {
            public bool Updated { get; set; }
            public void Update()
            {
                this.Updated = true;
            }
        }

        [TestMethod()]
        public void StartLoopTest()
        {
            IGameloop loop = new Gameloop();
            UpdatableForTest u = new UpdatableForTest();
            loop.RegisterUpdatable(u);
            Assert.IsTrue(loop.Stopped, "Loop is not stopped at the creation");
            Assert.IsFalse(loop.Paused, "Loop is paused at the creation");
            Assert.IsFalse(loop.Running, "Loop is running at the beginning");
            loop.RegisterUpdatable(u);
            Assert.IsFalse(u.Updated, "Loop updates Updatable before running");
            loop.Start();
            Thread.Sleep(1000);
            Assert.IsTrue(loop.Running, "Loop says he's not running after start");
            Assert.IsTrue(u.Updated, "Loop hasn't updated Updatable after 1 second");
            loop.Paused = true;
            Assert.IsTrue(loop.Paused, "Loop says he's not paused");
            Assert.IsFalse(loop.Running, "Loop says he's running on pause");
            u.Updated = false;
            Assert.IsFalse(u.Updated, "Loop updates Updatable when paused");
            loop.Paused = false;
            Thread.Sleep(1000);
            Assert.IsTrue(u.Updated, "Loop doesn't update Updatable when resumed");
            Assert.IsTrue(loop.Running, "Loop says he's not running after resume");
        }

    }
}