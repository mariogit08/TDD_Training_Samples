using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDD_GetStarted
{
    [TestClass]
    public class FizzBuzzReturnerTest
    {
        [TestMethod]
        public void FizzBuzzTest()
        {
            Assert.IsTrue(FizzBuzz(3) == "Fizz");
            Assert.IsTrue(FizzBuzz(5) == "Buzz");
            Assert.IsTrue(FizzBuzz(6) == "Fizz");
            Assert.IsTrue(FizzBuzz(10) == "Buzz");
            Assert.IsTrue(FizzBuzz(15) == "FizzBuzz");
            Assert.IsTrue(FizzBuzz(7) == "");
        }

        ////Refactor 4 - Final
        public string FizzBuzz(int number)
        {
            if (number % 5 == 0 && number % 3 == 0)
                return "FizzBuzz";
            if (number % 3 == 0)
                return "Fizz";
            if (number % 5 == 0)
                return "Buzz";

            return "";
        }

        //////Refactor 3
        //public string FizzBuzz(int number)
        //{
        //    if (number % 3 == 0)
        //        return "Fizz";
        //    if (number % 5 == 0)
        //        return "Buzz";
        //    if (number % 5 == 0 && number % 3 == 0)
        //        return "FizzBuzz";

        //    return "";
        //}

        //////Refactor 2 
        //public string FizzBuzz(int number)
        //{
        //    if (number % 3 == 0)
        //        return "Fizz";
        //    if (number % 5 == 0)
        //        return "Buzz";
        //}

        //////Refactor 1
        //public string FizzBuzz(int number)
        //{
        //    if (number % 3 == 0)
        //        return "Fizz";

        //    return "Buzz";
        //}


        ////Green/Fake
        //public string FizzBuzz(int number)
        //{
        //    if (number == 3)
        //        return "Fizz";

        //    return "Buzz";
        //}

        ////Red
        //public string FizzBuzz(int number)
        //{
        //    return null;
        //}
    }
}
