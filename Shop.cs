﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Shop :Store, IDrawable
    {
        private Texture2D _tx;
        private int _x, _y;
        private VendorInventory _inv;

        public Shop(int x, int y, Texture2D tx, Texture2D invTx, params Item[] items)
        {
            _tx = tx;
            _x = x;
            _y = y;
            if (items.Length <= 10)
                _inv = new VendorInventory(2, 5, 32);
            else
                _inv = new VendorInventory(items.Length / 5 + 1, 5, 32);
            _inv.CellTexture = invTx;
            foreach (Item item in items)
                _inv.AddItem(item);
        }
        public Texture2D Texture
        {
            get { return _tx; }
            set { _tx = value; }
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _x = value; }
        }
        public override Container Inventory
        {
            get { return _inv; }
        }
    }
}
