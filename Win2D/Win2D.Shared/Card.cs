using Microsoft.Graphics.Canvas;
using Windows.Foundation;

namespace Win2D
{
    class Card
    {
        public int Number { get; set; }
        public int ColorCard { get; set; }
        public Point Position { get; set; }
    }

    class UICard : Card
    {
        public bool Visible { get; set; }
    }
}