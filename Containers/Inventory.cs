
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
        public bool AddItem(Item newItem, int quantity = 1)
        {
            if (newItem != null && quantity > 0)
            {
                for (int q = 0; q < quantity; q++)
                {
                    Item item = (Item)newItem.Clone();
                    if (item is IStackable)
                    {
                        int capacity = Capacity, maxQuant = (item as IStackable).MaxQuantity;
                        bool isAdded = false;
                        for (int i = 0; i < capacity; i++)
                        {
                            if (_items[i] != null)
                            {
                                if (_items[i].Title == item.Title && (_items[i] as IStackable).Quantity < maxQuant)
                                {
                                    if ((_items[i] as IStackable).Quantity + (item as IStackable).Quantity <= maxQuant)
                                    {
                                        (_items[i] as IStackable).Quantity += (item as IStackable).Quantity;
                                        isAdded = true;
                                        break;
                                    }
                                    else
                                    {
                                        (_items[i] as IStackable).Quantity = maxQuant;
                                        (item as IStackable).Quantity = (_items[i] as IStackable).Quantity + (item as IStackable).Quantity - maxQuant;
                                        if (!InsertItem(item))
                                            return false;
                                        isAdded = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!isAdded && !InsertItem(item))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!InsertItem(item))
                            return false;
                        else
                            break;
                    }
                }
                return true;
            }
            else
                return false;
        }
    }
}
