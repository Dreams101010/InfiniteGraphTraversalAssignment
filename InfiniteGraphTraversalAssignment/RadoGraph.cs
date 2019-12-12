using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniteGraphTraversalAssignment
{
    // implements subset of Rado graph
    class RadoGraph : Graph
    {
        const int count = int.MaxValue;
        readonly List<int> offsets = new List<int>(new int[] { 2, 3, 5, 7 });
        public override int NodeCount 
        {
            get { return count; }
            protected set { throw new NotSupportedException("Operation not supported."); }
        }

        public override List<int> GetAdjacentNodes(int node)
        {
            List<int> adjNodes = new List<int>();
            for (int i = 0; i < offsets.Count; i++)
            {
                long testVal = (long)node + i;
                if (testVal < count)
                {
                    adjNodes.Add(node + i);
                }
            }
            return adjNodes;
        }

        public override bool HasEdgeBetween(int node1, int node2)
        {
            if (node1 >= node2)
            {
                return false;
            }
            int offset = node2 - node1;
            if (offsets.Contains(offset))
            {
                return true;
            }
            return false;
        }
    }
}
