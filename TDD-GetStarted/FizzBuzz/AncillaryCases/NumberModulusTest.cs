using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDD_GetStarted
{
    [TestClass]
    public class NumberModulusTest
    {
        [TestMethod]
        public void TestInputNumber()
        {
            Assert.IsTrue(ReturnModulus(-1) == 1);
        }

        //Refactor
        public int ReturnModulus(int number)
        {
            return Math.Abs(number);
        }

        ////Green
        //public int ReturnModulus(int number)
        //{
        //    return 1;
        //}

        //Red
        //public int ReturnModulus(int number)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
