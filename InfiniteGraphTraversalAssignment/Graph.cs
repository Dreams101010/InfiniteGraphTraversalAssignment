using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace InfiniteGraphTraversalAssignment
{
    abstract class Graph : IEnumerable<BigInteger>
    {
        internal int Version { get; set; } = 0;
        public abstract BigInteger NodeCount { get; protected set; }
        public abstract bool HasEdgeBetween(BigInteger node1, BigInteger node2);
        public abstract List<BigInteger> GetAdjacentNodes(BigInteger node);

        public GraphEnumerator GetTraversalEnumerator(TraversalStrategyEnum strategy)
        {
            return new GraphEnumerator(this, strategy);
        }

        public IEnumerator<BigInteger> GetEnumerator()
        {
            return new GraphEnumerator(this, TraversalStrategyEnum.BFS);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GraphEnumerator(this, TraversalStrategyEnum.BFS);
        }
    }
}
