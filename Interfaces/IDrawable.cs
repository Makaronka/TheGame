using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    interface IDrawable
    {
        int X
        { get; set; }

        int Y
        { get; set; }

        Texture2D Texture { get; set; }
    }
}
