using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame.Equip
{
    class Boots : Item, IEquippable, ITradable
    {
        private int _agility, _cost;
        public Boots(string title, string description, Texture2D icon, int agility = 0, int cost = 30) : base(title, description, icon)
        {
            _agility = agility;
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
                Item item = IM.Player.Equip.Boots;
                IM.Player.Equip.Boots = this;
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
