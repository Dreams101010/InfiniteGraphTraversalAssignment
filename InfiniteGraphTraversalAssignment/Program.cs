using System;

namespace InfiniteGraphTraversalAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            //RadoGraph g = new RadoGraph();
            AdjacencyMatrixGraph g = new AdjacencyMatrixGraph(6);
            g.SetEdge(0, 1);
            g.SetEdge(0, 4);
            g.SetEdge(1, 2);
            g.SetEdge(1, 3);
            g.SetEdge(3, 4);
            g.SetEdge(3, 5);
            var trav = g.GetTraversalEnumerator(TraversalStrategyEnum.DFS);
            trav.OnAllChildrenVisited += delegate(int node)
            {
                Console.WriteLine($"Node {node}'s children had been visited.");
            };
            trav.OnHasNoChildren += delegate (int node)
            {
                Console.WriteLine($"Node {node} has no children.");
            };
            while (trav.MoveNext())
            {
                Console.WriteLine(trav.Current);
                Console.ReadKey();
            }
        }
    }
}
