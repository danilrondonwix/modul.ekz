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
            Assert.AreEqual(8, Cr.MaximumElem(Test));
        }
        [TestMethod]
        public void TestMethod2()
        {
            var Test = Cr.Input();
            Assert.AreEqual(2, Cr.MinimalElem(Test));
        }
        [TestMethod]
        public void TestMethod3()
        {
            var Test = Cr.Input();
            Assert.AreEqual(43, Cr.LenFunc(Test));
        }
    }
}

