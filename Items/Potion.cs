using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Potion : Item, IStackable, ITradable, ICloneable
    {
        private int _influence, _maxQuantity, _quantity, _cost;

        public Potion(string title, string description, Texture2D icon, int influence, int maxQuantity, int cost, int quantity = 1) : base(title, description, icon)
        {
            _influence = influence;
            _maxQuantity = maxQuantity;
            _quantity = quantity > maxQuantity ? maxQuantity : quantity;
            _cost = cost;
        }

        public int Cost { get { return _cost; } }
        public int MaxQuantity { get {return _maxQuantity;}}
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value < 0)
                    _quantity = 0;
                else
                    _quantity = value > _maxQuantity ? _maxQuantity : value;
            }
        }

        public override object Clone()
        {
            return new Potion(_title, _description, _iconTx, _influence, _maxQuantity, _cost);
        }
        public override void Use(ItemManager IM)
        {
            IM.Player.Hp += _influence;
            IM.PlayerInventory.DellItem(this);
        }
    }
}
