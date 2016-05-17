using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Weapon: Item, IEquippable, ITradable
    {
        private int _damage, _cost;
        public Weapon(string title, string description, Texture2D icon, int damage = 10, int cost = 70) : base(title, description, icon)
        {
            _damage = damage;
            _cost = cost;
        }
        public override void Use(ItemManager IM)
        {
            Equip(IM);
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
            if (IM.Player.Equip.Weapon == null)
            {
                IM.Player.Equip.Weapon = this;
                IM.PlayerInventory.DellItem(this);
            }
            else
            {
                Item item = IM.Player.Equip.Weapon;
                IM.Player.Equip.Weapon = this;
                IM.PlayerInventory.DellItem(this);
                IM.PlayerInventory.AddItem(item);
            }
        }

        public override object Clone()
        {
            return new Weapon(_title, _description, _iconTx, _damage);
        }
    }
}
