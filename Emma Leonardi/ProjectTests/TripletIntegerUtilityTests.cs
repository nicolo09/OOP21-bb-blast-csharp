using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    [TestClass()]
    public class TripletIntegerUtilityTests
    {
        [TestMethod()]
        public void addTest()
        {
            Tuple<int, int, int> t1 = new Tuple<int, int, int>(1,2,3);
            Tuple<int, int, int> t2 = new Tuple<int, int, int>(-1, -2, -3);
            Tuple<int, int, int> t3 = new Tuple<int, int, int>(3, 2, 0);
            Tuple<int, int, int> t4 = new Tuple<int, int, int>(-3, 0, -2);

            Assert.AreEqual(TripletIntegerUtility.add(t1,t2),new Tuple<int, int, int> (0,0,0), "The result is the sum of the triplet components");
            Assert.AreEqual(TripletIntegerUtility.add(t1, t3), new Tuple<int, int, int>(4, 4, 3), "The result is the sum of the components");
            Assert.AreEqual(TripletIntegerUtility.add(t2, t4), new Tuple<int, int, int>(-4, -2, -5), "The sum of the components");
            Assert.AreEqual(TripletIntegerUtility.add(t2, t3), new Tuple<int, int, int>(2, 0, -3), "The sum of the triplet components");

        }
    }
}