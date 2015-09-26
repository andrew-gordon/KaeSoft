using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Kae.GraphLibrary
{
    public class Dijkstra<TNode, TEdge, TWeight>
        where TNode : IComparable
        where TEdge : Edge<TNode>
        where TWeight : struct, IComparable  // struct means TWeight must be a a value type.
    {
        // http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm


        private IGraph<TNode, TEdge> Graph { get; set; }

        readonly Func<TEdge, TWeight> _weightFunc;
        readonly IEqualityComparer<TWeight> _weightComparer;


        public Dijkstra(IGraph<TNode, TEdge> graph, Func<TEdge, TWeight> weightFunc, IEqualityComparer<TWeight> weightComparer)
        {
            Contract.Requires<ArgumentNullException>(graph != null);
            Contract.Requires<ArgumentNullException>(graph.Edges != null);
            Contract.Requires<ArgumentNullException>(weightFunc != null);
            Contract.Requires<ArgumentNullException>(weightComparer != null);
            Contract.Ensures(graph != null);

            Graph = graph;
            _weightComparer = weightComparer;
            _weightFunc = weightFunc;
        }

        public IEnumerable<TNode> CalculateRoute(TNode source, TNode target)
        {
            Contract.Requires<ArgumentNullException>(Graph != null);
            Contract.Requires<ArgumentNullException>(Graph.Edges != null);

            TWeight infinity = GenericHelper<TWeight>.MaxValue;
            TWeight zero = GenericHelper<TWeight>.Zero;

            var distance = new Dictionary<TNode, TWeight>();
            var previous = new Dictionary<TNode, TNode>();

            foreach (var v in Graph.Nodes)
            {
                distance[v] = infinity;
                previous[v] = default(TNode);
            }

            distance[source] = zero;   // Distance from source to source is 0

            ISet<TNode> nodesToProcess = new HashSet<TNode>(Graph.Nodes);  // Q := the set of all nodes in Graph ;
            var routeFound = false;

            while (nodesToProcess.Count > 0)
            {
                var u = (from entry in distance
                           join node in nodesToProcess
                               on entry.Key equals node
                           orderby entry.Value
                           select entry.Key).First();

                if (_weightComparer.Equals(distance[u], infinity))
                    break;

                if (u.Equals(target))
                {
                    routeFound = true;
                    break;
                }

                nodesToProcess.Remove(u);

                foreach (var v in Graph.GetNeighbours(u))
                {
                    if (v != null)
                    {
                        var alt = GenericHelper<TWeight>.Add(
                            distance[u],
                            DistanceBetween(u, v));

                        if (GenericHelper<TWeight>.LessThan(alt, distance[v]))
                        {
                            distance[v] = alt;
                            previous[v] = u;
                        }
                    }
                }
            }

            IList<TNode> route = new List<TNode>();

            if (routeFound)
            {
                var u = target;

                while (previous[u] != null)
                {
                    route.Insert(0, u);
                    u = previous[u];
                }

                route.Insert(0, source);
            }

            return route;
        }

        

        public TWeight DistanceBetween(TNode u, TNode v)
        {
            Contract.Requires<ArgumentNullException>(Graph != null);
            Contract.Requires<ArgumentNullException>(Graph.Edges != null);

            TWeight distance = GenericHelper<TWeight>.Zero;

            if (!Graph.Nodes.Contains(u))
                throw new Exception(string.Format("Node {0} is not on the graph", u));

            if (!Graph.Nodes.Contains(v))
                throw new Exception(string.Format("Node {0} is not on the graph", v));

            if (Graph.Nodes != null)
            {

                var edge = Graph.Edges.SingleOrDefault(e => e.EndPoint1.Equals(u) && e.EndPoint2.Equals(v)) ??
                           Graph.Edges.SingleOrDefault(e => e.EndPoint2.Equals(u) && e.EndPoint1.Equals(v));

                if (edge == null)
                    throw new Exception("This should not happen");

                distance = _weightFunc(edge);
            }

            return distance;
        }

        
    }
}
