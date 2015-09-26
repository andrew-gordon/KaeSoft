using System;
using System.Collections.Generic;

namespace Kae.GraphLibrary
{
    public interface IGraph<TNode, TEdge>
        where TNode : IComparable
        where TEdge : Edge<TNode>
    {
        bool AreAdjacent(TNode node1, TNode node2);
        ISet<TEdge> Edges { get; }
        ISet<TNode> Nodes { get; }

        IEnumerable<TNode> GetNeighbours(TNode node);
    }
}
