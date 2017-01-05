using Microsoft.Graphics.Canvas;

namespace Win2D
{
    class Boss
    {
        public CanvasBitmap Texture { get; set; }
        public CanvasBitmap LookDown { get; set; }
        public CanvasBitmap GiveLeft { get; set; }
        public CanvasBitmap GiveRight { get; set; }
        public CanvasBitmap GiveDown { get; set; }
        public CanvasBitmap TakeMoney { get; set; }

        public Boss()
        {
            
        }
    }
}
