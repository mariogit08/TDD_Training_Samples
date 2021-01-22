using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDD_GetStarted
{
    [TestClass]
    public class FizzBuzzExceptionCaseNegativeNumberTest
    {
        [TestMethod]
        public void TestInputZeroNumber()
        {
            Assert.IsTrue(FuzzBuzz(0) == null);
        }

        ////Refactor
        private string FuzzBuzz(int v)
        {
            return null;
        }

        ////Green
        //private string FuzzBuzz(int v)
        //{
        //    return null;
        //}

        ////Red
        //private string FuzzBuzz(int v)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
