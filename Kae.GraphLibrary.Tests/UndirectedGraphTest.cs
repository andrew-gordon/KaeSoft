using Kae.GraphLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kae.GraphLibrary.Tests
{
    
    
    /// <summary>
    ///This is a test class for UndirectedGraphTest and is intended
    ///to contain all UndirectedGraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UndirectedGraphTest
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
        ///A test for UndirectedGraph`2 Constructor
        ///</summary>
        public void UndirectedGraphConstructorTestHelper<TNode, TEdge>(IEnumerable<TNode> nodes, IEnumerable<TEdge> edges)
            where TNode : IComparable
            where TEdge : Edge<TNode>
        {
            UndirectedGraph<TNode, TEdge> target = new UndirectedGraph<TNode, TEdge>(nodes, edges);
            Assert.AreEqual<int>(nodes.Distinct().Count(), target.Nodes.Count);
            Assert.AreEqual<int>(edges.Distinct().Count(), target.Edges.Count);
        }


        [TestMethod()]
        public void UndirectedGraphConstructorTest()
        {
            UndirectedGraphConstructorTestHelper<string, Edge<string>>(null, null);
            UndirectedGraphConstructorTestHelper<int, Edge<int>>(null, null);
        }

        [TestMethod()]
        public void UndirectedGraphConstructorTest2()
        {
            UndirectedGraphConstructorTestHelper<string, Edge<string>>(null, null);
            UndirectedGraphConstructorTestHelper<int, Edge<int>>(null, null);
        }

        /// <summary>
        ///A test for UndirectedGraph`2 Constructor
        ///</summary>
        public void UndirectedGraphDefaultConstructorTestHelper<TNode, TEdge>()
            where TNode : IComparable
            where TEdge : Edge<TNode>
        {
            UndirectedGraph<TNode, TEdge> target = new UndirectedGraph<TNode, TEdge>();
            Assert.AreEqual<int>(target.Nodes.Count, 0);
            Assert.AreEqual<int>(target.Edges.Count, 0);
        }

        [TestMethod()]
        public void UndirectedGraphDefaultConstructorTest()
        {
            UndirectedGraphDefaultConstructorTestHelper<string, Edge<string>>();
            UndirectedGraphDefaultConstructorTestHelper<int, Edge<int>>();
        }
    }
}
