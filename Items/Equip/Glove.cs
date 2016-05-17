using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Glove : Item, IEquippable,ITradable
    {
        private float _crit;
        private int _cost;
        public Glove(string title, string description, Texture2D icon, float crit = 0.1f, int cost = 25) : base(title, description, icon)
        {
            _crit = crit;
            _cost = Cost;
        }

        public override object Clone()
        {
            return new Glove(_title, _description, _iconTx, _crit);
        }

        public int Cost
        {
            get
            {
                return _cost;
            }
        }

        public void Equip(ItemManager IM)
        {
            if (IM.Player.Equip.Glove == null)
            {
                IM.Player.Equip.Glove = this;
                IM.PlayerInventory.DellItem(this);
            }
            else
            {
                Item item = IM.Player.Equip.Glove;
                IM.Player.Equip.Glove = this;
                IM.PlayerInventory.DellItem(this);
                IM.PlayerInventory.AddItem(item);
            }
        }

        public override void Use(ItemManager IM)
        {
            Equip(IM);
        }
    }
}
