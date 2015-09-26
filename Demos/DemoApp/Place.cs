using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace DemoApp
{
    public class Place : IComparable, IComparable<Place>, IEquatable<Place>
    {
        public string Name { get; set; }

        public int CompareTo(Place other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(Place other)
        {
            return CompareTo(other) == 0;
        }

        public int CompareTo(object obj)
        {

            Place p = obj as Place;

            if (p!=null)
            {
                return CompareTo(p);
            }
            else
                throw new ArgumentException("obj");
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            Contract.Ensures(Contract.Result<String>() != null);

            if (Name == null)
                return base.ToString();
            else
                return Name;
        }
    }
}
