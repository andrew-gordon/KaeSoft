using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Kae.GraphLibrary
{
    /// <summary>
    /// Undirected Graph.
    /// </summary>
    /// <typeparam name="TNode">The type of each node (vertex).</typeparam>
    /// <typeparam name="TEdge">The type of each edge.</typeparam>
    public class UndirectedGraph<TNode, TEdge> : IGraph<TNode,TEdge>
        where TEdge : Edge<TNode>
        where TNode : IComparable
    {
        readonly ISet<TNode> _nodes = new HashSet<TNode>();
        readonly ISet<TEdge> _edges = new SortedSet<TEdge>();

        public ISet<TNode> Nodes
        {
            get { return _nodes; }
        }

        public ISet<TEdge> Edges
        {
            get { return _edges; }
        }

        public UndirectedGraph()
        {
        }

        public UndirectedGraph(IEnumerable<TNode> nodes, IEnumerable<TEdge> edges)
        {
            Contract.Requires<ArgumentNullException>(nodes != null);
            Contract.Requires<ArgumentNullException>(edges != null);

            foreach (var node in nodes)
            {
                _nodes.Add(node);
            }

            foreach (var edge in edges)
            {
                if (Edges.Contains(edge))
                {
                    var msg = string.Format("'edges' contains duplicate edge: [{0}-{1}]", edge.EndPoint1, edge.EndPoint2);
                    throw new ArgumentException(msg);
                }
                
                Edges.Add(edge);
            }
        }

        /// <summary>
        /// Are the nodes (vertices) adjacent (connected by an edge)?
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        public bool AreAdjacent(TNode node1, TNode node2)
        {
            //Contract.Requires<ArgumentNullException>(node1 != null);
            //Contract.Requires<ArgumentNullException>(node2 != null);
            //Contract.Requires<ArgumentNullException>(Edges != null);
            if (Edges == null)
                throw new InvalidOperationException();

            return Edges.Any(e => e.EndPoint1.Equals(node1) && e.EndPoint2.Equals(node2));
        }

        /// <summary>
        /// Gets the neighbours of the specified node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IEnumerable<TNode> GetNeighbours(TNode node)
        {
            //Contract.Requires<ArgumentNullException>(node != null);
            Contract.Ensures(Contract.Result<IEnumerable<TNode>>() != null);

            IEnumerable<TNode> neighbours;

            if (_edges == null)
            {
                neighbours = new TNode[] { };
            }
            else
            {
                IEnumerable<TNode> edges1 =
                    _edges.Where(e => e.EndPoint1.Equals(node)).Select(e => e.EndPoint2);

                IEnumerable<TNode> edges2 =
                    _edges.Where(e => e.EndPoint2.Equals(node)).Select(e => e.EndPoint1);

                neighbours = edges1.Union(edges2);
            }

            return neighbours;
        }
    }
}
