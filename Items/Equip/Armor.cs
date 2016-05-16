﻿using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Armor : Item, IEquippable
    {
        private int _defense;
        public Armor(string title, string description, Texture2D icon, int defense = 0) : base(title, description, icon)
        {
            _defense = defense;
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
                IM.PlayerInventory.AddItem(IM.Player.Equip.Armor);
                IM.Player.Equip.Armor = this;
                IM.PlayerInventory.DellItem(this);
            }
        }
        public override void Use(ItemManager IM)
        {
            Equip(IM);
        }
    }
}
