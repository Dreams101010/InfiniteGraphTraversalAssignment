using System;
using System.Collections.Generic;
using System.Text;

// TODO: event clean-up

namespace InfiniteGraphTraversalAssignment
{
    abstract class TraversalStrategy
    {
        protected Graph graph = null;

        public delegate void AllChildrenVisitedHandler(int nodeIndex);
        public virtual event AllChildrenVisitedHandler OnAllChildrenVisited;

        public delegate void HasNoChildrenHandler(int nodeIndex);
        public virtual event HasNoChildrenHandler OnHasNoChildren;

        public abstract bool IsFinished { get; protected set; }
        public abstract int Execute();
    }
}
