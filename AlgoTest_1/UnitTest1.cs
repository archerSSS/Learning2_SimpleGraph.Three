using System;
using System.Collections.Generic;
using AlgorithmsDataStructures2;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AlgoTest_1
{
    [TestClass]
    public class UnitTest1
    {


        /*
         *      0   1   0   0   1
         *      
         *      1   0   1   0   0
         *      
         *      0   1   0   1   0
         *      
         *      0   0   1   0   1
         *      
         *      1   0   0   1   0
         */
        [TestMethod]
        public void TestFirstSilly_1()
        {
            SimpleGraph<int> graph = new SimpleGraph<int>(5);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);

            List<Vertex<int>> list = graph.BreadthFirstSearch(0, 4);
        }


        [TestMethod]
        public void TestBreadthA_1()
        {
            SimpleGraph<int> graph = GetGraphA();
            List<Vertex<int>> list = graph.BreadthFirstSearch(2, 7);
            int[] array = new int[] { 2, 0, 1, 7 };
            int index = 0;
            
            foreach (Vertex<int> vert in list) Assert.AreEqual(array[index++], vert.Value);
        }

        [TestMethod]
        public void TestBreadthA_2()
        {
            SimpleGraph<int> graph = GetGraphA();
            List<Vertex<int>> list = graph.BreadthFirstSearch(9, 5);
            int[] array = new int[] { 9, 5 };
            int index = 0;

            foreach (Vertex<int> vert in list) Assert.AreEqual(array[index++], vert.Value);
        }

        [TestMethod]
        public void TestBreadthA_3()
        {
            SimpleGraph<int> graph = GetGraphA();
            List<Vertex<int>> list = graph.BreadthFirstSearch(6, 2);
            int[] array = new int[] { 6, 4, 0, 2 };
            int index = 0;

            foreach (Vertex<int> vert in list) Assert.AreEqual(array[index++], vert.Value);
        }

        [TestMethod]
        public void TestBreadthA_Empty()
        {
            SimpleGraph<int> graph = GetGraphA();
            List<Vertex<int>> list = graph.BreadthFirstSearch(7, 8);

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void TestBreadthB_1()
        {
            SimpleGraph<int> graph = GetGraphB();
            List<Vertex<int>> list = graph.BreadthFirstSearch(4, 9);
            int[] array = new int[] { 4, 5, 9 };
            int index = 0;

            foreach (Vertex<int> vert in list) Assert.AreEqual(array[index++], vert.Value);
        }

        [TestMethod]
        public void TestBreadthB_2()
        {
            SimpleGraph<int> graph = GetGraphB();
            List<Vertex<int>> list = graph.BreadthFirstSearch(0, 7);
            int[] array = new int[] { 0, 1, 7 };
            int index = 0;

            foreach (Vertex<int> vert in list) Assert.AreEqual(array[index++], vert.Value);
        }

        [TestMethod]
        public void TestBreadthB_3()
        {
            SimpleGraph<int> graph = GetGraphB();
            List<Vertex<int>> list = graph.BreadthFirstSearch(5, 6);
            int[] array = new int[] { 5, 9, 6 };
            int index = 0;

            foreach (Vertex<int> vert in list) Assert.AreEqual(array[index++], vert.Value);
        }

        [TestMethod]
        public void TestBreadthB_4()
        {
            SimpleGraph<int> graph = GetGraphB();
            List<Vertex<int>> list = graph.BreadthFirstSearch(2, 5);
            int[] array = new int[] { 2, 0, 4, 5 };
            int index = 0;

            foreach (Vertex<int> vert in list) Assert.AreEqual(array[index++], vert.Value);
        }

        [TestMethod]
        public void TestBreadthB_Empty()
        {
            SimpleGraph<int> graph = GetGraphB();
            List<Vertex<int>> list = graph.BreadthFirstSearch(3, 8);

            Assert.AreEqual(0, list.Count);
        }



        /*
         *          0   1   2   3   4   5   6   7   8   9
         *      
         * 
         * 0        0   1   1   0   1   0   0   0   0   0
         *      
         * 1        1   0   0   0   0   0   0   1   0   1
         *      
         * 2        1   0   0   1   0   0   0   0   0   0
         *      
         * 3        0   0   1   0   1   0   0   0   0   0
         *        
         * 4        1   0   0   1   0   1   1   0   0   0
         *      
         * 5        0   0   0   0   1   0   0   0   0   1
         *      
         * 6        0   0   0   0   1   0   0   1   0   0
         *      
         * 7        0   1   0   0   0   0   1   0   0   0
         *      
         * 8        0   0   0   0   0   0   0   0   0   0
         *      
         * 9        0   1   0   0   0   1   0   0   0   0
         */
        private SimpleGraph<int> GetGraphA()
        {
            SimpleGraph<int> graph = new SimpleGraph<int>(10);
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);
            graph.AddVertex(7);
            graph.AddVertex(8);
            graph.AddVertex(9);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 7);
            graph.AddEdge(1, 9);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(5, 9);
            graph.AddEdge(6, 7);

            return graph;
        }


        /*
         *          0   1   2   3   4   5   6   7   8   9
         *      
         * 
         * 0        0   1   1   0   1   0   0   0   0   0
         *      
         * 1        1   0   0   0   0   0   0   1   0   1
         *      
         * 2        1   0   0   1   0   0   0   0   0   0
         *      
         * 3        0   0   1   0   1   0   0   0   0   0
         *        
         * 4        1   0   0   1   0   1   0   0   0   0
         *      
         * 5        0   0   0   0   1   0   0   0   0   1
         *      
         * 6        0   0   0   0   0   0   0   1   0   1
         *      
         * 7        0   1   0   0   0   0   1   0   0   0
         *      
         * 8        0   0   0   0   0   0   0   0   0   0
         *      
         * 9        0   1   0   0   0   1   1   0   0   0
         */
        private SimpleGraph<int> GetGraphB()
        {
            SimpleGraph<int> graph = new SimpleGraph<int>(10);
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);
            graph.AddVertex(7);
            graph.AddVertex(8);
            graph.AddVertex(9);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 7);
            graph.AddEdge(1, 9);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 9);
            graph.AddEdge(6, 7);
            graph.AddEdge(6, 9);

            return graph;
        }
    }
}
