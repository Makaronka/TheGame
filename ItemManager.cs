namespace TheGame
{
    class ItemManager
    {
        private Player _player;
        private Inventory _playerInv;
        private Container _targetContainer;
        private Item _selectedItem;

        public ItemManager(Player player, Inventory inv)
        {
            _player = player;
            _playerInv = inv;
            _targetContainer = null;
        }
        public Player Player { get { return _player; } }
        public Inventory PlayerInventory { get { return _playerInv; } }
        public Container TargetContainer { get { return _targetContainer; } set { _targetContainer = value; } }
        public Item SelectedItem { get { return _selectedItem; } set { _selectedItem = value; } }
    }
}
