using System;
using System.Collections.Generic;

namespace KaeSoft.Core.Classes
{
    public class SynchronizedDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public SynchronizedDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

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
