using System;

namespace TheGame
{
    class VendorInventory : Inventory
    {
        public VendorInventory(int collSize, int rawSize, int cellSize, int x = 320, int y = 320, params Item[] items) : base(collSize, rawSize,cellSize, x ,y)
        {
            Array.Copy(items, _items, Math.Min(items.Length, _items.Length));
        }

        private bool FindItem(Item item)
        {
            for (int i = 0; i < _rawSize * _collSize; i++)
                if (_items[i] != null && _items[i].Title == item.Title)
                    return true;
            return false;
        }
        public override bool AddItem(Item newItem, int quantity = 1)
        {
            if(!FindItem(newItem))
            {
                Item item = (Item)newItem.Clone();
                for (int i = 0; i < _rawSize * _collSize; i++)
                    if (_items[i] == null)
                    {
                        _items[i] = item;
                        return true;
                    }
                return false;
            }
            return true;
        }
        public bool Sell(Item item, ItemManager IM)
        {
            if (item is ITradable)
            {
                IM.Player.Gold += (item as ITradable).Cost;
                return AddItem(item);
            }
            else
                return false;
        }
        public bool Buy(Item item, ItemManager IM)
        {
            if(IM.Player.Gold >= (item as ITradable).Cost)
            {
                if(IM.PlayerInventory.AddItem(item))
                {
                    IM.Player.Gold -= (item as ITradable).Cost;
                    //DellItem(item);
                    return true;
                }
            }
            return false;
        }
    }
}
