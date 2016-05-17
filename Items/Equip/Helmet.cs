using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Helmet : Item, IEquippable
    {
        private float _critResist;
        public Helmet(string title, string description, Texture2D icon, float critResist = 0.0f) : base(title, description, icon)
        {
            _critResist = critResist;
        }
        public override object Clone()
        {
            return new Helmet(_title, _description, _iconTx, _critResist);
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
