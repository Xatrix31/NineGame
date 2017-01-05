namespace Win2D
{
    class CenterScreen
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CenterScreen()
        {
            X = 0;
            Y = 0;
        }

        public CenterScreen(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}