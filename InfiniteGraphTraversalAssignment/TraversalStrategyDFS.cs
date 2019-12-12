using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace InfiniteGraphTraversalAssignment
{
    class TraversalStrategyDFS : TraversalStrategy
    {
        List<BigInteger> visited = new List<BigInteger>();
        List<BigInteger> allChildrenVisited = new List<BigInteger>();
        Stack<BigInteger> stack = new Stack<BigInteger>();

        public override event AllChildrenVisitedHandler OnAllChildrenVisited;
        public override event HasNoChildrenHandler OnHasNoChildren;

        public override bool IsFinished { get; protected set; }

        public TraversalStrategyDFS(Graph g)
        {
            graph = g ?? throw new NullReferenceException("The enumerable graph cannot be null.");
            stack.Push(0);
        }

        public bool HasChildren(BigInteger node)
        {
            List<BigInteger> adjacentNodes = graph.GetAdjacentNodes(node);
            foreach (var i in adjacentNodes)
            {
                if (!visited.Contains(i))
                {
                    return true;
                }
            }
            return false;
        }

        public override BigInteger Execute()
        {
            while (true)
            {
                if (stack.Count == 0) // if stack is empty traversal is over
                {
                    IsFinished = true;
                    return -1;
                }
                BigInteger node = stack.Pop();
                if (!visited.Contains(node)) // we haven't been here before
                {
                    visited.Add(node); // mark as visited
                    if (HasChildren(node))
                    {
                        stack.Push(node);
                        List<BigInteger> adjacentNodes = graph.GetAdjacentNodes(node);
                        foreach (var i in adjacentNodes) // push all children on stack
                        {
                            if (!visited.Contains(i))
                            {
                                stack.Push(i);
                            }
                        }
                    }
                    else
                    {
                        OnHasNoChildren?.Invoke(node);
                    }
                    return node;
                }
                else
                {
                    if (!allChildrenVisited.Contains(node))
                    {
                        OnAllChildrenVisited?.Invoke(node);
                        allChildrenVisited.Add(node);
                    }
                }
            }
        }
    }
}
