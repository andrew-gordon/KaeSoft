using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kae.GraphLibrary
{
    public interface INode<TKey> : /* IEquatable<TKey>, */ IComparable<TKey>
    {
        TKey Key { get; set; }
    }
}
