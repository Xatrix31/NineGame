using Microsoft.Graphics.Canvas;
using Windows.Foundation;

namespace Win2D
{
    static class Parameters
    {
        public static double Scale { get; set; }
        public static Size Original = new Size(640, 350);
        public static Point position = new Point(0, 0);
        public static Point centerScreen = new Point(320, 175);
    }

    static class Textures
    {
        public static CanvasBitmap CashTexture { get; set; }
        public static CanvasBitmap UIPlayer { get; set; }
        public static CanvasBitmap CardsTexture { get; set; }
        public static CanvasBitmap BoardCardsTexture { get; set; }
    }

}
