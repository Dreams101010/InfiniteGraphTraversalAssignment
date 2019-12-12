using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace InfiniteGraphTraversalAssignment
{
    class TraversalStrategyBFS : TraversalStrategy
    {
        // need to use a list instead of bool array
        // otherwise we wouldn't be able to allocate it for traversal
        List<BigInteger> visited = new List<BigInteger>();
        Queue<BigInteger> queue = new Queue<BigInteger>();

        public override event AllChildrenVisitedHandler OnAllChildrenVisited;
        public override event HasNoChildrenHandler OnHasNoChildren; 
        public override bool IsFinished { get; protected set; }

        public TraversalStrategyBFS(Graph g)
        {
            graph = g ?? throw new NullReferenceException("The enumerable graph cannot be null.");
            queue.Enqueue(0); // push starting node
            visited.Add(0);
        }

        public override BigInteger Execute()
        {
            if (queue.Count == 0)
            {
                IsFinished = true; // search ended, node has not been found
                return -1;
            }
            BigInteger node = queue.Dequeue();
            List<BigInteger> adjacentNodes = graph.GetAdjacentNodes(node);
            bool allVisited = true;
            foreach (var i in adjacentNodes)
            {
                if (!visited.Contains(i))
                {
                    queue.Enqueue(i);
                    visited.Add(i);
                    allVisited = false;
                }
            }
            if (allVisited)
            {
                OnAllChildrenVisited?.Invoke(node);
                OnHasNoChildren?.Invoke(node); // same thing in BFS
            }
            return node; // return current node
        }
    }
}
