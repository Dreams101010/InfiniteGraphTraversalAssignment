﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace InfiniteGraphTraversalAssignment
{
    enum TraversalStrategyEnum
    {
        BFS,
        DFS
    }
    class GraphEnumerator : IEnumerator<BigInteger>
    {
        Graph graph = null;
        TraversalStrategy strategy = null;
        int version = 0;

        public delegate void AllChildrenVisitedHandler(BigInteger nodeIndex);
        public event AllChildrenVisitedHandler OnAllChildrenVisited;

        public delegate void HasNoChildrenHandler(BigInteger nodeIndex);
        public event HasNoChildrenHandler OnHasNoChildren;

        public GraphEnumerator(Graph g, TraversalStrategyEnum ts = TraversalStrategyEnum.BFS)
        {
            graph = g ?? throw new NullReferenceException("The enumerable graph cannot be null.");
            version = g.Version;
            switch (ts)
            {
                case TraversalStrategyEnum.BFS:
                    {
                        strategy = new TraversalStrategyBFS(graph);
                        break;
                    }
                case TraversalStrategyEnum.DFS:
                    {
                        strategy = new TraversalStrategyDFS(graph);
                        break;
                    }
            }
            // if strategy's event fires, fire ours
            strategy.OnAllChildrenVisited += delegate(BigInteger nodeIndex)
            {
                OnAllChildrenVisited?.Invoke(nodeIndex);
            };
            strategy.OnHasNoChildren += delegate(BigInteger nodeIndex)
            {
                OnHasNoChildren?.Invoke(nodeIndex);
            };
        }
        public BigInteger Current { get; private set; } = 0;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            graph = null;
            strategy = null;
        }

        public bool MoveNext()
        {
            if (graph.Version != version)
            {
                throw new Exception("Graph changed while enumerating.");
            }
            if (!strategy.IsFinished)
            {
                Current = strategy.Execute();
                if (Current == -1) return false;
                return true;
            }
            return false;
        }

        // reset isn't supported
        public void Reset()
        {
            throw new NotSupportedException("Reset functionality not supported.");
        }
    }
}
