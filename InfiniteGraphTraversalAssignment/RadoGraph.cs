using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace InfiniteGraphTraversalAssignment
{
    // implements subset of Rado graph
    class RadoGraph : Graph
    {
        readonly List<BigInteger> offsets = new List<BigInteger>(new BigInteger[] { 2, 3, 5, 7 });
        public override BigInteger NodeCount 
        {
            get { throw new NotSupportedException("Operation not supported.");  } // graph is infinite so this operation doesn't make sense
            protected set { throw new NotSupportedException("Operation not supported."); }
        }

        public override List<BigInteger> GetAdjacentNodes(BigInteger node)
        {
            List<BigInteger> adjNodes = new List<BigInteger>();
            for (int i = 0; i < offsets.Count; i++)
            {
                adjNodes.Add(node + offsets[i]);
            }
            return adjNodes;
        }

        public override bool HasEdgeBetween(BigInteger node1, BigInteger node2)
        {
            if (node1 >= node2)
            {
                return false;
            }
            BigInteger offset = node2 - node1;
            if (offsets.Contains(offset))
            {
                return true;
            }
            return false;
        }
    }
}
