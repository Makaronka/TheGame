using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    abstract class Item: ICloneable
    {
        protected string _title, _description;
        protected Texture2D _iconTx;
        public Item(string title, string description, Texture2D icon)
        {
            _title = title;
            _description = description;
            _iconTx = icon;
        }
        public string Title { get { return _title; } }
        public string Description { get { return _description; } }
        public Texture2D Icon { get { return _iconTx; } set { _iconTx = value; } }
        public abstract void Use(ItemManager IM);
        public abstract object Clone();
    }
}
