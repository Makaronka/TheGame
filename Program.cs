using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame
{

    sealed class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Inventory inventory;
        ItemManager IM;
        Texture2D floor_tx;
        Equip.Weapon sword;
        Potion healPotion, poison;
        SpriteFont quantity_fn, main_fn, discription_fn;
        int MapSize = 20, TxSize = 32, equipStartX = 70, equipStartY = 44;
        List<IDrawable> MapElements;
        bool KeyIsDown = false, mouseRightClick = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferWidth = MapSize * TxSize;
            graphics.PreferredBackBufferHeight = MapSize * TxSize;
            player = new Player(2, 3, 100, equip: new Equipment(MapSize * TxSize + equipStartX, equipStartY));
            inventory = new Inventory(10, 6, TxSize, MapSize * TxSize, graphics.PreferredBackBufferHeight - 320);
            IM = new ItemManager(player, inventory);
            MapElements = new List<IDrawable>();
            MapElements.Add(player);
        }
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = MapSize * TxSize + inventory.RawSize * inventory.CellSize;
            graphics.PreferredBackBufferHeight = MapSize * TxSize;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.Texture = Content.Load<Texture2D>("hero");
            floor_tx = Content.Load<Texture2D>("floor");
            quantity_fn = Content.Load<SpriteFont>("Quantity");
            main_fn = Content.Load<SpriteFont>("Main");
            discription_fn = Content.Load<SpriteFont>("Discription");
            inventory.CellTexture = Content.Load<Texture2D>("invTx");
            player.Equip.CellTexture = Content.Load<Texture2D>("invTx");
            sword = new Equip.Weapon("Demon blade", "Best sword ever!", Content.Load<Texture2D>("demon_blade"));
            healPotion = new Potion("Heal Potion", "Hell 10 hp.", Content.Load<Texture2D>("redPotion"), 10, 5, 15);
            inventory.AddItem(sword);
            inventory.AddItem(healPotion, 10);
            poison = new Potion("Poison", "Hit 10 hp.", Content.Load<Texture2D>("greenPot"), -10, 6, 5);
            inventory.AddItem(poison, 10);
            MapElements.Add(new Chest(5, 5, Content.Load<Texture2D>("chest"), Content.Load<Texture2D>("invTx"), healPotion, poison, sword));
            healPotion.Quantity = 4;
            MapElements.Add(new Shop(7, 3, Content.Load<Texture2D>("shop"), Content.Load<Texture2D>("invTx"), healPotion, poison));

            player.Equip.Helmet = new Equip.Helmet("Helmet", "", Content.Load<Texture2D>("helmet1"));
            player.Equip.Armor = new Equip.Armor("Armor", "", Content.Load<Texture2D>("armor1"));
            player.Equip.Glove = new Equip.Glove("Glove", "", Content.Load<Texture2D>("glove1"));
            player.Equip.Boots = new Equip.Boots("Boots", "", Content.Load<Texture2D>("boots1"));

            base.LoadContent();
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        private void PlayerUpdate(KeyboardState currKeyboard)
        {
            if (currKeyboard.GetPressedKeys().Length != 0)
            {
                if (!KeyIsDown)
                {
                    switch (currKeyboard.GetPressedKeys()[0])
                    {
                        case Keys.S:
                            if (player.Y < MapSize - 1)
                                player.Y += 1;
                            KeyIsDown = true;
                            break;
                        case Keys.A:
                            if (player.X > 0)
                                player.X -= 1;
                            KeyIsDown = true;
                            break;
                        case Keys.W:
                            if (player.Y > 0)
                                player.Y -= 1;
                            KeyIsDown = true;
                            break;
                        case Keys.D:
                            if (player.X < MapSize - 1)
                                player.X += 1;
                            KeyIsDown = true;
                            break;
                    }
                    foreach (IDrawable el in MapElements)
                    {
                        if (el is Store && el.X == player.X && el.Y == player.Y)
                        {
                            IM.TargetContainer = (el as Store).Inventory;
                            break;
                        }
                        else
                            IM.TargetContainer = null;
                    }
                }
            }
            else
                KeyIsDown = false;
        }
        private Item GetItemFromContainer(int mX, int mY, Container container)
        {
            if (container != null && mX > container.X && mX < container.X + container.Wight && mY > container.Y && mY < container.Y + container.Hight)
            {
                return container.GetItem(mX - container.X, mY - container.Y);
            }
            return null;
        }
        private void MouseUpdate(MouseState mState)
        {
            Item item = GetItemFromContainer(mState.X, mState.Y, inventory);
            if (item != null)
                IM.SelectedItem = item;
            else
            {
                Item eq = GetItemFromContainer(mState.X, mState.Y, player.Equip);
                if (eq != null)
                    IM.SelectedItem = eq;
                else
                {
                    eq = GetItemFromContainer(mState.X, mState.Y, IM.TargetContainer);
                    if (eq != null)
                        IM.SelectedItem = eq;
                    else
                        IM.SelectedItem = null;
                }
            }

            if (mState.RightButton == ButtonState.Pressed)
            {
                if (!mouseRightClick)
                {
                    mouseRightClick = true;
                    if (IM.TargetContainer == null)
                    {
                        if (item != null)
                            item.Use(IM);
                    }
                    else
                    {
                        if (IM.TargetContainer is Inventory)
                        {
                            if (item != null)
                            {
                                if (IM.TargetContainer is VendorInventory)
                                {
                                    if ((IM.TargetContainer as VendorInventory).Sell(item, IM))
                                        inventory.DellItem(item);
                                }
                                else
                                    if ((IM.TargetContainer as Inventory).AddItem(item))
                                    inventory.DellItem(item);
                            }
                            else
                            {
                                item = GetItemFromContainer(mState.X, mState.Y, IM.TargetContainer);
                                if (item != null)
                                    if (IM.TargetContainer is VendorInventory)
                                        (IM.TargetContainer as VendorInventory).Buy(item, IM);
                                    else
                                    {
                                        inventory.AddItem(item);
                                        IM.TargetContainer.DellItem(item);
                                    }
                            }
                        }
                    }
                    item = GetItemFromContainer(mState.X, mState.Y, player.Equip);
                    if (item != null)
                    {
                        inventory.AddItem(item);
                        player.Equip.DellItem(item);
                    }
                }
            }
            else
                mouseRightClick = false;
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState currKeyboard = Keyboard.GetState();
            PlayerUpdate(currKeyboard);

            //Mouse check
            MouseState mState = Mouse.GetState();
            MouseUpdate(mState);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred);
            for (int i = 0; i < MapSize; i++)
                for (int j = 0; j < MapSize; j++)
                    spriteBatch.Draw(floor_tx, new Vector2(j * TxSize, i * TxSize), Color.White);
            foreach (IDrawable el in MapElements)
                spriteBatch.Draw(el.Texture, new Vector2(el.X * TxSize, el.Y * TxSize), Color.White);
            spriteBatch.End();
            if (IM.SelectedItem != null)
                DrawItemInfo(IM.SelectedItem);
            if (IM.TargetContainer != null)
                DrawInventory(IM.TargetContainer);
            DrawPlayerInfo(player);
            base.Draw(gameTime);
        }
        private void DrawPlayerInfo(Player player)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(main_fn, "Health: " + player.Hp.ToString() + "/" + player.MaxHp.ToString(), new Vector2(MapSize * TxSize + 10, 10), Color.DarkGreen);
            spriteBatch.DrawString(main_fn, "Gold:   " + player.Gold.ToString(), new Vector2(MapSize * TxSize + 10, 30), Color.Yellow);

            spriteBatch.DrawString(main_fn, "Helmet: ", new Vector2(MapSize * TxSize + 10, 52), Color.Black);
            spriteBatch.DrawString(main_fn, "Armour: ", new Vector2(MapSize * TxSize + 10, 84), Color.Black);
            spriteBatch.DrawString(main_fn, "Glove:  ", new Vector2(MapSize * TxSize + 10, 116), Color.Black);
            spriteBatch.DrawString(main_fn, "Boots:  ", new Vector2(MapSize * TxSize + 10, 148), Color.Black);
            spriteBatch.DrawString(main_fn, "Weapon: ", new Vector2(MapSize * TxSize + 10, 180), Color.Black);
            spriteBatch.End();
            DrawInventory(player.Equip);
            DrawInventory(inventory);
        }
        private void DrawInventory(Container inv)
        {
            int itemX, itemY;
            spriteBatch.Begin();
            for (int i = 0; i < inv.Capacity; i++)
            {
                itemX = inv.X + (i % inv.RawSize) * inv.CellSize;
                itemY = inv.Y + (i / inv.RawSize) * inv.CellSize;
                spriteBatch.Draw(inv.CellTexture, new Vector2(itemX, itemY), Color.White);
                if (inv.GetItem(i) != null)
                {
                    spriteBatch.Draw(inv.GetItem(i).Icon, new Vector2(itemX, itemY), Color.White);
                    if (inv.GetItem(i) is IStackable)
                        spriteBatch.DrawString(quantity_fn, ((IStackable)inv.GetItem(i)).Quantity.ToString(), new Vector2(itemX + inv.GetItem(i).Icon.Width - 8, itemY + inv.GetItem(i).Icon.Height - 14), Color.DarkGreen);
                }
            }
            spriteBatch.End();
        }
        private void DrawItemInfo(Item item)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(main_fn, "Item info:", new Vector2(MapSize * TxSize + 10, equipStartY + player.Equip.Hight + 10), Color.Purple);
            spriteBatch.DrawString(discription_fn, "Title: " + item.Title + "\nDiscription: " + item.Description, new Vector2(MapSize * TxSize + 10, equipStartY + player.Equip.Hight + 24), Color.Black);
            if (item is ITradable)
                spriteBatch.DrawString(discription_fn, "Cost: " + (item as ITradable).Cost.ToString(), new Vector2(graphics.PreferredBackBufferWidth - 50, inventory.Y - 15), Color.Black);
            spriteBatch.End();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game1 game = new Game1();
            game.Run();
        }
    }
}
