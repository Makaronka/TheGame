using System;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Equipment
    {
        private Weapon _weapon;
        private Equip.Boots _boots;
        private Equip.Armor _armor;
        private Equip.Glove _glove;
        private Equip.Helmet _helmet;

        public Equipment(Weapon weapon = null, Equip.Boots boots = null, Equip.Armor armor = null, Equip.Glove glove = null, Equip.Helmet helmet = null)
        {
            _weapon = weapon;
            _boots = boots;
            _armor = armor;
            _glove = glove;
            _helmet = helmet;
        }
        public Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }
        public Equip.Boots Boots
        {
            get { return _boots; }
            set { _boots = value; }
        }
        public Equip.Armor Armor {
            get { return _armor; }
            set { _armor = value; }
        }
        public Equip.Glove Glove
        {
            get { return _glove; }
            set { _glove = value; }
        }
        public Equip.Helmet Helmet
        {
            get { return _helmet; }
            set { _helmet = value; }
        }
    }
}
