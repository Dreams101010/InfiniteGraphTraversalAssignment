using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniteGraphTraversalAssignment
{
    class TraversalStrategyDFS : TraversalStrategy
    {
        List<int> visited = new List<int>();
        List<int> allChildrenVisited = new List<int>();
        Stack<int> stack = new Stack<int>();

        public override event AllChildrenVisitedHandler OnAllChildrenVisited;
        public override event HasNoChildrenHandler OnHasNoChildren;

        public override bool IsFinished { get; protected set; }

        public TraversalStrategyDFS(Graph g)
        {
            graph = g ?? throw new NullReferenceException("The enumerable graph cannot be null.");
            stack.Push(0);
        }

        public bool HasChildren(int node)
        {
            List<int> adjacentNodes = graph.GetAdjacentNodes(node);
            foreach (var i in adjacentNodes)
            {
                if (!visited.Contains(i))
                {
                    return true;
                }
            }
            return false;
        }

        public override int Execute()
        {
            while (true)
            {
                if (stack.Count == 0) // if stack is empty traversal is over
                {
                    IsFinished = true;
                    return -1;
                }
                int node = stack.Pop();
                if (!visited.Contains(node)) // we haven't been here before
                {
                    visited.Add(node); // mark as visited
                    if (HasChildren(node))
                    {
                        stack.Push(node);
                        List<int> adjacentNodes = graph.GetAdjacentNodes(node);
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
