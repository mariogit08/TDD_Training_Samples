using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDD_GetStarted
{
    [TestClass]
    public class LargeIntegerTest
    {
        [TestMethod]
        public void TestInputLargeNumber()
        {
            //var asd = 3*  2147483649;
            //Assert.IsTrue(FizzBuzz(2147483649) == "Fizz");
        }

        //////Refactor
        //public string FizzBuzz(int number)
        //{
        //    //if()
        //}


        ////Green
        //public string FizzBuzz(int number)
        //{
        //    return "Fizz";
        //}

        ////Red
        //public int FizzBuzz(int number)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
