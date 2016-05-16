using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Player : IDrawable
    {
        private int _x, _y, _hp, _maxHp, _gold;
        private Texture2D _tx;
        private Equipment _equip;

        public Player(int Xpos, int Ypos, int maxHp, int gold = 50)
        {
            _x = Xpos;
            _y = Ypos;
            _maxHp = maxHp;
            _hp = maxHp;
            _gold = gold;
            _equip = new Equipment();
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public int Hp
        {
            get { return _hp; }
            set 
            {
                if (value >= 0)
                    _hp = value > _maxHp ? _maxHp : value;
                else
                    _hp = 0;
            }
        }
        public int MaxHp { get { return _maxHp; } }
        public Texture2D Texture
        {
            get { return _tx; }
            set { _tx = value; }
        }
        public int Gold
        {
            get { return _gold; }
            set { _gold = value < 0 ? 0 : value; }
        }
        public Equipment Equip { get { return _equip; } }
    }
}
