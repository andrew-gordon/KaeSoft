using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kae.GraphLibrary
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>    
    public abstract class Edge<TNode> : IComparable, IEquatable<Edge<TNode>>
        where TNode : IComparable
    {
        /// <summary>
        /// The first endpoint belonging to the edge.
        /// </summary>
        public TNode EndPoint1 { get; set; }

        /// <summary>
        /// The second endpoint belonging to the edge.
        /// </summary>
        public TNode EndPoint2 { get; set; }

        /// <summary>
        /// Compares the current instance with another Edge<TNode> and returns
        /// an integer that indicates whether the current instance precedes, follows,
        /// or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An Edge<TNode> to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the edges being compared.
        /// The return value has these meanings: Value Meaning Less than zero This instance
        /// is less than obj. Zero This instance is equal to obj. Greater than zero This
        /// instance is greater than obj.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        ///     obj is not the same type as this instance.
        /// </exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            Edge<TNode> otherEdge = obj as Edge<TNode>;

            if (otherEdge!=null)
            {
                int compareValue = this.EndPoint1.CompareTo(otherEdge.EndPoint1);

                if (compareValue == 0)
                {
                    compareValue = this.EndPoint2.CompareTo(otherEdge.EndPoint2);
                }

                return compareValue;

            }
            else
                throw new ArgumentException("obj is not an " + obj.GetType().Name);
        }

        /// <summary>
        /// Determines whether the specified Edge<TNode> is equal to the current Edge<TNode>.
        /// </summary>
        /// <param name="other">Other Edge<TNode></param>
        /// <returns></returns>
        public bool Equals(Edge<TNode> other)
        {
            return (other.EndPoint1.Equals(EndPoint1) && other.EndPoint2.Equals(EndPoint2));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }



}
