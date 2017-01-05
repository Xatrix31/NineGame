using Microsoft.Graphics.Canvas;

namespace Win2D
{
    class UIPlayer
    {
        public CanvasBitmap Texture { get; set; }
        public CanvasBitmap SleepTexture { get; set; }
        public CanvasBitmap SmileTexture { get; set; }
        public CanvasBitmap CashTexture { get; }
        public int Cash { get; set; }
        UICard[,] cards = new UICard[2, 6];

        public CanvasBitmap Draw()
        {
            return Textures.UIPlayer;
        }
    }
}