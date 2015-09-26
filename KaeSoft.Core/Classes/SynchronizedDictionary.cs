using System;
using System.Collections.Generic;

namespace KaeSoft.Core.Classes
{
    /// <summary>
    /// A synchronized dictionary.
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    public class SynchronizedDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public SynchronizedDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Gets or addsa value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="valueFactory">Value factory</param>
        /// <returns></returns>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            TValue value;

            // ReSharper disable once InconsistentlySynchronizedField
            if (!_dictionary.TryGetValue(key, out value))
            {
                lock (_dictionary)
                {
                    if (_dictionary.TryGetValue(key, out value)) 
                        return value; // this could happen if another thread has stored the value between us testing for it on line 20 and us grabbing the lock

                    value = valueFactory(key);
                    _dictionary.Add(key, value);
                }
            }

            return value;
        }
    }
}
