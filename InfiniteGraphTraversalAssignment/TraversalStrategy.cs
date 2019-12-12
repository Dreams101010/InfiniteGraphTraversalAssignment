using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

// TODO: event clean-up

namespace InfiniteGraphTraversalAssignment
{
    abstract class TraversalStrategy
    {
        protected Graph graph = null;

        public delegate void AllChildrenVisitedHandler(BigInteger nodeIndex);
        public virtual event AllChildrenVisitedHandler OnAllChildrenVisited;

        public delegate void HasNoChildrenHandler(BigInteger nodeIndex);
        public virtual event HasNoChildrenHandler OnHasNoChildren;

        public abstract bool IsFinished { get; protected set; }
        public abstract BigInteger Execute();
    }
}
