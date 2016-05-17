using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Helmet : Item, IEquippable, ITradable
    {
        private float _critResist;
        private int _cost;
        public Helmet(string title, string description, Texture2D icon, float critResist = 0.0f, int cost = 35) : base(title, description, icon)
        {
            _critResist = critResist;
            _cost = cost;
        }
        public override object Clone()
        {
            return new Helmet(_title, _description, _iconTx, _critResist);
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
            if (IM.Player.Equip.Helmet == null)
            {
                IM.Player.Equip.Helmet = this;
                IM.PlayerInventory.DellItem(this);
            }
            else
            {
                Item item = IM.Player.Equip.Helmet;
                IM.Player.Equip.Helmet = this;
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
