using System;
using System.Collections.ObjectModel;

namespace KaeSoft.Core.Classes
{
    public class KeyedCollectionEx<TKey, TItem> : KeyedCollection<TKey, TItem> 

        // When an item is added to the KeyedCollection<TKey, TItem>, the item's key is extracted once and saved in the 
        // lookup dictionary for faster searches. 
        // When the internal lookup dictionary is used, it contains references to all the items in the collection if 
        // TItem is a reference type, or copies of all the items in the collection if TItem is a value type. Thus, 
        // using the lookup dictionary *may not* be appropriate if TItem is a value type.  
        // Source: https://msdn.microsoft.com/en-us/library/ms132438%28v=vs.110%29.aspx        
    {
        private readonly Func<TItem, TKey> _getKeyForItemDelegate;

        public KeyedCollectionEx(Func<TItem, TKey> getKeyForItemDelegate)
        {
            if (getKeyForItemDelegate == null)
                throw new ArgumentNullException("getKeyForItemDelegate");

            _getKeyForItemDelegate = getKeyForItemDelegate;
        }

        protected override TKey GetKeyForItem(TItem item)
        {
            return _getKeyForItemDelegate(item);
        }
    }
}
