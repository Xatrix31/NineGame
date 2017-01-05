using Microsoft.Graphics.Canvas;
using System.Collections.Generic;
using Windows.Foundation;
using System.Collections;

namespace Win2D
{
    class CandleList : IList<Candle>
    {
        public List<Candle> Candles { get; set; }
        public float FramesPerSecond { get; set; }
        public CanvasBitmap Texture { get; private set; }

        public CandleList(CanvasBitmap candle)
        {
            Texture = candle;
        }

        public Candle this[int index]
        {
            get
            {
                return Candles[index];
            }
            set
            {
                Candles[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return Candles.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public void Add(Candle item)
        {
            Candles.Add(item);
        }

        public void Clear()
        {
            Candles.Clear();
        }

        public bool Contains(Candle item)
        {
            return Candles.Contains(item);
        }

        public void CopyTo(Candle[] array, int arrayIndex)
        {
            Candles.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Candle> GetEnumerator()
        {
            return Candles.GetEnumerator();
        }

        public int IndexOf(Candle item)
        {
            return Candles.IndexOf(item);
        }

        public void Insert(int index, Candle item)
        {
            Candles.Insert(index, item);
        }

        public bool Remove(Candle item)
        {
            return Candles.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Candles.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Candle
    {
        public CanvasBitmap Texture { get; set; }
        public int Frame { get; set; }
        public Point position { get; }
        public Candle(int StartingFrame, Point Position)
        {
            Frame = StartingFrame;
            position = Position;
        }

        public void ChangeFrame()
        {
            Frame = (Frame > 2) ? 0 : Frame + 1;
        }
    }
}