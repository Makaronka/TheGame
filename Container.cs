using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Container
    {
        protected int _collSize, _cellSize, _rawSize, _x, _y;
        protected Texture2D _cellTx;
        protected Item[] _items;

        public Item GetItem(int x, int y)
        {
            int normX = x / _cellSize, normY = y / _cellSize;
            return _items[normY * _rawSize + normX];
        }
        public Item GetItem(int x)
        {
            return _items[x];
        }
        public int CollSize { get { return _collSize; } }
        public int CellSize { get { return _cellSize; } }
        public int RawSize { get { return _rawSize; } }
        public int Capacity { get { return _rawSize * _collSize; } }
        public Texture2D CellTexture { get { return _cellTx; } set { _cellTx = value; } }
        public int Wight { get { return _rawSize * _cellSize; } }
        public int Hight { get { return _collSize * _cellSize; } }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public void DellItem (Item item)
        {
            int capacity = Capacity;
            for (int i = 0; i < capacity; i++)
            {
                if (_items[i] == item)
                {
                    if (item is IStackable)
                    {
                        (_items[i] as IStackable).Quantity--;
                        if ((_items[i] as IStackable).Quantity <= 0)
                            _items[i] = null;
                        break;
                    }
                    else
                        _items[i] = null;
                }
            }
        }
    }
}
