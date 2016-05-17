using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Equipment:Container
    {
        public Equipment(int x = 320, int y = 320, int cellSize = 32, Equip.Weapon weapon = null, Equip.Boots boots = null, Equip.Armor armor = null, Equip.Glove glove = null, Equip.Helmet helmet = null)
        {
            _collSize = 5;
            _cellSize = cellSize;
            _rawSize = 1;
            _x = x;
            _y = y;
            _items = new Item[5];
            _items[0] = helmet;
            _items[1] = armor;
            _items[2] = glove;
            _items[3] = boots;
            _items[4] = weapon;
        }
        public Equip.Weapon Weapon
        {
            get { return (Equip.Weapon)_items[4]; }
            set { _items[4] = value; }
        }
        public Equip.Boots Boots
        {
            get { return (Equip.Boots)_items[3]; }
            set { _items[3] = value; }
        }
        public Equip.Armor Armor {
            get { return (Equip.Armor)_items[1]; }
            set { _items[1] = value; }
        }
        public Equip.Glove Glove
        {
            get { return (Equip.Glove)_items[2]; }
            set { _items[2] = value; }
        }
        public Equip.Helmet Helmet
        {
            get { return (Equip.Helmet)_items[0]; }
            set { _items[0] = value; }
        }
        public Item[] Items
        {
            get { return _items; }
        }
    }
}
