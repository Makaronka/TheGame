
namespace TheGame
{
    class Inventory : Container
    {
        public Inventory(int collSize, int rawSize, int cellSize, int x = 320, int y = 320)
        {
            _collSize = collSize;
            _cellSize = cellSize;
            _rawSize = rawSize;
            _items = new Item[collSize * rawSize];
            _x = x;
            _y = y;
        }
        protected bool InsertItem(Item item)
        {
            for (int i = 0; i < _rawSize * _collSize; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
                    return true;
                }
            }
            return false;
        }
        public bool AddItem(Item newItem)
        {
            if (newItem != null)
            {
                Item item = (Item)newItem.Clone();
                if (item is IStackable)
                {
                    int capacity = Capacity, maxQuant = (item as IStackable).MaxQuantity;
                    for (int i = 0; i < capacity; i++)
                    {
                        if (_items[i] != null)
                        {
                            if (_items[i].Title == item.Title && (_items[i] as IStackable).Quantity < maxQuant)
                            {
                                if ((_items[i] as IStackable).Quantity + (item as IStackable).Quantity <= maxQuant)
                                {
                                    (_items[i] as IStackable).Quantity += (item as IStackable).Quantity;
                                    return true;
                                }
                                else
                                {
                                    (_items[i] as IStackable).Quantity = maxQuant;
                                    (item as IStackable).Quantity = (_items[i] as IStackable).Quantity + (item as IStackable).Quantity - maxQuant;
                                    return InsertItem(item);
                                }
                            }
                        }
                    }
                    return InsertItem(item);
                }
                else
                    return InsertItem(item);
            }
            else
                return false;
        }
    }
}
