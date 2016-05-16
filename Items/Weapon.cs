using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Weapon: Item, IEquippable
    {
        private int _damage;
        public Weapon(string title, string description, Texture2D icon, int damage = 10) : base(title, description, icon)
        {
            _damage = damage;
        }
        public override void Use(ItemManager IM)
        {
            Equip(IM);
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
                IM.PlayerInventory.AddItem(IM.Player.Equip.Weapon);
                IM.Player.Equip.Weapon = this;
                IM.PlayerInventory.DellItem(this);
            }
        }

        public override object Clone()
        {
            return new Weapon(_title, _description, _iconTx, _damage);
        }
    }
}
