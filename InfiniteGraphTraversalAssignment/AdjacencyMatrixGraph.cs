using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniteGraphTraversalAssignment
{
    class AdjacencyMatrixGraph : Graph
    {
        private bool[][] adjacencyMatrix = null;
        public override int NodeCount { get; protected set; }

        public AdjacencyMatrixGraph(int nodeCount)
        {
            if (nodeCount <= 0) throw new ArgumentException("Node count must be positive.");
            adjacencyMatrix = new bool[nodeCount][];
            for (int i = 0; i < adjacencyMatrix.Length; i++)
            {
                adjacencyMatrix[i] = new bool[nodeCount];
            }
            NodeCount = nodeCount;
        }

        private bool IsInBounds(int index)
        {
            if (index < 0 || index >= NodeCount) return false;
            return true;
        }
        public void SetEdge(int node1, int node2, bool state = true)
        {
            if (!(IsInBounds(node1) && IsInBounds(node2))) throw new ArgumentException("Node index is out of bounds.");
            adjacencyMatrix[node1][node2] = state; // both directions
            adjacencyMatrix[node2][node1] = state;
            Version++;
        }

        public override bool HasEdgeBetween(int node1, int node2)
        {
            if (!(IsInBounds(node1) && IsInBounds(node2))) throw new ArgumentException("Node index is out of bounds.");
            return adjacencyMatrix[node1][node2];
        }

        public override List<int> GetAdjacentNodes(int node)
        {
            List<int> adjNodes = new List<int>();
            for (int i = 0; i < NodeCount; i++)
            {
                if (i != node)
                {
                    if (HasEdgeBetween(node, i))
                    {
                        adjNodes.Add(i);
                    }
                }
            }
            return adjNodes;
        }
    }
}
