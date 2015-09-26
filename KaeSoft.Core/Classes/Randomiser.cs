using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace KaeSoft.Core.Classes
{
    public class Randomiser 
    {
        private static readonly Lazy<Random> LazyRandom; 

        static Randomiser()
        {
            LazyRandom = new Lazy<Random>(
                () =>
                {
                    var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
                    var buffer = new byte[4];

                    rngCryptoServiceProvider.GetBytes(buffer);
                    var result = BitConverter.ToInt32(buffer, 0);
                    return new Random(result);
                });
        }

        /// <summary>
        /// Generates a random integer between minimumValue and maximumValue(inclusive)
        /// </summary>
        /// <param name="minimumValue">Minimum value</param>
        /// <param name="maximumValue">Maximum value</param>
        /// <returns>Random number greater or equal to minimumValue and less than or equal to maximumValue</returns>
        public int GenerateInteger(int minimumValue, int maximumValue)
        {
            var number = LazyRandom.Value.Next(minimumValue, maximumValue + 1);
            return number;
        }

        /// <summary>
        /// Generates a random double between minimumValue and maximumValue(inclusive)
        /// </summary>
        /// <param name="minimumValue">Minimum value</param>
        /// <param name="maximumValue">Maximum value</param>
        /// <returns>Random number greater or equal to minimumValue and less than or equal to maximumValue</returns>
        public double GenerateDouble(double minimumValue, double maximumValue)
        {
            return LazyRandom.Value.NextDouble() * (maximumValue - minimumValue) + minimumValue;
        }

        /// <summary>
        /// Gets a random value from a values of values
        /// </summary>
        /// <param name="values">List of values from which the value should be randomly selected</param>
        /// <returns>Random value from the values</returns>
        public T GetRandomValue<T>(IEnumerable<T> values)
        {
            var list = values.ToList();
            var count = list.Count;
            var randomNumber = GenerateInteger(0, count - 1);
            var randomItem = list[randomNumber];
            return randomItem;
        }


    }
}
