using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace InfiniteGraphTraversalAssignment
{
    abstract class Graph : IEnumerable<int>
    {
        internal int Version { get; set; } = 0;
        public abstract int NodeCount { get; protected set; }
        public abstract bool HasEdgeBetween(int node1, int node2);
        public abstract List<int> GetAdjacentNodes(int node);

        public GraphEnumerator GetTraversalEnumerator(TraversalStrategyEnum strategy)
        {
            return new GraphEnumerator(this, strategy);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new GraphEnumerator(this, TraversalStrategyEnum.BFS);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GraphEnumerator(this, TraversalStrategyEnum.BFS);
        }
    }
}
