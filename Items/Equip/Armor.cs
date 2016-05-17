using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Armor : Item, IEquippable, ITradable
    {
        private int _defense, _cost;
        public Armor(string title, string description, Texture2D icon, int defense = 0, int cost = 100) : base(title, description, icon)
        {
            _defense = defense;
            _cost = cost;
        }

        public int Cost
        {
            get
            {
                return _cost;
            }
        }

        public override object Clone()
        {
            return new Armor(_title, _description, _iconTx,_defense);
        }

        public void Equip(ItemManager IM)
        {
            if (IM.Player.Equip.Armor == null)
            {
                IM.Player.Equip.Armor = this;
                IM.PlayerInventory.DellItem(this);
            }
            else
            {
                Item item = IM.Player.Equip.Armor;                
                IM.Player.Equip.Armor = this;
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
