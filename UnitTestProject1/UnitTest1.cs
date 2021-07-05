using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using modul.ekz;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        modulekz Cr = new modulekz();
        [TestMethod]
        public void TestMethod1()
        {
            var Test = Cr.Input();
            Assert.AreEqual(Cr.MaximumElem(Test), 8);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var Test = Cr.Input();
            Assert.AreEqual(Cr.MinimalElem(Test), 2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var Test = Cr.Input();
            Assert.AreEqual(Cr.LenFunc(Test), 43);
        }
    }
}

