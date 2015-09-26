using DemoApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DemoApp.Tests
{
    
    
    /// <summary>
    ///This is a test class for PlaceTest and is intended
    ///to contain all PlaceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PlaceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            Place p1, p2, p3;

            p1 = new Place { Name = "A" };
            p2 = new Place { Name = "B" };
            p3 = new Place { Name = "A" };

            Assert.IsTrue(p1.Equals(p1));
            Assert.IsFalse(p1.Equals(p2));
            Assert.IsFalse(p2.Equals(p3));
            Assert.IsTrue(p1.Equals(p3));
        }
    }
}
