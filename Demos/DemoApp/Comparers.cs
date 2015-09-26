using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp
{
    public class WeightComparer : 
        IEqualityComparer<int>, 
        IEqualityComparer<TimeSpan>, 
        IEqualityComparer<float>,
        IEqualityComparer<decimal>
    {

        public bool Equals(int x, int y)
        {
            return x == y;
        }

        public int GetHashCode(int obj)
        {
            return obj;
        }

        public bool Equals(TimeSpan x, TimeSpan y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(TimeSpan obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(float x, float y)
        {
            return Math.Abs(x - y) < float.Epsilon;
        }

        public int GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(decimal x, decimal y)
        {
            return Math.Abs(x - y) < 0.01M;
        }

        public int GetHashCode(decimal obj)
        {
            return obj.GetHashCode();
        }
    }
}
