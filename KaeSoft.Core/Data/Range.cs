using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Andy.Lib.Data
{
    [DataContract]
    public class Range<T> : IEquatable<Range<T>> where T : IComparable<T>
    {
        public Range(T minimum, T maximum)
        {
            if (minimum.CompareTo(maximum) == 1)
                throw new ArgumentException("Range Minimum is greater than Maximum", "minimum");

            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Minimum value allowed in the range
        /// </summary>
        public T Minimum { get; private set; }

        /// <summary>
        /// Maximum value allowed in the range
        /// </summary>
        public T Maximum { get; private set; }

        /// <summary>
        /// Determines if the provided value is inside the range
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <returns>True if the value is inside Range, else false</returns>
        public bool ContainsValue(T value)
        {
            return (Minimum.CompareTo(value) <= 0) && (value.CompareTo(Maximum) <= 0);
        }

        /// <summary>
        /// Determines if this Range is inside the bounds of another range
        /// </summary>
        /// <param name="range">The parent range to test on</param>
        /// <returns>True if range is inclusive, else false</returns>
        public bool IsInsideRange(Range<T> range)
        {
            return range.ContainsValue(Minimum) && range.ContainsValue(Maximum);
        }

        /// <summary>
        /// Determines if another range is inside the bounds of this range
        /// </summary>
        /// <param name="range">The child range to test</param>
        /// <returns>True if range is inside, else false</returns>
        public bool ContainsRange(Range<T> range)
        {
            return ContainsValue(range.Minimum) && ContainsValue(range.Maximum);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            return Equals((Range<T>) other);
        }

        public bool Equals(Range<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Minimum, other.Minimum) && EqualityComparer<T>.Default.Equals(Maximum, other.Maximum);
        }

        /// <summary>
        /// Gets a hash function for the specified object for hashing algorithms and data structures, such as a hash table.
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Minimum) * 397) ^ EqualityComparer<T>.Default.GetHashCode(Maximum);
            }
        }

        /// <summary>
        /// Presents the Range in readable format
        /// </summary>
        /// <returns>String representation of the Range</returns>
        public override string ToString() { return string.Format("[{0} - {1}]", Minimum, Maximum); }
    }
}