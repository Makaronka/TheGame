using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Glove : Item, IEquippable
    {
        private float _crit;
        public Glove(string title, string description, Texture2D icon, float crit = 0.1f) : base(title, description, icon)
        {
            _crit = crit;
        }

        public override object Clone()
        {
            return new Glove(_title, _description, _iconTx, _crit);
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
