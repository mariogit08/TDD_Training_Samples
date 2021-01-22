using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDD_GetStarted
{
    [TestClass]
    public class NegativeNumberTest
    {
        [TestMethod]
        public void TestInputNegativeNumber()
        {
            Assert.IsTrue(FuzzBuzz(-1) == null);
        }        

        //Refactor
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
