﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Boots : Item, IEquippable
    {
        private int _agility;
        public Boots(string title, string description, Texture2D icon, int agility = 0) : base(title, description, icon)
        {
            _agility = agility;
        }

        public override object Clone()
        {
            return new Boots(_title, _description, _iconTx, _agility);
        }

        public void Equip(ItemManager IM)
        {
            if (IM.Player.Equip.Boots == null)
            {
                IM.Player.Equip.Boots = this;
                IM.PlayerInventory.DellItem(this);
            }
            else
            {
                IM.PlayerInventory.AddItem(IM.Player.Equip.Boots);
                IM.Player.Equip.Boots = this;
                IM.PlayerInventory.DellItem(this);
            }
        }

        public override void Use(ItemManager IM)
        {
            Equip(IM);
        }
    }
}